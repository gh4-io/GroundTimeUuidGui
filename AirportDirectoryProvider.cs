using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GroundTimeUuidGui
{
    /// <summary>
    /// Loads airport codes from an optional CSV file, or falls back to built-in defaults.
    /// 
    /// CSV format (UTF-8, with header):
    ///   code_type,code,canonical_icao,name
    /// 
    /// Where:
    ///   - code_type: ICAO or IATA (informational only)
    ///   - code:      any alias for the airport (e.g. KCVG, CVG)
    ///   - canonical_icao: the ICAO airport code to use in UUID seeds (e.g. KCVG)
    ///   - name:      human-friendly airport/field name
    /// 
    /// You can place 'airports.csv' next to the EXE to override the built-in set.
    /// </summary>
    public static class AirportDirectoryProvider
    {
        
        public static IReadOnlyDictionary<string, AirportInfo> LoadAirports()
        {
            var dict = new Dictionary<string, AirportInfo>(StringComparer.OrdinalIgnoreCase);

            // 1) Start with the embedded CSV (built at compile time from airports.csv),
            //    so the EXE always has a complete internal directory.
            bool anyLoaded = LoadFromEmbeddedCsv(dict);

            // 2) If there was no embedded CSV (or it failed), fall back to the small
            //    hard-coded set so the tool still works out-of-the-box.
            if (!anyLoaded && dict.Count == 0)
            {
                LoadBuiltIn(dict);
            }

            // 3) Finally, if an external airports.csv exists next to the EXE, load it
            //    and let those entries override or supplement whatever is already in
            //    the dictionary. This means:
            //      - External rows win for overlapping codes.
            //      - Internal-only rows (e.g. MIA) remain if external CSV omits them.
            try
            {
                string baseDir = AppContext.BaseDirectory;
                string csvPath = Path.Combine(baseDir, "airports.csv");
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


private static void LoadFromCsv(string path, Dictionary<string, AirportInfo> dict)
        {
            using var reader = new StreamReader(path);
            MergeFromCsv(reader, dict);
        }

        /// <summary>
        /// Loads the built-in airport directory from the airports.csv that was
        /// embedded into the EXE at build time. Returns true if any records
        /// were loaded.
        /// </summary>
        private static bool LoadFromEmbeddedCsv(Dictionary<string, AirportInfo> dict)
        {
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                string? resourceName = null;

                foreach (var name in asm.GetManifestResourceNames())
                {
                    if (name.EndsWith(".airports.csv", StringComparison.OrdinalIgnoreCase))
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
                MergeFromCsv(reader, dict);
                return dict.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Shared CSV parsing used for both embedded and external airports.csv.
        /// Rows later in the stream win for overlapping codes.
        /// </summary>
        private static void MergeFromCsv(TextReader reader, Dictionary<string, AirportInfo> dict)
        {
            string? header = reader.ReadLine(); // skip header

            while (true)
            {
                string? line = reader.ReadLine();
                if (line is null)
                    break;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                if (parts.Length < 4)
                    continue;

                string code = parts[1].Trim();
                string canonical = parts[2].Trim().ToUpperInvariant();
                string name = parts[3].Trim();

                if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(canonical))
                    continue;

                var info = new AirportInfo(canonical, name);
                dict[code] = info;
            }
        }

        private static void LoadBuiltIn(Dictionary<string, AirportInfo> dict)
        {
            // Minimal set for when CSV is not present; safe defaults for your common stations.
            dict["KCVG"] = new AirportInfo("KCVG", "CINCINNATI/NORTHERN KENTUCKY INTL, USA");
            dict["CVG"]  = new AirportInfo("KCVG", "CINCINNATI/NORTHERN KENTUCKY INTL, USA");

            dict["KJFK"] = new AirportInfo("KJFK", "JOHN F. KENNEDY INTL, NEW YORK, USA");
            dict["JFK"]  = new AirportInfo("KJFK", "JOHN F. KENNEDY INTL, NEW YORK, USA");

            dict["KSDF"] = new AirportInfo("KSDF", "LOUISVILLE MUHAMMAD ALI INTL, USA");
            dict["SDF"]  = new AirportInfo("KSDF", "LOUISVILLE MUHAMMAD ALI INTL, USA");

            dict["EDDF"] = new AirportInfo("EDDF", "FRANKFURT/MAIN INTL, GERMANY");
            dict["FRA"]  = new AirportInfo("EDDF", "FRANKFURT/MAIN INTL, GERMANY");

            dict["EGLL"] = new AirportInfo("EGLL", "LONDON HEATHROW, UNITED KINGDOM");
            dict["LHR"]  = new AirportInfo("EGLL", "LONDON HEATHROW, UNITED KINGDOM");

            dict["WSSS"] = new AirportInfo("WSSS", "SINGAPORE CHANGI, SINGAPORE");
            dict["SIN"]  = new AirportInfo("WSSS", "SINGAPORE CHANGI, SINGAPORE");
        }
    }
}
