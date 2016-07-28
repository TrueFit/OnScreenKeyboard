using OnScreenKeyboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnScreenKeyboard.Services
{
    public class OnScreenKeyboardService
    {
        public List<char> CalculateResults(string alphabet, string searchTerms)
        {
            List<char> output = new List<char>();
            Dictionary<char, LetterLocation> dictionary = CreateDictionary(alphabet);

            char[] userCharSearch = searchTerms.ToCharArray();

            LetterLocation currentLocation = new LetterLocation(0, 0);

            for (int i = 0; i < userCharSearch.Length; i++)
            {
                char desiredChar = char.ToUpper(userCharSearch[i]);

                if (desiredChar == ' ')
                {
                    output.Add('S');
                    continue;
                }

                LetterLocation desiredLocation = dictionary[desiredChar];
                while (currentLocation.Y != desiredLocation.Y)
                {
                    if (currentLocation.Y > desiredLocation.Y)
                    {
                        currentLocation.Y -= 1;
                        output.Add('U');
                    }
                    else if (currentLocation.Y < desiredLocation.Y)
                    {
                        currentLocation.Y += 1;
                        output.Add('D');
                    }
                }

                while (currentLocation.X != desiredLocation.X)
                {
                    if (currentLocation.X > desiredLocation.X)
                    {
                        currentLocation.X -= 1;
                        output.Add('L');
                    }
                    else if (currentLocation.X < desiredLocation.X)
                    {
                        currentLocation.X += 1;
                        output.Add('R');
                    }
                }

                output.Add('#');
            }

            return output;
        }

        private Dictionary<char, LetterLocation> CreateDictionary(string alphabet)
        {
            Dictionary<char, LetterLocation> dictionary = new Dictionary<char, LetterLocation>();

            string[] lines = alphabet.Split('\n');

            for (int y = 0; y < lines.Length; y++)
            {
                char[] characters = lines[y].ToCharArray();

                for (int x = 0; x < characters.Length; x++)
                {
                    dictionary.Add(characters[x], new LetterLocation(x, y));
                }
            }

            return dictionary;
        }
    }
}