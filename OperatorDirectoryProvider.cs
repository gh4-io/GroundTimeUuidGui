using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GroundTimeUuidGui
{
    /// <summary>
    /// Loads operator (airline) codes from an optional CSV file, or falls back to built-in defaults.
    /// 
    /// CSV format (UTF-8, with header):
    ///   code_type,code,canonical_icao,name
    /// 
    /// Where:
    ///   - code_type: ICAO, IATA, INTERNAL (informational only)
    ///   - code:      any alias for the operator (e.g. SIA, SQ, DHLUK)
    ///   - canonical_icao: the ICAO operator code to use in UUID seeds (e.g. SIA)
    ///   - name:      human-friendly operator name
    /// 
    /// You can place 'operators.csv' next to the EXE to override the built-in set.
    /// </summary>
    public static class OperatorDirectoryProvider
    {

        public static IReadOnlyDictionary<string, OperatorInfo> LoadOperators()
        {
            var dict = new Dictionary<string, OperatorInfo>(StringComparer.OrdinalIgnoreCase);

            // 1) Start with the embedded CSV (built at compile time from operators.csv),
            //    so the EXE always has a complete internal directory.
            bool anyLoaded = LoadFromEmbeddedCsv(dict);

            // 2) If there was no embedded CSV (or it failed), fall back to the small
            //    hard-coded set so the tool still works out-of-the-box.
            if (!anyLoaded && dict.Count == 0)
            {
                LoadBuiltIn(dict);
            }

            // 3) Finally, if an external operators.csv exists next to the EXE, load it
            //    and let those entries override or supplement whatever is already in
            //    the dictionary. This means:
            //      - External rows win for overlapping codes.
            //      - Internal-only rows remain if external CSV omits them.
            try
            {
                string baseDir = AppContext.BaseDirectory;
                string csvPath = Path.Combine(baseDir, "operators.csv");
                if (File.Exists(csvPath))
                {
                    LoadFromCsv(csvPath, dict);
                }
            }
            catch
            {
                // Swallow and keep whatever we already loaded (embedded + built-in).
            }

            return dict;
        }


        private static void LoadFromCsv(string path, Dictionary<string, OperatorInfo> dict)
        {
            using var reader = new StreamReader(path);
            MergeFromCsv(reader, dict, "external");
        }

        /// <summary>
        /// Loads the built-in operator directory from the operators.csv that was
        /// embedded into the EXE at build time. Returns true if any records
        /// were loaded.
        /// </summary>
        private static bool LoadFromEmbeddedCsv(Dictionary<string, OperatorInfo> dict)
        {
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                string? resourceName = null;

                foreach (var name in asm.GetManifestResourceNames())
                {
                    if (name.EndsWith(".operators.csv", StringComparison.OrdinalIgnoreCase))
                    {
                        resourceName = name;
                        break;
                    }
                }

                if (resourceName is null)
                    return false;

                using Stream? stream = asm.GetManifestResourceStream(resourceName);
                if (stream is null)
                    return false;

                using var reader = new StreamReader(stream);
                MergeFromCsv(reader, dict, "embedded");
                return dict.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Shared CSV parsing used for both embedded and external operators.csv.
        /// Rows later in the stream win for overlapping codes.
        /// </summary>
        private static void MergeFromCsv(TextReader reader, Dictionary<string, OperatorInfo> dict, string sourceLabel)
        {
            // CSV format (OpenFlights-like):
            // AirlineID,Name,Alias,IATA,ICAO,Country
            string? header = reader.ReadLine(); // discard header

            while (true)
            {
                string? line = reader.ReadLine();
                if (line is null)
                    break;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 6)
                    continue;

                string airlineId = parts[0].Trim();
                string name = parts[1].Trim();
                string alias = parts[2].Trim();
                string iata = parts[3].Trim();
                string icao = parts[4].Trim();
                string country = parts[5].Trim();

                // Skip entries that have neither code.
                if (string.IsNullOrEmpty(icao) && string.IsNullOrEmpty(iata))
                    continue;

                // Human-friendly display name
                string displayName = $"{name}, {country}".Trim(' ', ',');

                // Canonical code: prefer ICAO, then IATA, then AirlineID
                string canonical =
                    !string.IsNullOrEmpty(icao) ? icao.ToUpperInvariant() :
                    !string.IsNullOrEmpty(iata) ? iata.ToUpperInvariant() :
                    airlineId;

                // NOTE: OperatorInfo has (CanonicalIcao, Name)
                var info = new OperatorInfo(canonical, displayName);

                void AddKey(string code)
                {
                    if (string.IsNullOrWhiteSpace(code))
                        return;

                    string key = code.Trim().ToUpperInvariant();
                    if (key.Length == 0)
                        return;

                    dict[key] = info;
                }

                // ICAO and IATA are valid lookup keys.
                AddKey(icao);
                AddKey(iata);

                // OpenFlights "Alias" can also be used as a lookup key if present.
                AddKey(alias);
            }
        }


        private static void LoadBuiltIn(Dictionary<string, OperatorInfo> dict)
        {
            // Minimal set for when CSV is not present; safe defaults for your common customers.
            dict["DHK"] = new OperatorInfo("DHK", "DHL AIR UK");
            dict["DHLUK"] = new OperatorInfo("DHK", "DHL AIR UK");

            dict["SIA"] = new OperatorInfo("SIA", "SINGAPORE AIRLINES");
            dict["SQ"] = new OperatorInfo("SIA", "SINGAPORE AIRLINES");

            dict["3S"] = new OperatorInfo("3S", "AEROLOGIC");
            dict["BOX"] = new OperatorInfo("3S", "AEROLOGIC");

            dict["CKS"] = new OperatorInfo("CKS", "KALITTA AIR");
            dict["K4"] = new OperatorInfo("CKS", "KALITTA AIR");

            dict["CSB"] = new OperatorInfo("CSB", "21 AIR");
            dict["2I"] = new OperatorInfo("CSB", "21 AIR");
            dict["21"] = new OperatorInfo("CSB", "21 AIR");

            dict["KFS"] = new OperatorInfo("KFS", "KALITTA CHARTERS");
            dict["CSJ"] = new OperatorInfo("KFS", "KALITTA CHARTERS");
        }
    }
}
