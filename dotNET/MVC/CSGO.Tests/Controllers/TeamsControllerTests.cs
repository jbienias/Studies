using CSGO.Controllers;
using CSGO.Data;
using CSGO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGO.Tests.Controllers
{
    [TestClass]
    public class TeamsControllerTests
    {
        [TestMethod] //Pilot test
        public async Task Index_MultipleTeamsInDbWithEmptyString_ReturnsCorrectNumberOfTeams()
        {
            //Arrange
            var teams = new List<Team>()
            {
                new Team(),
                new Team(),
                new Team()
            };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeams()).ReturnsAsync(teams);
            var controller = new TeamsController(service.Object);

            //Act
            var result = await controller.Index(String.Empty);

            //Assert
            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Team>;
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public async Task Index_EmptyDb_ReturnsNoTeams()
        {
            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeams()).ReturnsAsync(new List<Team>());
            var controller = new TeamsController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Team>;
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public async Task Index_OneTeamWithGivenStringExist_ReturnsTeamWithSameName()
        {
            var teams = new List<Team>()
            {
                new Team { Name = "team0" },
                new Team { Name = "team1" },
                new Team { Name = "team2" }
            };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeams("team0")).ReturnsAsync(new List<Team>() { teams[0] });
            service.Setup(x => x.GetTeams("team1")).ReturnsAsync(new List<Team>() { teams[1] });
            service.Setup(x => x.GetTeams("team2")).ReturnsAsync(new List<Team>() { teams[2] });
            var controller = new TeamsController(service.Object);

            var result = await controller.Index("team0");

            var viewResult = (ViewResult)result;
            var model = ((ViewResult)result).Model as List<Team>;
            Assert.AreEqual(teams[0].Name, model[0].Name);
        }

        [TestMethod]
        public async Task Index_MultipleTeamsWithSameNameWithGivenStringQuery_ReturnsTeamsWithSameNames()
        {
            var teams = new List<Team>()
            {
                new Team { Name = "team1" },
                new Team { Name = "team2" },
                new Team { Name = "team2" }
            };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeams("team2")).ReturnsAsync(new List<Team>() { teams[1], teams[2] });
            var controller = new TeamsController(service.Object);

            var result = await controller.Index("team2");

            var model = ((ViewResult)result).Model as List<Team>;
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public async Task Details_TeamExists_ReturnsSameTeam()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };
            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);
            var controller = new TeamsController(service.Object);

            var result = await controller.Details(1);

            var model = ((ViewResult)result).Model as Team;
            Assert.AreEqual(team1, model);
        }

        [TestMethod]
        public async Task Details_TeamDoesNotExists_ReturnsNotFoundView()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };
            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);
            var controller = new TeamsController(service.Object);

            var result = await controller.Details(1555);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public void Create_ReturnsViewResult()
        {
            var service = new Mock<ITeamRepository>();
            var controller = new TeamsController(service.Object);

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_ValidTeam_ReturnsRedirectToActionResult()
        {
            var validTeam = new Team() { Name = "Wirus", DateOfFounding = new DateTime(1999, 10, 10) };

            var service = new Mock<ITeamRepository>();
            var controller = new TeamsController(service.Object);

            var result = await controller.Create(validTeam);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Create_InvalidTeam_ReturnsSameModel()
        {
            string invalidName = "serioCokolwiek";
            var invalidTeam = new Team() { Name = invalidName, DateOfFounding = new DateTime(1999, 10, 10) };

            var service = new Mock<ITeamRepository>();
            var controller = new TeamsController(service.Object);
            controller.ModelState.AddModelError("NiePodobaMiSie", "BoTak");

            var result = await controller.Create(invalidTeam);

            var model = (Team)((ViewResult)result).Model;
            Assert.AreEqual(invalidName, model.Name);
        }

        [TestMethod]
        public async Task Edit_TeamIdExists_ReturnsSameTeam()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);
            var controller = new TeamsController(service.Object);

            var result = await controller.Edit(1);

            var model = ((ViewResult)result).Model as Team;
            Assert.AreEqual(team1, model);
        }

        [TestMethod]
        public async Task Edit_TeamIdDoesNotExists_ReturnsNotFoundView()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);
            var controller = new TeamsController(service.Object);

            var result = await controller.Edit(1555);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task Edit_TeamWithIdDoesNotExists_ReturnsNotFoundView()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);
            var controller = new TeamsController(service.Object);

            var result = await controller.Edit(1555, team1);

            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task Edit_TeamWithIdExistsAndIsInvalid_ReturnsSameModel()
        {
            var invalidTeam = new Team() { TeamId = 0, Name = "Wi", DateOfFounding = new DateTime(1999, 10, 10) };
            var teams = new List<Team>() { invalidTeam };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(0)).ReturnsAsync(invalidTeam);
            var controller = new TeamsController(service.Object);
            controller.ModelState.AddModelError("NiePodobaMiSie", "BoTak");

            var result = await controller.Edit(0, invalidTeam);

            var model = ((ViewResult)result).Model as Team;
            Assert.AreEqual(invalidTeam, model);
        }


        [TestMethod]
        public async Task Edit_NewTeamInformationIsValid_ReturnsRedirectToResultAction()
        {
            string validName = "serioCokolwiek";
            var team = new Team() { Name = validName, DateOfFounding = new DateTime(1999, 10, 10) };
            var teamToEditWith = new Team() { Name = validName, DateOfFounding = new DateTime(1999, 12, 1) };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(0)).ReturnsAsync(team);
            service.Setup(x => x.TeamExists(0)).Returns(true);
            var controller = new TeamsController(service.Object);

            var result = await controller.Edit(0, teamToEditWith);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

        }

        [TestMethod]
        public async Task Edit_NewTeamInformationIsInValid_ReturnsObjectWithNewWrongData()
        {
            var team = new Team() { Name = "old", DateOfFounding = new DateTime(1999, 10, 10) };
            var newTeamData = new Team() { Name = "newData", DateOfFounding = new DateTime(1999, 12, 1) };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(0)).ReturnsAsync(team);
            service.Setup(x => x.TeamExists(0)).Returns(true);
            var controller = new TeamsController(service.Object);
            controller.ModelState.AddModelError("Nie", "I juz");

            var result = await controller.Edit(0, newTeamData);
            var model = ((ViewResult)result).Model as Team;
            Assert.AreEqual(newTeamData, model);
        }

        [TestMethod]
        public async Task Delete_TeamIdExists_ReturnsSameTeam()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);

            var controller = new TeamsController(service.Object);

            var result = await controller.Delete(1);
            var model = ((ViewResult)result).Model as Team;
            Assert.AreEqual(team1, model);
        }

        [TestMethod]
        public async Task Delete_TeamIdDoesNotExists_ReturnsNotFoundView()
        {
            var team1 = new Team() { Name = "team1" };
            var team2 = new Team() { Name = "team2" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            service.Setup(x => x.GetTeam(2)).ReturnsAsync(team2);

            var controller = new TeamsController(service.Object);

            var result = await controller.Delete(1555);
            var viewResult = (ViewResult)result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task DeleteConfirmed_TeamIdExists_ReturnsRedirectToResultAction()
        {
            var team1 = new Team() { Name = "team1" };

            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            var controller = new TeamsController(service.Object);

            var result = await controller.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task DeleteConfirmed_TeamIdDoesNotExists_ReturnsRedirectToResultAction()
        {
            var team1 = new Team() { Name = "team1" };
            var service = new Mock<ITeamRepository>();
            service.Setup(x => x.GetTeam(1)).ReturnsAsync(team1);
            var controller = new TeamsController(service.Object);

            var result = await controller.DeleteConfirmed(232);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
