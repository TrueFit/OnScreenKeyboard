using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OnScreenKeyboard
{
    public partial class MainForm : Form
    {
        // Declare the keyboard width and height (number of characters per row/column).
        private static int KEYBOARD_WIDTH = 6;
        private static int KEYBOARD_HEIGHT = 6;

        // Character array of the keys available in the OnScreenKeyboard
        private static char[] KEYS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        // Create a dictionary to hold all OnScreenKeyboard characters with their respective indices.
        private static Dictionary<char, int> KEYBOARD_DICTIONARY = new Dictionary<char, int>();


        public MainForm()
        {
            InitializeComponent();
        }

        /*
         * buttonGo_Click()
         * Summary: Go button event handler. Opens a file dialog for the user to select
         *          a file to process. Reads in the lines of the file and makes a call to
         *          ProcessLine for each line in a loop. 
         *          Appends the result of ProcessLine to the outputLabel.
         *          
         * Input:   object sender - The button that was clicked. 
         *          EventArgs e - Even arguments, if any.
         *          
         * Output:  None
         */
        private void buttonGo_Click(object sender, EventArgs e)
        {
            // Show OpenFileDialog and get the result of the file search.
            DialogResult result = openInputFileDialog.ShowDialog();

            // If locating and finding a file in the dialog is successful...
            if (result == DialogResult.OK)
            {
                try
                {
                    PopulateKeyboardDictionary();

                    // Read the lines of the input file
                    string[] inputLines = File.ReadAllLines(openInputFileDialog.FileName);

                    // For each line of the input file...
                    foreach (string line in inputLines)
                    {
                        // Process the line and append the output to the outputLabel
                        outputLabel.Text += ProcessLine(line);  
                    }   
                }
                catch (IOException ex)
                {
                    MessageBox.Show("An error occurred when opening the file: \n" + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: \n" + ex.Message);
                }
            }
        }

        /*
         * PopulateKeyboardDictionary()
         * Summary: Loops through the characters in the KEYS array and adds each one as a key
         *          to the KEYBOARD_DICTIONARY with its index in the KEYS array as its value.
         *          
         * Input:   None
         * 
         * Output:  None
         */
        private void PopulateKeyboardDictionary()
        {
            // Loop through each available character... 
            for (int i = 0; i < KEYS.Length; i++)
            {
                // Add the character to the dictionary as the key, using its index as the value.
                KEYBOARD_DICTIONARY.Add(KEYS[i], i);
            }
        }

        /*
         * GetPathCharacter()
         * Summary: Takes in a distance the cursor must travel and whether or not the cursor
         *          is travelling through rows or columns in an attempt to calculate which
         *          direction the cursor needs to be moved. Returns a character corresponding 
         *          to that direction.
         *          
         * Input:   int distanceFromCursor - the distance the target character is away from the 
         *                                   current cursor position 
         *          bool isRow - whether the cursor is moving through a row (true) or column (false)
         *          
         * Output:  char pathCharacter - the character corresponding to the direction the cursor must
         *                               travel to reach the target character.
         */
        private char GetPathCharacter(int distanceFromCursor, bool isRow)
        {
            // If the distance is positive...
            if (distanceFromCursor > 0)
            {
                // If we are moving through rows...
                if (isRow)
                {
                    // We must move down the rows of the grid; return D
                    return 'D';
                }
                else
                {
                    // We must move right across the columns of the grid; return R
                    return 'R';
                }
            }
            else
            {
                // If we are moving through rows...
                if (isRow)
                {
                    // We must move up the rows of the grid; return U
                    return 'U';
                }
                else
                {
                    // We must move left across the columns; return L
                    return 'L';
                }
            }
        }

        /*
         * AddPathCharacters()
         * Summary: Adds the character returned from GetPathCharacter to the pathList once for each time
         *          the keyboard cursor must be moved in that direction to reach the target character.
         *          
         * Input:   List<char> pathList - the list of movements the cursor would need to make to type the input string
         *          int distanceFromCursor - the distance the target character is away from the 
         *                                   current cursor position 
         *          bool isRowChange - whether the cursor is moving through a row (true) or column (false)
         *          
         * Output:  None
         */
        private void AddPathCharacters(List<char> pathList, int distanceFromCursor, bool isRowChange)
        {
            // Get the path character necessary to move the cursor
            char pathCharacter = GetPathCharacter(distanceFromCursor, isRowChange);

            // Add the pathCharacter to the path repeatedly until the cursor would be in the correct row or column.
            for (int i = 0; i < Math.Abs(distanceFromCursor); i++)
            {
                pathList.Add(pathCharacter);
            }
        }

        /*
         * ProcessLine()
         * Summary: Takes a string, which is a line of an input file to be processed. Breaks the string
         *          into its individual characters and then calculates how an on-screen keyboard cursor
         *          would need to move in order to enter that input string.
         *          
         * Input:   string line - a line of text from the input file
         * 
         * Output:  string - returns the original input along with the scripted path that enters that input
         *                   on an on-screen keyboard. If an invalid character is encountered during processing,
         *                   a string detailing the invalid character is returned instead.
         */
        private string ProcessLine(string line)
        {
            // Coordinates of the cursor in the keyboard grid.
            //  Initialize to top-left corner (0, 0), or A
            Tuple<int, int> currentCursorPosition = new Tuple<int, int>(0, 0);

            // Capitalize all characters in the array to prevent casing issues
            //  then convert the string into a character array for processing.
            char[] lineArray = line.ToUpper().ToCharArray();

            int charIndex;                              // Index of a given character in the 1D array of available characters
            int targetColumnIndex;                      // The column of the keyboard grid the target character is in
            int targetRowIndex;                         // The row of the keyboard grid the target character is in
            int distanceFromCursor;                     // The distance of the target character, in relation to the current cursor position.
            List<char> pathList = new List<char>();     // List of all individual cursor movements that make up the path.

            // For each character in this line of input...
            foreach (char character in lineArray)
            {
                // If the character is a space... 
                if (character == ' ')
                {
                    // Add an S to the path
                    pathList.Add('S');
                }
                else
                {
                    // Try to get the position of the current letter from the dictionary...
                    if (!KEYBOARD_DICTIONARY.TryGetValue(character, out charIndex))
                    {
                        // If an invalid character is encountered, output an error string and stop processing the line.
                        return line + "\nInvalid character ('" + character + "'). Skipping.\n\n";
                    }

                    // index of target row = index of character among all options divided by number of rows in keyboard grid
                    targetRowIndex = charIndex / KEYBOARD_HEIGHT;
                    // distance to target row = index of target row in grid - current cusor position (y)
                    distanceFromCursor = targetRowIndex - currentCursorPosition.Item2;

                    // Add the path characters necessary to move the cursor through the rows.
                    AddPathCharacters(pathList, distanceFromCursor, true);
                   
                    // index of target column = index of character among all options mod number of columns in keyboard grid
                    targetColumnIndex = charIndex % KEYBOARD_WIDTH;
                    // distance to target column = index of target column in grid - current cusor position (x)
                    distanceFromCursor = targetColumnIndex - currentCursorPosition.Item1;

                    // Add the path characters necessary to move the cursor through the columns.
                    AddPathCharacters(pathList, distanceFromCursor, false);

                    // After adding path characters to reach the correct coordinate, 
                    //  add a # to the path to indicate character selection
                    pathList.Add('#');

                    // Update the current cursor position to be the target character's coordinates
                    currentCursorPosition = new Tuple<int, int>(targetColumnIndex, targetRowIndex);
                }
            }

            // Return the original input with its scripted path 
            return line + "\n" + string.Join(",", pathList.ToArray()) + "\n\n";
        }
    }
}
