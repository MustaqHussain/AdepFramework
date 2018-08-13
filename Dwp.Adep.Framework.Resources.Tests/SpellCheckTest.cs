using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck;

namespace Dwp.Adep.Framework.EmailService.Tests
{
    [TestClass]
    public class SpellCheckTest
    {
        [TestMethod]
        public void TestSpelling()
        {
            SpellCheckService svc = new SpellCheckService();

            String text = "spelllt wrong";

            List<String> errors = svc.GetSpellingErrors(text, null);

            Assert.IsNotNull(errors);

            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void TestSuggestions()
        {
            SpellCheckService svc = new SpellCheckService();

            String text = "spelllt";

            List<String> suggestions = svc.GetSuggestions(text, null);

            Assert.IsNotNull(suggestions);

            Assert.IsTrue(suggestions.Count > 0);

            foreach (String suggestion in suggestions)
            {
                Assert.IsTrue(suggestion.StartsWith("sp"));
            }
        }
    }
}
