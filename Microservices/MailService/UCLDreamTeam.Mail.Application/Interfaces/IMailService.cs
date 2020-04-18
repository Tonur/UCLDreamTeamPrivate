﻿using System.Net.Mail;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Application.Interfaces
{
    public interface IMailService
    {
        public void SendMail(Reservation reservation, Template template);
    }
}