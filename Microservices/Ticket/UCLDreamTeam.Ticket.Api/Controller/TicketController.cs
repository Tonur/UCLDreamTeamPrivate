﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.Ticket.Application.Services;
using UCLDreamTeam.Ticket.Domain.Interfaces;
using UCLDreamTeam.Ticket.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLDreamTeam.Ticket.Api.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: <controller>?ticketId={id}
        [HttpGet]
        public async Task<ApiResponse<Domain.Models.Ticket>> GetTicket([FromQuery] Guid ticketId)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(ticketId);
                if (ticket == null)
                    return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.NotFound, null); //return NotFound();
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.OK, ticket); //return Ok(ticket);
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.ServiceUnavailable, null); //return StatusCode(503, e.Message);
            }
        }

        // GET <controller>/User?userId={id}
        [HttpGet("User")]
        public async Task<ApiResponse<IEnumerable<Domain.Models.Ticket>>> GetUserTicket([FromQuery] Guid userId)
        {
            try
            {
                var result = await _ticketService.GetByUserIdAsync(userId);
                if (result == null)
                    return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.BadRequest, null); //return BadRequest();
                if (!result.Any())
                    return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.NotFound, null); //return NotFound();
                return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.OK, result); //return Ok(ticket);
            }
            catch (Exception e)
            {
                return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }

        // Put <controller>/User?userId={id}
        [HttpPut]
        public async Task<ApiResponse<Domain.Models.Ticket>> UpdateTicket([FromBody] Domain.Models.Ticket ticket)
        {
            try
            {
                var result = await _ticketService.GetByIdAsync(ticket.Id);
                if (result == null)
                    return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.NotFound, null); //return NotFound();
                await _ticketService.UpdateAsync(ticket);
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.OK, result); //return Ok(ticket);
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }
    }
}
