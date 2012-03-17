using System.Collections.Generic;
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
    }
}