using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        private const string InputFileName = "Input";
        
        private static readonly Dictionary<string, int> ConversionsDictionary = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            
            // Unnecessary, but works
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
        };
        
        public static void Main()
        {
            string[] decodedInput = File.ReadLines(InputFileName).ToArray();;

            
            int calibrationSum = 0; 
            foreach (string line in decodedInput)
            {
                var firstIndex = line.Length;
                var lastIndex = -1;
                int firstDigit = 0;
                int lastDigit = 0;

                foreach (var conversion in ConversionsDictionary)
                {
                    var index = line.IndexOf(conversion.Key, StringComparison.Ordinal);
                    if (index == -1)
                        continue;

                    if (index < firstIndex)
                    {
                        firstIndex = index;
                        firstDigit = conversion.Value;
                    }

                    index = line.LastIndexOf(conversion.Key, StringComparison.Ordinal);
                    if (index == -1)
                        continue;

                    if (index > lastIndex)
                    {
                        lastIndex = index;
                        lastDigit = conversion.Value;
                    }
                }

                var fullNumber = firstDigit * 10 + lastDigit;
                calibrationSum += fullNumber;
            }
            
            Console.WriteLine($"Sum of Calibration Values is {calibrationSum}");
        }
    }
}