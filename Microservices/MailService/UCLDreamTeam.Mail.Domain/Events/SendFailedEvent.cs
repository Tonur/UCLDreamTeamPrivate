﻿using System;
using System.Net.Mail;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class SendFailedEvent : Event
    {
        public MailMessage MailMessage { get; }
        public Exception Exception { get; }

        public SendFailedEvent(MailMessage mailMessage, Exception exception)
        {
            MailMessage = mailMessage;
            Exception = exception;
        }
    }
}