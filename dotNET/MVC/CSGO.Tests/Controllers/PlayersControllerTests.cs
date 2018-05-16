using CSGO.Controllers;
using CSGO.Data;
using CSGO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace CSGO.Tests.Controllers
{
    [TestClass]
    public class PlayersControllerTests
    {
        [TestMethod]
        public async Task Create_NoArguments_ReturnsViewResult()
        {
            var teamRepository = new FakeTeamRepository();
            var playerRepository = new FakePlayerRepository();
            var controller = new PlayersController(playerRepository, teamRepository);

            var result = await controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_ValidUser_ReturnsRedirectToActionResult()
        {
            var player = new Player();
            var teamRepository = new FakeTeamRepository();
            var playerRepository = new FakePlayerRepository();
            var controller = new PlayersController(playerRepository, teamRepository);

            var result = await controller.Create(player);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }


        [TestMethod]
        public async Task Create_InValidUser_ReturnsSameModel()
        {
            var expected = new Player();
            var teamRepository = new FakeTeamRepository();
            var playerRepository = new FakePlayerRepository();
            var controller = new PlayersController(playerRepository, teamRepository);
            controller.ModelState.AddModelError("", "cos");

            var result = await controller.Create(expected) as ViewResult;

            Assert.AreEqual(expected, result.Model);
        }

        [TestMethod]
        public async Task Delete_UserInDb_ReturnsSameModel()
        {
            var player = new Player() { PlayerId = 1 };
            var teamRepository = new FakeTeamRepository();
            var playerRepository = new FakePlayerRepository();
            var controller = new PlayersController(playerRepository, teamRepository);
            playerRepository.AddPlayer(player);

            var result = await controller.Delete(1) as ViewResult;

            Assert.AreEqual(player, result.Model);
        }

        [TestMethod]
        public async Task Delete_UserNotInDb_ReturnsNotFoundView()
        {
            var teamRepository = new FakeTeamRepository();
            var playerRepository = new FakePlayerRepository();
            var controller = new PlayersController(playerRepository, teamRepository);

            var result = await controller.Delete(1) as ViewResult;

            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod] //http://gunnarpeipman.com/2017/04/aspnet-core-ef-inmemory/
        public async Task Tests_On_NewLocalDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var context = new ApplicationDbContext(options);
            context.Players.Add(new Player());
            context.SaveChanges();

            PlayerRepository pr = new PlayerRepository(context);
            pr.AddPlayer(new Player());
            await pr.Save();

            TeamRepository tr = new TeamRepository(context);
            PlayersController t = new PlayersController(pr, tr);
            await t.Create(new Player());

            Assert.AreEqual(3, await context.Players.CountAsync());
        }
    }
}
