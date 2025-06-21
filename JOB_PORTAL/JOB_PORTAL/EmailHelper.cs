using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace JOB_PORTAL
{
    public class EmailHelper
    {
        public static async Task SendEmailAsync(IConfiguration config, string toEmail, string subject, string body)
        {
            var es = config.GetSection("EmailSettings");
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(es["SenderName"], es["SenderEmail"]));
            msg.To.Add(MailboxAddress.Parse(toEmail));
            msg.Subject = subject;
            msg.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(es["SmtpServer"], int.Parse(es["Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(es["Username"], es["Password"]);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
