using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class EmptyStackException : Exception
    {
        public EmptyStackException() : base("Son qaytarılan kitablar siyahısı boşdur.") { }

        public EmptyStackException(string message) : base(message) { }
    }
}

