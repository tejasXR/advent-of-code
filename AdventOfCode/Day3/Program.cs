using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        private const string InputFileName = "Input";
        private static readonly char[] SpecialCharacters = new char[]{'*', '#', '+', '$'};
        
        public static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines(InputFileName);
            var specialCharacterIndexByLineIndex = new List<(int, int)>();
            int enginePartSum = 0;
            
            // Get first and last index of number
            for (int lineIndex = 0; lineIndex < inputLines.Length; lineIndex++)
            {
                var line = inputLines[lineIndex];
                
                // Does this line contain any digits
                var stringToSearch = line;
                var lastDigitIndex = 0;
                while (stringToSearch.Any(char.IsDigit))
                {
                    var firstDigitIndex =  line.IndexOf(line.First(char.IsDigit)) + lastDigitIndex;
                    for (int i = firstDigitIndex + 1; i < line.Length; i++)
                    {
                        if (!char.IsDigit(line[i]))
                        {
                            lastDigitIndex = i - 1;

                            if (IsAdjacentToSymbol(inputLines, lineIndex, firstDigitIndex, lastDigitIndex))
                            {
                                var numberFound = int.Parse(line.Substring(firstDigitIndex, i));
                                enginePartSum += numberFound;
                            }
                        }
                    }

                    stringToSearch = line.Substring(lastDigitIndex);
                }
            }

            // Get where the special characters are
            /*for (int lineIndex = 0; lineIndex < inputLines.Length; lineIndex++)
            {
                var line = inputLines[lineIndex];
                if (line.Any(IsSpecialCharacter))
                {
                    var tempString = line;
                    while (tempString.Any(IsSpecialCharacter))
                    {
                        var foundChar = tempString.First(IsSpecialCharacter);
                        var indexOfFoundChar = tempString.IndexOf(foundChar);
                        tempString = tempString.Remove(0, indexOfFoundChar + 1);
                        
                        specialCharacterIndexByLineIndex.Add((lineIndex, indexOfFoundChar));
                    }
                }
            }

            foreach (var (lineIndex, stringIndex) in specialCharacterIndexByLineIndex)
            {
                Console.WriteLine($"Found special character at line index {lineIndex}, and string index {stringIndex}");
            }*/
        }
        
        private static bool IsSpecialCharacter(char c)
        {
            return SpecialCharacters.Contains(c);
        }

        private static bool IsAdjacentToSymbol(string[] inputLines, int lineIndex, int startIndex, int endIndex)
        {
            var lineLength = inputLines[0].Length;
            var searchStartIndex = startIndex == 0 ? 0 : startIndex - 1;
            var searchEndIndex = endIndex == lineLength ? endIndex : endIndex + 1;

            string lineUp = "";
            string lineDown = "";

            var currentLine = inputLines[lineIndex].Substring(searchStartIndex, searchEndIndex);
            
            if (lineIndex != 0) 
                lineUp = inputLines[lineIndex - 1].Substring(searchStartIndex, searchEndIndex);

            if (lineIndex + 1 < inputLines.Length)
                lineDown = inputLines[lineIndex + 1].Substring(searchStartIndex, searchEndIndex);

            if (string.Concat(lineUp, lineDown, currentLine).Any(IsSpecialCharacter))
                return true;
            
            return false;
        }
    }
}