/// This code was written on February 19, 2015 by Zachary Shelhamer.
/// It's purpose is to demonstrate coding ability to TrueFit Solutions
/// Email: zshelhamer@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace OnScreenKeyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create a dictionary of DVRChar objects, with each object using it's character as it's key
        Dictionary<char, DVRChar> characterDictionary = new Dictionary<char, DVRChar>()
        {
            {'A', new DVRChar(0, 0, 'A')},
            {'B', new DVRChar(0, 1, 'B')},
            {'C', new DVRChar(0, 2, 'C')},
            {'D', new DVRChar(0, 3, 'D')},
            {'E', new DVRChar(0, 4, 'E')},
            {'F', new DVRChar(0, 5, 'F')},
            {'G', new DVRChar(1, 0, 'G')},
            {'H', new DVRChar(1, 1, 'H')},
            {'I', new DVRChar(1, 2, 'I')},
            {'J', new DVRChar(1, 3, 'J')},
            {'K', new DVRChar(1, 4, 'K')},
            {'L', new DVRChar(1, 5, 'L')},
            {'M', new DVRChar(2, 0, 'M')},
            {'N', new DVRChar(2, 1, 'N')},
            {'O', new DVRChar(2, 2, 'O')},
            {'P', new DVRChar(2, 3, 'P')},
            {'Q', new DVRChar(2, 4, 'Q')},
            {'R', new DVRChar(2, 5, 'R')},
            {'S', new DVRChar(3, 0, 'S')},
            {'T', new DVRChar(3, 1, 'T')},
            {'U', new DVRChar(3, 2, 'U')},
            {'V', new DVRChar(3, 3, 'V')},
            {'W', new DVRChar(3, 4, 'W')},
            {'X', new DVRChar(3, 5, 'X')},
            {'Y', new DVRChar(4, 0, 'Y')},
            {'Z', new DVRChar(4, 1, 'Z')},
            {'1', new DVRChar(4, 2, '1')},
            {'2', new DVRChar(4, 3, '2')},
            {'3', new DVRChar(4, 4, '3')},
            {'4', new DVRChar(4, 5, '4')},
            {'5', new DVRChar(5, 0, '5')},
            {'6', new DVRChar(5, 1, '6')},
            {'7', new DVRChar(5, 2, '7')},
            {'8', new DVRChar(5, 3, '8')},
            {'9', new DVRChar(5, 4, '9')},
            {'0', new DVRChar(5, 5, '0')}
        };

        // Characters that are not on our DVR Characters List that we do not want to process normally
        List<char> ignoreList = new List<char> { ',', '.', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')',
                                                '+', '=', '-', '_', '<', '>', '`', '~', ';', '\'', '\"', '/',
                                                '?', '\\', '[', '{', ']', '}', ':', '|', ' ', '\n'};

        Stream myStream = null;
        OpenFileDialog selectedFile = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method prompts the user to select a file containing search term. It then parses
        /// the file and determines the appropriate DVR key stroke combination to correctly search
        /// for each term. I decided to use a button instead of launching directly from MainWindow() 
        /// because I did not the user to be immediately prompted to select a file whenever this
        /// was initialized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onScreenKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            String title;
            char[] titleArray;
            char cursorCharacter = 'A';
            int cursorRow = 0;
            int cursorColumn = 0;
            DVRChar desiredCharacter;
            // I left the StreamWriter in the code to show that the results could be easily exported
            // and used at a later time. All occurances are commented out, however, as I did not want
            // to write files to your C: drive.
            //StreamWriter outputFile = new StreamWriter(@"C:\Users\Public\OnScreenKeyboardOutput.txt");
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.outputTextBlock.Width = this.Width;
            this.onScreenKeyboardButton.Visibility = Visibility.Hidden;

            selectedFile.InitialDirectory = @"C:\";
            selectedFile.Title = "Please select a file";
            if (selectedFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = selectedFile.OpenFile()) != null)
                    {
                        StreamReader reader = new StreamReader(myStream);

                        // While the file still has lines to be read
                        while ((title = reader.ReadLine()) != null)
                        {
                            // Print the title to the Main Window
                            this.outputTextBlock.Text += title + Environment.NewLine;
                            //outputFile.Write(title + Environment.NewLine);
                            titleArray = title.ToCharArray();

                            // This will go through each letter in the title to check where we need to go
                            for (int currentLetterIndex = 0; currentLetterIndex < titleArray.Length; currentLetterIndex++)
                            {
                                // If the cursor is NOT on the character we want and is not a special case character
                                if (titleArray[currentLetterIndex] != cursorCharacter && !ignoreList.Contains(titleArray[currentLetterIndex]))
                                {
                                    desiredCharacter = characterDictionary[Char.ToUpper(titleArray[currentLetterIndex])];

                                    // If the cursor is not on the character we want 
                                    if (desiredCharacter.Character != cursorCharacter)
                                    {
                                        // If the cursor is not in the correct row
                                        if (desiredCharacter.Row != cursorRow)
                                        {
                                            // The cursor is above where it needs to be.
                                            while (desiredCharacter.Row > cursorRow)
                                            {
                                                this.outputTextBlock.Text += "D,";
                                                //outputFile.Write("D,");
                                                cursorRow++;
                                            }

                                            // The cursor is below where it needs to be.
                                            while (desiredCharacter.Row < cursorRow)
                                            {
                                                this.outputTextBlock.Text += "U,";
                                                //outputFile.Write("U,");
                                                cursorRow--;
                                            }
                                        }

                                        // If the cursor is not in the correct colunm
                                        if (desiredCharacter.Column != cursorColumn)
                                        {
                                            // The cursor is to the left of where it needs to be.
                                            while (desiredCharacter.Column > cursorColumn)
                                            {
                                                this.outputTextBlock.Text += "R,";
                                                //outputFile.Write("R,");
                                                cursorColumn++;
                                            }

                                            // The cursor is to the right of where it needs to be.
                                            while (desiredCharacter.Column < cursorColumn)
                                            {
                                                this.outputTextBlock.Text += "L,";
                                                //outputFile.Write("L,");
                                                cursorColumn--;
                                            }
                                        }
                                        cursorCharacter = desiredCharacter.Character;
                                    }
                                    // Do NOT print comma if not at the end of line. Also, reset cursor after each line.
                                    if (currentLetterIndex != titleArray.Length - 1)
                                    {
                                        this.outputTextBlock.Text += "#,";
                                        //outputFile.Write("#,");
                                    }
                                    else
                                    {
                                        this.outputTextBlock.Text += "#";
                                        //outputFile.Write("#");
                                        cursorColumn = 0;
                                        cursorRow = 0;
                                        cursorCharacter = 'A';
                                    }
                                }

                                // Print "S" for Space
                                else if (Char.IsWhiteSpace(titleArray[currentLetterIndex]))
                                {
                                    this.outputTextBlock.Text += "S,";
                                    //outputFile.Write("S,");
                                }
                            }
                            this.outputTextBlock.Text += Environment.NewLine;
                            this.outputTextBlock.Text += Environment.NewLine;
                            //outputFile.Write(Environment.NewLine);
                            //outputFile.Write(Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read the file. Error Message: {0}", ex.Message);
                }
                this.Topmost = true;
            }
            else
            {
                System.Windows.MessageBox.Show("No file was selected. The application will now close.");
                this.Close();
            }
        }
    }
}
