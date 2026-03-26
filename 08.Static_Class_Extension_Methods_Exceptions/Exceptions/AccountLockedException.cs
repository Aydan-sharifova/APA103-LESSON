using System;
namespace _08.Static_Class_Extension_Methods_Exceptions.Exceptions
{
    public class AccountLockedException : Exception
    {
        public AccountLockedException()
            : base("Hesab bloklanib.")
        {
        }
    }
}

