using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageService;
using MessageService.Fakes;
using System.Collections.Generic;

namespace MessageServiceTests
{
    [TestClass]
    public class UserTests
    {
        User user;
        string properNickname;
        string properPassword;
        string content;
        string subject;

        [TestInitialize]
        public void TestInitialize()
        {
            properNickname = "Zdzichu";
            properPassword = "dobreHaslo123";
            content = "Super mail, odbierz to!";
            subject = "Temat";
            user = new User(properNickname, properPassword);
        }

        //LISTMESSAGE INBOX TESTS

        [TestMethod]
        public void ListMessagesInbox_InboxNotEmpty_ReturnsStringWithMessagesInfo()
        {
            user.Inbox.Add(new StubIServiceMessage() { SubjectGet = () => { return subject; } });
            StringAssert.Contains(user.ListMessagesInbox(), subject);
        }

        [TestMethod]
        public void ListMessagesInbox_InboxEmpty_ReturnsEmptyString()
        {
            StringAssert.Equals(user.ListMessagesInbox(), string.Empty);
        }

        //LISTMESSAGE SENT TESTS

        [TestMethod]
        public void ListMessagesSent_SentNotEmpty_ReturnsStringWithMessagesInfo()
        {
            user.Sent.Add(new StubIServiceMessage() { SubjectGet = () => { return subject; } });
            StringAssert.Contains(user.ListMessagesSent(), subject);
        }

        [TestMethod]
        public void ListMessagesSent_SentEmpty_ReturnsEmptyString()
        {
            StringAssert.Equals(user.ListMessagesSent(), string.Empty);
        }

        //SHOWMESSAGE INBOX TESTS

        [TestMethod]
        public void ShowMessageInbox_IndexOfMessageExists_ReturnsStringWithProperMessage()
        {
            user.Inbox.Add(new StubIServiceMessage()
            {
                SubjectGet = () => { return subject; },
                ContentGet = () => { return content; }
            });
            StringAssert.Contains(user.ShowMessageInbox(0), content);
        }

        [TestMethod]
        public void ShowMessageInbox_IndexWithMessageDoesNotExist_ReturnsErrorString()
        {
            StringAssert.Contains(user.ShowMessageInbox(0), "Message does not exist!");
        }

        //SHOWMESSAGE SENT TESTS

        [TestMethod]
        public void ShowMessageSent_IndexWithMessageExists_ReturnsStringWithProperMessage()
        {
            user.Sent.Add(new StubIServiceMessage()
            {
                SubjectGet = () => { return subject; },
                ContentGet = () => { return content; }
            });
            StringAssert.Contains(user.ShowMessageSent(0), content);
        }

        [TestMethod]
        public void ShowMessageSent_IndexOfMessageDoesNotExist_ReturnsErrorString()
        {
            StringAssert.Contains(user.ShowMessageSent(0), "Message does not exist!");
        }

        //DELETEMESSAGE INBOX TESTS

        [TestMethod]
        public void DeleteMessageInbox_IndexOfMessageExist_ReturnsTrue()
        {
            var message = new StubIServiceMessage();
            user.Inbox.Add(message);
            user.DeleteMessageInbox(0);
            CollectionAssert.DoesNotContain(user.Inbox, message);
        }

        [TestMethod]
        public void DeleteMessageInbox_IndexOfMessageDoesNotExist_ReturnsFalse()
        {
            Assert.IsFalse(user.DeleteMessageInbox(0));
        }

        //DELETEMESSAGE SENT TESTS

        [TestMethod]
        public void DeleteMessageSent_IndexOfMessageExist_ReturnsTrue()
        {
            var message = new StubIServiceMessage();
            user.Sent.Add(message);
            user.DeleteMessageSent(0);
            CollectionAssert.DoesNotContain(user.Sent, message);
        }

        [TestMethod]
        public void DeleteMessageSent_IndexOfMessageDoesNotExist_ReturnsFalse()
        {
            Assert.IsFalse(user.DeleteMessageSent(0));
        }

        //DELETEALLMESSAGES INBOX TESTS

        [TestMethod]
        public void DeleteAllMessagesInbox_InboxListIsReset()
        {
            int numTimes = 10;
            for (int i = 0; i < numTimes; i++)
                user.Inbox.Add(new StubIServiceMessage());
            //Assert.AreEqual(user.Inbox.Count, numTimes);
            user.DeleteAllMessagesInbox();
            Assert.AreEqual(user.Inbox.Count, 0);
        }

        //DELETEALLMESSAGES SENT TESTS

        [TestMethod]
        public void DeleteAllMessagesSent_SentListIsReset()
        {
            int numTimes = 10;
            for (int i = 0; i < numTimes; i++)
                user.Sent.Add(new StubIServiceMessage());
            //Assert.AreEqual(user.Sent.Count, numTimes);
            user.DeleteAllMessagesSent();
            Assert.AreEqual(user.Sent.Count, 0);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            user = null;
            properNickname = null;
            properPassword = null;
        }
    }
}
