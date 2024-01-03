namespace Domain.Notifications
{
    public interface INotification
    {
        IList<Notification> Errors { get; }
    }
}
