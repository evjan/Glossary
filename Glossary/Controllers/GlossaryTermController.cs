using System.Data;
using System.Linq;
using System.Web.Mvc;
using Glossary.DataAccess;
using Glossary.Models;

namespace Glossary.Controllers
{ 
    public class GlossaryTermController : Controller
    {
        IGlossaryTermRepository _repository;

        public GlossaryTermController() : this(new GlossaryTermRepository())
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
        // GET: /GlossaryTerm/Details/5

        public ViewResult Details(int id)
        {
            return View(_repository.Get(id));
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
                _repository.Create(glossaryterm);
                return RedirectToAction("Index");  
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
                _repository.Edit(glossaryterm);
                return RedirectToAction("Index");
            }
            return View(glossaryterm);
        }

        //
        // GET: /GlossaryTerm/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_repository.Get(id));
        }

        //
        // POST: /GlossaryTerm/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            GlossaryTerm glossaryterm = _repository.Get(id);
            _repository.Delete(glossaryterm);
            return RedirectToAction("Index");
        }
    }
}