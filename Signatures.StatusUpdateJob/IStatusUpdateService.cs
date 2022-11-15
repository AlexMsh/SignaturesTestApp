namespace Signatures.StatusUpdateJob
{
    public interface IStatusUpdateService
    {
        Task HandlePendingSignatureRequests();
    }
}
