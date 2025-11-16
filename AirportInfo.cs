namespace GroundTimeUuidGui
{
    /// <summary>
    /// Represents a normalized airport entry (canonical ICAO + display name) plus an optional source
    /// indicator showing where the entry came from (embedded CSV, external CSV, or built-in defaults).
    /// </summary>
    public sealed class AirportInfo
    {
        public string CanonicalIcao { get; }
        public string Name { get; }
        /// <summary>
        /// Optional label describing the origin of this entry, e.g. "embedded", "external", "built-in".
        /// This is primarily intended for diagnostics / developer display and has no impact on logic.
        /// </summary>
        public string Source { get; }

        public AirportInfo(string canonicalIcao, string name, string source = "embedded")
        {
            CanonicalIcao = canonicalIcao;
            Name = name;
            Source = source;
        }
    }
}
