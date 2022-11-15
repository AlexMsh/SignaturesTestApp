using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Repositories.Contracts;
using Signatures.Repositories.Contracts.Configuration;

namespace Signatures.Repositories
{
    public class MongoRepository<T> : IRepository<T>
        where T : EntityBase
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<T> _signatures;
        public MongoRepository(IOptions<DbConnectionConfiguration> dbConfiguration)
        {
            // TODO: mpve connection string to configuration
            var config = dbConfiguration.Value;
            _client = new MongoClient(config.ConnectionString);
            _database = _client.GetDatabase(config.DbName);
            _signatures = _database.GetCollection<T>(nameof(T));
            
        }

        public async Task<T> Get(string id)
        {
            var result = _signatures.Find(item => item.Id == id);
            return await result.FirstOrDefaultAsync();
        }


        // TODO: paging?
        public async Task<IEnumerable<T>> GetAll() => await _signatures.Find(new BsonDocument()).ToListAsync();

        public async Task Save(T request) => await _signatures.InsertOneAsync(request);

        public async Task Update(T item) => await _signatures.ReplaceOneAsync<T>(curr => curr.Id == item.Id, item);

        public async Task Delete(T item) => await _signatures.DeleteOneAsync<T>(curr => curr.Id == item.Id);
    }
}
