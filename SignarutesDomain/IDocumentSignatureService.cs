using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;

namespace Signarutes.Domain.Contracts
{
    public interface IDocumentSignatureService
    {
        Task<SignedAttachment> GetSignedAttachment(string signatureRequestId);
        Task<SignedDocData> SendDocumentForSigning(SignatureRequest signatureRequest);
    }
}
