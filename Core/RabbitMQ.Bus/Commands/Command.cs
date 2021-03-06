﻿using System;
using RabbitMQ.Bus.Events;

namespace RabbitMQ.Bus.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; protected set; }
    }
}