using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAppWpf
{
    internal class Language
    {
        private NameOfLanguage nameOfLanguage { get; set;}
        private List<Unit> units { get; set; }

        public Language(NameOfLanguage nameOfLanguage, List<Unit> units)
        {
            this.nameOfLanguage = nameOfLanguage;
            this.units = units;
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
