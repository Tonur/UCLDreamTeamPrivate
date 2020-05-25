﻿using System.Threading.Tasks;
using AdminPanel.Client.DTOs;
using AdminPanel.Client.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Services
{
    public interface IAuthService
    {
        User CurrentUser { get; }

        Task<bool> Login(LoginDTO loginDTO);

        void Logout();
    }
}