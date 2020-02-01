using Adapter.PluggableAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdapterTest
{
    [TestClass]
    public class PluggableAdapterTests
    {
        string login = "Noobmaster69";
        string password = "loveU3O0O";

        [TestMethod]
        public void PageLogin_Facebook()
        {
            var pageLoginAdapter = new PageLoginAdapter(new Facebook());
            Assert.IsTrue(pageLoginAdapter.Login(login, password));
        }

        [TestMethod]
        public void PageLogin_Google()
        {
            var pageLoginAdapter = new PageLoginAdapter(new Google());
            Assert.IsTrue(pageLoginAdapter.Login(login, password));
        }

        [TestMethod]
        public void PageLogin_Twitter()
        {
            var pageLoginAdapter = new PageLoginAdapter(new Twitter());
            Assert.IsTrue(pageLoginAdapter.Login(login, password));
        }
    }
}
