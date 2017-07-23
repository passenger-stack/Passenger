using System;

namespace Passenger.Core.Domain
{
    public abstract class PassengerException : Exception
    {
        public string Code { get; }

        protected PassengerException()
        {
        }

        protected PassengerException(string code)
        {
            Code = code;
        }

        protected PassengerException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected PassengerException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected PassengerException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected PassengerException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}