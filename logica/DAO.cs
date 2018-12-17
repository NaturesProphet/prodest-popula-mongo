using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace popMongo
{
    class DAO
    {
        private String mongoHost;
        private int mongoPort;
        private String mongoUser;
        private String mongoPassword;
        private String mongoAuthMethod;
        private String MongoConnectionURL;
        private String mongoDatabase;
        private String mongoCollection;
        public DAO()
        {
            this.mongoHost = Ambiente.getMongoHost();
            this.mongoPort = Ambiente.getMongoPort();
            this.mongoUser = Ambiente.getMongoUser();
            this.mongoPassword = Ambiente.getMongoPassword();
            this.mongoDatabase = Ambiente.getMongoDatabase();
            this.mongoCollection = Ambiente.getMongoCollection();
            this.mongoAuthMethod = Ambiente.getMongoAuthMethod();
            this.MongoConnectionURL = "mongodb://" + this.mongoUser + ":" + this.mongoPassword + "@" +
            this.mongoHost + ":" + this.mongoPort + "/" + this.mongoAuthMethod;
        }
        public void connect(String message)
        {
            Console.WriteLine(this.MongoConnectionURL);
            MongoClient client = new MongoClient(this.MongoConnectionURL);
            IMongoDatabase db = client.GetDatabase(this.mongoDatabase);
            var collection = db.GetCollection<BsonDocument>(this.mongoCollection);
            var document = BsonSerializer.Deserialize<BsonDocument>(message);
            Console.WriteLine(document);
            collection.InsertOneAsync(document);
            Console.WriteLine("query " + collection);
        }
    }
}

