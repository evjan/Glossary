using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public abstract class EndToEndTest
    {
        const int IisPort = 2020;
        private readonly string _applicationName;
        private Process _iisProcess;

        protected EndToEndTest(string applicationName)
        {
            _applicationName = applicationName;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Start IISExpress
            StartIIS();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Ensure IISExpress is stopped
            if (_iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
        }

        private void StartIIS()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process
                              {
                                  StartInfo =
                                      {
                                          FileName = programFiles + "\\IIS Express\\iisexpress.exe",
                                          Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, IisPort)
                                      }
                              };
            _iisProcess.Start();
        }

        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            if (solutionFolder != null) 
                return Path.Combine(solutionFolder, applicationName);
            else
                throw new FileNotFoundException("Could not find the base directory");
        }

        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format("http://localhost:{0}{1}", IisPort, relativeUrl);
        }
    }
}