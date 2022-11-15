using Signarutes.Domain.Contracts.models.Response;

namespace Signatures.Repositories.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        Task Save(T request);
        Task Update(T item);
        Task Delete(T item);
        Task<IEnumerable<T>> GetAll();

        // a custom Id class should ideally be used here instead of Guid (for the sake of flexibility)
        // TODO: fix it, in case there will be free time for that
        Task<T> Get(string id);
    }
}
