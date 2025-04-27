using IceCreamService.Core.Configurations;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IceCreamService.Infrastructure.Repositories;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly AuthSettings _authSettings;

    public JwtTokenGenerator(IOptions<AuthSettings> authOptions)
    {
        _authSettings = authOptions.Value;

    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_authSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_authSettings.TokenExpirationMinutes),
            signingCredentials: credentials
        );

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}