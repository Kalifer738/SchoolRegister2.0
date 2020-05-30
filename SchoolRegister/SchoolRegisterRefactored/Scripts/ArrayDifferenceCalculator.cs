using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Scripts
{
    public static class ArrayDifferenceCalculator
    {
        private static Dictionary<int, int> TimesSeenInArrayPositive(int[] array)
        {
            Dictionary<int, int> timesSeenValue = new Dictionary<int, int>();

            for (int i = 0; i < array.Count(); i++)
            {
                if (!timesSeenValue.ContainsKey(array[i]))
                {
                    timesSeenValue.Add(array[i], 1);
                }
                else
                {
                    timesSeenValue[array[i]]++;
                }
            }
            return timesSeenValue;
        }

        private static Dictionary<int, int> TimesSeenInArrayNegative(int[] array)
        {
            Dictionary<int, int> timesSeenValue = new Dictionary<int, int>();

            for (int i = 0; i < array.Count(); i++)
            {
                if (!timesSeenValue.ContainsKey(array[i]))
                {
                    timesSeenValue.Add(array[i], -1);
                }
                else
                {
                    timesSeenValue[array[i]]--;
                }
            }
            return timesSeenValue;
        }

        /// <summary>
        /// Returns a dictinary, in which contains the number (key), and number seen times (value).
        /// </summary>
        /// <param name="array">The sorted array.</param>
        /// <param name="comparingArray">The sorted array to compare against.</param>
        /// <returns></returns>
        public static Dictionary<int, int> GetDifferenceDictinary(int[] array, int[] comparingArray)
        {
            if (comparingArray.Length == 0)
            {
                return TimesSeenInArrayNegative(array);
            }
            if (array.Length == 0)
            {
                return TimesSeenInArrayPositive(comparingArray);
            }

            Dictionary<int, int> timeseSeenInComparing = SeenTimesInComparingArray(array, comparingArray);
            Dictionary<int, int> timeseSeenInArray = SeenTimesInComparingArray(comparingArray, array);
            return GetFullDifference(timeseSeenInComparing, timeseSeenInArray);
        }

        private static Dictionary<int, int> GetFullDifference(Dictionary<int, int> dictionary1, Dictionary<int, int> dictionary2)
        {
            Dictionary<int, int> fullDifference = new Dictionary<int, int>();
            List<int> keysSearchedFor = new List<int>();
            foreach (int keyValue in dictionary1.Keys)
            {
                keysSearchedFor.Add(keyValue);
                if (dictionary2.Keys.Contains(keyValue))
                {
                    if (Math.Abs(dictionary1[keyValue]) - Math.Abs(dictionary2[keyValue]) == 0)
                    {
                        keysSearchedFor.Add(keyValue);
                        fullDifference.Add(keyValue, dictionary1[keyValue]);
                    }
                    else if (dictionary1[keyValue] == 0)
                    {
                        if (dictionary2.ContainsKey(keyValue))
                        {
                            keysSearchedFor.Add(keyValue);
                            fullDifference[keyValue] = -dictionary2[keyValue];
                        }
                    }
                    else if (dictionary2[keyValue] != 0)
                    {
                        //keysSearchedFor.Add(keyValue);
                        //fullDifference.Add(keyValue, dictionary1[keyValue]);
                        //throw new Exception("AAAAAAAAA");
                    }
                }
                else
                {
                    fullDifference.Add(keyValue, dictionary1[keyValue]);
                }
            }
            foreach (int keyValue in dictionary2.Keys)
            {
                if (!keysSearchedFor.Contains(keyValue))
                {
                    fullDifference.Add(keyValue, Math.Abs(dictionary2[keyValue]));
                }
            }
            return fullDifference;
        }

        /// <summary>
        /// How many times the nubmers have been seen in the comparing array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="comparingArray"></param>
        /// <returns></returns>
        private static Dictionary<int, int> SeenTimesInComparingArray(int[] array, int[] comparingArray)
        {
            Dictionary<int, int> timesSeenValue = new Dictionary<int, int>();

            for (int i = 0; i < array.Length; i++)
            {
                if (timesSeenValue.ContainsKey(array[i]))
                {
                    if (array.Length - 1 != i && array[i] == array[i + 1])
                    {
                        timesSeenValue[array[i]]--;
                        for (int i2 = i + 1; i2 < array.Length + 1; i2++)
                        {
                            if (i2 == array.Length)
                            {
                                i = i2;
                                break;
                            }
                            if (array[i] != array[i2])
                            {
                                i = i2 - 1;
                                break;
                            }
                            timesSeenValue[array[i]]--;
                        }
                    }
                    continue;
                }

                timesSeenValue.Add(array[i], 0);
                for (int i2 = 0; i2 < comparingArray.Length; i2++)
                {
                    if (array[i] == comparingArray[i2])
                    {
                        timesSeenValue[array[i]]++;
                    }
                }
                timesSeenValue[array[i]]--;
            }
            return timesSeenValue;
        }
    }
}
