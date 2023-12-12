using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4_Part2
{
    internal class Program
    {
        private const string InputFileName = "Input";
        
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines(InputFileName);
            
            // Start a card counter and initialize the array
            int[] cardCount = new int[lines.Length];
            for (var i = 0; i < cardCount.Length; i++)
            {
                cardCount[i] = 1;
            }

            for (var cardId = 0; cardId < lines.Length; cardId++)
            {
                var line = lines[cardId];
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

                int matchCount = 0;
                foreach (var numberString in winningNumberStrings)
                {
                    if (drawnNumberStrings.IndexOf(numberString) == -1) continue;

                    /*
                    // Match!
                    if (cardPoints <= 1)
                        cardPoints++;
                    else
                        cardPoints *= 2;
                        */

                    matchCount++;
                }

                for (int i = 0; i < matchCount; i++)
                {
                    cardCount[cardId + 1 + i] += cardCount[cardId];
                }
            }
            
            Console.WriteLine(cardCount.Sum());

            /*Dictionary<int, int> cardInstanceDictionary = new Dictionary<int, int>();
            
            for (var i = 0; i < cards.Count; i++)
            {
                // Add original instance of card to dictionary
                if (cardInstanceDictionary.TryGetValue(i, out var cardInstance))
                {
                    cardInstanceDictionary[i] = cardInstance + 1;
                }
                else
                {
                    cardInstanceDictionary.Add(i, 1);
                }

                for (int j = 0; j <= cardInstance; j++)
                {
                    for (int k = 0; k <= cards[i].amountOfWinningNumbers; k++)
                    {
                        if (cardInstanceDictionary.TryGetValue(i + k, out var cardInstances))
                        {
                            cardInstanceDictionary[i + k] = cardInstances + 1;
                        }
                        else
                        {
                            cardInstanceDictionary.Add(i + k, 1);
                        }
                    }
                }
            }*/

            // Console.WriteLine($"Total amount of points is {scratchPoints}");
        }
    }

    public struct Card
    {
        public int amountOfWinningNumbers;
        public int cardScore;
    }
}