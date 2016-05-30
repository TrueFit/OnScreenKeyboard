#include "CTextFileReader.h"
#include "Initialization.h"

#include <iostream>
#include <string>

using namespace std;

bool ParseCommandLineOptions(int cntArgs, wchar_t* pArgs[], COMMANDLINEOPTIONS &rOptions) {
	for (int x = 1; x < (cntArgs - 1); x += 2) {
		wstring parm(pArgs[x]);
		MakeLowerCase(parm);

		if (parm == L"-source") {
			rOptions.SourceFile.assign(pArgs[x + 1]);
		}
		else if (parm == L"-paths") {
			rOptions.PathsFile.assign(pArgs[x + 1]);
		}
		else if (parm == L"-traversal") {
			wstring traversal(pArgs[x + 1]);
			MakeLowerCase(traversal);
			
			if (traversal == L"infinite")
				rOptions.TraversalOption = GridTraversal::InfiniteGrid;
			else if (traversal == L"bounded")
				rOptions.TraversalOption = GridTraversal::BoundedGrid;
			else
				return false;
		}
		else if (parm == L"-keys") {
			rOptions.KeysFile.assign(pArgs[x + 1]);
		}
		else if (parm == L"-startchar") {
			wstring charVal(pArgs[x + 1]);
			if (charVal.size() != 1)
				return false;

			rOptions.startChar = charVal[0];
		}
		else
			return false;
	}

	return rOptions.SourceFile.size() > 0 && rOptions.PathsFile.size() > 0;
}

void ShowCommandLineOptions() {
	cout << "\n\nUsage: OnScreenKeyboard -source <Source File> -paths <Paths File>\n"
		<< "                              [-traversal <Bounded or Infinite>] [-keys <Keys File>]\n"
		<< "                              [-startchar <Character to Start At>\n\n"
		<< "     -source: Specifies the source file containing the list of strings.\n"
		<< "              This must be a text file that is UTF-8 encoded.\n\n"
		<< "      -paths: Specifies the output file into which navigation paths will be written.\n"
		<< "              This file, if it previously exists, will not be appended. It will be overwritten.\n\n"
		<< " [-traversal:] Dictates whether or not the cursor can move freely beyond the boundary of the grid\n"
		<< "               and automatically wrap-around. When in \"Infinite\" mode, a cursor on the left-most\n"
		<< "               coordinate can move left and the cursor will move to the far right. This behavior\n"
		<< "               carries over to all bounds. A \"Bounded\" grid will not permit cursor movement past the\n"
		<< "               four boundaries. (Default = Infinite)\n\n"
		<< "      [-keys:] Specifies the name of a file containing an alternate mapping of keys. This file\n"
		<< "               must contain both the mappings along with definitions for what \"alternate\"\n"
		<< "               characters should be mapped to.\n\n"
		<< " [-startchar:] Specifies the character on the grid where the cursor will start (default = A)\n";
}

bool LoadKeyMappings(const wchar_t *szFile, GRIDDATA &rGridData) {
	CTEXTFILEREADER theReader(szFile);
	int currRow = 0;
	wstring buffer;
	wstring *pBuffer = &buffer;
	wstring delimiter;
	bool isKeyMapSection = true;
	while (theReader.CopyLineToBuffer(pBuffer)) {
		int len = buffer.size();

		// validation of row length
		if (currRow) {
			if (isKeyMapSection) {
				if (len != rGridData.gridWidth) {
					cout << "Key data at row #" << (currRow + 1) << " is different from rest of data block.\n";
					return false;
				}
			}
			else if (len != 2) {
				cout << "Alternate character data at row #" << (currRow + 1) << " is invalid. (Length is " << len << ".)\n";
				return false;
			}
		}

		if (isKeyMapSection) {
			if (buffer != delimiter) {
				for (int currCol = 0; currCol < len; currCol++) {
					wchar_t c = buffer[currCol];

					if (rGridData.KeyMappings.find(c) != rGridData.KeyMappings.end()) {
						cout << "Key character " << c << " is defined multiple times.\n";
						return false;
					}

					rGridData.KeyMappings[c][0] = currRow;
					rGridData.KeyMappings[c][1] = currCol;
				}

				if (!currRow) {
					rGridData.gridWidth = len;
					delimiter.append(len, '-');
				}
			}
			else {
				rGridData.gridHeight = currRow;
				isKeyMapSection = false;
			}
		}
		else {
			if (rGridData.AlternateCharMappings.find(buffer[0]) != rGridData.AlternateCharMappings.end()) {
				wcout << "Alternate key character " << buffer[0] << " is defined multiple times.\n";
				return false;
			}

			rGridData.AlternateCharMappings[buffer[0]] = buffer[1];
		}

		currRow++;
	}

	// In case config has NO alternate character mappings (and no delimiter)
	if (isKeyMapSection)
		rGridData.gridHeight = currRow;

	return currRow > 0;
}

