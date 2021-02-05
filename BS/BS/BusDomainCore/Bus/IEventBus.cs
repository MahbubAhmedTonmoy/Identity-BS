using BusDomainCore.Commands;
using BusDomainCore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusDomainCore.Bus
{
    public interface IEventBus
    {
        Task SandCommand<T>(T command) where T : Command;  //MediatR library to send command
        void Publish<T>(T @event) where T : Event;
        void Subscriber<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }

    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event  // <in TEvent> takes in any type of event
    {
        Task Handle(TEvent @event);
    }
    public interface IEventHandler
    {

    }
}
