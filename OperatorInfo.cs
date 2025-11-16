namespace GroundTimeUuidGui
{
    /// <summary>
    /// Represents a normalized operator entry (canonical ICAO + display name).
    /// </summary>
    public sealed class OperatorInfo
    {
        public string CanonicalIcao { get; }
        public string Name { get; }

        public OperatorInfo(string canonicalIcao, string name)
        {
            CanonicalIcao = canonicalIcao;
            Name = name;
        }
    }
}
