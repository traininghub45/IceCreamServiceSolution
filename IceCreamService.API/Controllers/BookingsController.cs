﻿using AutoMapper;
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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _mapper = mapper;
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        [ActionName("GetByIdAsync")] // Explicit action name
        public async Task<ActionResult<BookingDto>> GetByIdAsync(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllAsync()
        {
            return Ok(await _bookingService.GetAllAsync());
        }

        [HttpGet("getByUserId")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllByUserIdAsync(
            [FromQuery] int userId,
            [FromQuery] int skip,
            [FromQuery] int take)
        {
            return Ok(await _bookingService.GetAllByUserIdAsync(userId, skip, take));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] BookingDto bookingDto)
        {
            await _bookingService.AddAsync(_mapper.Map<Booking>(bookingDto));
            return CreatedAtAction(nameof(GetByIdAsync), new { id = bookingDto.Id }, bookingDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] BookingDto bookingDto)
        {
            if (id != bookingDto.Id)
            {
                return BadRequest("ID mismatch");
            }
            await _bookingService.UpdateAsync(_mapper.Map<Booking>(bookingDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookingService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}