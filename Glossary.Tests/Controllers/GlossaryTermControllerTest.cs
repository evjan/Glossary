using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
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
        public void Edit_should_display_error_message_if_there_is_a_data_exception()
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();
            
            var glossaryTermCausingError = new GlossaryTerm();

            repositoryStub.Expect(m => m.Edit(glossaryTermCausingError)).Throw(new DataException(""));

            var controller = new GlossaryTermController(repositoryStub);
            controller.Edit(glossaryTermCausingError);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual("Changes could not be saved. Please try again.", controller.ModelState[""].Errors.First().ErrorMessage);
        }

        [TestMethod, TestCategory("Unit")]
        public void Create_should_display_error_message_if_there_is_a_data_exception()
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();

            var glossaryTermCausingError = new GlossaryTerm();

            repositoryStub.Expect(m => m.Create(glossaryTermCausingError)).Throw(new DataException(""));

            var controller = new GlossaryTermController(repositoryStub);
            controller.Create(glossaryTermCausingError);

            Assert.AreEqual("The term could not be saved. Please try again.", controller.ModelState[""].Errors.First().ErrorMessage);
        }

        [TestMethod, TestCategory("Unit")]
        public void Delete_should_stay_on_same_page_if_there_is_a_data_exception()
        {
            var repositoryStub = MockRepository.GenerateStub<IGlossaryTermRepository>();

            repositoryStub.Expect(m => m.Delete(null)).IgnoreArguments().Throw(new DataException());

            var controller = new GlossaryTermController(repositoryStub);
            var result = (RedirectToRouteResult) controller.DeleteConfirmed(1);
            
            Assert.AreEqual("Delete", result.RouteValues["action"]);
        }
    }
}
