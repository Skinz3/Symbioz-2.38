using System;

namespace Symbioz.ORM
{
    public class TranslationAttribute : Attribute
    {
        public bool readingMode;

        public TranslationAttribute(bool readingMode = true)
        {
            this.readingMode = readingMode;
        }
    }
}
