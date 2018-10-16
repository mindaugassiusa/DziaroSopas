using System;
using System.Runtime.Serialization;
using BackEnd;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataContract
{
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity<string>
    {
        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ModifiedDateTime { get; set; }
    }
}
