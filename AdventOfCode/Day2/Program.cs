using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
   internal class Program
    {
        private const string InputFileName = "Input";
        
        public static void Main()
        {
            List<GameResult> games = new List<GameResult>();
            
            // Read lines in input and create a string array
            var inputLine = File.ReadLines(InputFileName).ToArray();

            for (var index = 0; index < inputLine.Length; index++)
            {
                var gameString = inputLine[index];
                var gameResult = new GameResult();

                char startSeparator = ':';
                char setSeparator = ';';
                char labelSeparator = ',';

                var results = gameString.Remove(0, gameString.IndexOf(startSeparator) + 1);
                var setResults = results.Split(setSeparator);

                foreach (var setResult in setResults)
                {
                    var setCubeResults = setResult.Split(labelSeparator);

                    foreach (var cubeResult in setCubeResults)
                    {
                        if (cubeResult.Contains("blue"))
                        {
                            var numberResult = Int32.Parse(Regex.Match(cubeResult, @"\d+").ToString());
                            if (gameResult.maxCountedBlueCubesInSet < numberResult)
                                gameResult.maxCountedBlueCubesInSet = numberResult;
                        }

                        if (cubeResult.Contains("red"))
                        {
                            var numberResult = Int32.Parse(Regex.Match(cubeResult, @"\d+").ToString());
                            if (gameResult.maxCountedRedCubesInSet < numberResult)
                                gameResult.maxCountedRedCubesInSet = numberResult;
                        }

                        if (cubeResult.Contains("green"))
                        {
                            var numberResult = Int32.Parse(Regex.Match(cubeResult, @"\d+").ToString());
                            if (gameResult.maxCountedGreenCubesInSet < numberResult)
                                gameResult.maxCountedGreenCubesInSet = numberResult;
                        }
                    }
                }

                games.Add(gameResult);
            }

            int cubePowerSum = 0;
            for (var i = 0; i < games.Count; i++)
            {
                var currentGame = games[i];
                
                var cubePower = currentGame.maxCountedBlueCubesInSet 
                                * currentGame.maxCountedRedCubesInSet
                                * currentGame.maxCountedGreenCubesInSet;

                cubePowerSum += cubePower;
            }
            
            Console.WriteLine(cubePowerSum);
        }
    }

    public struct GameResult
    {
        public int maxCountedRedCubesInSet;
        public int maxCountedBlueCubesInSet;
        public int maxCountedGreenCubesInSet;
    }
}