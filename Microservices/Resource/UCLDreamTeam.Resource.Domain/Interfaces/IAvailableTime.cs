﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.Resource.Domain.Interfaces
{
    interface IAvailableTime
    {
        Guid Id { get; set; }
        Guid ResourceId { get; set; }
        bool Recurring { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
