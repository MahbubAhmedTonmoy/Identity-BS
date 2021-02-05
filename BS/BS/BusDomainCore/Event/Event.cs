using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusDomainCore.Events
{
    public abstract class Event
    {
        public DateTime Timestamp { get; protected set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }

    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
