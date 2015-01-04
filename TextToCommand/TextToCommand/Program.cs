using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboardTruefit
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call VoiceToTextDVR.InputFlatDirectory(" Your file path to a .txt flat file ") to get results.
            VoiceToTextDVR.InputFlatFileDirectory("c:\\Users\\Nick\\test.txt");
        }

    }


    /// <summary>
    /// Voice to text search for DVR
    /// </summary>
    public static class VoiceToTextDVR
    {

        /// <summary>
        /// You'll want to input a .txt directory of that has a search item on each line.
        /// This program will rewrite the .txt file, adding
        /// the DVR Commands to the right of the search item. 
        /// </summary>
        /// <param name="FilePath">"c:\\Users\\Nick\\test.txt"</param>
        public static void InputFlatFileDirectory(string FilePath)
        {
            string extension = Path.GetExtension(FilePath);
            if (extension == ".txt")
            {
                var text = new StringBuilder();

                foreach (string s in File.ReadAllLines(FilePath))
                {
                    if (s.Length != 0)
                    {
                        string command = TextToCommand(s);
                        text.AppendLine(s.Replace(s, s + " : " + command));
                    }
                }

                using (var file = new StreamWriter(File.Create(FilePath)))
                {
                    file.Write(text.ToString());
                }
            }
            else
            {
                Console.WriteLine("Make sure your path to your .txt document is correct.");
                Console.Read();
            }
        }


        /// <summary>
        /// This takes a search item for the DVR and returns a list of commands for the DVR remote to execute.
        /// </summary>
        /// <param name="input">"IT Crowd"</param>
        /// <returns>D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#</returns>
        private static string TextToCommand(string input)
        {
            string[,] remote = new string[6, 6]{
                                                {"A","B","C","D","E","F"},
                                                {"G","H","I","J","K","L"},
                                                {"M","N","O","P","Q","R"},
                                                {"S","T","U","V","W","X"},
                                                {"Y","Z","1","2","3","4"},
                                                {"5","6","7","8","9","0"}};

            int NextButtonC;
            int NextButtonR;
            int CurrButtonC = 0;
            int CurrButtonR = 0;
            List<string> Commands = new List<string>();

            if (input.All(c => Char.IsLetterOrDigit(c) || c == ' '))
            {
                foreach (char c in input.ToUpper())
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        NextButtonR = Convert.ToInt16(CoordinatesOf<string>(remote, c.ToString()).Item1.ToString());
                        NextButtonC = Convert.ToInt16(CoordinatesOf<string>(remote, c.ToString()).Item2.ToString());

                        int UpDown = NextButtonR - CurrButtonR;
                        int LeftRight = NextButtonC - CurrButtonC;

                        if (UpDown > 0)
                        {
                            for (int i = 0; i < UpDown; i++)
                            {
                                Commands.Add("D");
                            }
                        }
                        if (UpDown < 0)
                        {
                            for (int i = -1; i >= UpDown; i--)
                            {
                                Commands.Add("U");
                            }
                        }
                        if (LeftRight > 0)
                        {
                            for (int i = 0; i < LeftRight; i++)
                            {
                                Commands.Add("R");
                            }
                        }
                        if (LeftRight < 0)
                        {
                            for (int i = -1; i >= LeftRight; i--)
                            {
                                Commands.Add("L");
                            }
                        }

                        Commands.Add("#");
                        CurrButtonC = NextButtonC;
                        CurrButtonR = NextButtonR;
                    }
                    else
                    {
                        Commands.Add("S");
                    }
                }


                return String.Join(", ", Commands.ToArray());
            }
            else
            {
                return "Only Numbers and Letters are allowed in the search.";
            }
        }
        /// <summary>
        /// This gets the coordinates of the searched item in the multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }
    }

}
