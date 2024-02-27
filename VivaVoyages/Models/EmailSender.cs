using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential("tiendat552@gmail.com", "etkhsrdhmkoydftf")
        };

        return client.SendMailAsync(
            new MailMessage(from: "tiendat552@gmail.com",
                            to: email,
                            subject,
                            message
                            ));
    }
}