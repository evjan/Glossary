using System.Data.Entity;
using Glossary.Models;

namespace Glossary.DatabaseContexts
{
    public class GlossaryContext : DbContext
    {
        public DbSet<GlossaryTerm> GlossaryTerms { get; set; }
    }
}