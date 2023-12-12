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
        private const string SEED_TO_SOIL = "seed-to-soil map:";
        private const string SOIL_TO_FERTILIZER = "soil-to-fertilizer map:";
        
        public static void Main(string[] args)
        {
            var input = File.ReadAllLines(InputFileName);
            List<double> seeds = new List<double>();
            
            foreach (var line in input)
            {
                if (!line.Contains(SEEDS)) continue;
                
                var index = line.IndexOf(SEEDS, StringComparison.Ordinal);
                var seedNumbers = line.Substring(index + SEEDS.Length + 1);
                var seedStrings = seedNumbers.Split(null);

                seeds.AddRange(seedStrings.Select(double.Parse));
            }
            
            // create a dataTable
            ulong rows = (ulong)seeds.Max();
            var columns = 8;

            string[,] array = new string[columns, rows];
            
            /*DataTable dataTable = new DataTable("Almanac")
            {
                Columns = { "SEED", "SOIL", "FERTILIZER", "WATER", "LIGHT", "TEMPERATURE", "HUMIDITY", "LOCATION" },
            };
            
            for (int j = 0; j <= rows; j++)
            {
                dataTable.Rows.Add();
            }

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                dataTable
            }*/
        }
    }
}