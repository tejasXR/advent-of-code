using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3
{
    internal class Program
    {
        private const string InputFileName = "Input";
        // private static readonly char[] SpecialCharacters = new char[]{'*', '#', '+', '$', '&'};
        
        public static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines(InputFileName);
            int enginePartSum = 0;
            
            // Get first and last index of number
            for (int lineIndex = 0; lineIndex < inputLines.Length; lineIndex++)
            {
                var line = inputLines[lineIndex];
                var listOfValidNumbersInCurrentLine = new List<int>();
                
                // Does this line contain any digits
                int firstDigitIndex = 0;
                int lastDigitIndex = 0;

                firstDigitIndex = TryGetFirstDigitInString(line, lastDigitIndex);
                if (firstDigitIndex == -1)
                    continue;
                    
                for (int i = firstDigitIndex + 1; i <= line.Length; i++)
                {
                    if (!char.IsDigit(line[i]) || (char.IsDigit(line[i]) && i == line.Length))
                    {
                        lastDigitIndex = i;

                        if (IsAdjacentToSymbol(inputLines, lineIndex, firstDigitIndex, lastDigitIndex))
                        {
                            var numberString = line.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex);
                            var numberFound = Int32.Parse(numberString);
                            enginePartSum += numberFound;
                            listOfValidNumbersInCurrentLine.Add(numberFound);
                        }
                        
                        // Try to see if there are still more digits in the strings
                        firstDigitIndex = TryGetFirstDigitInString(line, lastDigitIndex);
                        
                        if (firstDigitIndex == -1)
                            break;
                        
                        i = firstDigitIndex;
                    }
                }

                var lineReport = $"The valid number in line {lineIndex + 1} are: ";
                foreach (var validNumber in listOfValidNumbersInCurrentLine)
                {
                    lineReport += $"| {validNumber} |";
                }
                
                Console.WriteLine($"{lineReport}");

            }
            
            Console.WriteLine($"Engine Part Sum is {enginePartSum}");
        }

        private static int TryGetFirstDigitInString(string inputString, int startIndex)
        {
            try
            {
                var subString = inputString.Substring(startIndex);
                var firstFoundDigit = subString.First(char.IsDigit);
                var indexOfFoundDigit = subString.IndexOf(firstFoundDigit);
                return indexOfFoundDigit + startIndex;
            }
            catch (InvalidOperationException e)
            {
                return -1;
            }
        }
        
        private static bool IsSpecialCharacter(char c)
        {
            Regex numericalRegEx = new Regex("\\d");
            Regex periodCharacterRegex = new Regex("\\.");

            return !numericalRegEx.IsMatch(c.ToString()) && !periodCharacterRegex.IsMatch(c.ToString());
        }

        private static bool IsAdjacentToSymbol(string[] inputLines, int lineIndex, int startIndex, int endIndex)
        {
            var lineLength = inputLines[0].Length;
            var searchStartIndex = startIndex == 0 ? 0 : startIndex - 1;
            // var searchLength = (endIndex == lineLength - 1 ? endIndex : endIndex + 1) - searchStartIndex;
            var searchLength = (endIndex + 1) - searchStartIndex;

            string lineUp = "";
            string lineDown = "";

            var currentLine = inputLines[lineIndex].Substring(searchStartIndex, searchLength);
            
            if (lineIndex != 0) 
                lineUp = inputLines[lineIndex - 1].Substring(searchStartIndex, searchLength);

            if (lineIndex + 1 < inputLines.Length)
                lineDown = inputLines[lineIndex + 1].Substring(searchStartIndex, searchLength);

            if (string.Concat(lineUp, lineDown, currentLine).Any(IsSpecialCharacter))
                return true;
            
            return false;
        }
    }
}