/**
* Filename: "FileUtility.h"
* 
* Purpose: This class is a static helper class for reading text files.
*
* Author: Kevin Bender
*/

#include <iostream>
#include <fstream>
#include <string>
#include <queue>

using namespace std;

class FileUtility_Cl
{
	public :

		/**
		* This function takes a file path string. It reads each line from the file and 
		* adds it to a queue of strings to be processed.
		*/
		static queue<std::string> readLinesFromFile(string filePath);
};

