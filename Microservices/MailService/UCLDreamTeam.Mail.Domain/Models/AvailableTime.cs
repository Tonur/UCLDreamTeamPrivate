﻿using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class AvailableTime : IAvailableTime
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public bool Recurring { get; set; }
        public DateTime To { get; set; }
        public Guid ResourceId { get; set; }
    }
}