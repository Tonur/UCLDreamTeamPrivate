﻿using System;
using System.Threading.Tasks;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Application.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(Domain.Models.User user, Role role = null);

        Task<Domain.Models.User> Update(Domain.Models.User user, Role role = null);

        Task<Domain.Models.User> GetUserFromIdAsync(Guid id);

        Task<Domain.Models.User> GetUserFromUserNameAsync(string userName);

        Task DeleteUserFromIdAsync(Guid id);
    }
}
