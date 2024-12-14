using MohamedRefaat_TechnicalTask.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using static PersonService;

public class MongoRepository
{
    private readonly IMongoCollection<MongoPerson> _personCollection;

    public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _personCollection = mongoDatabase.GetCollection<MongoPerson>(collectionName);
    }

    public List<MongoPerson> GetPersonsFromMongo()
    {
        return _personCollection.Find(_ => true).ToList();
    }
  
}
