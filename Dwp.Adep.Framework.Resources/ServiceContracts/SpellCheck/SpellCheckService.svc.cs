using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpellCheckService" in code, svc and config file together.
    public class SpellCheckService : ISpellCheckService
    {
        /// <summary>
        /// Takes a chunk of text and returns a list of words in that text which are not present in Microsoft's dictionary
        /// </summary>
        /// <param name="text">Text to spell-check</param>
        /// <param name="language">Language to use for spellchecker</param>
        /// <returns></returns>
        public List<String> GetSpellingErrors(String text, String language)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            SpellingHelper spellingHelper = new SpellingHelper(false, language);
            
            List<SpellString> spellStrings = new List<SpellString>(1);
            spellStrings.Add(new SpellString());
            spellStrings.First().TextValue = text;

            spellingHelper.BeginSpellCheck(spellStrings);

            spellStrings = spellingHelper.EndSpellCheck();

            if (spellStrings.Count != 1)
            {
                throw new IndexOutOfRangeException("Incorrect number of spell checks returned");
            }
            SpellString result = spellStrings.First();
            List<String> spellingErrors = new List<String>(result.Errors != null ? result.Errors.Count : 0);

            if (result.Errors != null)
            {
                foreach (SpellingError error in result.Errors)
                {
                    spellingErrors.Add(error.MisspeltText);
                }
            }

            return spellingErrors;
        }

        /// <summary>
        /// Given a badly spelled word, this method returns a list of possible alternatives
        /// </summary>
        /// <param name="misSpelledWord">word to search alternatives for</param>
        /// <param name="language">language the word is written in</param>
        /// <returns></returns>
        public List<String> GetSuggestions(String misSpelledWord, String language)
        {
            if (misSpelledWord == null)
            {
                throw new ArgumentNullException("misSpelledWord");
            }

            SpellingHelper spellingHelper = new SpellingHelper(true, language);

            List<SpellString> spellStrings = new List<SpellString>(1);
            spellStrings.Add(new SpellString());
            spellStrings.First().TextValue = misSpelledWord;

            spellingHelper.BeginSpellCheck(spellStrings);

            spellStrings = spellingHelper.EndSpellCheck();

            if (spellStrings.Count != 1)
            {
                throw new IndexOutOfRangeException("Incorrect number of spell checks returned");
            }
            SpellString result = spellStrings.First();
            List<String> suggestions = new List<String>(1);

            if (result.Errors != null)
            {
                // Should only be one
                foreach (SpellingError error in result.Errors)
                {
                    suggestions.AddRange(error.Suggestions);
                }
            }

            return suggestions;
        }
    }
}
