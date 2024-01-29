namespace Domain.Notifications
{
    public interface INotificationList
    {
        IList<Notification> Errors { get; }
    }
}
