#ifndef _INC_INITIALIZATION_H
#define _INC_INITIALIZATION_H

#include <vector>
#include <string>
#include "GridData.h"

struct COMMANDLINEOPTIONS {
	std::wstring SourceFile;
	std::wstring PathsFile;
	std::wstring KeysFile;
	GridTraversal TraversalOption;
	wchar_t startChar;
};

bool ParseCommandLineOptions(int cntArgs, wchar_t* pArgs[], COMMANDLINEOPTIONS &rOptions);
void ShowCommandLineOptions();
bool LoadKeyMappings(const wchar_t *szFile, GRIDDATA &rGridData);
bool LoadStrings(const wchar_t *szFile, std::vector<std::wstring*> &rStrings);
void LoadDefaultKeyMappings(GRIDDATA &rGridData);
void MakeLowerCase(std::wstring &rOriginal);

#endif