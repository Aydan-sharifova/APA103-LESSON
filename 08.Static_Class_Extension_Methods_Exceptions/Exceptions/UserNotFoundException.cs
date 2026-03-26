using System;
namespace _08.Static_Class_Extension_Methods_Exceptions.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("Istifadeci tapilmadi.")
        {
        }

        public UserNotFoundException(string username)
            : base($"'{username}' username-li istifadeci tapilmadi.")
        {
        }
    }
}

