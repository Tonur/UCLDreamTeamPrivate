﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class UserCreatedEvent : Event
    {
        public IUser User { get; }

        public UserCreatedEvent(IUser user)
        {
            User = user;
        }   
    }
}
