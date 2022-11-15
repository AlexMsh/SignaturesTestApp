namespace Signarutes.Domain.Contracts.models.Request
{
    public class SignatureRequestFile
    {
        public SignatureRequestFile(string name, string type, byte[] content)
        {
            Name = name;
            Type = type;
            Content = content;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public byte[] Content { get; private set; }

    }
}
