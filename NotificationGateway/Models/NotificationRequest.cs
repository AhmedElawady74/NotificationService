namespace NotificationGateway.Models
{
    public class NotificationRequest
    {
        public string Type { get; set; } // email, sms, push
        public string Recipient { get; set; } 
        public string Message { get; set; } 
    }
}
