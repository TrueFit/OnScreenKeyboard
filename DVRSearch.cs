using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
public class DVRSearch
{
    //fileRead "C:\\wwwroot\\DVR\\Text\\DVR.txt"
    //fileWrite "C:\\wwwroot\\DVR\\Text\\DVROutput.txt"
    public void getInput(string inputFilePath, string outputFilePath)
    {
        if (File.Exists(inputFilePath))
        {
            //get text from DVR.txt File
            string dvrFileText = File.ReadAllText(inputFilePath);
            
            //step through each letter and space
            string dvrOutput = getDVRInput(dvrFileText.ToUpper());

            //U D L R S # to Output file
            writeToFile(outputFilePath, dvrOutput);
        }
    }

    private string getDVRInput(string fileText)
    {
        string dvrOutput = "";

        const short totalRows = 6; //TR
        int curLocation = 0; //CL
        int nextLocation = 0; //NL
        int curRow = 0; //CR
        int quotient = 0; //DD
        int numberUpDown = 0; //NUD (- UP + DOWN)
        int numberLeftRight = 0; //NLR (- LEFT + RIGHT)
        int newCurrent = 0; //NC  CL + (NUD + TR)

        char[] alphaNum = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O'
                                        ,'P','Q','R','S','T','U','V','W','X','Y','Z','1','2','3','4','5','6','7'
                                        ,'8','9','0'};
        ArrayList list = new ArrayList(alphaNum);
        
        foreach (char letter in fileText)
        {
            if (letter != ' ')
            {
                nextLocation = Convert.ToInt32(list.IndexOf(letter));

                //moving up '-' and down '+'
                quotient = Convert.ToInt32(Math.Floor(Convert.ToDecimal(nextLocation / totalRows)));
                numberUpDown = quotient - curRow;
                if (numberUpDown > 0)
                {
                    for (int d = 0; d < numberUpDown; d++)
                    {
                        dvrOutput += "D,";
                    }
                }
                else if (numberUpDown < 0)
                {
                    for (int u = 0; u > numberUpDown; u--)
                    {
                        dvrOutput += "U,";
                    }
                }

                //moving left '-' and right '+'

                newCurrent = curLocation + (numberUpDown * totalRows);
                numberLeftRight = nextLocation - newCurrent;

                if (numberLeftRight > 0)
                {
                    for (int r = 0; r < numberLeftRight; r++)
                    {
                        dvrOutput += "R,";
                    }
                }
                else if (numberLeftRight < 0)
                {
                    for (int l = 0; l > numberLeftRight; l--)
                    {
                        dvrOutput += "L,";
                    }
                }

            }
            else
                dvrOutput += "S,";

            
            dvrOutput += "#,";
            
            //updating new location of the cursor
            curRow = quotient;
            curLocation = nextLocation;
        }
        dvrOutput = dvrOutput.TrimEnd(',');

        return dvrOutput;
    }

    private void writeToFile(string outputPath, string dvrOutput)
    {
        //write to DVR Output
        if (File.Exists(outputPath))
        {
            File.WriteAllText(outputPath, dvrOutput);
        }
    }
}
