using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }

        DateTime CreatedDateTime { get; set; }
        DateTime ModifiedDateTime { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}
