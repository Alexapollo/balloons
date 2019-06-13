using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace balloons
{
    class Program
    {
        static void Main(string[] args)
        {
            string val;
            string target;

            Console.Write("Enter target String: ");

            target = Console.ReadLine();
            var dictTarget = generateDictionaryTarget(target);

            while (true)
            {
                Console.Write("Enter String: ");
                val = Console.ReadLine();

                var num = findTarget(stringToArray(val), dictTarget, 0);
                Console.WriteLine($"The maximum number of {target} compinations is {num} times");
            }
        }

        /*
        Generates the dictionary of the target array with initial values based on the input
         */
        private static Dictionary<string, int> generateDictionaryTarget(string target)
        {
            var dict = new Dictionary<string, int>();
            var array = stringToArray(target);
            foreach (var s in array)
            {
                int value;
                if (dict.TryGetValue(s, out value)) { value++; }
                else { value = 1; }
                dict[s] = value;
            }
            return dict;
        }

        /*
        The main functionality to find the matches for target
         */
        private static int findTarget(string[] array, Dictionary<string, int> targetDict, int count)
        {
            List<int> positions = new List<int>();
            Dictionary<string, int> tempDict = new Dictionary<string, int>(targetDict);
            // Dictionary<string, int> tempDict = new Dictionary<string, int>() { { "B", 1 }, { "A", 1 }, { "L", 2 }, { "O", 2 }, { "N", 1 } };
            for (var i = 0; i < array.Length; i++)
            {
                var value = 0;
                if (tempDict.TryGetValue(array[i], out value))
                {
                    if (tempDict[array[i]] == 0) { break; }
                    tempDict[array[i]] = value - 1;
                    positions.Add(i);
                }

            }
            if (isTouched(tempDict))
            {
                var newArray = shortArray(array, positions);
                count = count + 1;
                return findTarget(newArray, targetDict, count);
            }

            return count;
        }

        /*
        Check if key-value pairs is down to zero
         */
        private static bool isTouched(Dictionary<string, int> array)
        {
            if (array.Any(x => x.Value != 0)) { return false; }

            return true;
        }

        /*
        Returns a new smaller array by removing the target characters
         */
        private static string[] shortArray(string[] array, List<int> positions)
        {
            positions.Reverse();
            ArrayList copyArray = new ArrayList(array);
            foreach (var pair in positions)
            {
                copyArray.RemoveAt(pair);
            }
            if (copyArray.Count == 0) { return new string[0]; }
            return (string[])copyArray.ToArray(typeof(string));
        }

        /*
        Converts the input string to Array
         */
        private static string[] stringToArray(string msg)
        {
            return msg.ToCharArray().Select(c => c.ToString()).ToArray();
        }
    }
}
