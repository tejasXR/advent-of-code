using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3
{
    internal class Program
    {
        private const string InputFileName = "Input";
        private static readonly Regex regexNumberOrPeriod = new Regex(@"^(?=\d)(?=\.)");
        
        // private static readonly char[] SpecialCharacters = new char[]{'*', '#', '+', '$', '&'};
        
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines(InputFileName);
            var width = lines[0].Length;
            var height = lines.Length;

            // Store input in a dual char array. This makes is easier to understand the symbols
            // around a number after we've identified the start/end of a number
            var charMap = new char[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    charMap[x, y] = lines[y][x];
                }
            }

            var enginePartSum = 0;
            var currentNumber = 0;
            var isNumberAdjacentToSymbol = false;
            
            for (int y = 0; y < height; y++)
            {
                void EndCurrentNumber()
                {
                    if (currentNumber != 0 && isNumberAdjacentToSymbol)
                    {
                        enginePartSum += currentNumber;
                    }

                    currentNumber = 0;
                    isNumberAdjacentToSymbol = false;
                }
                
                for (int x = 0; x < width; x++)
                {
                    // check if we are reading a number
                    var currentChar = charMap[x, y];
                    if (char.IsDigit(currentChar))
                    {
                        var characterValue = currentChar - '0';
                        currentNumber = currentNumber * 10 + characterValue;
                        foreach (var (xCord, yCord) in Directions.WithDiagonals)
                        {
                            var neighborX = x + xCord;
                            var neighborY = y + yCord;
                            if (neighborX < 0 || neighborX >= width || neighborY < 0 || neighborY >= height)
                                continue;

                            var neighborCharacter = charMap[neighborX, neighborY];

                            var regexNumber = new Regex("\\d");
                            var regexPeriod = new Regex("\\.");
                            var stringToMatch = neighborCharacter.ToString();
                            
                            if (!regexNumber.IsMatch(stringToMatch) && !regexPeriod.IsMatch(stringToMatch))
                                isNumberAdjacentToSymbol = true;
                        }
                    }
                    else
                    {
                        // we've encountered a non-digit
                        EndCurrentNumber();
                    }
                }
                
                EndCurrentNumber();
            }

            Console.Write(enginePartSum);
        }
        
        private static bool IsSpecialCharacter(char c)
        {
            return !regexNumberOrPeriod.IsMatch(c.ToString());
            /*string pattern = 
            Regex numericalRegEx = new Regex("\\d");
            Regex periodCharacterRegex = new Regex("\\.");

            return !numericalRegEx.IsMatch(c.ToString()) && !periodCharacterRegex.IsMatch(c.ToString());*/
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

    public static class Directions
    {
        public static (int, int)[] WithDiagonals = new(int, int) []
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0),
            (1, 1),
            (-1, 1),
            (1, -1),
            (-1, -1)
        };
    }
}