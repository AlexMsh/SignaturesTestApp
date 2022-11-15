namespace Signarutes.Domain.Contracts.models.Response
{
    public class SignedAttachment
    {
        public SignedAttachment(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }

        public string Id { get; private set; }
    }
}
