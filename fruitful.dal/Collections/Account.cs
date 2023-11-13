namespace fruitful.dal.Collections;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("username")]
    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
    public string Username { get; set; }
    
    [BsonElement("name")]
    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 50 characters.")]
    public string Name { get; set; }

    [JsonIgnore] // Keep password out of any serialized responses
    [BsonElement("password")]
    [Required]
    [StringLength(100, ErrorMessage = "Password must be strong and at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [BsonElement("image")] [Url] public string? Image { get; set; }

    [BsonElement("phone")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; }

    [BsonElement("address")] public string? Address { get; set; }

    [BsonElement("dob")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? Dob { get; set; }

    [BsonElement("role")] [Required] public string Role { get; set; } = "EMPLOYEE"; // Default role is USER

    [BsonElement("created_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("is_active")] public bool IsActive { get; set; } = true; // Accounts are active by default

    // Override ToString for better debug display
    public override string ToString()
    {
        return $"Account: {Username} (Role: {Role}, Active: {IsActive})";
    }
}