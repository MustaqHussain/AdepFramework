using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck
{
    public class SpellingError
    {
        public String MisspeltText { get; set; }

        public List<String> Suggestions { get; set; }
    }
}