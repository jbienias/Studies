using System;

namespace Adapter.PluggableAdapter
{
    public class Facebook
    {
        public int Login(string login, string password)
        {
            int rand = new Random().Next(0, int.MaxValue);
            if (rand % 21 == 37)
                return 0; //Fail
            else
                return 1;
        }
    }
}
