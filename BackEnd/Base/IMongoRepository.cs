using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace BackEnd
{
    public interface IMongoRepository<T, TKey> where T : IEntity<TKey>
    {
        IMongoCollection<T> Collection { get; }

        IMongoDatabase Database { get; }

        T GetEntityById(TKey id);

        IList<T> GetEntitiesByFilter(Expression<Func<T, bool>> clause = null);

        T GetEntityByFilter(Expression<Func<T, bool>> clause = null);

        T AddEntity(T entity);

        IList<T> AddEntities(IEnumerable<T> entities);

        T Update(T entity);

        T UpdateById(TKey id, T entity);

        IList<T> UpdateMany(IEnumerable<T> entities);

        bool DeleteById(TKey id);

        bool DeleteEntity(T entity);

        bool DeleteMany(IEnumerable<T> entities);

        bool Exist(T entity);

        bool EntitiesExist(Expression<Func<T, bool>> clause = null);

        long EntitiesCount();

        long EntitiesByFilterCount(Expression<Func<T, bool>> clause = null);

        void DeleteByFilter(Expression<Func<T, bool>> clause = null);

    }
    public interface IMongoRepository<T> : IMongoRepository<T, string>
        where T : IEntity<string>
    { }
}
