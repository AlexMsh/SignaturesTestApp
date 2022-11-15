namespace Signarutes.Domain.Contracts.Exceptions
{
    public class SignedAttachmentNotExistsException : DomainException
    {
        public SignedAttachmentNotExistsException() : base("Signed Attachment not found") { }

        public SignedAttachmentNotExistsException(string message) : base(message) { }

        public SignedAttachmentNotExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
