using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringExtensions
{
    public static class Converters
    {
        #region Public String Extension Method: ToDirections
        /// <summary>
        /// Converts a string of letters and numbers into a string of directions on a 6 x 6 grid. 
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>String</returns>
        public static string ToDirections(this string s)
        {
            string result = "";
            GridLocation loc = new GridLocation() { X = 0, Y = 0 }; // x and y coordintes of the current location on the grid.

            foreach(char c in s)
            {
                if (c == 32) result += "S"; // add S character if c is a space.
                else
                {
                    int b = AlignCharToGrid(c); // align the input character to conform to our 6x6 transform grid.

                    // if the character is within acceptible parameters, translate it to grid directions.
                    if (b >= 0 && b <= 36)
                    {
                        // calculate the column and row of the character.
                        int col = (int)Math.Floor((decimal)b % 6);
                        int row = (int)Math.Floor((decimal)b / 6);

                        // calculate the distance between the current grid location and the location of our desired character.
                        int distanceX = col - loc.X, distanceY = row - loc.Y;

                        if (distanceX == 0 && distanceY == 0) result += "#";
                        else
                        {
                            result += new String((distanceX > 0) ? 'R' : 'L', Math.Abs(distanceX));
                            result += new String((distanceY > 0) ? 'D' : 'U', Math.Abs(distanceY));

                            result += "#";

                            loc.X = col;
                            loc.Y = row;
                        }

                    }
                    else throw new Exception();
                }
            }
            
            return result.Delimit(','); // delimit the result.
        }

        private class GridLocation
        {
            public int X;
            public int Y;
        }

        private static int AlignCharToGrid(int c)
        {
            int b = (c == 48) ? 100 : c;
            b = b + ((b >= 49 && b <= 57) ? 41 : 0);
            b = b - 65;

            return b;
        }
        #endregion

        #region Public String Extension Method: Delimit
        /// <summary>
        /// Insert the character specified by the delimiter at every other character location in the string, effectively alternating the contents of the string with the delimiter.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="delimiter">char</param>
        /// <returns>string</returns>
        public static string Delimit(this string s, char delimiter)
        {
            string result = "";
            int count = 0;
            foreach(char c in s)
            {
                result += c;
                if (count < s.Count()-1) result += delimiter;
                count++;
            }
            return result;
        }
        #endregion

    }
}
