using System.Collections.Generic;
using System.Data.Entity;
using Glossary.Models;

namespace Glossary.DatabaseContexts
{
    public class GlossaryInitializer : DropCreateDatabaseIfModelChanges<GlossaryContext>
    {
        protected override void Seed(GlossaryContext context)
        {
            var glossaryTerms = new List<GlossaryTerm>
                {
                    new GlossaryTerm { Term = "alkaline", Definition =  "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium."},
                    new GlossaryTerm { Term = "accrete", Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass."},
                    new GlossaryTerm { Term = "abyssal plain", Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope."}
                };

            glossaryTerms.ForEach(g => context.GlossaryTerms.Add(g));
            context.SaveChanges();
        }
    }
}