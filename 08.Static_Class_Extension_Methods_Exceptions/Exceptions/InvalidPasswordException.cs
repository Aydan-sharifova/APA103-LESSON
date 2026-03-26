using System;
namespace _08.Static_Class_Extension_Methods_Exceptions.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            : base("Password bos ola bilmez ve en az 6 simvol olmalidir.")
        {
        }

        public InvalidPasswordException(string message)
            : base(message)
        {
        }
    }
}

