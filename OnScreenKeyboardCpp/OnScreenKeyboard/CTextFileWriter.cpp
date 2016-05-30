#include <iostream>
#include <fstream>
#include <locale>
#include <codecvt>

#include "CTextFileWriter.h"

using namespace std;

CTEXTFILEWRITER::CTEXTFILEWRITER(const wchar_t *szFile) {
	pFile = new wofstream(szFile);
	pFile->imbue(locale(locale::empty(), new codecvt_utf8<wchar_t, 0x10ffff, consume_header>));

	if (!pFile->good())
		CloseFile();
}

CTEXTFILEWRITER::~CTEXTFILEWRITER() {
	CloseFile();
}

bool CTEXTFILEWRITER::CopyLineToFile(wstring &rLineToWrite) {
	if (pFile == nullptr)
		return false;

	*pFile << rLineToWrite << "\n";
}

void CTEXTFILEWRITER::CloseFile() {
	if (pFile) {
		delete pFile;
		pFile = nullptr;
	}
}
