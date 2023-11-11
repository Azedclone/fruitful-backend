using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fruitful.dal.Collections;

public class OrderItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("order_id")] public ObjectId OrderId { get; set; }
    [BsonElement("fruit_id")] public ObjectId FruitId { get; set; }
    [BsonElement("quantity")] public int Quantity { get; set; }
}