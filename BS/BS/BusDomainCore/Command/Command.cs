using BusDomainCore.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusDomainCore.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; } // only those inherit set this time

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
