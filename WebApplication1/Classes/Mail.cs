using System;
using System.Net;
using System.Net.Mail;
using SendGrid;


namespace WebApplication1.Classes
{
    public class Mail
    {
        public int sendMail(string mode, string email, string body, string value)
        {
            int status = 1;

            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new MailAddress("noreply@citycrowd.cc", "CityCrowd");
            myMessage.Subject = subject(mode, value);
            myMessage.Html = body;

            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential("azure_139ab79009e0fe7e8fd0c1f7f15de2cf@azure.com", "Y10YQwX29Sc3Ii6");

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            // You can also use the **DeliverAsync** method, which returns an awaitable task.
            transportWeb.DeliverAsync(myMessage);

            return status;
        }

        public string subject(string mode, string value)
        {
            switch (mode)
            {
                case "password":
                    {
                        return "Forgot Password";
                        break;
                    }
                case "register":
                    {
                        return "Welcome to CityCrowd!";
                        break;
                    }
                case "invite":
                    {
                        return value + " invited you to join CityCrowd!";
                        break;
                    }
                case "verifyemail":
                    {
                        return "Verify your email address";
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
        }
    }
}