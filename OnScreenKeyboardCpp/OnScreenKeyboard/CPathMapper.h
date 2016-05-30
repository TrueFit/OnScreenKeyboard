#ifndef _INC_CPATHMAPPER_H
#define _INC_CPATHMAPPER_H

#include <unordered_map>

#include "GridData.h"

class CPATHMAPPER {
public:
	CPATHMAPPER(GRIDDATA *pGridData, std::wstring *pInputString, std::wstring *pDestBuffer);
	void WritePathToBuffer();

private:
	void WriteLateral(int currPos, int newPos, int extent, int maxTravel, wchar_t posChar, wchar_t negChar);
	void AppendSequenceChar(wchar_t c);

	GRIDDATA *pGridData;
	std::wstring *pInputString;
	std::wstring *pDestBuffer;
	int maxTravelRows, maxTravelCols, startRow, startCol;
};

#endif