using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageService;
using MessageService.Fakes;
using System.Collections.Generic;

namespace MessageServiceTests
{

    //https://www.nwcadence.com/blog/shims-and-stubs-and-the-microsoft-fakes-framework
    //https://msdn.microsoft.com/en-us/library/hh549174.aspx
    //https://github.com/aadennis/YtDataDrivenC-CSV

    [TestClass]
    public class ServiceTests
    {

        public TestContext TestContext { get; set; }

        Service service;
        string properNickname;
        string properNickname1;
        string properPassword;
        string subject;
        string content;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new Service();
            properNickname = "Herek";
            properNickname1 = "Super";
            properPassword = "jakiesRandomowe123";
            content = "To jest przykladowy mail.";
            subject = "Dzien dobry";
        }

        //USEREXISTS TESTS

        [TestMethod]
        public void UserExists_UserInList_ReturnsFalse()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            service.AddUser(user);
            Assert.IsTrue(service.UserExists(user));
        }

        [TestMethod]
        public void UserExists_UserNotInList_ReturnsFalse()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            Assert.IsFalse(service.UserExists(user));
        }

        [TestMethod]
        public void UserExists_NicknameInList_ReturnsFalse()
        {
            service.AddUser(properNickname, properPassword);
            Assert.IsTrue(service.UserExists(properNickname));
        }

        [TestMethod]
        public void UserExists_NicknameNotInList_ReturnsFalse()
        {
            Assert.IsFalse(service.UserExists(properNickname));
        }

        [TestMethod]
        public void UserExists_NullString_ReturnsFalse()
        {
            string nullString = null;
            Assert.IsFalse(service.UserExists(nullString));
        }

        [TestMethod]
        public void UserExists_EmptyString_ReturnsFalse()
        {
            Assert.IsFalse(service.UserExists(String.Empty));
        }

        [TestMethod]
        public void UserExists_NullUser_ReturnsFalse()
        {
            IServiceUser nullUser = null;
            Assert.IsFalse(service.UserExists(nullUser));
        }

        //GETUSER TESTS
        [TestMethod]
        public void GetUser_UserExists_ReturnsUser()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; }
            };
            service.AddUser(user);
            Assert.AreEqual(service.GetUser(user.NicknameGet()), user);
        }

        [TestMethod]
        public void GetUser_UserDoesNotExists_ReturnsNull()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; }
            };
            Assert.AreEqual(service.GetUser(user.NicknameGet()), null);
        }
        //LOGIN TESTS

        [TestMethod]
        public void LogIn_UserExists_ReturnsUser()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            service.AddUser(user);
            Assert.AreEqual(service.LogIn(properNickname, properPassword), user);
        }

        [TestMethod]
        public void LogIn_UserDoesNotExist_ReturnsNull()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            Assert.AreEqual(service.LogIn(properNickname, properPassword), null);
        }

        //ADDUSER TESTS

        [TestMethod]
        public void AddUser_ProperNicknameAndPassword_AddsUserToUsersList()
        {
            Assert.IsTrue(service.AddUser(properNickname, properPassword));
        }

        [TestMethod]
        public void AddUser_ProperUserObject_AddsUserToUsersList()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            service.AddUser(user);
            CollectionAssert.Contains(service.Users, user);
        }

        [TestMethod]
        public void AddUser_NicknameIsAlreadyInList_DoesNotAddUserToUsersList()
        {
            int numTimes = 5;
            for (int i = 0; i < numTimes; i++)
                service.AddUser(properNickname, properPassword);
            Assert.AreEqual(service.Users.Count, 1);
        }

        [TestMethod]
        public void AddUser_UserIsAlreadyInList_ThrowsException()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; }
            };
            int numTimes = 5;
            for (int i = 0; i < numTimes; i++)
                service.AddUser(user);
            Assert.AreEqual(service.Users.Count, 1);
        }

        [TestMethod]
        public void AddUser_MultipleUniqueNicknames_AddsMultipleUsersToList()
        {
            int numTimes = 5;
            for (int i = 0; i < numTimes; i++)
                service.AddUser(properNickname + i, properPassword);
            Assert.AreEqual(service.Users.Count, numTimes);
        }

        [TestMethod]
        public void AddUser_MultipleUniqueUsers_AddsMultipleUsersToList()
        {
            int numTimes = 5;
            for (int i = 0; i < numTimes; i++)
            {
                string concat = properNickname + i;
                var user = new StubIServiceUser() { NicknameGet = () => { return concat; } };
                service.AddUser(user);
            }
            Assert.AreEqual(service.Users.Count, numTimes);
        }

        [TestMethod, DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\AddCorrectUserData.csv", "AddCorrectUserData#csv",
            DataAccessMethod.Sequential), DeploymentItem("AddCorrectUserData.csv")]
        public void AddUser_AddCorrectUserFromData_ReturnsTrue()
        {
            string nickname = TestContext.DataRow["nickname"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            Assert.IsTrue(service.AddUser(nickname, password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_ProperNicknameAndBlankPassword_ThrowsException()
        {
            service.AddUser(properNickname, String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_BlankNicknameAndProperPassword_ThrowsException()
        {
            service.AddUser(String.Empty, properPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_BlankNicknameAndBlankPassword_ThrowsException()
        {
            service.AddUser(String.Empty, String.Empty);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_ProperNicknameAndNullPassword_ThrowsException()
        {
            service.AddUser(properNickname, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullNicknameAndProperPassword_ThrowsException()
        {
            service.AddUser(null, properPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullNicknameAndNullPassword_ThrowsException()
        {
            service.AddUser(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullUser_ThrowsException()
        {
            service.AddUser(null);
        }

        //DELETEUSER TESTS

        [TestMethod]
        public void DeleteUser_ExistingNickameString_ReturnsTrue()
        {
            service.AddUser(properNickname, properPassword);
            Assert.IsTrue(service.DeleteUser(properNickname));
        }

        [TestMethod]
        public void DeleteUser_ExistingUserObject_ReturnsTrue()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; },
            };
            service.AddUser(user);
            service.DeleteUser(user);
            CollectionAssert.DoesNotContain(service.Users, user);
        }

        [TestMethod]
        public void DeleteUser_SimilarUserObject_ReturnsFalse()
        {
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; },
            };
            service.AddUser(properNickname, properPassword);
            Assert.IsFalse(service.DeleteUser(user));
        }

        [TestMethod]
        public void DeleteUser_NonExistingNickname_ReturnsFalse()
        {
            Assert.IsFalse(service.DeleteUser(properNickname));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUser_NullUser_ThrowsException()
        {
            IServiceUser nullUser = null;
            service.DeleteUser(nullUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUser_NullString_ThrowsException()
        {
            string nullString = null;
            service.DeleteUser(nullString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUser_EmptyString_ThrowsException()
        {
            service.DeleteUser(String.Empty);
        }

        //SENDMESSAGE TESTS
        [TestMethod]
        public void SendMessage_ExistingUsers_AddsMessageToInbox()
        {
            var user1 = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; },
                SentGet = () => { return new List<IServiceMessage>(); },
                InboxGet = () => { return new List<IServiceMessage>(); }
            };
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname1; },
                PasswordGet = () => { return properPassword; },
                SentGet = () => { return new List<IServiceMessage>(); },
                InboxGet = () => { return new List<IServiceMessage>(); }
            };
            service.AddUser(user1); service.AddUser(user);
            service.SendMessage(user1, user, subject, content);
            StringAssert.Equals(service.Messages[0].Subject, subject);
        }

        [TestMethod]
        public void SendMessage_ExistingNicknames_AddsMessageToInbox()
        {
            var user1 = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; },
                SentGet = () => { return new List<IServiceMessage>(); },
                InboxGet = () => { return new List<IServiceMessage>(); }
            };
            var user = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname1; },
                PasswordGet = () => { return properPassword; },
                SentGet = () => { return new List<IServiceMessage>(); },
                InboxGet = () => { return new List<IServiceMessage>(); }
            };
            service.AddUser(user1); service.AddUser(user);
            service.SendMessage(user1.NicknameGet(), user.NicknameGet(), subject, content);
            StringAssert.Equals(service.Messages[0].Subject, subject);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SendMessage_UserDoesNotExists_AddsMessageToMessagesList()
        {
            var user1 = new StubIServiceUser()
            {
                NicknameGet = () => { return properNickname; },
                PasswordGet = () => { return properPassword; },
                SentGet = () => { return new List<IServiceMessage>(); },
                InboxGet = () => { return new List<IServiceMessage>(); }
            };
            var user = new StubIServiceUser();
            service.AddUser(user1);
            service.SendMessage(user1, user, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_FirstNullUser_ThrowsException()
        {
            IServiceUser userNull = null;
            var user = new StubIServiceUser();
            service.AddUser(user);
            service.SendMessage(userNull, user, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_SecondNullUser_ThrowsException()
        {
            IServiceUser userNull = null;
            var user = new StubIServiceUser();
            service.AddUser(user);
            service.SendMessage(user, userNull, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_NullUsers_ThrowsException()
        {
            IServiceUser userNull = null;
            IServiceUser userNull1 = null;
            service.SendMessage(userNull, userNull1, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_EmptyNicknames_ThrowsException()
        {
            service.SendMessage(String.Empty, String.Empty, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_FirstEmptyNickname_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(String.Empty, user.NicknameGet(), subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_SecondEmptyNickname_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user.NicknameGet(), String.Empty, subject, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageContentEmpty_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user, user, subject, String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageContentEmptyWithNicknames_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user.NicknameGet(), user.NicknameGet(), subject, String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageSubjectEmpty_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user, user, String.Empty, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageSubjectEmptyWithNicknames_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user.NicknameGet(), user.NicknameGet(), String.Empty, content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageEmptyWithNicknames_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user.NicknameGet(), user.NicknameGet(), String.Empty, String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendMessage_MessageSubjectAndContentEmpty_ThrowsException()
        {
            var user = new StubIServiceUser() { NicknameGet = () => { return properNickname; } };
            service.AddUser(user);
            service.SendMessage(user, user, String.Empty, String.Empty);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            service = null;
            properNickname = null;
            properPassword = null;
        }
    }
}
