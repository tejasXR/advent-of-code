using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        private const string InputFileName = "PuzzleInput";
        private static readonly char[] SpecialCharacters = new char[]{'*', '#', '+', '$'};
        
        public static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines(InputFileName);
            var specialCharacterIndexByLineIndex = new List<(int, int)>();

            // Get where the special characters are
            for (int lineIndex = 0; lineIndex < inputLines.Length; lineIndex++)
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
            }
        }
        
        private static bool IsSpecialCharacter(char c)
        {
            return SpecialCharacters.Contains(c);
        }
    }
}