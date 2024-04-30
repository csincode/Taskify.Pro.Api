using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Taskfy.Pro.Api.Models
{
    public class Todo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null; 
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? UserId { get; set; }
    }
}