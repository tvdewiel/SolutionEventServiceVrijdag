using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventServiceBL.Exceptions
{
    public class EventModelException : Exception
    {
        public EventModelException(string? message) : base(message)
        {
        }

        public EventModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
