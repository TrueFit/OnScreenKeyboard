#include <iostream>
#include <fstream>
#include <strsafe.h>
#include <locale>
#include <codecvt>

#include "CTextFileReader.h"

using namespace std;

CTEXTFILEREADER::CTEXTFILEREADER(const wchar_t *szFile) {
	pFile = new wifstream(szFile);
	pFile->imbue(locale(locale::empty(), new codecvt_utf8<wchar_t, 0x10ffff, consume_header>));

	if (!pFile->good())
		CloseFile();
}

CTEXTFILEREADER::~CTEXTFILEREADER() {
	CloseFile();
}

bool CTEXTFILEREADER::CopyLineToBuffer(wstring *&rpBuffer) {
	if (pFile && pFile->is_open()) {
		wchar_t szBuffer[16384];
		pFile->getline(szBuffer, sizeof(szBuffer) / sizeof(wchar_t));

		if (rpBuffer == nullptr) {
			rpBuffer = new wstring(szBuffer);
		}
		else
			rpBuffer->assign(szBuffer);

		bool fileGood = pFile->good();
		if (!fileGood)
			CloseFile();

		return fileGood || szBuffer[0];
	}
	else if (rpBuffer != nullptr) {
		rpBuffer->assign(L"");
	}

	return false;
}

void CTEXTFILEREADER::CloseFile() {
	if (pFile) {
		delete pFile;
		pFile = nullptr;
	}
}