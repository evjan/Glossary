using System.Collections.Generic;
using System.Linq;
using Glossary.Controllers;
using Glossary.DataAccess;
using Glossary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Glossary.Tests.Controllers
{
    [TestClass]
    public class GlossaryTermControllerTest
    {
        [TestMethod]
        public void Index_should_sort_the_terms_alphabetically_when_in_right_order()
        {
            var termStartingWithA = new GlossaryTerm {Term = "a"};
            var termStartingWithB = new GlossaryTerm {Term = "b"};

            var glossaryTermsFromRepository = new List<GlossaryTerm>
                                    {
                                        termStartingWithA,
                                        termStartingWithB
                                    };

            var glossaryTerms = GetGlossaryTermsFromController(CreateRepositoryStub(glossaryTermsFromRepository));

            Assert.AreEqual(termStartingWithA.Term, glossaryTerms.ElementAt(0).Term);
            Assert.AreEqual(termStartingWithB.Term, glossaryTerms.ElementAt(1).Term);
        }

        [TestMethod]
        public void Index_should_sort_the_terms_alphabetically_when_in_reversed_order()
        {
            var termStartingWithA = new GlossaryTerm { Term = "a" };
            var termStartingWithB = new GlossaryTerm { Term = "b" };

            var glossaryTermsFromRepository = new List<GlossaryTerm>
                                    {
                                        termStartingWithB,
                                        termStartingWithA
                                    };

            var glossaryTerms = GetGlossaryTermsFromController(CreateRepositoryStub(glossaryTermsFromRepository));

            Assert.AreEqual(termStartingWithA.Term, glossaryTerms.ElementAt(0).Term);
            Assert.AreEqual(termStartingWithB.Term, glossaryTerms.ElementAt(1).Term);
        }

        private static IGlossaryTermRepository CreateRepositoryStub(List<GlossaryTerm> glossaryTermsFromRepository)
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();
            repositoryStub.Expect(m => m.GetAll()).Return(glossaryTermsFromRepository);
            return repositoryStub;
        }

        private static IEnumerable<GlossaryTerm> GetGlossaryTermsFromController(IGlossaryTermRepository repositoryStub)
        {
            var controller = new GlossaryTermController(repositoryStub);
            var result = controller.Index();

            var glossaryTerms = (IEnumerable<GlossaryTerm>) result.Model;
            return glossaryTerms;
        }
    }
}
