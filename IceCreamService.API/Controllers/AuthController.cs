using AutoMapper;
using IceCreamService.Application.DTOs;
using IceCreamService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;



        public AuthController(IAuthService authService, IMapper mapper, ILogger<AuthController> logger)
        {
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var authResult = await _authService.AuthenticateAsync(userLoginDto.Email, userLoginDto.Password);

                return Ok(new AuthResponseDto
                {
                    Token = authResult.Token,
                    User = _mapper.Map<UserDto>(authResult.User)
                });
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Failed login attempt for {Email}", userLoginDto.Email);
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", userLoginDto.Email);
                return StatusCode(500, new { Message = "An error occurred while processing your request" });
            }
        }
    }
}
