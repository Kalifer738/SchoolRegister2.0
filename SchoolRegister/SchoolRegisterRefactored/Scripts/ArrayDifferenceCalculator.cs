using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Scripts
{
    public static class ArrayDifferenceCalculator
    {
        /// <summary>
        /// Returns a dictinary, in which contains the number (key), and number seen times (value).
        /// </summary>
        /// <param name="array">The sorted array.</param>
        /// <param name="comparingArray">The sorted array to compare against.</param>
        /// <returns></returns>
        public static Dictionary<int, int> GetDifferenceDictinary(int[] array, int[] comparingArray)
        {
            Dictionary<int, int> timeseSeenInComparing = GetDicinaryDifference(array, comparingArray);
            Dictionary<int, int> timeseSeenInArray = GetDicinaryDifference(comparingArray, array);
            return GetFullDifference(timeseSeenInComparing, timeseSeenInArray);
        }

        public static int[] GetDifferenceArray(int[] array, int[] comparingArray)
        {
            Dictionary<int, int> differenceDictinary = GetDicinaryDifference(array, comparingArray);
            int[] arrayToReturn = new int[differenceDictinary.Count];

            int index = 0;
            foreach (int key in differenceDictinary.Keys)
            {
                for (int i = 0; i < differenceDictinary[key]; i++)
                {
                    arrayToReturn[index] = differenceDictinary[key];
                    index++;
                }
            }
            return arrayToReturn;
        }

        private static Dictionary<int, int> GetFullDifference(Dictionary<int, int> dictionary1, Dictionary<int, int> dictionary2)
        {
            Dictionary<int, int> fullDifference = new Dictionary<int, int>();
            List<int> keysSearchedFor = new List<int>();
            foreach (int keyValue in dictionary1.Keys)
            {
                keysSearchedFor.Add(keyValue);
                int calc = dictionary1[keyValue];
                if (dictionary2.Keys.Contains(keyValue))
                {
                    calc -= dictionary2[keyValue];
                }

                fullDifference.Add(keyValue, calc);
            }
            foreach (int keyValue in dictionary2.Keys)
            {
                if (keysSearchedFor.Contains(keyValue))
                {
                    continue;
                }

                fullDifference.Add(keyValue, Math.Abs(dictionary2[keyValue]));
            }
            return fullDifference;
        }

        private static Dictionary<int, int> GetDicinaryDifference(int[] array1, int[] array2)
        {
            Dictionary<int, int> timesSeenValue = new Dictionary<int, int>();

            for (int i = 0; i < array1.Length; i++)
            {
                if (timesSeenValue.ContainsKey(array1[i]))
                {
                    continue;
                }
                timesSeenValue.Add(array1[i], 0);
                for (int i2 = 0; i2 < array2.Length; i2++)
                {
                    if (array1[i] == array2[i2])
                    {
                        timesSeenValue[array1[i]]++;
                    }
                }
                if (timesSeenValue[array1[i]] == 0)
                {
                    for (int i2 = 0; i2 < array1.Length; i2++)
                    {
                        if (array1[i] == array1[i2])
                        {
                            timesSeenValue[array1[i]]--;
                        }
                    }
                }
            }
            return timesSeenValue;
        }
    }
}
