using Domain.Notifications;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public abstract class Entity : INotificationList
    {
        [BsonId()]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; } 
        

        public IList<Notification> Errors { get; protected set; }

        protected Entity()
        {
            Errors = new List<Notification>();
        }

        protected abstract void Validate();
    }
}
