using AutoMapper;
using IceCreamService.Application.DTOs;
using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamService.API.Controllers
{
    [Authorize] // For all authenticated users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IEmailService emailService)
        {
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] UserDto userDto, IFormFile? file)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }
            return Ok(await _userService.UpdateAsync(_mapper.Map<User>(userDto), file));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDto userDto)
        {
            try
            {
                userDto.UserName = userDto.Email;
                await _userService.AddAsync(_mapper.Map<User>(userDto));
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            // Validate email format
            if (string.IsNullOrEmpty(request.Email) || !new EmailAddressValidator().IsValidEmail(request.Email))
            {
                return BadRequest("Invalid email format.");
            }
            await _emailService.SendEmailAsync(request.Email, "Forgot Password");
            return Ok();
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            await _userService.ResetPasswordAsync(request.Token, request.NewPassword);
            return NoContent();
        }
    }
}

