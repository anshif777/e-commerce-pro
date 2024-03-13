using e_commerce_pro.Models;

namespace e_commerce_pro.Services
{
    public interface IEmailService
    {
        Task<string> GanerateOtpAsync(int length = 4);
        Task EmailSenderAsync(string email, string subject, string message);
    }
}