using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Signarutes.Domain.Contracts.Configuration;
using Signarutes.Domain.Contracts.models;
using Signarutes.Domain.Contracts.models.Request;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Client.Proxy.Contracts;
using Signatures.Repositories.Contracts;

namespace Signatures.StatusUpdateJob
{
    // it should be a cron job
    // but for the sake of a simple demo - it should be enough to simply run it once
    // StatusUpdatorJob should only be responsible for scheduling
    // the logic should be moved out (TODO?)
    public class StatusUpdatorJob : IHostedService
    {
        private readonly IRepository<SignedDocData> _repository;
        private readonly IDocumentSignatureProxy _proxy;
        private readonly FileStorageConfiguration _fileStorageConfiguration;

        public StatusUpdatorJob(
            IRepository<SignedDocData> repository, 
            IDocumentSignatureProxy proxy,
            IOptions<FileStorageConfiguration> fileStorageConfiguration)
        {
            _repository = repository;
            _proxy = proxy;
            _fileStorageConfiguration = fileStorageConfiguration.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // that's far away from optimal solution (e.g. we should only get pending items from db)
            // for the real life scenarions mostly likely even "paged"
            var signatureRequests = await _repository.GetAll();
            var pendingRequests = signatureRequests
                .Where(r => r.Status == SignatureRequestStatus.New)
                // the result of incremental db "schema" changes
                // the db could be cleaned out and the following line would be removed, but was left intentionally
                .Where(r => !string.IsNullOrWhiteSpace(r.Attachment?.Id))
                .ToList();

            foreach (var r in pendingRequests)
            {
                var status = _proxy.GetSignatureRequestStatus(r.Id);
                if (status == SignatureRequestStatus.Failed)
                {
                    // could we consider this as logging? (wouldn't like to bother with adding any loggers, even with the console as an output
                    Console.WriteLine($"Failed to retrieve status for posting {r.Id}");
                }

                if (status == SignatureRequestStatus.Expired)
                {
                    await _repository.Delete(r);
                }

                if (status == SignatureRequestStatus.Signed)
                {
                    r.Status = status;
                    var file = await _proxy.DownloadAttachmentAsync(r.Id, r.Attachment.Id);
                    SaveFile(file);
                    
                    await  _repository.Update(r);
                }
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void SaveFile(SignatureRequestFile file)
        {
            var filePath = Path.Combine(_fileStorageConfiguration.Path, file.Name);
            using (var writer = new BinaryWriter(File.Create(filePath)))
            {
                writer.Write(file.Content);
            }
        }
    }
}
