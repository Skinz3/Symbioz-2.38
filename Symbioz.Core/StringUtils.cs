using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbioz.Core
{
    public class StringUtils
    {
        private static string[] Hash = { "a", "z", "e", "r", "t", "y", "u", "i", "o", "p", "q", "s", "d", "f", "g", "h", "j", "k", "l", "m", "w", "x", "c", "v", "b", "n" };
        private static AsyncRandom Randomizer = new AsyncRandom();

        public static string RandomString(int lenght)
        {
            string str = string.Empty;
            for (int i = 1; i <= lenght; i++)
            {
                int randomInt = Randomizer.Next(0, Hash.Length);
                str += Hash[randomInt];
            }
            return str;
        }

        public static string RandomName()
        {
            string[] syllables =
                {
                        "la", "le", "ly", "lu", "li", "lo",
                        "za", "ze", "zy", "zu", "zi", "zo",
                        "ra", "re", "ry", "ru", "ri", "ro",
                        "ta", "te", "ty", "tu", "ti", "to",
                        "pa", "pe", "py", "pu", "pi", "po",
                        "qa", "qe", "qy", "qu", "qi", "qo",
                        "sa", "se", "sy", "su", "si", "so",
                        "da", "de", "dy", "du", "di", "do",
                        "fa", "fe", "fy", "fu", "fi", "fo",
                        "ga", "ge", "gy", "gu", "gi", "go",
                        "ha", "he", "hy", "hu", "hi", "ho",
                        "ja", "je", "jy", "ju", "ji", "jo",
                        "ka", "ke", "ky", "ku", "ki", "ko",
                        "na", "ne", "ny", "nu", "ni", "no",
                        "ma", "me", "my", "mu", "mi", "mo",
                        "ca", "ce", "cy", "cu", "ci", "co",
                        "ba", "be", "by", "bu", "bi", "bo",
                        "va", "ve", "vy", "vu", "vi", "vo",
                        "xa", "xe", "xy", "xu", "xi", "xo",
                };
            AsyncRandom random = new AsyncRandom();
            string result = "";

            for (int i = 0; i < random.Next(2, 4); i++)
            {
                result += syllables[random.Next(0, syllables.Length)];
            }

            if (random.Next(2) == 0)
            {
                result += "-";
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    result += syllables[random.Next(0, syllables.Length)];
                }
            }
            result = Char.ToUpper(result[0]) + result.Substring(1);
            return result;
        }

        public static string HtmlEntities(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }
    }
}
