using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Earnit.Ninja.DataAccess.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IMongoDatabase _database;

        public GenericRepository(string connectionString, string dataBaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dataBaseName);
        }


        public void DeleteMany<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            //todo: look if any chance to extract collection on top
            var collection = _database.GetCollection<T>(typeof (T).Name);
            collection.DeleteManyAsync(expression);
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            //todo: look if any chance to extract collection on top
            var collection = _database.GetCollection<T>(typeof(T).Name);
            collection.DeleteOneAsync(expression);
        }

        public async void DeleteAll<T>() where T : class, new()
        {
            await _database.DropCollectionAsync(typeof (T).Name);
        }

        public async Task<T> Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var res = await collection.Find(Builders<T>.Filter.Where(expression)).SingleOrDefaultAsync();
            return res;
        }

        public Task<List<T>> Get<T>() where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            return collection.Find(new BsonDocument()).ToListAsync();
        }

        public Task<List<T>> Get<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            return collection.Find(expression).ToListAsync();
        }

        public void Add<T>(T item) where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            collection.InsertOneAsync(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            collection.InsertManyAsync(items);
        }
    }
}