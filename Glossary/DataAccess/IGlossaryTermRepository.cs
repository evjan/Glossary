using System.Collections.Generic;
using Glossary.Models;

namespace Glossary.DataAccess
{
    public interface IGlossaryTermRepository
    {
        IList<GlossaryTerm> GetAll();
    }
}