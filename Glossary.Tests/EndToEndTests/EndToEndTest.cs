using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public abstract class EndToEndTest
    {
        const int IISPort = 2020;
        
        private const string DataBaseFileName = "Glossary.sdf";
        private const string DataBaseFileNameBackupEnding = "bak";

        private readonly string _applicationName;
        private Process _iisProcess;

        protected EndToEndTest(string applicationName)
        {
            _applicationName = applicationName;
        }

        public IList<IWebDriver> WebDrivers { get; set; }
 
        [TestInitialize]
        public void TestInitialize() {
            StartIIS();
            StartAllWebDrivers();
            CreateBackupOfDatabase(DataBaseFileName, DataBaseFileNameBackupEnding);
        }

        [TestCleanup]
        public void TestCleanup() {
            StopIIS();
            QuitAllWebDrivers();
            RestoreBackupOfDatabase(DataBaseFileName, DataBaseFileNameBackupEnding);
        }

        private void StartAllWebDrivers()
        {
            WebDrivers = new List<IWebDriver> { new FirefoxDriver() };
        }

        private void QuitAllWebDrivers()
        {
            foreach (var webDriver in WebDrivers)
            {
                webDriver.Quit();
            }
        }

        private void StopIIS()
        {
            if (_iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
        }

        private void CreateBackupOfDatabase(string databaseFileName, string databaseFileNameBackupEnding)
        {
            var databasePath = GetDatabasePath();
            File.Copy(databasePath + "\\" + databaseFileName, databasePath + "\\" + databaseFileName + "." + databaseFileNameBackupEnding);
        }

        private void RestoreBackupOfDatabase(string databaseFileName, string databaseFileNameBackupEnding)
        {
            var databasePath = GetDatabasePath();
            File.Delete(databasePath + "\\" + databaseFileName);
            File.Move(databasePath + "\\" + databaseFileName + "." + databaseFileNameBackupEnding, databasePath + "\\" + databaseFileName);
        }

        private string GetDatabasePath()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var databasePath = applicationPath + "\\App_Data";
            return databasePath;
        }

        private void StartIIS() {
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process
                              {
                                  StartInfo =
                                      {
                                          FileName = programFiles + "\\IIS Express\\iisexpress.exe",
                                          Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, IISPort)
                                      }
                              };
            _iisProcess.Start();
        }
 
        protected virtual string GetApplicationPath(string applicationName) {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            
            if(solutionFolder == null)
                throw new Exception("Solution folder for application not found.");

            return Path.Combine(solutionFolder, applicationName);
        }
 
        public string GetAbsoluteUrl(string relativeUrl) {
            if (!relativeUrl.StartsWith("/")) {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format("http://localhost:{0}{1}", IISPort, relativeUrl);
        }
    }
}