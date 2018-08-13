using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck
{
    [ServiceContract]
    interface ISpellCheckService
    {
        /// <summary>
        /// Takes a chunk of text and returns a list of words in that text which are not present in Microsoft's dictionary
        /// </summary>
        /// <param name="text">Text to spell-check</param>
        /// <param name="language">Language to use for spellchecker</param>
        /// <returns>List of misspelt words within the text</returns>
        [OperationContract]
        List<String> GetSpellingErrors(String text, String language);

        /// <summary>
        /// Given a badly spelled word, this method returns a list of possible alternatives
        /// </summary>
        /// <param name="misSpelledWord">word to search alternatives for</param>
        /// <param name="language">language the word is written in</param>
        /// <returns>List of suggestions for the misspelt word</returns>
        [OperationContract]
        List<String> GetSuggestions(String misSpelledWord, String language);
    }
}
