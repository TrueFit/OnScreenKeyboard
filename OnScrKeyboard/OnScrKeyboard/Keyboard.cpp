/**
* Filename: "Keyboard.cpp"
*
* Purpose: Implement the functions defined in  "Keyboard.h"
*
* Author: Kevin Bender
*/

using namespace std;
#include "Keyboard.h"
#include "FileUtility.h"
#include <queue>


Keyboard_Cl::Keyboard_Cl()
{
	// create the map
}


Keyboard_Cl::~Keyboard_Cl()
{
	// destroy the map.
}


bool Keyboard_Cl::initializeKeyboardLayout()
{
	// success of the operation
	bool returnStatus = false;

	// read a file and set the keyboard layout based on this file
	// currently hardcoded to "keyboardLayout.txt"
	queue<string> keyboardRows = FileUtility_Cl::readLinesFromFile("keyboardLayout.txt");

	if (keyboardRows.empty())
	{
		// log, as this is going to be bad
		cout << "NO KEYBOARD LAYOUT FOUND! \n";
	}
	else
	{
		int row = 0;

		// create the keyboard
		while (!keyboardRows.empty())
		{
			string keyboardRow = keyboardRows.front();
			
			// insert each character into the map at its proper row 
			// and col location.
			for (int col = 0; col < keyboardRow.length(); col++)
			{
				pair<int, int> coords(row, col);
				char myKey = toupper(keyboardRow.at(col));
				m_keyboardMap[myKey] = coords;
			}
			
			keyboardRows.pop();
			row++;
		}

		// assume we have a good layout if it's not empty.
		returnStatus = true;
	}

	return returnStatus;
}


string Keyboard_Cl::determineCursorPath(string searchTerm)
{
	// hold the result here
	string result = searchTerm + ":";

	// set the location of the cursor to 0,0 (or "A" in the default case")
	m_cursor.first = 0;
	m_cursor.second = 0;

	// iterate through the characters to find the path from the cursor to each character
	for (int i = 0; i < searchTerm.length(); i++)
	{
		char c = searchTerm.at(i);

		// check if the character is a space
		if (c == ' ')
		{
			// add a S to the result
			result += "S,";
		}
		else
		{
			// it is a character on the keyboard find the path to it
			result += findPathToChar(c);
		}
	}

	// add new line at the end and return
	result += "\n";

	return result;
}


string Keyboard_Cl::findPathToChar(char c)
{
	string result =  "";

	// check to see where the character is located on the keyboard.
	// if we can't find it, ignore that character.
	if (m_keyboardMap.find(toupper(c)) != m_keyboardMap.end())
	{
		pair<int,int> coords = m_keyboardMap.find(toupper(c))->second;

		// we have coordinates... figure out how to get from cursor to the current location.
		// find the difference between our current row, and the character row
		int rowDiff = m_cursor.first - coords.first;
		// find the difference between our current column and the character column
		int colDiff = m_cursor.second - coords.second;

		// iterate until we match
		while (rowDiff != 0 || colDiff != 0)
		{
			// check the row.
			if (rowDiff > 0)
			{
				// Our cursor is below the intended character, we need to go up
				result += "U,";
				m_cursor.first--;
			}
			else if (rowDiff < 0)
			{
				// our cursor is above the intended character, we need to go down.
				result += "D,";
				m_cursor.first++;
			}

			if (colDiff > 0)
			{
				// our cursor is to the right of the intended character, we need to go left
				result += "L,";
				m_cursor.second--;
			}
			else if (colDiff < 0)
			{
				// our cursor is to the left of the intended character, we 
				// need to go right
				result += "R,";
				m_cursor.second++;
			}

			//update the differences.
			rowDiff = m_cursor.first - coords.first;
			colDiff = m_cursor.second - coords.second;
		}
		
		// once we match, we can select that character
		result += "#,";
	}

	return result;

}