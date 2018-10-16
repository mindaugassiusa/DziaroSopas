using System;
using System.Configuration;
using DataContract;
using MongoDB.Driver;

namespace BackEnd
{
    public static class Util<U>
    {
        private static string DefaultConnectionstringName = "MySuperConnectionString";
        private static string DefaultDatabaseName = "MyDatabase";
        private static IMongoDatabase mongoDatabase;

        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.AppSettings[DefaultConnectionstringName].ToString();
        }

        public static IMongoDatabase GetDatabase()
        {
            return mongoDatabase;
        }

        public static IMongoDatabase GetDatabaseFromUrl(string connection)
        {

            var url = new MongoUrl(connection);
            MongoClient client;

            if (!string.IsNullOrEmpty(url.Username) || !string.IsNullOrEmpty(url.Password))
            {
                var credentials = GetMongoCredentialsFromUrl(connection);
                var mongoSettings = new MongoClientSettings
                {
                    Credentials = new MongoCredential[] { credentials },
                    Server = url.Server
                };
                client = new MongoClient(mongoSettings);
                DefaultDatabaseName = url.DatabaseName;
                mongoDatabase = client.GetDatabase(DefaultDatabaseName);
            }
            else
            {
                client = new MongoClient(url);
                DefaultDatabaseName = url.DatabaseName;
                mongoDatabase = client.GetDatabase(DefaultDatabaseName);
            }

            return mongoDatabase;
        }

        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString)
            where T : IEntity<U>
        {
            return GetCollectionFromConnectionString<T>(connectionString, GetCollectionName<T>());
        }

        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString, string collectionName)
            where T : IEntity<U>
        {
            return GetDatabaseFromUrl(connectionString).GetCollection<T>(collectionName);
        }

        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url)
            where T : IEntity<U>
        {
            return GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName)
            where T : IEntity<U>
        {
            return GetDatabaseFromUrl(url.ToString()).GetCollection<T>(collectionName);
        }

        public static string GetCollectionName<T>() where T : IEntity<U>
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectioNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }


        private static MongoCredential GetMongoCredentialsFromUrl(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity("admin", mongoUrl.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(mongoUrl.Password);
            MongoCredential mongoCredential = new MongoCredential("SCRAM-SHA-1", internalIdentity, passwordEvidence);
            return mongoCredential;
        }

        private static string GetCollectioNameFromInterface<T>()
        {
            string collectionname;
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            if (att != null)
            {
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }
            return collectionname;
        }

        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));
            if (att != null)
            {
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                if (typeof(Entity).IsAssignableFrom(entitytype))
                {
                    while (entitytype.BaseType != null && !entitytype.BaseType.Equals(typeof(Entity)))
                    {
                        entitytype = entitytype.BaseType;
                    }
                }
                collectionname = entitytype.Name;
            }

            return collectionname;
        }
    }
}
