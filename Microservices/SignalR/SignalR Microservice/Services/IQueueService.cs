﻿using System;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public interface IQueueService
    {
        int QueueCount { get; }
        void Enqueue(string connectionId, Guid ticketId);
        Task<Guid> Dequeue(string groupId);
    }
}