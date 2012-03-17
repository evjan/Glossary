using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public class TermTests : EndToEndTest
    {
        public TermTests() : base("Glossary") { }
 
        [TestMethod]
        public void SanityCheck() {
            var client = new WebClient();
            var result = client.DownloadString(GetAbsoluteUrl("/home/index"));
            StringAssert.Contains(result, "MVC");
        }
    }
}
