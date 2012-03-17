using System.Data.Entity;
using Glossary.Models;

namespace Glossary.DataAccess
{
    public class GlossaryContext : DbContext
    {
        public DbSet<GlossaryTerm> GlossaryTerms { get; set; }
    }
}