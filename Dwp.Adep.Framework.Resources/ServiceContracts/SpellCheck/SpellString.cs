using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck
{
    public class SpellString
    {
        public String ControlKey { get; set; }

        public String TextValue { get; set; }

        public List<SpellingError> Errors { get; set; }
    }
}