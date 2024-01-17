using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAppWpf
{
    public class Word
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

        public int Correct
        {
            get
            {
                return _correct;
            }
            set
            {
                _correct = value;
            }
        }
        public int Mistake
        {
            get
            {
                return _mistake;
            }
            set
            {
                _mistake = value;
            }
        }
        public NameOfLanguage Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }
        public string WordName
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }
        public string Translation
        {
            get
            {
                return _translation;
            }
            set
            {
                _translation = value;
            }
        }
    }
    public enum Category
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Conjunction,
        Pronoun,
        Numeral,
        Other,
    }
}
