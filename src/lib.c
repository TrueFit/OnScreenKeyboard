#include "../header/constants.h"
#include "../header/lib.h"

///
/// File reading
/// ----------------------------------------
///

/// <summary>
/// Take the program input arguments, determine if a path to an input
/// file was given, and if not, ask the user for an input file. This 
/// function will ask the user for a valid file path, until one is given,
/// if an invalid or non-existent file path was supplied.
/// </summary>
///
/// <param name="fp">An out parameter which will be assigned the file handle</param>
/// <param name="argc">The argc parameter given to the program by the terminal</param>
/// <param name="argv">The argv parameter given to the program by the terminal</param>

void openFile(FILE **fp, int argc, char **argv) {
	char file[256];
	
//Was any input file given?
	if(argc == 1) {
		fprintf(stderr, "An input file was not specified.\n");
		promptForFile(file);
	} else {
		strncpy(file, argv[1], sizeof(file));
	}
	
//Does this file exist?
	do {
		if((*fp = fopen(file, "r")) == NULL) {
			fprintf(stderr, "The specified file does not exist or is not accessable.\n");
			promptForFile(file);
		}
	} while(*fp == NULL);
}

/// <summary>
/// Prompt the user for a path to a file, and continue to do so until
/// valid input is given. Note: this function does not check if the
/// input is indeed a path to an existing, readable file. It simply
/// gathers input from the user.
/// </summary>
///
/// <param name="buffer">A pointer to a character array to hold the path to the file</param>
void promptForFile(char *buffer) {
	char path[256];
	
	do {
		printf("Please enter the input file path here: ");
		fgets(path, sizeof(path), stdin);
	} while(strlen(path) == 1); // 1 for new line
	
//Remove the newline character from the input
	int length = strlen(path);
	
	if(path[length - 1] == '\n' || path[length - 1] == '\r') {
		path[length - 1] = '\0';
	}
	
	memcpy(buffer, path, sizeof(path));
}

///
/// Parsing
/// ----------------------------------------
///

/// <summary>
/// Read in a line from the input file, and return an array of numbers,
/// which is equal in length to that of the input string, and correspond
/// to the ASCII code of each character in the input string. This function
/// will output a warning for characters which do not appear on the 
/// keyboard, and will place a -1 as the ASCII code for the unsupported
/// character.
/// </summary>
///
/// <param name="line">A C-string corresponding a line from the input file</param>
/// <param name="ascii">An out parameter, which is an array of integers for the characters' ASCII codes</param>
/// <param nane-"length">An out parameter, which is the length of the input string</param>
void lineToASCII(char *line, int *ascii, int *length) {
	char c = '\0';
	int code = 0;
	int i = 0;
	*length = strlen(line);
	
//Convert the characters (A - Z, 0 - 9, and <space>) to their ASCII codes
	for(i = 0; i < *length; ++i) {
		c = toupper(line[i]);
		code = c;
		
	//Output any warnings
		if (c != line[i]) {
			fprintf(stderr, "Warning: \"%c\" was converted to uppercase.\n", line[i]);
		} else if (c == '\n' || c == '\r') {
			//New line, we're at the end
			return;
		} else if (!(code == 32 || (code >= 48 && code <= 57) || (code >= 65 && code <= 90))) {
			fprintf(stderr, "Warning: \"%c\" is not present on the keyboard, ignoring.", c);
			
			code = -1;
		}
		
	//Save the output
		ascii[i] = code;
	}
}