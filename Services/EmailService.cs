
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace e_commerce_pro.Services
{
    public class EmailService : IEmailService
    {
        private readonly Smtpsettingscs _smtpsettingscs;

        public EmailService(IOptions<Smtpsettingscs> smtpSettings)
        {
            _smtpsettingscs = smtpSettings.Value;
        }
        public async Task EmailSenderAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient
            {
                Host = _smtpsettingscs.SmtpServer,
                Port = _smtpsettingscs.Port,
                Credentials = new NetworkCredential(_smtpsettingscs.Username,_smtpsettingscs.Password),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpsettingscs.Username),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                // Handle the exception appropriately
            }
        }
        public async Task<string> GanerateOtpAsync(int length = 4)
        {
            var random = new Random();
            var otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 9).ToString();
            }
            return otp;

        }

      
    }
}

