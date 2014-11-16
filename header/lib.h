#ifndef LIB_H
#define LIB_H

/// <summary>
/// Contains a series of functions as part of a library which is used
/// by the driver program to accomplish the program goal. Some of the
/// functions in this library includes file reading, parsing, and 
/// data structures.
/// </summary>
///
/// <author>Oliver Spryn</author>
/// <date>2014-11-15</date>
/// <project>On Screen Keyboard</project>

#include <ctype.h>
#include <stdio.h>
#include <string.h>

///
/// Data structures
/// ----------------------------------------
///

struct Position {
	int x;
	int y;
};

///
/// File reading
/// ----------------------------------------
///

void openFile(FILE **fp, int argc, char **argv);
void promptForFile(char *path);

///
/// Parsing
/// ----------------------------------------
///

void lineToASCII(char *line, int *ascii, int *length);

#endif