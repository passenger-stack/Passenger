using System;
using System.Threading.Tasks;
using NLog;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IHandler _handler;
        private readonly Func<Task> _runAsync;
        private Func<Task> _validateAsync;
        private Func<Task> _alwaysAsync;
        private Func<Task> _onSuccessAsync;
        private Func<Exception, Task> _onErrorAsync;
        private Func<Exception, Logger, Task> _onErrorWithLoggerAsync;
        private Func<PassengerException, Task> _onCustomErrorAsync;
        private Func<PassengerException, Logger, Task> _onCustomErrorWithLoggerAsync;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Func<Task> runAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync, 
            Func<Task> validateAsync = null)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validateAsync = validateAsync;
        }

        public IHandlerTask Always(Func<Task> always)
        {
            _alwaysAsync = always;

            return this;
        }

        public IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<PassengerException, Logger, Task> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLoggerAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false)
        {
            _onErrorAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Logger, Task> onError, bool propagateException = false)
        {
            _onErrorWithLoggerAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccessAsync = onSuccess;

            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;

            return this;
        }

        public IHandler Next() => _handler;

        public async Task ExecuteAsync()
        {
            try
            {
                if(_validateAsync != null)
                {
                    await _validateAsync();
                }
                await _runAsync();
                if(_onSuccessAsync != null)
                {
                    await _onSuccessAsync();
                }
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(exception);
                if(_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if (_alwaysAsync != null)
                {
                    await _alwaysAsync();
                }
            }
        }

        private async Task HandleExceptionAsync(Exception exception)
        {
			var customException = exception as PassengerException;
			if (customException != null)
			{
				if (_onCustomErrorWithLoggerAsync != null)
				{
					await _onCustomErrorWithLoggerAsync(customException, Logger);
				}
				if (_onCustomErrorAsync != null)
				{
					await _onCustomErrorAsync(customException);
				}
			}

			var executeOnError = _executeOnError || customException == null;
			if (executeOnError)
			{
				if (_onErrorWithLoggerAsync != null)
				{
					await _onErrorWithLoggerAsync(exception, Logger);
				}
				if (_onErrorAsync != null)
				{
					await _onErrorAsync(exception);
				}
			}
        }
    }
}