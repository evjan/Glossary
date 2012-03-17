using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public class TermTests : EndToEndTest
    {
        public TermTests() : base("Glossary") { }

        [TestMethod, Ignore]
        public void As_a_glossary_author_I_would_like_to_add_a_term_and_definition_to_the_system_so_I_can_continually_grow_our_knowledge_base_of_terms()
        {
        }

        [TestMethod, Ignore]
        public void As_a_glossary_author_I_would_like_to_edit_a_term_so_I_can_fix_mistakes_and_update_definitions()
        {
        }

        [TestMethod, Ignore]
        public void As_a_glossary_author_I_would_like_to_remove_terms_that_I_no_longer_feel_are_necessary_or_valid()
        {
        }

        [TestMethod]
        public void As_a_glossary_author_I_would_like_to_view_the_alphabetically_sorted_list_of_terms_and_definitions_so_I_can_find_a_particular_term_quickly()
        {
            foreach(var driver in WebDrivers)
            {
                driver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
                
                var terms = driver.FindElements(By.ClassName("term"));
                Assert.AreEqual("abyssal plain", terms[0].Text);
                Assert.AreEqual("accrete", terms[1].Text);

                var definitions = driver.FindElements(By.ClassName("definition"));
                Assert.AreEqual("The ocean floor offshore from the continental margin, usually very flat with a slight slope.", definitions[0].Text);
                Assert.AreEqual("v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.", definitions[1].Text);
            }
        }
    }
}
