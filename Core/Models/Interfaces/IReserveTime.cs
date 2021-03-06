﻿using System;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IReserveTime
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        Guid ReservationId { get; set; }

        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}