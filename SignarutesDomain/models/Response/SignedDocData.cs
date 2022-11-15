namespace Signarutes.Domain.Contracts.models.Response
{
    public class EntityBase
    {
        public EntityBase(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class SignedDocData : EntityBase
    {
        public SignedDocData(
            Recipient recipient,
            SignatureRequestStatus status,
            string id,
            SignedAttachment attachment) : base(id)
        {
            Recipient = recipient;
            Status = status;
            Attachment = attachment;
        }

        public Recipient Recipient { get; set; }

        public SignedAttachment Attachment { get; set; }

        public SignatureRequestStatus Status { get; set; }
    }
}
