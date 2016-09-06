/**
* Filename: "FileUtility.cpp"
*
* Purpose: Implement the functions defined in  "FileUtility.h"
*
* Author: Kevin Bender
*/


#include "FileUtility.h"

queue<std::string> FileUtility_Cl::readLinesFromFile(string filePath)
{
		// create a keyboard to use for our search terms.
		

		// the queue of strings read from the file...
		queue<string> lines;
		ifstream infile;

		infile.open(filePath);

		while (infile.good()) // To get you all the lines.
		{
			// get the line from the file, and add it into the Q.
			string result;
			getline(infile, result);
			// add the line into the Q
			lines.push(result);
		}

		infile.close();

		// check if we read anything, if we didn't log a warning, but continue on anyway
		if (lines.empty())
		{
			cout << "No data found in file " + filePath + "\n";
		}

		return lines;
}
