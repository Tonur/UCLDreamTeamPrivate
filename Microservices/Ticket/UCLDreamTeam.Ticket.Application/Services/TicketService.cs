﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Domain.Models.Ticket> GetByIdAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Ticket>> GetByUserIdAsync(Guid id)
        {
            return await _ticketRepository.GetByUserIdAsync(id);
        }

        public async Task AddAsync(Domain.Models.Ticket ticket)
        {
            await _ticketRepository.AddAsync(ticket);
        }

        public async Task CreateAsync(Domain.Models.Ticket ticket)
        {
            await _ticketRepository.CreateAsync(ticket);
        }

        public async Task UpdateAsync(Domain.Models.Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task AddMessageAsync(Domain.Models.Message message)
        {
            await _ticketRepository.AddMessageAsync(message);
        }

        public async Task ChangeStatusById(Guid id, Status status)
        {
            await _ticketRepository.ChangeStatusById(id, status);
        }

        public async Task MessageSeen(Guid messageId, bool seen)
        {
            await _ticketRepository.MessageSeen(messageId, seen);
        }
    }
}