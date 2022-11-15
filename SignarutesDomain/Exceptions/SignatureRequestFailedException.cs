namespace Signarutes.Domain.Contracts.Exceptions
{
    public class SignatureRequestFailedException : DomainException
    {
        public SignatureRequestFailedException() : base("Signature request posting failed") { }

        public SignatureRequestFailedException(string message) : base(message) { }

        public SignatureRequestFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
