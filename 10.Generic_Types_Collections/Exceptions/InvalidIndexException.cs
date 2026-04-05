using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class InvalidIndexException : Exception
    {
        public InvalidIndexException() : base("Daxil edilən indeks yanlışdır.") { }

        public InvalidIndexException(string message) : base(message) { }
    }
}

