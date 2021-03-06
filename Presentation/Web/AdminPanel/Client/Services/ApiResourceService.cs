﻿using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class ApiResourceService : IResourceService
    {
        private readonly ApiClient _client;

        public ApiResourceService(ApiClient client)
        {
            _client = client;
        }

        public async Task Add(ResourceDTO resource)
        {
            await _client.PostAsync<ResourceDTO>("Resource", resource);
        }

        public async Task DeleteFromId(Guid id)
        {
            await _client.DeleteAsync<List<ResourceDTO>>("Resource/guid=" + id.ToString());
        }

        public async Task<List<ResourceDTO>> GetAll()
        {
            var response = await _client.GetAsync<List<ResourceDTO>>("Resource");

            return response.Value;
        }

        public async Task<ResourceDTO> GetFromId(Guid id)
        {
            var response = await _client.GetAsync<ResourceDTO>("Resource/guid=" + id.ToString());

            Console.WriteLine(id);

            return response.Value;
        }

        public async Task Update(ResourceDTO resource)
        {
            await _client.PutAsync<ResourceDTO>("Resource", resource);
        }
    }
}
