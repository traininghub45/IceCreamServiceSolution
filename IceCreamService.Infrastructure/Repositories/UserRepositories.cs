using IceCreamService.Application.Helpers;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IceCreamService.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username) ?? null;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email) ?? null;
        }

        public async Task SavePasswordResetTokenAsync(int userId, string token, DateTime expirationTime)
        {
            var resetToken = new PasswordResetToken
            {
                UserId = userId,
                Token = token,
                ExpirationTime = expirationTime
            };

            await _context.PasswordResetTokens.AddAsync(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token && t.ExpirationTime > DateTime.UtcNow);

            if (resetToken == null)
                throw new ArgumentException("Invalid or expired token");

            var user = await GetByIdAsync(resetToken.UserId);
            if (user == null)
                throw new ArgumentException("User not found");

            user.Password = CryptoHelper.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            // Optional: delete the token
            _context.PasswordResetTokens.Remove(resetToken);
            await _context.SaveChangesAsync();
        }
    }

}
