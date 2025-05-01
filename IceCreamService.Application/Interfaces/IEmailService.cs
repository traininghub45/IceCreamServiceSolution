using IceCreamService.Core.Entities;
using Microsoft.AspNetCore.Http;


namespace IceCreamService.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject);
    }
}
