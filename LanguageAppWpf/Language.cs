using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAppWpf
{
    internal class Language
    {
        public NameOfLanguage nameOfLanguage { get; set;}
        public string abbreviation { get; set;}
        public Language(NameOfLanguage nameOfLanguage)
        {
            this.nameOfLanguage = nameOfLanguage;
            abbreviation = nameOfLanguage.ToString().Substring(0, 3);
        }
    }
    enum NameOfLanguage
    {
        English,
        Polish,
        German,
        French,
        Spanish,
        Italian,
        Russian,
        Portuguese,
        Swedish,
        Norwegian,
        Chinese,
        Arabic,
    }
}
