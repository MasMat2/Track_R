namespace TrackrAPI.Models
{
    public class JwtSettings
    {
        public const string SectionName = "AppSettings:JwtSettings";
        public string Secret { get; set; } = null!;
        public int ExpiryHours { get; set; }
        public string Issuer { get; set; } = null!;
    }
}
