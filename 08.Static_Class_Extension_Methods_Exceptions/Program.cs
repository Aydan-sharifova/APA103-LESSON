using _08.Static_Class_Extension_Methods_Exceptions.Exceptions;
using _08.Static_Class_Extension_Methods_Exceptions.Services;

namespace _08.Static_Class_Extension_Methods_Exceptions;

internal class Program
{
    static void Main(string[] args)
    {
        LoginSystem loginSystem = new LoginSystem();

        while (true)
        {
            try
            {
                Console.Write("Username daxil et: ");
                string username = Console.ReadLine();

                Console.Write("Password daxil et: ");
                string password = Console.ReadLine();

                bool result = loginSystem.Login(username, password);

                if (result)
                {
                    break;
                }
            }
            catch (InvalidUsernameException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                loginSystem.ShowUsers();
            }
            catch (IncorrectPasswordException ex)
            {
                Console.WriteLine("WARNING: " + ex.Message);
            }
            catch (AccountLockedException ex)
            {
                Console.WriteLine("CRITICAL: " + ex.Message + " Contact admin.");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UNEXPECTED ERROR: " + ex.Message);
            }

            Console.WriteLine();
        }
    }
}
