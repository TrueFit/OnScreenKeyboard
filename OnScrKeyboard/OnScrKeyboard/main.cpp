/**
* Filename: "main.cpp"
*	
* Purpose: The on screen keyboard application reads search terms from a user specified input file, and returns the 
* cursor path for each input term on the console.
* 
* Author: kbender
*/
#include "stdafx.h"
#include <iostream>
#include <string>
#include "FileUtility.h"
#include "Keyboard.h"

using namespace std;

int main()
{
	// Build the keyboard layout.
	// Hold the user supplied file path.
	Keyboard_Cl myKeyboard;

	// initialize it, 
	if (!myKeyboard.initializeKeyboardLayout())
	{
		// problem occured, log and exit
		cout << "Problem reading keyboard layout, press enter key to exit...";
	    cin.get();

		return -1;
	}
	else
	{
		cout << "Got keyboard layout... \n";
	}

	string filePath = " ";

	while (filePath != "exit")
	{
		// request the input file from the user
		cout << "Input file path to read, or type \"exit\": ";

		cin >> filePath;
		cout << "ReadFile " + filePath + "\n";

		if (filePath != "exit")
		{
			// get a que of search terms from the file.
			queue<string> searchTerms = FileUtility_Cl::readLinesFromFile(filePath);

			// process each term
			while (!searchTerms.empty())
			{
				cout << myKeyboard.determineCursorPath(searchTerms.front()) + "\n" << flush;
				searchTerms.pop();
			}
		}
	}

	return 0;
}

