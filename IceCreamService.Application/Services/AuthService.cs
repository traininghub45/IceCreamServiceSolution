using IceCreamService.Application.Helpers;
using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;

namespace IceCreamService.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<AuthResult> AuthenticateAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            _logger.LogWarning("Empty username or password in login attempt");
            throw new ArgumentException("Username and password must be provided");
        }

        try
        {
            User user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !CryptoHelper.VerifyHash(password, user.Password))
            {
                _logger.LogWarning("Failed login attempt for username: {Username}", username);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning("Attempt to login to disabled account: {Username}", username);
                throw new InvalidOperationException("User account is disabled");
            }

            string token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            return new AuthResult
            {
                Token = token,
                User = user
            };
        }
        catch (Exception ex) when (ex is not UnauthorizedAccessException)
        {
            _logger.LogError(ex, "Authentication error for username: {Username}", username);
            throw new AuthenticationException("Authentication failed", ex);
        }
    }
}