﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    public interface IResourceService
    {
        Task<List<ResourceDTO>> GetAll();

        Task<ResourceDTO> GetFromId(Guid id);

        Task DeleteFromId(Guid id);

        Task Add(ResourceDTO resource);

        Task Update(ResourceDTO resource);
    }
}