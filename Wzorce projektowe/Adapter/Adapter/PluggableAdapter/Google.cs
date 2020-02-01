using System;

namespace Adapter.PluggableAdapter
{
    public class Google
    {
        public string SignIn(string login, string password)
        {
            int rand = new Random().Next(0, int.MaxValue);
            if (rand % 14 == 10)
                return "failure";
            else
                return "success";
        }
    }
}
