using AutoMapper;
using IceCreamService.Application.DTOs;
using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactsController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactMessageDto>> GetByIdAsync(int id)
        {
            var contactMessage = _mapper.Map<ContactMessageDto>(await _contactService.GetByIdAsync(id));
            if (contactMessage == null)
            {
                return NotFound();
            }
            return Ok(contactMessage);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetAllAsync()
        {
            return Ok(_mapper.Map<ContactMessageDto>(await _contactService.GetAllMessagesAsync()));
        }

        [HttpGet("unread")]
        public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetUnreadAsync()
        {
            return Ok(_mapper.Map<ContactMessageDto>(await _contactService.GetUnreadMessagesAsync()));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SubmitContactForm([FromBody] ContactMessageDto contactMessageDto)
        {
            await _contactService.SubmitContactFormAsync(_mapper.Map<ContactMessage>(contactMessageDto));
            return CreatedAtAction(nameof(GetByIdAsync), new { id = contactMessageDto.Id }, contactMessageDto);
        }

        [HttpPut("{id}/mark-as-read")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            await _contactService.MarkMessageAsReadAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ContactMessageDto contactMessageDto)
        {
            if (id != contactMessageDto.Id)
            {
                return BadRequest();
            }
            await _contactService.UpdateAsync(_mapper.Map<ContactMessage>(contactMessageDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _contactService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}