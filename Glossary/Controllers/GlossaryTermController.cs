using System.Data;
using System.Linq;
using System.Web.Mvc;
using Glossary.Models;
using Glossary.DatabaseContexts;

namespace Glossary.Controllers
{ 
    public class GlossaryTermController : Controller
    {
        private readonly GlossaryContext _db = new GlossaryContext();

        //
        // GET: /GlossaryTerm/

        public ViewResult Index()
        {
            return View(_db.GlossaryTerms.ToList());
        }

        //
        // GET: /GlossaryTerm/Details/5

        public ViewResult Details(int id)
        {
            GlossaryTerm glossaryterm = _db.GlossaryTerms.Find(id);
            return View(glossaryterm);
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
                _db.GlossaryTerms.Add(glossaryterm);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(glossaryterm);
        }
        
        //
        // GET: /GlossaryTerm/Edit/5
 
        public ActionResult Edit(int id)
        {
            GlossaryTerm glossaryterm = _db.GlossaryTerms.Find(id);
            return View(glossaryterm);
        }

        //
        // POST: /GlossaryTerm/Edit/5

        [HttpPost]
        public ActionResult Edit(GlossaryTerm glossaryterm)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(glossaryterm).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(glossaryterm);
        }

        //
        // GET: /GlossaryTerm/Delete/5
 
        public ActionResult Delete(int id)
        {
            GlossaryTerm glossaryterm = _db.GlossaryTerms.Find(id);
            return View(glossaryterm);
        }

        //
        // POST: /GlossaryTerm/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            GlossaryTerm glossaryterm = _db.GlossaryTerms.Find(id);
            _db.GlossaryTerms.Remove(glossaryterm);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}