#ifndef _DEF_GRIDDATA_H
#define _DEF_GRIDDATA_H

#include <unordered_map>

enum GridTraversal {
	BoundedGrid = 1,
	InfiniteGrid = 2
};

struct GRIDDATA {
	int gridWidth;
	int gridHeight;
	GridTraversal traversalOption;
	wchar_t startingChar;
	std::unordered_map<wchar_t, int[2]> KeyMappings;
	std::unordered_map<wchar_t, wchar_t> AlternateCharMappings;
};

#endif