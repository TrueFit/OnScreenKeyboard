#ifndef _DEF_CTEXTFILEWRITER_H
#define _DEF_CTEXTFILEWRITER_H

#include <string>

class CTEXTFILEWRITER {
public:
	CTEXTFILEWRITER(const wchar_t *szFile);
	~CTEXTFILEWRITER();

	bool CopyLineToFile(std::wstring &rLineToWrite);

private:
	void CloseFile();

	std::wofstream *pFile;
};

#endif