using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public class TermTests : EndToEndTest
    {
        public TermTests() : base("Glossary") { }

        [TestMethod]
        public void SanityCheck()
        {
            var client = new WebClient();
            var result = client.DownloadString(GetAbsoluteUrl("/home/index"));
            StringAssert.Contains(result, "MVC");
        }

        [TestMethod]
        public void As_a_glossary_author_I_would_like_to_add_a_term_and_definition_to_the_system_so_I_can_continually_grow_our_knowledge_base_of_terms()
        {
            foreach (var webDriver in WebDrivers)
            {
                webDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));

            }
        }
    }
}
