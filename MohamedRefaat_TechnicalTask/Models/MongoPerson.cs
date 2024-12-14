using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoPerson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]  // Ensure MongoDB's ObjectId is mapped to a string
    public string Id { get; set; }

    [BsonElement("name")]  // Map MongoDB field 'name' to 'Name' property
    public string Name { get; set; }

    [BsonElement("telephone Number")]  // Map MongoDB field 'telephone Number' to 'TelephoneNumber' property
    public string TelephoneNumber { get; set; }

    [BsonElement("address")]  // Map MongoDB field 'address' to 'Address' property
    public string Address { get; set; }

    [BsonElement("country")]  // Map MongoDB field 'country' to 'Country' property
    public string Country { get; set; }
}
