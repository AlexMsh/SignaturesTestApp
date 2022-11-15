using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Signarutes.Domain.Contracts;
using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Repositories.Contracts;
using Sigtatures.Web.Filters;
using Sigtatures.Web.ModelBinders;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignaturesController : ControllerBase
    {
        private readonly IDocumentSignatureService _documentSignatureService;
        private readonly IRepository<SignedDocData> _repository;
        public SignaturesController(
            IDocumentSignatureService documentSignatureService,
            IRepository<SignedDocData> repository)
        {
            _documentSignatureService = documentSignatureService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<SignedDocData>> Get() => await _repository.GetAll();


        // TODO: input validation
        [HttpGet("{id}")]
        public async Task<SignedDocData> Get(string id) => await _repository.Get(id);

        // POST api/<SignaturesController>
        [HttpPost, DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidationFilter<SignatureRequest>]
        public async Task<IActionResult> Post(
            [ModelBinder(BinderType = typeof(SignatureRequestBinder))] SignatureRequest signatureRequest)
        {
            await _documentSignatureService.SendDocumentForSigning(signatureRequest);
            return Ok();
        }
    }
}
