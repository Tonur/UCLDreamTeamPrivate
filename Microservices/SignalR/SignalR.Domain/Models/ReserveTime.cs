﻿using System;
using Models.Interfaces;

namespace SignalR.Domain.Models
{
    public class ReserveTime : IReserveTime
    {
        public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}