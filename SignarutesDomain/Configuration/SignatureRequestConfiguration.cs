namespace Signarutes.Domain.Contracts.Configuration
{
    public class SignatureRequestConfiguration
    {
        public int RequestTTLDays { get; set; }

        public int RequestValidityDays { get; set; }
    }
}
