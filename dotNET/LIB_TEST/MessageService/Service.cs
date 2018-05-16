using System;
using System.Collections.Generic;

namespace MessageService
{
    public class Service
    {
        public List<IServiceUser> Users { get; private set; }
        public List<IServiceMessage> Messages { get; private set; }

        public Service()
        {
            Users = new List<IServiceUser>();
            Messages = new List<IServiceMessage>();
        }

        public bool UserExists(string nickname)
        {
            if (String.IsNullOrEmpty(nickname) || GetUser(nickname) == null)
                return false;
            return true;
        }

        public bool UserExists(IServiceUser user)
        {
            if (user == null || !Users.Contains(user))
                return false;
            return true;
        }

        public IServiceUser GetUser(string nickname)
        {
            return Users.Find(u => u.Nickname == nickname);
        }

        public IServiceUser LogIn(string nickname, string password)
        {
            return Users.Find(u => u.Nickname == nickname && u.Password == password);
        }

        public bool AddUser(string nickname, string password) //Register
        {
            if (String.IsNullOrEmpty(nickname) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException();
            if (!UserExists(nickname))
            {
                Users.Add(new User(nickname, password));
                return true;
            }
            else
                return false;
        }

        public bool AddUser(IServiceUser user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (!UserExists(user.Nickname))
            {
                Users.Add(user);
                return true;
            }
            else
                return false;
        }

        public bool DeleteUser(string nickname)
        {
            if (String.IsNullOrEmpty(nickname))
                throw new ArgumentNullException();
            if (UserExists(nickname))
            {
                Users.Remove(GetUser(nickname));
                return true;
            }
            else
                return false;
        }

        public bool DeleteUser(IServiceUser user)
        //More strict -> object MUST be the same
        {
            if (user == null)
                throw new ArgumentNullException();
            if (UserExists(user))
            {
                Users.Remove(user);
                return true;
            }
            else
                return false;
        }

        public void SendMessage(string sender, string addressee, string subject, string content)
        {
            if (String.IsNullOrEmpty(subject) || String.IsNullOrEmpty(content))
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(sender) || String.IsNullOrEmpty(addressee))
                throw new ArgumentNullException();
            var s = GetUser(sender);
            var a = GetUser(addressee);
            var newMsg = new Message(s, a, subject, content);
            s.Sent.Add(newMsg);
            a.Inbox.Add(newMsg);
            Messages.Add(newMsg);
        }

        public void SendMessage(IServiceUser sender, IServiceUser addressee, string subject, string content)
        {
            if (String.IsNullOrEmpty(subject) || String.IsNullOrEmpty(content))
                throw new ArgumentNullException();
            if (sender == null || addressee == null)
                throw new ArgumentNullException();
            if (!UserExists(sender) || !UserExists(addressee))
                throw new Exception();
            var newMsg = new Message(sender, addressee, subject, content);
            sender.Sent.Add(newMsg);
            addressee.Inbox.Add(newMsg);
            Messages.Add(newMsg);
        }
    }
}