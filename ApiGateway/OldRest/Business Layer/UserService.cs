﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business_Layer.Models;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace Business_Layer
{
    public class UserService
    {
        private readonly IdentityContext _identityContext;
        private readonly Mapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IdentityContext identityContext, Mapper mapper, UserManager<User> userManager)
        {
            _identityContext = identityContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> RegisterAsync(User userToRegister)
        {
            var user = userToRegister;
            var result = await _userManager.CreateAsync(user, userToRegister.PasswordHash);
            if (result.Succeeded) return new ApiResponse<string>(ApiResponseCode.OK, "");
            return new ApiResponse<string>(ApiResponseCode.InternalServerError, "");
        }

        public ApiResponse<User> Update(User userData)
        {
            //Prevent changing the ID
            userData.Id = Guid.Empty;
            var userToChange = GetUserFromIdAsync(userData.Id).Result;
            // Can only update an existing user
            if (userToChange == null) return new ApiResponse<User>(ApiResponseCode.UnAuthenticated, null);

            // Update the user
            if (!string.IsNullOrWhiteSpace(userData.PasswordHash) && userData.PasswordHash != userToChange.PasswordHash)
                //If the password is unchanged or empty, this does not update the password
                userData.PasswordHash = userToChange.PasswordHash;
            // Automapper is configured to only overwrite the fields that are not null
            _mapper.Map(userData, userToChange);

            _identityContext.Update(userToChange);
            _identityContext.SaveChanges();

            return new ApiResponse<User>(ApiResponseCode.OK, userToChange);
        }

        // ----- Internal methods -----

        public async Task<User> GetUserFromUserNameAsync(string Username)
        {
            return await _userManager.FindByNameAsync(Username);
        }

        public async Task<User> GetUserFromEmailAsync(string Email)
        {
            return await _userManager.FindByEmailAsync(Email);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<User> GetUserFromIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}