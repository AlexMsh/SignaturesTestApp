namespace Signarutes.Domain.Contracts.models.Request
{
    public class SignatureRequest
    {
        public SignatureRequest
            (Recipient recipient,
            string message,
            SignatureRequestFile? file)
        {
            Recipient = recipient;
            Message = message;
            File = file;
        }

        public Recipient Recipient { get; private set; }

        public string Message { get; private set; }

        public SignatureRequestFile? File { get; private set; }
    }
}
