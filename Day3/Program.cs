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
        private static readonly Regex regexNumberOrPeriod = new Regex(@"^(?=\d)(?=\.)");
        
        // private static readonly char[] SpecialCharacters = new char[]{'*', '#', '+', '$', '&'};
        
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines(InputFileName);
            var width = lines[0].Length;
            var height = lines.Length;

            var asteriskNumberDictionary = new Dictionary<(int, int), List<int>>(); 

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
            var isNumberAdjacentToAsterisk = false;
            (int, int) foundAsteriskPoint = (0, 0);
            
            for (int y = 0; y < height; y++)
            {
                void EndCurrentNumber()
                {
                    if (currentNumber != 0 && isNumberAdjacentToAsterisk)
                    {
                        if (asteriskNumberDictionary.TryGetValue(foundAsteriskPoint,
                                out var currentNumberListForAsterisk))
                        {
                            currentNumberListForAsterisk.Add(currentNumber);
                            asteriskNumberDictionary[foundAsteriskPoint] = currentNumberListForAsterisk;
                        }
                        else
                        { 
                            currentNumberListForAsterisk = new List<int> { currentNumber };
                            asteriskNumberDictionary.Add(foundAsteriskPoint, currentNumberListForAsterisk);
                        }
                    }

                    currentNumber = 0;
                    isNumberAdjacentToAsterisk = false;
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

                            var regexAsterisk = new Regex("\\*");
                            var stringToMatch = neighborCharacter.ToString();
                            
                            if (regexAsterisk.IsMatch(stringToMatch))
                            {
                                isNumberAdjacentToAsterisk = true;
                                foundAsteriskPoint = (neighborX, neighborY);
                            }
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
            
            // Find and multiple numbers that share the same asterisk
            foreach (var kvp in asteriskNumberDictionary)
            {
                if (kvp.Value.Count >= 2)
                {
                    var multipliedNumber = 1;
                    foreach (var foundNumber in kvp.Value)
                    {
                        multipliedNumber *= foundNumber;
                    }

                    enginePartSum += multipliedNumber;
                }
            }
            
            Console.Write(enginePartSum);
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