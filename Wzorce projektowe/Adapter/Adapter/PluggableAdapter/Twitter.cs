using System;

namespace Adapter.PluggableAdapter
{
    public class Twitter
    {
        public bool LoginUser(string login, string password)
        {
            DateTime now = DateTime.Now;
            if (((DayOfWeek)now.Day == DayOfWeek.Saturday) || ((DayOfWeek)now.Day == DayOfWeek.Sunday))
                return false;
            else
                return true;

        }
    }
}
