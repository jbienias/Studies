using CSGO.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSGO.Tests.Models
{
    [TestClass]
    public class TeamTests
    {
        string _name;
        DateTime _dateOfFounding;

        [TestInitialize]
        public void InitializeTests()
        {
            _name = "Najlepsza druzyna";
            _dateOfFounding = new DateTime(1967, 12, 3);
        }

        [TestMethod]
        public void AllTeamData_IsValid()
        {
            var team = new Team()
            {
                Name = _name,
                DateOfFounding = _dateOfFounding
            };

            var result = TestModelHelper.Validate(team);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Name_LengthIsLessThan3_NotValid()
        {
            var team = new Team()
            {
                Name = "aa",
            };

            var result = TestModelHelper.Validate(team);
            Assert.AreEqual(1, result.Count);
            //lub Assert.AreEqual("message", result[0].ErrorMessage);
        }


        [TestMethod]
        public void Name_LengthIsGreaterThan50_NotValid()
        {
            var team = new Team()
            {
                Name = new string('C', 51)
            };

            var result = TestModelHelper.Validate(team);
            Assert.AreEqual(1, result.Count);
        }

        [TestCleanup]
        public void CleanupTests()
        {
            _name = null;
            _dateOfFounding = new DateTime();
        }
    }
}
