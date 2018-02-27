using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.Core
{
    public static class Extensions
    {
        public static bool VerifyPredicates<T>(List<Predicate<T>> predicates, T item)
        {
            foreach (Predicate<T> predicate in predicates)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;

        }
        public static int GetPercentageOf(this int value, int percentage)
        {
            return (int)((double)value * (double)((double)percentage / (double)100));
        }
        public static int Percentage(this int current, int lenght)
        {
            return (int)(((double)current / (double)lenght) * (double)100);
        }
        public static long Percentage(this long current, long lenght)
        {
            return (long)(((double)current / (double)lenght) * 100d);
        }
        public static long GetPercentageOf(this long value, int percentage)
        {
            return (long)((double)value * (double)((double)percentage / (double)100));
        }
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            int count = enumerable.Count();

            if (count <= 0)
                return default(T);

            return enumerable.ElementAt(new AsyncRandom().Next(count));
        }
        public static T[] Random<T>(this IEnumerable<T> enumerable, int count)
        {
            T[] array = new T[count];

            int lenght = enumerable.Count();

            if (lenght <= 0)
                return new T[0];

            var random = new AsyncRandom();

            for (int i = 0; i < count; i++)
            {
                array[i] = enumerable.ElementAt(random.Next(lenght));
            }

            return array;
        }
        public static string XMLSerialize(this object obj)
        {
            YAXSerializer serializer = new YAXSerializer(obj.GetType());
            return serializer.Serialize(obj);
        }
        public static double Med<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                return 0;
            }
            dynamic sum = 0;

            foreach (var item in array)
            {
                sum += item;
            }

            return (double)(sum / array.Length);
        }
        public static object XMLDeserialize(this string content, Type type)
        {
            if (content == string.Empty)
                return Activator.CreateInstance(type);

            YAXSerializer serializer = new YAXSerializer(type);
            return Convert.ChangeType(serializer.Deserialize(content), type);
        }
        /// <summary>
        /// Less Faster then XMLDeserialize(this string content,Type type)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static object XMLDeserialize(this string content, Assembly assembly)
        {
            string typeAsString = new string(content.Split('>')[0].Skip(1).ToArray());
            var type = assembly.GetTypes().FirstOrDefault(x => x.Name == typeAsString);
            return XMLDeserialize(content, type);
        }
        public static T XMLDeserialize<T>(this string content)
        {
            return (T)XMLDeserialize(content, typeof(T));
        }
        public static string ToCSV(this IList list)
        {
            string str = string.Empty;
            if (list.Count == 0)
                return str;
            foreach (var value in list)
            {
                str += value.ToString() + ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }

        public static List<T> FromCSV<T>(this string str, char separator = ',')
        {
            if (str == string.Empty)
                return new List<T>();
            var list = new List<T>();
            foreach (var value in str.Split(separator))
            {
                list.Add((T)Convert.ChangeType(value, typeof(T)));
            }
            return list;
        }
        public static T Random<T>(this List<T> list)
        {

            if (list.Count > 0)
            {
                return list[new AsyncRandom().Next(0, list.Count)];
            }
            else
                return default(T);
        }

        public static T[] ParseCollection<T>(string str, Func<string, T> converter)
        {
            T[] result;
            if (string.IsNullOrEmpty(str))
            {
                result = new T[0];
            }
            else
            {
                int num = 0;
                int num2 = str.IndexOf(',', 0);
                if (num2 == -1)
                {
                    result = new T[]
					{
						converter(str)
					};
                }
                else
                {
                    T[] array = new T[str.CountOccurences(',', num, str.Length - num) + 1];
                    int num3 = 0;
                    while (num2 != -1)
                    {
                        array[num3] = converter(str.Substring(num, num2 - num));
                        num = num2 + 1;
                        num2 = str.IndexOf(',', num);
                        num3++;
                    }
                    array[num3] = converter(str.Substring(num, str.Length - num));
                    result = array;
                }
            }
            return result;
        }
        public static TConvert DynamicPop<TObject, TConvert>(this IEnumerable<TObject> obj, Converter<TObject, TConvert> converter, long @default = 1)
        {
            if (obj.Count() == 0)
            {
                dynamic _defaut = @default;
                return (TConvert)_defaut;
            }
            var collection = Array.ConvertAll(obj.ToArray(), converter);
            Array.Sort(collection); 
            dynamic lastValue = collection.Last();
            return (TConvert)(lastValue + 1);
        }
        public static IEnumerable<T> ShuffleWithProbabilities<T>(this IEnumerable<T> enumerable,
                                                                IEnumerable<int> probabilities)
        {
            var rand = new Random();
            var elements = enumerable.ToList();
            var result = new T[elements.Count];
            var indices = probabilities.ToList();

            if (elements.Count != indices.Count)
                throw new Exception("Probabilities must have the same length that the enumerable");

            int sum = indices.Sum();

            if (sum == 0)
                return Shuffle(elements);

            for (int i = 0; i < result.Length; i++)
            {
                int randInt = rand.Next(sum + 1);
                int currentSum = 0;
                for (int j = 0; j < indices.Count; j++)
                {
                    currentSum += indices[j];

                    if (currentSum >= randInt)
                    {
                        result[i] = elements[j];

                        sum -= indices[j];
                        indices.RemoveAt(j);
                        elements.RemoveAt(j);
                        break;
                    }
                }
            }

            return result;
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();

            T[] elements = enumerable.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rand.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            // Lazily yield (avoiding aliasing issues etc)
            foreach (T element in elements)
            {
                yield return element;
            }
        }
        public static string ByteArrayToString(this byte[] bytes)
        {
            var output = new StringBuilder(bytes.Length);

            foreach (var t in bytes)
            {
                output.Append(t.ToString("X2"));
            }

            return output.ToString().ToLower();
        }
        public static bool ScramEqualDictionary<T, T2>(this Dictionary<T, T2> first, Dictionary<T, T2> second)
        {
            if (first.Count != second.Count)
                return false;
            foreach (var data in first)
            {
                if (second.ContainsKey(data.Key))
                {
                    var value = second.First(x => x.Key.Equals(data.Key));
                    if (!value.Value.Equals(data.Value))
                        return false;
                }
                else
                    return false;
            }
            return true;
        }
        public static Type[] WithAttributes(this Type[] types, Type attributeType)
        {
            return Array.FindAll(types, x => x.GetCustomAttribute(attributeType) != null);
        }
        public static Dictionary<T, MethodInfo> MethodsWhereAttributes<T>(this Type type) where T : Attribute
        {
            Dictionary<T, MethodInfo> results = new Dictionary<T, MethodInfo>();

            foreach (var method in type.GetMethods())
            {
                var attributes = method.GetCustomAttributes<T>();
                foreach (var attribute in attributes)
                {
                    results.Add(attribute, method);
                }
            }
            return results;
        }
        /// <summary>
        /// percentage Pourcent de chance
        /// </summary>
        /// <param name="randomizer"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static bool TriggerAleat(this AsyncRandom randomizer, int percentage)
        {
            if (percentage >= 100)
            {
                return true;
            }
            return randomizer.Next(0, 101) <= percentage;
        }
    }
}
