using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fruitful.dal.Collections;

public abstract class OrderItem
{
    [BsonElement("product_id")] public string ProductId { get; set; }
    [BsonElement("quantity")] public int Quantity { get; set; }
}