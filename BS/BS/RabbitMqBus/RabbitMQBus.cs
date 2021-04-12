using BusDomainCore.Bus;
using BusDomainCore.Commands;
using BusDomainCore.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqBus
{
    public sealed class RabbitMQBus : IEventBus  //sealed lock other class not inherit this class
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers; // hold our hanlder for all the event
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }
        public Task SandCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        //use different microservices to publish event 
        //public void Publish<T>(T @event) where T : Event
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        var eventName = @event.GetType().Name;

        //        channel.QueueDeclare(eventName, false, false, false, null);
        //        var message = JsonConvert.SerializeObject(@event);
        //        var body = Encoding.UTF8.GetBytes(message);
        //        channel.BasicPublish("", eventName, null, body); // exchange type default cz [routing key = queue name ]
        //    }
        //}


        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;
                channel.ExchangeDeclare(eventName, type: "fanout", false, false, null);
                channel.ExchangeBind("Exchange-A", eventName, eventName); //your all handler - bind - 1 common exchange---bind-- - 1 Queue for this application

                channel.QueueDeclare("mahbub", false, false, false, null);
                channel.QueueBind("mahbub", "Exchange-A", eventName, null);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(eventName, eventName, null, body); // exchange type default cz [routing key = queue name ]
            }
        }

        public void Subscriber<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name; // get event name
            var handlerType = typeof(TH); // get handler name

            if (!_eventTypes.Contains(typeof(T))) // we have list of event so check already exist
            {
                _eventTypes.Add(typeof(T));
            }
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.Name.GetType() == handlerType)) // handler type already exist check
            {
                throw new ArgumentException("already exist");
            }
            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };
            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            //channel.QueueDeclare(eventName, false, false, false, null);
            channel.QueueDeclare("mahbub", false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;  //delegate : placeholder for an EVENT , method pointer 

            //channel.BasicConsume(eventName, true, consumer);
            channel.BasicConsume("mahbub", true, consumer);

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;  // key == eventName
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                // know which handler is subscribe to this type of event, do all in background
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subsciptions = _handlers[eventName];
                    foreach (var sub in subsciptions)
                    {
                        var handler = scope.ServiceProvider.GetService(sub);
                        if (handler == null) continue;
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                    /*
                    var subsciptions = _handlers[eventName]; // multiple subscriber to this event
                    foreach(var subscription in subsciptions)
                    {
                        var handler = Activator.CreateInstance(subscription);
                        if(handler == null) continue;
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event }); */
                }
            }
        }
    }
}
