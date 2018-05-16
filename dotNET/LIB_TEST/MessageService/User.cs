using System.Collections.Generic;
using System;

namespace MessageService
{
    public interface IServiceUser
    {
        string Nickname { get; }
        string Password { get; }
        List<IServiceMessage> Inbox { get; }
        List<IServiceMessage> Sent { get; }
        string ListMessagesInbox();
        string ListMessagesSent();
        string ShowMessageInbox(int i);
        string ShowMessageSent(int i);
        bool DeleteMessageInbox(int i);
        bool DeleteMessageSent(int i);
        void DeleteAllMessagesInbox();
        void DeleteAllMessagesSent();
    }

    public class User : IServiceUser
    {
        public string Nickname { get; private set; }
        public string Password { get; private set; }
        public List<IServiceMessage> Sent { get; private set; }
        public List<IServiceMessage> Inbox { get; private set; }

        public User(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
            Inbox = new List<IServiceMessage>();
            Sent = new List<IServiceMessage>();
        }

        public string ListMessagesInbox()
        {
            if (Inbox.Count == 0)
                return string.Empty;
            else
            {
                string tmp = "";
                for (int i = 0; i < Inbox.Count; i++)
                    tmp += "[" + i + "]" + " " + Inbox[i].Subject + " od:" + Inbox[i].Sender;
                return tmp;
            }
        }

        public string ListMessagesSent()
        {
            if (Sent.Count == 0)
                return string.Empty;
            else
            {
                string tmp = "";
                for (int i = 0; i < Sent.Count; i++)
                    tmp += "[" + i + "]" + " " + Sent[i].Subject + " do:" + Sent[i].Addressee;
                return tmp;
            }
        }

        public string ShowMessageInbox(int index)
        {
            if (index < Inbox.Count)
                return "[Inbox, id." + index + "]\n" + "Subject :" + Inbox[index].Subject + "\nContent:" + Inbox[index].Content;
            else
                return "Message does not exist!";
        }

        public string ShowMessageSent(int index)
        {
            if (index < Sent.Count)
                return "[Sent, id." + index + "]\n" + "Subject :" + Sent[index].Subject + "\nContent:" + Sent[index].Content;
            else
                return "Message does not exist!";
        }

        public bool DeleteMessageInbox(int index)
        {
            if (index < Inbox.Count)
            {
                Inbox.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public bool DeleteMessageSent(int index)
        {
            if (index < Sent.Count)
            {
                Sent.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public void DeleteAllMessagesInbox()
        {
            Inbox = new List<IServiceMessage>();
        }

        public void DeleteAllMessagesSent()
        {
            Sent = new List<IServiceMessage>();
        }
    }
}
