﻿using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class MockResourceService : IResourceService
    {
        private List<ResourceDTO> _resources = new List<ResourceDTO>
        {
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Køkken",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                        ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    }
                }
            },
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Mødelokale 1",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                        ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    },
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        FromDateTime = new DateTime(2020, 1, 1, 3, 1, 1),
                        ToDateTime = new DateTime(2020, 1, 1, 4, 2, 2),
                        Recurring = 1
                    }
                }
            },
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Biograf",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                        ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    }
                }
            }
        };

        public async Task<List<ResourceDTO>> GetAll()
        {
            return _resources;
        }

        public async Task<ResourceDTO> GetFromId(Guid id)
        {
            return _resources.First(resource => resource.Id == id);
        }

        public async Task DeleteFromId(Guid id)
        {
            _resources.Remove(await GetFromId(id));
        }
    }
}