bool LoadStrings(const wchar_t *szFile, vector<wstring*> &rStrings) {
	CTEXTFILEREADER theReader(szFile);
	wstring *pBuffer = nullptr;
	while (theReader.CopyLineToBuffer(pBuffer)) {
		if (pBuffer->size() && pBuffer->find_first_not_of(L' ') != string::npos) {
			rStrings.push_back(pBuffer);
		}
		else
			delete pBuffer;

		pBuffer = nullptr;
	}

	return rStrings.size() > 0;
}

void LoadDefaultKeyMappings(GRIDDATA &rGridData) {
	rGridData.KeyMappings.clear();
	rGridData.AlternateCharMappings.clear();

	rGridData.KeyMappings[L'A'][0] = 0; rGridData.KeyMappings[L'A'][1] = 0;
	rGridData.KeyMappings[L'B'][0] = 0; rGridData.KeyMappings[L'B'][1] = 1;
	rGridData.KeyMappings[L'C'][0] = 0; rGridData.KeyMappings[L'C'][1] = 2;
	rGridData.KeyMappings[L'D'][0] = 0; rGridData.KeyMappings[L'D'][1] = 3;
	rGridData.KeyMappings[L'E'][0] = 0; rGridData.KeyMappings[L'E'][1] = 4;
	rGridData.KeyMappings[L'F'][0] = 0; rGridData.KeyMappings[L'F'][1] = 5;

	rGridData.KeyMappings[L'G'][0] = 1; rGridData.KeyMappings[L'G'][1] = 0;
	rGridData.KeyMappings[L'H'][0] = 1; rGridData.KeyMappings[L'H'][1] = 1;
	rGridData.KeyMappings[L'I'][0] = 1; rGridData.KeyMappings[L'I'][1] = 2;
	rGridData.KeyMappings[L'J'][0] = 1; rGridData.KeyMappings[L'J'][1] = 3;
	rGridData.KeyMappings[L'K'][0] = 1; rGridData.KeyMappings[L'K'][1] = 4;
	rGridData.KeyMappings[L'L'][0] = 1; rGridData.KeyMappings[L'L'][1] = 5;

	rGridData.KeyMappings[L'M'][0] = 2; rGridData.KeyMappings[L'M'][1] = 0;
	rGridData.KeyMappings[L'N'][0] = 2; rGridData.KeyMappings[L'N'][1] = 1;
	rGridData.KeyMappings[L'O'][0] = 2; rGridData.KeyMappings[L'O'][1] = 2;
	rGridData.KeyMappings[L'P'][0] = 2; rGridData.KeyMappings[L'P'][1] = 3;
	rGridData.KeyMappings[L'Q'][0] = 2; rGridData.KeyMappings[L'Q'][1] = 4;
	rGridData.KeyMappings[L'R'][0] = 2; rGridData.KeyMappings[L'R'][1] = 5;

	rGridData.KeyMappings[L'S'][0] = 3; rGridData.KeyMappings[L'S'][1] = 0;
	rGridData.KeyMappings[L'T'][0] = 3; rGridData.KeyMappings[L'T'][1] = 1;
	rGridData.KeyMappings[L'U'][0] = 3; rGridData.KeyMappings[L'U'][1] = 2;
	rGridData.KeyMappings[L'V'][0] = 3; rGridData.KeyMappings[L'V'][1] = 3;
	rGridData.KeyMappings[L'W'][0] = 3; rGridData.KeyMappings[L'W'][1] = 4;
	rGridData.KeyMappings[L'X'][0] = 3; rGridData.KeyMappings[L'X'][1] = 5;

	rGridData.KeyMappings[L'Y'][0] = 4; rGridData.KeyMappings[L'Y'][1] = 0;
	rGridData.KeyMappings[L'Z'][0] = 4; rGridData.KeyMappings[L'Z'][1] = 1;
	rGridData.KeyMappings[L'1'][0] = 4; rGridData.KeyMappings[L'1'][1] = 2;
	rGridData.KeyMappings[L'2'][0] = 4; rGridData.KeyMappings[L'2'][1] = 3;
	rGridData.KeyMappings[L'3'][0] = 4; rGridData.KeyMappings[L'3'][1] = 4;
	rGridData.KeyMappings[L'4'][0] = 4; rGridData.KeyMappings[L'4'][1] = 5;

	rGridData.KeyMappings[L'5'][0] = 5; rGridData.KeyMappings[L'5'][1] = 0;
	rGridData.KeyMappings[L'6'][0] = 5; rGridData.KeyMappings[L'6'][1] = 1;
	rGridData.KeyMappings[L'7'][0] = 5; rGridData.KeyMappings[L'7'][1] = 2;
	rGridData.KeyMappings[L'8'][0] = 5; rGridData.KeyMappings[L'8'][1] = 3;
	rGridData.KeyMappings[L'9'][0] = 5; rGridData.KeyMappings[L'9'][1] = 4;
	rGridData.KeyMappings[L'0'][0] = 5; rGridData.KeyMappings[L'0'][1] = 5;

	rGridData.AlternateCharMappings[L'a'] = L'A';
	rGridData.AlternateCharMappings[L'b'] = L'B';
	rGridData.AlternateCharMappings[L'c'] = L'C';
	rGridData.AlternateCharMappings[L'd'] = L'D';
	rGridData.AlternateCharMappings[L'e'] = L'E';
	rGridData.AlternateCharMappings[L'f'] = L'F';
	rGridData.AlternateCharMappings[L'g'] = L'G';
	rGridData.AlternateCharMappings[L'h'] = L'H';
	rGridData.AlternateCharMappings[L'i'] = L'I';
	rGridData.AlternateCharMappings[L'j'] = L'J';
	rGridData.AlternateCharMappings[L'k'] = L'K';
	rGridData.AlternateCharMappings[L'l'] = L'L';
	rGridData.AlternateCharMappings[L'm'] = L'M';
	rGridData.AlternateCharMappings[L'n'] = L'N';
	rGridData.AlternateCharMappings[L'o'] = L'O';
	rGridData.AlternateCharMappings[L'p'] = L'P';
	rGridData.AlternateCharMappings[L'q'] = L'Q';
	rGridData.AlternateCharMappings[L'r'] = L'R';
	rGridData.AlternateCharMappings[L's'] = L'S';
	rGridData.AlternateCharMappings[L't'] = L'T';
	rGridData.AlternateCharMappings[L'u'] = L'U';
	rGridData.AlternateCharMappings[L'v'] = L'V';
	rGridData.AlternateCharMappings[L'w'] = L'W';
	rGridData.AlternateCharMappings[L'x'] = L'X';
	rGridData.AlternateCharMappings[L'y'] = L'Y';
	rGridData.AlternateCharMappings[L'z'] = L'Z';

	rGridData.gridWidth = rGridData.gridHeight = 6;
}

void MakeLowerCase(wstring &rOriginal) {
	int len = rOriginal.size();
	for (int x = 0; x < len; x++) {
		wchar_t c = rOriginal[x];

		if (c >= 'A' && c <= 'Z')
			rOriginal[x] = 'a' + c % 'A';
	}
}

void MakeUpperCase(wstring &rOriginal) {
	int len = rOriginal.size();
	for (int x = 0; x < len; x++) {
		wchar_t c = rOriginal[x];

		if (c >= 'a' && c <= 'z')
			rOriginal[x] = 'A' + c % 'a';
	}
}