using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Day5_Part1
{
    internal class Program
    {
        private const string InputFileName = "Input";
        private const string SEEDS = "seeds:";
        private const string SEED_TO_SOIL_MAPKEY = "seed-to-soil map:";
        private const string SOIL_TO_FERTILIZER_MAPKEY = "soil-to-fertilizer map:";
        
        public static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(InputFileName);
            List<int> seeds = new List<int>();
            
            // Get seeds
            foreach (var line in input)
            {
                if (!line.Contains(SEEDS)) continue;
                
                var index = line.IndexOf(SEEDS, StringComparison.Ordinal);
                var seedNumbers = line.Substring(index + SEEDS.Length + 1);
                var seedStrings = seedNumbers.Split(null);

                seeds.AddRange(seedStrings.Select(int.Parse));

                break;
            }

            // Get seed-to-soil strings
            var seedToSoilStringArray = GetMappedStrings(input, SEED_TO_SOIL_MAPKEY);
        }

        public static string[] GetMappedStrings(string[] fileInput, string mapKey)
        {
            List<string> mappedStrings = new List<string>();
            int startIndex = 0;
            for (int i = 0; i < fileInput.Length; i++)
            {
                if (fileInput[i].Contains(mapKey))
                    startIndex = i;
            }

            var readIndex = startIndex + 1;
            while (!string.IsNullOrEmpty(fileInput[readIndex]))
            {
                mappedStrings.Add(fileInput[readIndex]);
                readIndex++;
            }

            return mappedStrings.ToArray();
        }

        public static string FindStringWithinSourceRange(string[] stringConversions, int sourceNumber)
        {
            foreach (var conversion in stringConversions)
            {
                var splitConversion = conversion.Split(null);
                var intConversion = splitConversion.Select(int.Parse);

            }


        }

        public static void ConvertSourceNumberToDestinationNumber()
        {

        }
    }
}