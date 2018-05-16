using CSGO.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Tests.Models
{
    [TestClass]
    public class PlayerTests
    {
        string _name, _surname, _nickname;
        DateTime _dateOfBirth;
        int _salary;

        [TestInitialize]
        public void InitializeTests()
        {
            _name = "Jan";
            _surname = "Bienias";
            _nickname = "Hergroth";
            _salary = 2500;
            _dateOfBirth = new DateTime(1996, 04, 19);
        }

        [TestMethod]
        public void AllPlayerData_isValid()
        {
            var player = new Player()
            {
                Name = _name,
                Surname = _surname,
                Nickname = _nickname,
                DateOfBirth = _dateOfBirth,
                Salary = _salary
            };

            var context = new ValidationContext(player);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(player, context, result, true);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void NameSurname_IncorrectStringRegex_NotValid()
        {
            var player = new Player()
            {
                Name = "Glup0t7j@k",
                Surname = "Bzduryafa3q2ads1",
                Nickname = "Whatever;)"
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void NameNicknameSurname_LengthIsLessThan3_NotValid()
        {
            var player = new Player()
            {
                Name = "AA",
                Surname = "BB",
                Nickname = "CC",
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void NameNicknameSurname_LengthIsGreaterThan50_NotValid()
        {
            var player = new Player()
            {
                Name = new string('A', 51),
                Surname = new string('B', 51),
                Nickname = new string('C', 51)
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Salary_CannotBeLessThan0_NotValid()
        {
            var player = new Player()
            {
                Name = _name,
                Surname = _surname,
                Nickname = _nickname,
                Salary = -10
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Nickname_CannotContaint666Substring_NotValid()
        {
            var player = new Player()
            {
                Name = _name + "666",
                Surname = _surname,
                Nickname = _nickname
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Nickname_CannotBeEqualToSurname_NotValid()
        {
            var player = new Player()
            {
                Name = _name,
                Surname = _surname,
                Nickname = _surname
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Nickname_CannotBeEqualToName_NotValid()
        {
            var player = new Player()
            {
                Name = _name,
                Surname = _surname,
                Nickname = _name
            };

            var result = TestModelHelper.Validate(player);
            Assert.AreEqual(1, result.Count);
        }

        [TestCleanup]
        public void CleanupTests()
        {
            _name = null;
            _surname = null;
            _nickname = null;
            _dateOfBirth = new DateTime();
            _salary = 0;
        }
    }
}
