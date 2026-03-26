using System;
namespace _08.Static_Class_Extension_Methods_Exceptions.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public int AttemptsLeft { get; set; }

        public IncorrectPasswordException(int attemptsLeft)
            : base($"Password sehvdir. Qalan cehd sayi: {attemptsLeft}")
        {
            AttemptsLeft = attemptsLeft;
        }
    }
}

