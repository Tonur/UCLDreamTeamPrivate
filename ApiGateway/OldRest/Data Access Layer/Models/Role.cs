﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role(string roleName) : base(roleName)
        {
        }
    }
}