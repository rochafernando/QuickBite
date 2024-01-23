using Domain.Notifications;

namespace Domain.Entities
{
    public abstract class Entity : INotificationList
    {
        public int Id { get; protected set; }   

        public IList<Notification> Errors { get; protected set; }

        protected Entity()
        {
            Errors = new List<Notification>();
        }

        protected abstract void Validate();
    }
}
