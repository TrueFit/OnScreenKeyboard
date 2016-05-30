#ifndef _DEF_CTEXTFILEREADER_H
#define _DEF_CTEXTFILEREADER_H

#include <fstream>

class CTEXTFILEREADER {
public:
	CTEXTFILEREADER(const wchar_t *szFile);
	~CTEXTFILEREADER();

	bool CopyLineToBuffer(std::wstring *&rpBuffer);

private:
	void CloseFile();

	std::wifstream *pFile;
};

#endif