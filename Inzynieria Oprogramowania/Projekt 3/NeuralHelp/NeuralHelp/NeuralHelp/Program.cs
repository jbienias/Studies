using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace NeuralHelp {
    internal class Program {
        private const string Url = "file:///C:/Users/janbi/Desktop/PD-3-TANKS/tanks/tanks.html";
        private static IWebDriver _driver;
        
        private const string Header = "myTank.x,myTank.y,myTank.rotation,myTank.cannonRotation,myTank.velocityX,myTank.velocityY," +
                               "myTank.accelerationX,myTank.accelerationY,myTank.shootCooldown,myTank.controls.turnLeft,myTank.controls.turnRight," +
                               "myTank.controls.goForward,myTank.controls.goBack,myTank.controls.shoot,myTank.controls.cannonLeft,myTank.controls.cannonRight," +
                               "myBullet1.x,myBullet1.y,myBullet1.velocityX,myBullet1.velocityY,myBullet2.x,myBullet2.y,myBullet2.velocityX,myBullet2.velocityY,myBullet3.x,myBullet3.y," +
                               "myBullet3.velocityX,myBullet3.velocityY,enemyTank.x,enemyTank.y,enemyTank.rotation,enemyTank.cannonRotation,enemyTank.velocityX,enemyTank.velocityY,enemyTank.accelerationX," +
                               "enemyTank.accelerationY,enemyTank.shootCooldown,enemyTank.controls.turnLeft,enemyTank.controls.turnRight,enemyTank.controls.goForward,enemyTank.controls.goBack," +
                               "enemyTank.controls.shoot,enemyTank.controls.cannonLeft,enemyTank.controls.cannonRight,enemyBullet1.x,enemyBullet1.y,enemyBullet1.velocityX,enemyBullet1.velocityY,enemyBullet2.x," +
                               "enemyBullet2.y,enemyBullet2.velocityX,enemyBullet2.velocityY,enemyBullet3.x,enemyBullet3.y,enemyBullet3.velocityX,enemyBullet3.velocityY,currentGameTime";
        private const string LogsPath = "data_logs_";
        private const string RubyNeuralNetworkCode = @"data_neural_network.txt";
        
        public static int Main(string[] args) {
            var winnerIdentifierExitCode = 0;
            
            Initialize();

            var rubyScript = File.ReadAllText(RubyNeuralNetworkCode);

            Play(PrepareScript(rubyScript), PrepareScript(ScriptGenerator.GenerateScript()));
                
            var logs = GetJsConsoleLogs();
            if (logs.Count == 0) return 0;


            var winnersName = logs[logs.Count - 1];
            if (winnersName.Contains("udefined"))
            {
                for (var i = logs.Count - 1; i > 0; i--)
                {
                    if (!logs[i].Contains("undefined"))
                    {
                        winnersName = logs[i];
                        break;
                    }
                }
            }


            if (winnersName.Contains("Br"))
            {
                winnersName = "brown";
                winnerIdentifierExitCode = 1;
            } else if (winnersName.Contains("Gr"))
            {
                winnersName = "green";
                winnerIdentifierExitCode = 2;
            } else
            {
                return 0;
            }

            logs.RemoveAt(logs.Count - 1);
            var winnerTankFilePath = LogsPath + winnersName + ".csv";
            var content = Header + "\n";

            foreach (var log in logs) {
                content += log + "\n";
            }
            File.WriteAllText(winnerTankFilePath, content);

            _driver.Quit();

            return winnerIdentifierExitCode;
        }
        
        public static void Initialize() {
            var chromeDriverPath = Directory.GetCurrentDirectory();
            var options = new ChromeOptions();

            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            _driver = new ChromeDriver(chromeDriverPath, options);
        }

        public static void Play(string firstScript, string secondScript) {
            _driver.Navigate().GoToUrl(Url);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.FindElement(By.Id("controll-brown")).Click();
            _driver.FindElement(By.Id("brown-script")).SendKeys(Keys.Control + "a" + Keys.Control);
            _driver.FindElement(By.Id("brown-script")).SendKeys(firstScript);

            _driver.FindElement(By.Id("controll-green")).Click();
            _driver.FindElement(By.Id("green-script")).SendKeys(Keys.Control + "a" + Keys.Control);
            _driver.FindElement(By.Id("green-script")).SendKeys(secondScript);

            _driver.FindElement(By.Id("tanksCanvas")).Click();
            Thread.Sleep(41000);
        }

        public static void Refresh() {
            _driver.Navigate().Refresh();
        }

        public static string PrepareScript(string unpreparedScript)
        {
            var script = unpreparedScript;

            const string replaceWith = "";
            script = script.Replace("\n", replaceWith).Replace("\r", replaceWith).Replace("\t", replaceWith);

            return script;
        }
        
        public static List<string> GetJsConsoleLogs()
        {
            var logsAsEntries = GetConsoleLogs();
            var logsAsStrings = new List<string>();


            if (logsAsEntries.Count != 0 && (LogContains(logsAsEntries[logsAsEntries.Count - 1], "Brown") || (LogContains(logsAsEntries[logsAsEntries.Count - 1], "Green"))))
            {
                logsAsEntries.RemoveAt(0);

                foreach (var log in logsAsEntries)
                {
                    if (!LogContains(log, "myTank") && !LogContains(log, "Uncaught"))
                    {
                        logsAsStrings.Add(CleanLog(log));
                    }
                }
                return logsAsStrings;
            }
            return new List<string>();
        }

        public static List<LogEntry> GetConsoleLogs()
        {
            return _driver.Manage().Logs.GetLog(LogType.Browser).ToList();
        }

        public static string CleanLog(LogEntry log)
        {
            var separator = new[] { "\"" };
            var tmp = log.ToString().Split(separator, StringSplitOptions.None)[1];
            tmp = tmp.Remove(tmp.Length - 2);

            return tmp;
        }

        public static bool LogContains(LogEntry log, string str)
        {
            return log.ToString().Contains(str);
        }

        public static long GetFileCombinedSize(string firstFile, string secondFile)
        {
            long result;
            var firstExists = File.Exists(firstFile);
            var secondExists = File.Exists(secondFile);

            if (firstExists && secondExists)
                result = new FileInfo(firstFile).Length + new FileInfo(secondFile).Length;
            else if (firstExists)
                result = new FileInfo(firstFile).Length;
            else if (secondExists)
                result = new FileInfo(secondFile).Length;
            else
                result = 0;

            return result;
        }
    }











}