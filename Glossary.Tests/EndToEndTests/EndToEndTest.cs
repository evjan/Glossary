using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public abstract class EndToEndTest
    {
        const int IISPort = 2020;
        private readonly string _applicationName;
        private Process _iisProcess;

        protected EndToEndTest(string applicationName)
        {
            _applicationName = applicationName;
        }

        public IList<IWebDriver> WebDrivers { get; set; }
 
        [TestInitialize]
        public void TestInitialize() {
            // Start IISExpress
            StartIIS();
 
            // Start Selenium drivers
            WebDrivers = new List<IWebDriver> {new FirefoxDriver()};
        }
 
        [TestCleanup]
        public void TestCleanup() {
            // Ensure IISExpress is stopped
            if (_iisProcess.HasExited == false) {
                _iisProcess.Kill();
            }
 
            foreach(var webDriver in WebDrivers)
            {
                webDriver.Quit();
            }
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