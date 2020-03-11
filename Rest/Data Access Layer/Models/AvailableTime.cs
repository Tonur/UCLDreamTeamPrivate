﻿using Data_Access_Layer.Interfaces;
using System;

namespace Data_Access_Layer.Models
{
    public class AvailableTime : IAvailableTime
    {
        public bool Available { get; set; }
        public int? Recurring { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
