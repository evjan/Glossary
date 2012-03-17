using System.Collections.Generic;
using System.Data;
using System.Linq;
using Glossary.Models;

namespace Glossary.DataAccess
{
    public class GlossaryTermRepository : IGlossaryTermRepository
    {
        private readonly GlossaryContext _db = new GlossaryContext();

        public IList<GlossaryTerm> GetAll()
        {
            return _db.GlossaryTerms.ToList();
        }

        public GlossaryTerm Get(int id)
        {
            return _db.GlossaryTerms.Find(id);
        }

        public void Create(GlossaryTerm glossaryTerm)
        {
            _db.GlossaryTerms.Add(glossaryTerm);
            _db.SaveChanges();
        }

        public void Edit(GlossaryTerm glossaryTerm)
        {
            _db.Entry(glossaryTerm).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(GlossaryTerm glossaryTerm)
        {
            _db.GlossaryTerms.Remove(glossaryTerm);
            _db.SaveChanges();
        }
    }
}