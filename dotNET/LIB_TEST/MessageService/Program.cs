using System;

namespace MessageService
{
    class Program
    {
        static void Main(string[] args)
        {
            Service s = new Service();
            User n = new User("Ala", "Ma");
            s.AddUser(n);
            Console.WriteLine(s.Users.Count);
            s.AddUser(n);
            Console.WriteLine(s.Users.Count);
            s.AddUser(new User("Ala", "Ma"));
            Console.WriteLine(s.Users.Count);
            s.AddUser(new User("Ala", "Ma"));
            Console.WriteLine(s.Users.Count);
            s.DeleteUser(new User("Ala", "Ma")); // nie zadziala
            Console.WriteLine("Delete 'klon' usera");
            Console.WriteLine(s.Users.Count);
            Console.WriteLine("Delete same object usera");
            s.DeleteUser(n);
            Console.WriteLine(s.Users.Count);
        }
    }
}
