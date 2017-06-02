using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTaskRunner : IHandlerTaskRunner
    {
        private readonly IHandler _handler;
        private readonly Func<Task> _validateAsync;
        private readonly ISet<IHandlerTask> _handlerTasks;

        public HandlerTaskRunner(IHandler handler, 
            Func<Task> validateAsync, ISet<IHandlerTask> handlerTasks)
        {
            _handler = handler;
            _validateAsync = validateAsync;
            _handlerTasks = handlerTasks;
        }

        public IHandlerTask Run(Func<Task> runAsync)
        {
            var handlerTask = new HandlerTask(_handler, runAsync,
                                              _validateAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }
    }
}