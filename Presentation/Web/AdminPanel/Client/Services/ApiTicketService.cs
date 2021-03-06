﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.Services
{
    public class ApiTicketService : ITicketService
    {
        private readonly ApiClient _client;

        public ApiTicketService(ApiClient client)
        {
            _client = client;
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            var response = await _client.GetAsync<Ticket>($"/Ticket?ticketId={id.ToString()}");
           
            return response.Value;
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
        {
            var response = await _client.GetAsync<IEnumerable<Ticket>>($"Ticket/User?userId={userId.ToString()}");
            
            return response.Value;
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            await _client.PutAsync<Ticket>($"Ticket", ticket);
        }
    }
}