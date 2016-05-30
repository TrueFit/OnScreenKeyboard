#include <iostream>
#include <ctime>

#include "CTextFileReader.h"
#include "CTextFileWriter.h"
#include "CPathMapper.h"
#include "Initialization.h"

using namespace std;

int wmain(int argc, wchar_t* argv[]) {
	cout << "Running\n";

	COMMANDLINEOPTIONS options;
	options.TraversalOption = GridTraversal::InfiniteGrid;
	options.startChar = L'A';
	if (!ParseCommandLineOptions(argc, argv, options)) {
		ShowCommandLineOptions();
		return 0;
	}

	clock_t tracker = clock();

	int entryCnt = 0;
	for (long long x = 0; x < 1LL; x++) {
		GRIDDATA gridData;
		gridData.traversalOption = options.TraversalOption;
		gridData.startingChar = options.startChar;
		if (options.KeysFile.size()) {
			if (!LoadKeyMappings(options.KeysFile.c_str(), gridData))
			{
				cout << "Failed to load keyboard data from file. Use default keys pattern?\n\n"
					<< "  ABCDEF\n"
					<< "  GHIJKL\n"
					<< "  MNOPQR\n"
					<< "  STUVWX\n"
					<< "  YZ1234\n"
					<< "  567890\n";

				wstring useDefault(L"");
				do {
					cout << "\n(y/n) ";
					wcin >> useDefault;
					MakeLowerCase(useDefault);
				} while (useDefault != L"y" && useDefault != L"n");

				if (useDefault == L"n")
					return 1;

				cout << "Using default keyboard mapping.\n";

				LoadDefaultKeyMappings(gridData);
			}
		}
		else
			LoadDefaultKeyMappings(gridData);

		vector<wstring*> strings;
		if (!LoadStrings(options.SourceFile.c_str(), strings)) {
			cout << "Failed to load file containing strings to process at " << "C:\\Git\\KeyboardData\\Strings.txt" << "\n";
			return 2;
		}

		wstring path;

		CTEXTFILEWRITER theFileWriter(options.PathsFile.c_str());

		for (wstring* ps : strings) {
			++entryCnt;
			int row = 0, col = 0;

			CPATHMAPPER theMapper(
				&gridData,
				ps,
				&path);

			theMapper.WritePathToBuffer();

			theFileWriter.CopyLineToFile(path);

			if (!(entryCnt % 100000) || !(entryCnt % 99544))
				wcout << L"Path for " << *ps << L" at row " << entryCnt << ": " << path << L"\n";
		}

		while (strings.size()) {
			delete strings[strings.size() - 1];
			strings.pop_back();
		}
	}

	tracker = clock() - tracker;

	long totalSecs = (tracker / 1000L);

	cout << "\nTotal running seconds: " << totalSecs << "\n";
	cout << "Average records per second: " << (totalSecs > 0 ? entryCnt / totalSecs : entryCnt) << "\n";

	return 0;
}