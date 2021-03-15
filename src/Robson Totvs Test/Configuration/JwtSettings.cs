namespace Robson_Totvs_Test.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string ValidAt { get; set; }
    }
}
