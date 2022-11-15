using Microsoft.Extensions.Hosting;

namespace Signatures.StatusUpdateJob
{
    // it should be a cron job
    // but for the sake of a simple demo - it should be enough to simply run it once
    // StatusUpdatorJob should only be responsible for scheduling
    // the logic should be moved out (TODO?)
    public class StatusUpdatorJob : IHostedService
    {
        private readonly IStatusUpdateService _statusUpdateService;

        public StatusUpdatorJob(IStatusUpdateService statusUpdateService)
        {
            _statusUpdateService = statusUpdateService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // that's far away from optimal solution (e.g. we should only get pending items from db)
            // for the real life scenarions mostly likely even "paged"
            await _statusUpdateService.HandlePendingSignatureRequests();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
