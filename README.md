# Notification Service

## Overview
The Notification Service is a microservices-based application designed to handle notifications across multiple channels (email, SMS, and push notifications). It provides a modular architecture for sending different types of notifications efficiently and reliably.

---

## Key Features
- **Email Notifications**: Send emails to specified recipients.
- **SMS Notifications**: Deliver SMS messages to recipients.
- **Push Notifications**: Push notifications to devices or platforms.
- **Centralized Gateway**: A Notification Gateway handles routing and queuing notifications.
- **Security**: Authentication is implemented using JWT (JSON Web Tokens).
- **Scalable Design**: RabbitMQ is used for queuing and distributing messages among microservices.
- **Error Handling**: Custom middleware for better error handling.
- **Logging**: Integrated with Serilog for structured logging and diagnostics.

---

## Services Architecture
1. **Notification Gateway**
   - Acts as a central hub for receiving and routing notification requests.
   - Validates and dispatches requests to the corresponding service (email, SMS, or push).

2. **Email Service**
   - Handles email notifications by consuming messages from the email queue.

3. **SMS Service**
   - Handles SMS notifications by consuming messages from the SMS queue.

4. **Push Service**
   - Handles push notifications by consuming messages from the push queue.

---

## Prerequisites
- .NET 7 SDK
- RabbitMQ server (ensure it is running locally or remotely)
- Visual Studio or any preferred code editor
- Postman (optional, for testing API requests)

---

## How to Run

1. **Clone the Repository**
   ```bash
   git clone https://github.com/YourUsername/NotificationService.git
   cd NotificationService
   ```

2. **Set Up RabbitMQ**
   - Ensure RabbitMQ is running locally or remotely.
   - Default configurations assume `localhost` as the RabbitMQ server.

3. **Build and Run the Solution**
   - Open the solution in Visual Studio.
   - Restore NuGet packages.
   - Run the solution (Notification Gateway and all services).

4. **Send Notifications**
   - Use Postman or another API client to send POST requests to the Notification Gateway:
     ```http
     POST http://localhost:5181/api/notifications
     Content-Type: application/json

     {
       "Type": "email", // or "sms" or "push"
       "Recipient": "recipient@example.com",
       "Message": "Your message here"
     }
     ```

---

## Logging and Monitoring
- All logs are stored in the `logs` folder under the root directory.
- Real-time logs can also be viewed in the application console output.

---

## Future Enhancements
- Add support for additional notification channels (e.g., WhatsApp, Telegram).
- Integrate a UI dashboard for managing and monitoring notifications.
- Implement retry mechanisms for failed notifications.

---

## License
This project is licensed under the MIT License. See the LICENSE file for details.

---

## Acknowledgments
- RabbitMQ: For providing a reliable message-queuing system.
- Serilog: For structured logging capabilities.
- .NET 7: For the robust framework enabling microservices architecture.

