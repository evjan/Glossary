using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Glossary.Tests.EndToEndTests
{
    [TestClass]
    public class TermTests : EndToEndTest
    {
        public TermTests() : base("Glossary") { }

        [TestMethod]
        public void As_a_glossary_author_I_would_like_to_add_a_term_and_definition_to_the_system_so_I_can_continually_grow_our_knowledge_base_of_terms()
        {
            foreach(var driver in WebDrivers)
            {
                driver.Navigate().GoToUrl(GetAbsoluteUrl("/"));

                driver.FindElement(By.LinkText("Create New")).Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.Id("Term")));
                
                driver.FindElement(By.Id("Term")).SendKeys("Test-term");
                driver.FindElement(By.Id("Definition")).SendKeys("Test definition entered by automated test. Should be automatically removed after each test run.");

                driver.FindElement(By.Id("Create")).Click();
                wait.Until(d => d.FindElement(By.ClassName("term")));

                var allTerms = driver.FindElements(By.ClassName("term"));
                
                var termsMatchingNewName = allTerms.Where(termElement => termElement.Text == "Test-term");

                Assert.AreEqual(1, termsMatchingNewName.Count());
            }
        }

        [TestMethod]
        public void As_a_glossary_author_I_would_like_to_edit_a_term_so_I_can_fix_mistakes_and_update_definitions()
        {
            foreach (var driver in WebDrivers)
            {
                driver.Navigate().GoToUrl(GetAbsoluteUrl("/"));

                driver.FindElement(By.ClassName("editTerm")).Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.Id("Term")));

                var termTextBox = driver.FindElement(By.Id("Term"));
                termTextBox.Clear();
                termTextBox.SendKeys("Updated-term");
                
                driver.FindElement(By.XPath("//input[@value='Save']")).Click();
                wait.Until(d => d.FindElement(By.ClassName("term")));

                var allTerms = driver.FindElements(By.ClassName("term"));

                Assert.AreEqual(1, allTerms.Count(t => t.Text == "Updated-term"));
            }
        }

        [TestMethod]
        public void As_a_glossary_author_I_would_like_to_remove_terms_that_I_no_longer_feel_are_necessary_or_valid()
        {
            foreach (var driver in WebDrivers)
            {
                driver.Navigate().GoToUrl(GetAbsoluteUrl("/"));

                var allTermsBefore = driver.FindElements(By.ClassName("term"));
                int numberOfTermsBefore = allTermsBefore.Count;
                var nameOfElementToBeDeleted = allTermsBefore.First().Text;

                driver.FindElement(By.ClassName("deleteTerm")).Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.XPath("//input[@value='Delete']")));

                driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
                wait.Until(d => d.FindElement(By.ClassName("term")));

                var allTermsAfter = driver.FindElements(By.ClassName("term"));
                int numberOfTermsAfter = allTermsAfter.Count;

                Assert.AreEqual(numberOfTermsBefore - 1, numberOfTermsAfter);
                Assert.AreEqual(0, allTermsAfter.Count(t => t.Text == nameOfElementToBeDeleted));
            }
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
