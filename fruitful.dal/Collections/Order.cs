using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fruitful.dal.Collections;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("account_id")] public string AccountId { get; set; }

    [BsonElement("created_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; } = DateTime.Now;

    [BsonElement("items")] 
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    [BsonElement("total_price")] public decimal TotalPrice { get; set; }
    [BsonElement("delivery_address")] public string DeliveryAddress { get; set; }
}