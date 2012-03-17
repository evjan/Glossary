using System.Collections.Generic;
using System.Data;
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
        [TestMethod, TestCategory("Unit")]
        public void Index_should_sort_the_terms_alphabetically_when_in_right_order()
        {
            var termStartingWithA = new GlossaryTerm {Term = "a"};
            var termStartingWithB = new GlossaryTerm {Term = "b"};

            var glossaryTermsFromRepository = new List<GlossaryTerm>
                                    {
                                        termStartingWithA,
                                        termStartingWithB
                                    };

            var glossaryTerms = GetGlossaryTermsFromController(CreateRepositoryForGettingAllTerms(glossaryTermsFromRepository));

            Assert.AreEqual(termStartingWithA.Term, glossaryTerms.ElementAt(0).Term);
            Assert.AreEqual(termStartingWithB.Term, glossaryTerms.ElementAt(1).Term);
        }

        [TestMethod, TestCategory("Unit")]
        public void Index_should_sort_the_terms_alphabetically_when_in_reversed_order()
        {
            var termStartingWithA = new GlossaryTerm { Term = "a" };
            var termStartingWithB = new GlossaryTerm { Term = "b" };

            var glossaryTermsFromRepository = new List<GlossaryTerm>
                                    {
                                        termStartingWithB,
                                        termStartingWithA
                                    };

            var glossaryTerms = GetGlossaryTermsFromController(CreateRepositoryForGettingAllTerms(glossaryTermsFromRepository));

            Assert.AreEqual(termStartingWithA.Term, glossaryTerms.ElementAt(0).Term);
            Assert.AreEqual(termStartingWithB.Term, glossaryTerms.ElementAt(1).Term);
        }

        private static IGlossaryTermRepository CreateRepositoryForGettingAllTerms(List<GlossaryTerm> glossaryTermsFromRepository)
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

        [TestMethod, TestCategory("Unit")]
        public void Edit_should_display_error_message_if_term_is_too_long()
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();
            
            var glossaryTermCausingError = new GlossaryTerm();

            const string errorMessage = "Error message!";
            repositoryStub.Expect(m => m.Edit(glossaryTermCausingError)).IgnoreArguments().Throw(new DataException(errorMessage));

            var controller = new GlossaryTermController(repositoryStub);
            controller.Edit(glossaryTermCausingError);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual("Changes could not be saved. Please try again.", controller.ModelState[""].Errors.First().ErrorMessage);
        }

        [TestMethod, TestCategory("Unit")]
        public void Create_should_display_error_message_if_term_is_too_long()
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();

            var glossaryTermCausingError = new GlossaryTerm();

            const string errorMessage = "Error message!";
            repositoryStub.Expect(m => m.Create(glossaryTermCausingError)).IgnoreArguments().Throw(new DataException(errorMessage));

            var controller = new GlossaryTermController(repositoryStub);
            controller.Create(glossaryTermCausingError);

            Assert.AreEqual("The term could not be saved. Please try again.", controller.ModelState[""].Errors.First().ErrorMessage);
        }
    }
}
