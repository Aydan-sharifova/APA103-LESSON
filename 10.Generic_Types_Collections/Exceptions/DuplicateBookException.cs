using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class DuplicateBookException : Exception
    {
        public DuplicateBookException() : base("Bu kitab artıq sistemdə mövcuddur.") { }

        public DuplicateBookException(string message) : base(message) { }
    }
}

