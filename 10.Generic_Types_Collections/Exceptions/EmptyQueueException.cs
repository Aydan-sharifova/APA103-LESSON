using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class EmptyQueueException : Exception
    {
        public EmptyQueueException() : base("Gözləmə növbəsi boşdur.") { }

        public EmptyQueueException(string message) : base(message) { }
    }
}

