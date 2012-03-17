using System.Collections.Generic;
using Glossary.Controllers;
using Glossary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glossary.Tests.Controllers
{
    [TestClass]
    public class GlossaryTermControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new GlossaryTermController();

            var result = controller.Index();

            var glossaryTerms = (List<GlossaryTerm>) result.Model;

            Assert.IsNotNull(glossaryTerms);
        }
    }
}
