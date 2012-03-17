using System.Collections.Generic;
using Glossary.Models;

namespace Glossary.DataAccess
{
    public interface IGlossaryTermRepository
    {
        IList<GlossaryTerm> GetAll();
        GlossaryTerm Get(int id);
        void Create(GlossaryTerm glossaryTerm);
        void Edit(GlossaryTerm glossaryTerm);
        void Delete(GlossaryTerm glossaryTerm);
    }
}