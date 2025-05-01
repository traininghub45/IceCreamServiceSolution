using IceCreamService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using IceCreamService.Core.Interfaces;
using MimeKit;   // (لإنشاء الإيميل).       
using MailKit.Net.Smtp;  // (لإرسال الإيميل).

namespace IceCreamService.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public EmailService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task SendEmailAsync(string email, string subject)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                var token = Guid.NewGuid().ToString();
                var expirationTime = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour
                await _userRepository.SavePasswordResetTokenAsync(user.Id, token, expirationTime);

                // Send the email with the password reset link
                var resetLink = $"{_configuration["AppBaseUrl"]}/auth/reset-password?token={token}";
                var emailSettings = _configuration.GetSection("EmailSettings");
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Ice Cream", emailSettings["Sender"]));
                message.To.Add(new MailboxAddress(user.FullName, email));
                message.Subject = subject;
                var bodyText = $"<h1>Password Reset</h1>" +
                  $"<p>Click the link below to reset your password:</p>" +
                  $"<a href='{resetLink}'>Reset Password</a>";
                message.Body = new TextPart("html") { Text = bodyText };
            
                using var client = new SmtpClient();// SmtpClient هو المسؤول عن الاتصال بخادم البريد وإرسال الرسالة
                await client.ConnectAsync(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]), true);
                await client.AuthenticateAsync(emailSettings["Sender"], emailSettings["Password"]);
                await client.SendAsync(message);    // 11. إرسال الرسالة عبر الخادم
                await client.DisconnectAsync(true);
            }
        }
    }
}

