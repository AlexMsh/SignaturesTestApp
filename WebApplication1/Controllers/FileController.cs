using Microsoft.AspNetCore.Mvc;
using Signarutes.Domain.Contracts;
using Signatures.HelperServices;

namespace Sigtatures.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDocumentSignatureService _documentSignatureService;
        private readonly IFileStorageService _fileStorageService;
        public FileController(
            IDocumentSignatureService documentSignatureService,
            IFileStorageService fileStorageService
            )
        {
            _documentSignatureService = documentSignatureService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        [Route("signedFile/{signatureRequestId}")]
        public async Task<IActionResult> PhysicalLocation(string signatureRequestId)
        {
            // in a pefect world we would have application level services
            // which would combine domain with "helpers" (e.g. tach stuff)
            // but i don't think it makes sense to add it for this endpoint only

            var signedAttachment = await _documentSignatureService.GetSignedAttachment(signatureRequestId);
            var fileContent = await _fileStorageService.GetFileContent(signedAttachment.Name);
           
            return new FileStreamResult(fileContent, "application/pdf");
        }
    }
}
