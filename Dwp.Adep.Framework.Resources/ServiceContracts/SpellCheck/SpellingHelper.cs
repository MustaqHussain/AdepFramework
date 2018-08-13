using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Threading;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.SpellCheck
{
    public class SpellingHelper
    {
        #region Properties
        private readonly object sync = new object();
        private List<SpellString> spellStrings;

        public bool isComplete { get; private set; }
        private bool inProgress;
        public bool WithSuggestions { get; set; }
        private XmlLanguage language = XmlLanguage.GetLanguage("en-GB");

        private Exception lastException = null;
        #endregion
        #region constructors
        /// <summary>
        /// Create a SpellingHelper
        /// </summary>
        /// <param name="withSuggestions_">whether to return suggestions of alternate words with the spelling error(s)</param>
        public SpellingHelper(Boolean withSuggestions_, String language_)
        {
            this.WithSuggestions = withSuggestions_;
            if (language_ != null)
            {
                this.language = XmlLanguage.GetLanguage(language_);

                if (language == null)
                {
                    throw new ArgumentOutOfRangeException(language_ + " not a valid ietf language tag");
                }
            }
        }
        #endregion

        private void GetSpellingErrors()
        {
            try
            {
                foreach (SpellString spellString in spellStrings)
                {
                    TextBox surrogateTextBox = new TextBox();
                    surrogateTextBox.Text = spellString.TextValue;
                    surrogateTextBox.AcceptsReturn = true;
                    surrogateTextBox.AcceptsTab = true;
                    surrogateTextBox.Language = language;
                    surrogateTextBox.SpellCheck.IsEnabled = true;

                    int index = 0;

                    List<SpellingError> errors = new List<SpellingError>();
                    spellString.Errors = errors;

                    while ((index = surrogateTextBox.GetNextSpellingErrorCharacterIndex(index, System.Windows.Documents.LogicalDirection.Forward)) != -1)
                    {
                        SpellingError error = new SpellingError();
                        errors.Add(error);
                        error.MisspeltText = surrogateTextBox.Text.Substring(index, surrogateTextBox.GetSpellingErrorLength(index));
                        if (WithSuggestions)
                        {
                            error.Suggestions = new List<String>();
                            foreach (string suggestion in surrogateTextBox.GetSpellingError(index).Suggestions)
                            {
                                error.Suggestions.Add(suggestion);
                            }
                        }
                        index += error.MisspeltText.Length;

                    }

                }
            }
            catch (Exception e)
            {
                this.lastException = e;
            }
            finally
            {
                lock (sync)
                {
                    isComplete = true;
                    Monitor.PulseAll(sync);
                }
            }
        }

        public void BeginSpellCheck(List<SpellString> spellStrings_)
        {
            if (inProgress) throw new InvalidOperationException("Spell check is already in progress");
            this.spellStrings = spellStrings_;
            Thread backgroundThread = new Thread(new ThreadStart(GetSpellingErrors));
            backgroundThread.SetApartmentState(ApartmentState.STA);
            backgroundThread.Start();
            isComplete = false;
            inProgress = true;
        }

        public List<SpellString> EndSpellCheck()
        {
            if (!inProgress) throw new InvalidOperationException("Spell check is not in progress");
            lock (sync)
            {
                if (!isComplete)
                {
                    Monitor.Wait(sync);
                }
            }
            isComplete = false;
            inProgress = false;
            if (this.lastException != null)
            {
                Exception ex = this.lastException;
                this.lastException = null;
                throw ex;
            }
            return this.spellStrings;
        }
    }
}