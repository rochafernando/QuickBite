namespace Domain.Notifications
{
    public class Notification
    {
        public int Code { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
