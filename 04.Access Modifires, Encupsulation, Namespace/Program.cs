namespace _04.Access_Modifires__Encupsulation__Namespace;

    class User
    {
        public string Username;
        private string Password;
        protected string Email;
        internal int Id;

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void ShowUser()
        {
            Console.WriteLine("Username: " + Username);
            Console.WriteLine("Password: " + Password);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Id: " + Id);
        }
    }

    class Admin : User
    {
        public void SetEmail(string email)
        {
            Email = email;
        }
    }

    class Program
    {
        static void Main()
        {
            Admin admin = new Admin();

            admin.Username = "admin01";
            admin.Id = 1;
            admin.SetPassword("12345");
            admin.SetEmail("admin@gmail.com");

            admin.ShowUser();
        }
    }


