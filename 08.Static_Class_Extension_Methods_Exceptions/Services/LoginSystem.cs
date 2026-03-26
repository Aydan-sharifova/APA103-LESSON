using System;
using _08.Static_Class_Extension_Methods_Exceptions.Exceptions;
using _08.Static_Class_Extension_Methods_Exceptions.Models;

namespace _08.Static_Class_Extension_Methods_Exceptions.Services
{
    public class LoginSystem
    {
        private User[] users;
        private const int MaxAttempts = 3;

        public LoginSystem()
        {
            users = new User[3];

            users[0] = new User("admin", "admin123");
            users[1] = new User("student", "student123");
            users[2] = new User("teacher", "teacher123");
        }

        public void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidUsernameException();
            }

            if (username.Trim().Length < 3)
            {
                throw new InvalidUsernameException("Username en az 3 simvoldan ibaret olmalidir.");
            }
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException();
            }

            if (password.Length < 6)
            {
                throw new InvalidPasswordException("Password en az 6 simvoldan ibaret olmalidir.");
            }
        }

        private User FindUser(string username)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Username.ToLower() == username.ToLower())
                {
                    return users[i];
                }
            }

            return null;
        }

        public bool Login(string username, string password)
        {
            ValidateUsername(username);
            ValidatePassword(password);

            User user = FindUser(username);

            if (user == null)
            {
                throw new UserNotFoundException(username);
            }

            if (user.IsLocked)
            {
                throw new AccountLockedException();
            }

            if (user.Password == password)
            {
                user.FailedAttempts = 0;
                Console.WriteLine($"Login successful! Welcome, {user.Username}!");
                return true;
            }
            else
            {
                user.FailedAttempts++;

                int attemptsLeft = MaxAttempts - user.FailedAttempts;

                if (attemptsLeft > 0)
                {
                    throw new IncorrectPasswordException(attemptsLeft);
                }
                else
                {
                    user.IsLocked = true;
                    throw new AccountLockedException();
                }
            }
        }

        public void ShowUsers()
        {
            Console.WriteLine("Movcud userler:");
            for (int i = 0; i < users.Length; i++)
            {
                Console.WriteLine($"- {users[i].Username}");
            }
        }
    }
}

