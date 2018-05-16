using System;

namespace MessageService
{
    public interface IServiceMessage
    {
        IServiceUser Sender { get; }
        IServiceUser Addressee { get; }
        string Subject { get; }
        string Content { get; }
        DateTime SentTime { get; }
    }
    public class Message : IServiceMessage
    {
        public IServiceUser Sender { get; private set; }
        public IServiceUser Addressee { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
        public DateTime SentTime { get; private set; }

        public Message(IServiceUser sender, IServiceUser addressee, string subject, string content)
        {
            Subject = subject;
            Sender = sender;
            Addressee = addressee;
            Content = content;
            SentTime = DateTime.Now;
        }
    }
}
