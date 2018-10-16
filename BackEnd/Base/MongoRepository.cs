using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;


namespace BackEnd
{
    public class MongoRepository<T, TKey> : IMongoRepository<T, TKey>
        where T : IEntity<TKey>
    {
        public IMongoCollection<T> Collection { get; }
        public IMongoDatabase Database { get; }

        public MongoRepository()
            : this(Util<TKey>.GetDefaultConnectionString())
        {
        }

        public MongoRepository(string connectionString)
        {
            this.Collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
            this.Database = Util<TKey>.GetDatabase();
        }

        public MongoRepository(string connectionString, string collectionName)
        {
            this.Collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public MongoRepository(MongoUrl url)
        {
            this.Collection = Util<TKey>.GetCollectionFromUrl<T>(url);
        }

        public MongoRepository(MongoUrl url, string collectionName)
        {
            this.Collection = Util<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }

        public T GetEntityById(TKey id)
        {
            var collectionName = Util<TKey>.GetCollectionName<T>();
            var collection = this.Database.GetCollection<T>(collectionName);

            T result = default(T);
            if (id == null)
            {
                return result;
            }
            var filter = Builders<T>.Filter.Eq(m => m.Id, id);
            var getByFilter = collection.Find<T>(filter);
            if (getByFilter.Any())
            {
                result = getByFilter.FirstOrDefault();
            }
            return result;
        }

        public IList<T> GetEntitiesByFilter(Expression<Func<T, bool>> clause = null)
        {
            if (clause == null)
            {
                return Collection.AsQueryable().ToList();
            }
            else
            {
                var filterPredicate = Builders<T>.Filter.Where(clause);
                return Collection.Find(filterPredicate).ToList();
            }
        }

        public T GetEntityByFilter(Expression<Func<T, bool>> clause = null)
        {
            if (clause == null)
            {
                return default(T);
            }
            else
            {
                var filterPredicate = Builders<T>.Filter.Where(clause);
                return Collection.Find(filterPredicate).FirstOrDefault();
            }
        }

        public T AddEntity(T entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            entity.ModifiedDateTime = DateTime.Now;
            Collection.InsertOne(entity);
            return entity;
        }

        public IList<T> AddEntities(IEnumerable<T> entities)
        {
            var result = new List<T>();
            foreach (var entity in entities)
            {
                result.Add(AddEntity(entity));
            }
            return result;
        }

        public T Update(T entity)
        {
            entity.ModifiedDateTime = DateTime.Now;
            var filter = Builders<T>.Filter.Eq(m => m.Id, entity.Id);
            Collection.ReplaceOne(filter, entity);
            return entity;
        }

        public T UpdateById(TKey id, T entity)
        {
            entity.ModifiedDateTime = DateTime.Now;
            var filter = Builders<T>.Filter.Eq(m => m.Id, id);
            Collection.ReplaceOne(filter, entity);
            return entity;
        }

        public IList<T> UpdateMany(IEnumerable<T> entities)
        {
            var result = new List<T>();
            foreach (var entity in entities)
            {
                var updateEntity = Update(entity);
                result.Add(updateEntity);
            }
            return result;
        }

        public bool DeleteById(TKey id)
        {
            var filter = Builders<T>.Filter.Eq(m => m.Id, id);
            Collection.DeleteOne(filter);
            return GetEntityById(id).Id == null;
        }

        public bool DeleteEntity(T entity)
        {
            var filter = Builders<T>.Filter.Eq(m => m.Id, entity.Id);
            Collection.DeleteOne(filter);
            return GetEntityById(entity.Id).Id == null;
        }

        public bool DeleteMany(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    DeleteEntity(entity);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Exist(T entity)
        {
            var filter = Builders<T>.Filter.Eq(m => m.Id, entity.Id);
            return Collection.Count(filter) > 0;
        }

        public bool EntitiesExist(Expression<Func<T, bool>> clause = null)
        {
            if (clause == null)
            {
                return Collection.AsQueryable().Any();
            }
            var filter = Builders<T>.Filter.Where(clause);
            return Collection.Count(filter) > 0;
        }

        public long EntitiesCount()
        {
            return Queryable.Count(Collection.AsQueryable());
        }

        public long EntitiesByFilterCount(Expression<Func<T, bool>> clause = null)
        {
            if (clause == null)
            {
                return Collection.AsQueryable().Count();
            }
            var filter = Builders<T>.Filter.Where(clause);
            return Collection.Count(filter);
        }

        public void DeleteByFilter(Expression<Func<T, bool>> clause = null)
        {
            var filter = Builders<T>.Filter.Where(clause);
            Collection.DeleteMany(filter);
        }
    }
    public class MongoRepository<T> : MongoRepository<T, string>, IMongoRepository<T>
        where T : IEntity<string>
    {
        public MongoRepository()
            : base() { }

        public MongoRepository(MongoUrl url)
            : base(url) { }

        public MongoRepository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }

        public MongoRepository(string connectionString)
            : base(connectionString) { }

        public MongoRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}
