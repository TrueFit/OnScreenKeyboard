#include "CPathMapper.h"

#include <string>
#include <iostream>

using namespace std;

CPATHMAPPER::CPATHMAPPER(GRIDDATA *pGridData, wstring *pInputString, wstring *pDestBuffer) {
	this->pGridData = pGridData;
	this->pInputString = pInputString;
	this->pDestBuffer = pDestBuffer;
	
	maxTravelCols = (pGridData->traversalOption == GridTraversal::InfiniteGrid
		? pGridData->gridWidth / 2 + pGridData->gridWidth % 2
		: pGridData->gridWidth);
	maxTravelRows = (pGridData->traversalOption == GridTraversal::InfiniteGrid
		? pGridData->gridHeight / 2 + pGridData->gridHeight % 2
		: pGridData->gridHeight);

	auto startCoords = pGridData->KeyMappings.find(pGridData->startingChar);
	if (startCoords == pGridData->KeyMappings.end()) {
		auto altChar = pGridData->AlternateCharMappings.find(pGridData->startingChar);
		if (altChar != pGridData->AlternateCharMappings.end()) {
			startCoords = pGridData->KeyMappings.find(altChar->second);
		}
	}

	if (startCoords != pGridData->KeyMappings.end()) {
		startRow = startCoords->second[0];
		startCol = startCoords->second[1];
	}
	else
		startRow = startCol = 0;
}

void CPATHMAPPER::WritePathToBuffer() {
	pDestBuffer->clear();

	int row = startRow, col = startCol;
	for (wchar_t c : *pInputString) {
		if (c == L' ') {
			AppendSequenceChar(L'S');
			continue;
		}

		auto mapIter = pGridData->KeyMappings.find(c);
		if (mapIter == pGridData->KeyMappings.end()) {
			auto alternateIter = pGridData->AlternateCharMappings.find(c);
			if (alternateIter == pGridData->AlternateCharMappings.end()) {
				AppendSequenceChar(L'X');
				continue;
			}

			c = alternateIter->second;
			mapIter = pGridData->KeyMappings.find(c);

			if (mapIter == pGridData->KeyMappings.end()) {
				AppendSequenceChar('X');
				continue;
			}
		}

		int newRow = mapIter->second[0];
		int newCol = mapIter->second[1];

		WriteLateral(row, newRow, pGridData->gridHeight, maxTravelRows, L'D', L'U');
		WriteLateral(col, newCol, pGridData->gridWidth, maxTravelCols, L'R', L'L');
		AppendSequenceChar(L'#');

		row = newRow;
		col = newCol;
	}
}

void CPATHMAPPER::WriteLateral(int currPos, int newPos, int extent, int maxTravel, wchar_t posChar, wchar_t negChar) {
	int diff = newPos - currPos;
	int absDiff = abs(diff);

	if (absDiff > maxTravel) {
		diff = (extent - absDiff) * (diff > 0 ? -1 : 1);
		absDiff = abs(diff);
	}

	for (int y = 0; y < absDiff; y++)
		AppendSequenceChar(diff > 0 ? posChar : negChar);
}

void CPATHMAPPER::AppendSequenceChar(wchar_t c) {
	if (pDestBuffer->size())
		pDestBuffer->append(1, L',');

	pDestBuffer->append(1, c);
}