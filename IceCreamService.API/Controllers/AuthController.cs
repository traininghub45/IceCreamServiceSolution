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


        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
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
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
        {
            try
            {
                await _authService.RegisterAsync(request);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
