using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Models;
using NotificationGateway.Services;

namespace NotificationGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly RabbitMQPublisher _emailPublisher;
        private readonly RabbitMQPublisher _smsPublisher;
        private readonly RabbitMQPublisher _pushPublisher;

        public NotificationsController()
        {
            _emailPublisher = new RabbitMQPublisher("email_queue");
            _smsPublisher = new RabbitMQPublisher("sms_queue");
            _pushPublisher = new RabbitMQPublisher("push_queue");
        }

        [HttpPost]
        public IActionResult SendNotification([FromBody] NotificationRequest request)
        {
            if (request.Type == "email")
            {
                _emailPublisher.Publish(request);
            }
            else if (request.Type == "sms")
            {
                _smsPublisher.Publish(request);
            }
            else if (request.Type == "push")
            {
                _pushPublisher.Publish(request);
            }
            else
            {
                return BadRequest("Invalid notification type.");
            }

            return Ok("Notification sent successfully!");
        }
    }
}
