using System;
using System.IO;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        private const string InputFileName = "Input";
        
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines(InputFileName);

            int scratchPoints = 0;
            foreach (var line in lines)
            {
                var colonIndex = line.IndexOf(':') + 1;
                var numbers = line.Substring(colonIndex, line.Length - colonIndex);
                var splitString = numbers.Split('|');

                // Get numbers in each section
                // Can we just parse to INT right here?
                var winningNumberStrings = splitString[0].Split(null).ToList();
                var drawnNumberStrings = splitString[1].Split(null).ToList();

                // Remove white space
                winningNumberStrings.RemoveAll(string.IsNullOrWhiteSpace);
                drawnNumberStrings.RemoveAll(string.IsNullOrWhiteSpace);

                int cardPoints = 0;
                foreach (var numberString in winningNumberStrings)
                {
                    if (drawnNumberStrings.IndexOf(numberString) == -1) continue;
                    
                    // Match!
                    if (cardPoints <= 1)
                        cardPoints++;
                    else
                        cardPoints *= 2;
                }

                scratchPoints += cardPoints;
            }
            
            Console.WriteLine($"Total amount of points is {scratchPoints}");
        }
    }
}