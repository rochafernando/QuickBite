using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> notifications;
        public IReadOnlyList<Notification> Notifications => notifications;
        public bool HasNotifications => notifications.Any();

        public NotificationContext()
        {
            notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public void AddNotification(int code, string title, string message)
        {
            notifications.Add(new Notification { Code = code, Title = title, Message = message });
        }

        public void AddNotification(INotification notification)
        {
            notification.Errors.ToList().ForEach(n => notifications.Add(n));
        }

        public void Clean()
        {
            notifications?.Clear();
        }
    }
}
