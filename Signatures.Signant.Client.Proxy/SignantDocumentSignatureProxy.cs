using AutoMapper;
using Microsoft.Extensions.Options;
using Signant.Common.Models;
using Signant.Global.Common.Enums;
using Signarutes.Domain.Contracts.Configuration;
using Signarutes.Domain.Contracts.models;
using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Client.Proxy.Contracts;
using Signatures.Signant.Client.Proxy.Configuration;

namespace Signatures.Signant.Client.Proxy
{
    public class SignantDocumentSignatureProxy : IDocumentSignatureProxy
    {
        private readonly IMapper _mapper;
        private readonly IPostingsService _postingsService;
        private readonly SignantConfiguration _signantConfiguration;

        public SignantDocumentSignatureProxy(IOptions<SignantConfiguration> signantConfiguration, IMapper mapper)
        {
            _mapper = mapper;
            _postingsService = PostingServiceFactory.GetPostingsService();
            _signantConfiguration = signantConfiguration.Value;
        }

        public async Task<SignatureRequestFile> DownloadAttachmentAsync(string postingId, string attachmentId)
        {
            Guid guidPostirngId;
            Guid guidAttachmentId;

            if (!Guid.TryParse(postingId, out guidPostirngId)) { throw new InvalidCastException("Signature id is not of a GUID type"); }
            if (!Guid.TryParse(attachmentId, out guidAttachmentId)) { throw new InvalidCastException("Attachment id is not of a GUID type"); }

            var response = await _postingsService.DownloadAttachmentAsync(
                _signantConfiguration.DistributorId,
                _signantConfiguration.AccessCode,
                guidPostirngId,
                guidAttachmentId,
                _signantConfiguration.AccessCode);

            // todo: send failed status
            if (response?.Success != true) { throw new Exception(); }
            return new SignatureRequestFile(response.FileName, "pdf", response.AttachmentFile);
        }

        public SignatureRequestStatus GetSignatureRequestStatus(string id)
        {
            Guid guidId;
            if (!Guid.TryParse(id, out guidId)) { throw new InvalidCastException("Signature id is not of a GUID type"); }

            var postingStatusResponse = _postingsService.GetPostingStatus(_signantConfiguration.DistributorId, _signantConfiguration.AccessCode, guidId);
           
            if (!postingStatusResponse.Success) { return SignatureRequestStatus.Failed; }
            
            if (postingStatusResponse.Status == PostingStatusTypes.Completed) { return SignatureRequestStatus.Signed; }
            if (postingStatusResponse.Status == PostingStatusTypes.Expired) { return SignatureRequestStatus.Expired; }

            return SignatureRequestStatus.New;
        }

        public SignedDocData SendDocumentForSigning(SignatureRequest request, SignatureRequestConfiguration config)
        {
            var posting = _mapper.Map<Posting>(request);
            var postingAdmins = new List<PostingAdmin>()
            {
                new PostingAdmin {

                    Email = _signantConfiguration.SenderEmail,
                    Name = _signantConfiguration.SenderName,
                    NotifyByEmail = true
                }
            };

            // TODO: add custom DateTime wrapper for mocking purposes
            posting.PostingAdmins = postingAdmins.ToArray();
            posting.WillBeDeletedDateTime = DateTime.UtcNow.AddDays(config.RequestTTLDays);
            posting.ActiveTo = DateTime.UtcNow.AddDays(config.RequestValidityDays);

            var response = _postingsService.CreateSignPosting(
                _signantConfiguration.DistributorId, _signantConfiguration.AccessCode, posting);


            var status = response.Success ? SignatureRequestStatus.New : SignatureRequestStatus.Failed;
            

            var attachmentId = response.AttachmentInfos.Select(info => info.AttachmentID).First();
            var attachment = new SignedAttachment(posting.Attachments.First().FileName, attachmentId.ToString());
            
            return new SignedDocData(request.Recipient, status, response.PostingID.ToString(), attachment);
        }
    }
}
