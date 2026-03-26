using System;
namespace _08.Static_Class_Extension_Methods_Exceptions.Exceptions
{
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException()
            : base("Username bos ola bilmez ve en az 3 simvol olmalidir.")
        {
        }

        public InvalidUsernameException(string message)
            : base(message)
        {
        }
    }
}

