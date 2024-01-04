using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAppWpf
{
    internal class Word
    {
        private NameOfLanguage _language;
        private string _word;
        private string _translation;
        private int _mistake;
        private int _correct;
        private Category _category;
        private string _unit;
        public Word(NameOfLanguage language, string word, string translation, Category category, string unit)
        {
            _language = language;
            _unit = unit;
            if (word == null || translation == null)
            {
                throw new FormatException("Word or translation is null");
            }
            _word = word;
            _translation = translation;
            _category = category;
        }
        public Word(NameOfLanguage language, string word, string translation, Category category, string unit, int mistake, int correct)
        {
            _language = language;
            _unit = unit;
            if (word == null || translation == null)
            {
                throw new FormatException("Word or translation is null");
            }
            _word = word;
            _translation = translation;
            _category = category;
            _mistake = mistake;
            _correct = correct;
        }
        public Word()
        {

        }

    }
    enum Category
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Conjunction,
        Pronoun,
        Numeral,
        Interjection,
        Article,
        Other,
    }
}
