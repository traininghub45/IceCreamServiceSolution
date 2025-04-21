using System.Security.Cryptography;
using IceCreamService.Application.DTOs;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;

namespace IceCreamService.Application.Interfaces
{
    public class AuthService : IAuthService
    {
        // Add dependencies like user repository, token service, etc.
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private static readonly byte[] StaticKey = System.Text.Encoding.UTF8.GetBytes("mohammed1961998");

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateAsync(string userName,string password)
        {
            //var user = await _userRepository.GetByUsernameAsync(userName);
            //if (user == null || !VerifyPasswordHash(password, user?.Password))
            //    throw new UnauthorizedAccessException("Invalid credentials");

            //// Generate token
            return "";
        }

        public async Task RegisterAsync(UserDto request)
        {
            //f (await _userRepository.UserExistsAsync(request.Email))
              //  throw new InvalidOperationException("Username already exists");

            var user = new User
            {
                FullName = request.FullName,
                Username = request.Email,
                Email = request.Email,
                Password = HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user);
        }



        private string HashPassword(string password)
        {
            using var hmac = new HMACSHA512(StaticKey);
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using var hmac = new HMACSHA512(StaticKey);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return storedHash == Convert.ToBase64String(computedHash);
        }
    }
}
