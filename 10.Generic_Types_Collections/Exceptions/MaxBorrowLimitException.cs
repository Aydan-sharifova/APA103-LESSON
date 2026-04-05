using System;
namespace _10.Generic_Types_Collections.Exceptions
{
    public class MaxBorrowLimitException : Exception
    {
        public MaxBorrowLimitException() : base("Maksimum 3 kitab götürə bilərsiniz!") { }

        public MaxBorrowLimitException(string message) : base(message) { }
    }
}

