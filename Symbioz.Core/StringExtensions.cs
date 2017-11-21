using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Symbioz.Core
{
    public static class StringExtensions
    {
        public static string GetMD5(this string encryptString)
        {
            var passByteCrypt = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(encryptString));
            return passByteCrypt.ByteArrayToString();
        }
        public static string ConcatCopy(this string str, int times)
        {
            StringBuilder builder = new StringBuilder(str.Length * times);
            for (int i = 0; i < times; i++)
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        public static string DeleteLastLetter(this string str)
        {
            if (str.Length < 2)
            {
                return str;
            }
            return str.Substring(0, str.Length - 1);
        }
        public static int CountOccurences(this string str, char chr)
        {
            return str.CountOccurences(chr, 0, str.Length);
        }
        public static int CountOccurences(this string str, char chr, int startIndex, int count)
        {
            int occurences = 0;
            for (int i = startIndex; i < startIndex + count; i++)
            {
                if (str[i] == chr)
                {
                    occurences++;
                }
            }
            return occurences;
        }

        public static string EscapeString(this string str)
        {
            string str1;
            if (str == null)
            {
                str1 = null;
            }
            else
            {
                str1 = Regex.Replace(str, "[\\r\\n\\x00\\x1a\\\\'\"]", "\\$0");
            }
            return str1;
        }

        public static string FirstLetterUpper(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            char[] chArray = source.ToCharArray();
            chArray[0] = char.ToUpper(chArray[0]);
            return new string(chArray);
        }

        public static string HtmlEntities(this string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            return str;
        }

        public static string RandomString(this Random random, int size)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                stringBuilder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0))));
            }
            return stringBuilder.ToString();
        }

        public static string[] SplitAdvanced(this string expression, string delimiter)
        {
            return expression.SplitAdvanced(delimiter, string.Empty, false);
        }

        public static string[] SplitAdvanced(this string expression, string delimiter, string qualifier)
        {
            return expression.SplitAdvanced(delimiter, qualifier, false);
        }

        public static string[] SplitAdvanced(this string expression, string delimiter, string qualifier, bool ignoreCase)
        {
            bool qualifierState = false;
            int startIndex = 0;
            ArrayList values = new ArrayList();
            for (int charIndex = 0; charIndex < expression.Length - 1; charIndex++)
            {
                if (qualifier != null)
                {
                    if (string.Compare(expression.Substring(charIndex, qualifier.Length), qualifier, ignoreCase) == 0)
                    {
                        qualifierState = !qualifierState;
                    }
                    else if (!qualifierState & delimiter != null & string.Compare(expression.Substring(charIndex, delimiter.Length), delimiter, ignoreCase) == 0)
                    {
                        values.Add(expression.Substring(startIndex, charIndex - startIndex));
                        startIndex = charIndex + 1;
                    }
                }
            }
            if (startIndex < expression.Length)
            {
                values.Add(expression.Substring(startIndex, expression.Length - startIndex));
            }
            string[] returnValues = new string[values.Count];
            values.CopyTo(returnValues);
            return returnValues;
        }
        public static string RemoveAccents(this string source)
        {
            return string.Concat(
                source.Normalize(NormalizationForm.FormD)
                      .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                   UnicodeCategory.NonSpacingMark)
                ).Normalize(NormalizationForm.FormC);
        }

        public static bool HasAccents(this string source)
        {
            return
                source.Normalize(NormalizationForm.FormD)
                      .Any(x => CharUnicodeInfo.GetUnicodeCategory(x) == UnicodeCategory.NonSpacingMark);
        }
    }
}