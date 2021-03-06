﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? ZipCode { get; set; }
        public string PasswordHash { get; set; }
    }
}
