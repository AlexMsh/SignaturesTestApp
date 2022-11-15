namespace Signarutes.Domain.Contracts.Exceptions
{
    public class InvalidSignatureRequestException : DomainException
    {
        public InvalidSignatureRequestException() { }

        public InvalidSignatureRequestException(string message) : base(message) { }

        public InvalidSignatureRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
