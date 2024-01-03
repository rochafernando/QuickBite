using Domain.Notifications;

namespace Domain.Entities
{
    public abstract class Entity : INotification
    {
        public IList<Notification> Errors { get; protected set; }

        protected Entity()
        {
            Errors = new List<Notification>();
        }

        protected abstract void Validate();
    }
}
