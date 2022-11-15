using Microsoft.Extensions.Options;
using Signarutes.Domain.Contracts;
using Signarutes.Domain.Contracts.Configuration;
using Signarutes.Domain.Contracts.Exceptions;
using Signarutes.Domain.Contracts.models;
using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Client.Proxy.Contracts;
using Signatures.Repositories.Contracts;

namespace Signatures.Domain
{
    // the idea is to keep this layer separated from the specifics of the Signant API (e.g. models/generated client)
    // so that we can reuse it with other implementations as well
    // this is simply highlighting the concept
    // in reality it doesn't make to much sense as long as we're having 1 API provider only
    // (e.g. good as a concept, good for code clarity/readability
    // BUT overcomplicates project structure & no warranty that a wrapper can be reused/easily adjusted)
    public class DocumentSignatureService : IDocumentSignatureService
    {
        private readonly IDocumentSignatureProxy _signatureProxy;
        private readonly SignatureRequestConfiguration _config;
        private readonly IRepository<SignedDocData> _repository;

        public DocumentSignatureService(
            IOptions<SignatureRequestConfiguration> config, 
            IDocumentSignatureProxy signatureProxy,
            IRepository<SignedDocData> repository)
        {
            _signatureProxy = signatureProxy;
            _config = config.Value;
            _repository = repository;
        }

        public async Task<SignedAttachment> GetSignedAttachment(string signatureRequestId)
        {
            var signatureResponse = await _repository.Get(signatureRequestId);
            if (string.IsNullOrWhiteSpace(signatureResponse?.Attachment?.Name))
            {
                throw new SignedAttachmentNotExistsException();
            }

            return signatureResponse.Attachment;
        }

        public async Task<SignedDocData> SendDocumentForSigning(SignatureRequest signatureRequest)
        {
            validateInput(signatureRequest);

            var response = _signatureProxy.SendDocumentForSigning(signatureRequest, _config);
            if (response.Status == SignatureRequestStatus.Failed) { throw new SignatureRequestFailedException(); }

            //TODO: save postingid in some storage (db?)
            await _repository.Save(response);
            return response;
        }

        private void validateInput(SignatureRequest signatureRequest)
        {
            if (signatureRequest == null) { throw new ArgumentNullException(nameof(signatureRequest)); }

            if (string.IsNullOrEmpty(signatureRequest.Recipient?.Email)) { 
                throw new InvalidSignatureRequestException("Recipient email can't be empty"); 
            }

            if (signatureRequest.File == null || signatureRequest.File.Content.Length == 0) { 
                throw new InvalidSignatureRequestException("No file uploaded"); 
            }

            if (!signatureRequest.File.Type?.Equals("pdf") != true) { 
                throw new InvalidSignatureRequestException("only pdf files are supported"); 
            }
        }
    }
}
