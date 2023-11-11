using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fruitful.dal.Collections;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("account_id")] public ObjectId AccountId { get; set; }
    [BsonElement("created_at")] public DateTime CreatedAt { get; set; } = DateTime.Now;
    [BsonElement("total_price")] public decimal TotalPrice { get; set; }
    [BsonElement("delivery_address")] public string DeliveryAddress { get; set; }
}