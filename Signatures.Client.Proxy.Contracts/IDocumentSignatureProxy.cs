using Signarutes.Domain.Contracts.Configuration;
using Signarutes.Domain.Contracts.models;
using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;

namespace Signatures.Client.Proxy.Contracts
{
    public interface IDocumentSignatureProxy
    {
        // TODO: we should use some custom type as an envelope for the "request/postingId"
        Task<SignatureRequestFile> DownloadAttachmentAsync(string postingId, string attachmentId);

        SignatureRequestStatus GetSignatureRequestStatus(string id);

        SignedDocData SendDocumentForSigning(SignatureRequest request, SignatureRequestConfiguration config);
    }
}