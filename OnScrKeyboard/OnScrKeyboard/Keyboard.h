/**
* Filename: "Keyboard.h" 
*
* Purpose: The keyboard class creates a layout of the DVR keyboard in char arrays, it performs operations 
* to determine cursor path for given search terms.
*
* Author: Kevin Bender
*/

using namespace std;

#include<string>
#include<map>

class Keyboard_Cl
{
	
public:

	/**
	 * Constructor for the class, 
     */
	Keyboard_Cl();

	/**
	 * Destructor for the class, nothing to do...
	 */
	~Keyboard_Cl();

	/**
	 * Initializes the keyboard layout by reading from a "keyboardLayout.txt" file.
	 * returns false if initialization fails (no layout found)
     */
	bool initializeKeyboardLayout();

	/**
	 * This function takes a given search term, and returns a string representation of the path (see README.md) 
	 * to take to get that search term. Initialize keyboard layout should be called before using this function.
	 */
	string determineCursorPath(
		// The search term to check for.
		string searchTerm);

private:
	/**
	 * Defines a map which contains a character, and it's row, col location on the keyboard
	 */
	map<char, pair<int,int>> m_keyboardMap;

	/**
	 * Define a location to the cursor, we use this to walk through the path. location 
	 * is defined as row,col on the keyboard layout;
	 */
	pair<int, int> m_cursor;

	/**
	 * Helper function that gets the string representation of the path from the m_cursor 
	 * to the given character. the function moves the m_cursor to the given character location.
	 */
	string findPathToChar(
		// the character to find
		char c);
};

