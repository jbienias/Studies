using System;

namespace Adapter.PluggableAdapter
{
    public class PageLoginAdapter
    {
        public Func<string, string, bool> Login { get; private set; }

        public PageLoginAdapter(Google google)
        {
            Login = (login, password) =>
            {
                var result = google.SignIn(login, password).Contains("success");
                if (result)
                    return true;
                else
                    return false;
            };
        }

        public PageLoginAdapter(Facebook facebook)
        {
            Login = (login, password) =>
            {
                var result = facebook.Login(login, password);
                if (result == 1)
                    return true;
                else
                    return false;
            };
        }

        public PageLoginAdapter(Twitter twitter)
        {
            Login = (login, password) =>
            {
                return twitter.LoginUser(login, password);
            };
        }
    }
}
