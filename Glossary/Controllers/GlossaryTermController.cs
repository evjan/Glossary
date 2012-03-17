using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Glossary.DataAccess;
using Glossary.Models;

namespace Glossary.Controllers
{
    public class GlossaryTermController : Controller
    {
        IGlossaryTermRepository _repository;

        public GlossaryTermController()
            : this(new GlossaryTermRepository())
        {
        }

        public GlossaryTermController(IGlossaryTermRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /GlossaryTerm/

        public ViewResult Index()
        {
            return View(_repository.GetAll().OrderBy(gt => gt.Term));
        }

        //
        // GET: /GlossaryTerm/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GlossaryTerm/Create

        [HttpPost]
        public ActionResult Create(GlossaryTerm glossaryterm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Create(glossaryterm);
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "The term could not be saved. Please try again.");
                }
            }

            return View(glossaryterm);
        }

        //
        // GET: /GlossaryTerm/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_repository.Get(id));
        }

        //
        // POST: /GlossaryTerm/Edit/5

        [HttpPost]
        public ActionResult Edit(GlossaryTerm glossaryterm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(glossaryterm);
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Changes could not be saved. Please try again.");
                }

            }
            return View(glossaryterm);
        }

        //
        // GET: /GlossaryTerm/Delete/5

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete of the glossary term failed. Try again.";
            }

            return View(_repository.Get(id));
        }

        //
        // POST: /GlossaryTerm/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                GlossaryTerm glossaryterm = _repository.Get(id);
                _repository.Delete(glossaryterm);

            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                    new RouteValueDictionary
                        {
                            { "id", id }, 
                            { "saveChangesError", true }
                        });
            }

            return RedirectToAction("Index");
        }
    }
}