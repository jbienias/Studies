using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CSGO.SeleniumTests
{
    [TestClass]
    public class TeamsAndPlayersTest
    {
        string _url;
        InternetExplorerOptions _options;
        InternetExplorerDriver _driver;
        string _email;
        string _password;
        string _teamName;
        string _playerName;

        [TestInitialize]
        public void Initialize()
        {
            _url = "http://localhost:5000";
            _options = new InternetExplorerOptions() { InitialBrowserUrl = _url };
            _driver = new InternetExplorerDriver(_options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _email = "admin@admin.com";
            _password = "@dministrat0R";
            _teamName = "Testowa";
            _playerName = "Hergroth";
        }

        [TestMethod]
        public void Home_ContactOnNavigationBar_RedirectsToContactPage()
        {
            var expected = _url + "/Home/Contact";
            _driver.FindElement(By.Id("contact")).Click();
            Assert.AreEqual(expected, _driver.Url);
        }

        [TestMethod]
        public void Home_HrefToAboutExists_RedirectsToAboutPage()
        {
            var expected = _url + "/Home/About";
            _driver.FindElement(By.CssSelector("[href*='About']")).Click();
            Assert.AreEqual(expected, _driver.Url);
        }

        [TestMethod]
        public void Login_LogsToAdminUser_NavbarContainsLogoutButton()
        {
            LogInAsAdmin(_email, _password);
            var button = _driver.FindElement(By.Id("logout_btn"));
            StringAssert.Contains("Log out", button.Text);
        }

        [TestMethod]
        public void Footer_LinkToGitHubExists_HrefLinksToAuthorsRepository()
        {
            var expected = "github.com/jbienias";
            var githubLink = _driver.FindElement(By.PartialLinkText("Bienias"));
            StringAssert.Contains(githubLink.GetAttribute("href"), expected);
        }

        [TestMethod]
        public void Logout_UserIsLoggedIn_RedirectsToHomePage()
        {
            LogInAsAdmin(_email, _password);
            LogOut();
            Assert.AreEqual(_url + "/", _driver.Url);
        }

        [TestMethod]
        public void Teams_HrefToTeamsExistsWhenUserIsLogggedIn_RedirectsToTeamsPage()
        {
            LogInAsAdmin(_email, _password);
            var expected = _url + "/Teams";
            _driver.FindElement(By.CssSelector("[href*='Teams']")).Click();
            Assert.AreEqual(expected, _driver.Url);
        }

        //https://stackoverflow.com/questions/20711300/controlling-execution-order-of-unit-tests-in-visual-studio/20711652?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        [TestMethod]
        public void TeamsAndPlayers_CRUDOperations_ExecutesWithoutAnyErrors()
        {
            AddTeam_MissingDateInformation_StaysOnCreatePage();
            AddTeam_ValidInformation_TeamTableRowsIncrements();
            AddPlayer_CorrectInformation_RowCountIncrements();
            DeletePlayer_PlayerExists_PlayerIsDeleted();
            DetailsTeam_TeamWithGivenNameIsInDb_RedirectsToChosenTeamDetails();
            EditTeam_TeamWithGivenNameIsInDb_TeamIsUpdated();
            DeleteTeam_TeamWithGivenNameIsInDb_TeamIsDeleted();
        }

        private void AddTeam_MissingDateInformation_StaysOnCreatePage()
        {
            var name = _teamName;
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Teams']")).Click();
            try
            {
                _driver.FindElement(By.CssSelector("[href *= 'Create']")).Click();
                _driver.FindElement(By.Id("Name")).SendKeys(name);
                _driver.FindElement(By.ClassName("btn-success")).Click();

                StringAssert.Contains(_driver.Url, "Create");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void AddTeam_ValidInformation_TeamTableRowsIncrements()
        {
            var name = _teamName;
            var date = "1999-10-10";
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Teams']")).Click();

            try
            {
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                var expected = elements.Count + 1;

                _driver.FindElement(By.CssSelector("[href *= 'Create']")).Click();
                _driver.FindElement(By.Id("Name")).SendKeys(name);
                _driver.FindElement(By.Id("DateOfFounding")).SendKeys(date);
                _driver.FindElement(By.ClassName("btn-success")).Click();

                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                Assert.AreEqual(expected, elements.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void DetailsTeam_TeamWithGivenNameIsInDb_RedirectsToChosenTeamDetails()
        {
            var name = _teamName;
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Teams']")).Click();
            try
            {
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                      "[normalize-space(text())='" + name + "']]//" +
                      "a[@id='details_team']"))
                      .Click();

                StringAssert.Contains(_driver.Url, "/Teams/Details/");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void EditTeam_TeamWithGivenNameIsInDb_TeamIsUpdated()
        {
            var name = _teamName;
            var date = "2000-12-12";
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Teams']")).Click();
            try
            {
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                      "[normalize-space(text())='" + name + "']]//" +
                      "a[@id='edit_team']"))
                      .Click();
                var teamName = _driver.FindElement(By.Id("Name"));
                teamName.Clear();
                teamName.SendKeys(name);
                var teamDate = _driver.FindElement(By.Id("DateOfFounding"));
                teamDate.Click();
                var currentDate = teamDate.GetAttribute("value");
                for (int i = 0; i < currentDate.Length; i++)
                    teamDate.SendKeys(Keys.Backspace);
                teamDate.SendKeys(date);
                _driver.FindElement(By.Id("edit_team")).Click();

                StringAssert.Contains(_url + "/Teams", _driver.Url);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void DeleteTeam_TeamWithGivenNameIsInDb_TeamIsDeleted()
        {
            var name = _teamName;
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Teams']")).Click();
            try
            {
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                var expected = elements - 1;

                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                       "[normalize-space(text())='" + name + "']]//" +
                       "a[@id='delete_team']"))
                       .Click();
                _driver.FindElement(By.Id("delete_team")).Click();
                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void AddPlayer_CorrectInformation_RowCountIncrements()
        {
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Players']")).Click();
            try
            {
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                var expected = elements.Count + 1;

                _driver.FindElement(By.CssSelector("[href *= 'Create']")).Click();
                _driver.FindElement(By.Id("Name")).SendKeys("Janek");
                _driver.FindElement(By.Id("Nickname")).SendKeys(_playerName);
                _driver.FindElement(By.Id("Surname")).SendKeys("Bienias");
                _driver.FindElement(By.Id("Salary")).SendKeys("2137");
                _driver.FindElement(By.Id("DateOfBirth")).SendKeys("1996-10-10");
                var teamDropDownList = new SelectElement(_driver.FindElement(By.Id("TeamId")));
                teamDropDownList.SelectByText(_teamName);
                _driver.FindElement(By.ClassName("btn-success")).Click();

                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr"));
                Assert.AreEqual(expected, elements.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        private void DeletePlayer_PlayerExists_PlayerIsDeleted()
        {
            LogInAsAdmin(_email, _password);
            _driver.FindElement(By.CssSelector("[href *= 'Players']")).Click();
            try
            {
                var elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                var expected = elements - 1;

                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                       "[normalize-space(text())='" + _playerName + "']]//" +
                       "a[@id='delete_player']"))
                       .Click();
                _driver.FindElement(By.Id("delete_player")).Click();
                elements = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                LogOut();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
            _driver = null;
            _options = null;
            _url = null;
            _teamName = null;
            _playerName = null;
        }

        //Pare metod D.R.Y
        private void LogInAsAdmin(string email, string password)
        {
            _driver.FindElement(By.CssSelector("[href*='Login']")).Click();
            _driver.FindElement(By.Id("Email")).SendKeys(email);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.XPath("//button[@type='submit'][text()='Log in']")).Click();
        }

        private void LogOut()
        {
            _driver.FindElement(By.Id("logout_btn")).Click();
        }
    }
}
