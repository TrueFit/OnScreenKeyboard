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
		fprintf(stderr, LIB_NO_INPUT_FILE);
		promptForFile(file);
	} else {
		strncpy(file, argv[1], sizeof(file));
	}
	
//Does this file exist?
	do {
		if((*fp = fopen(file, "r")) == NULL) {
			fprintf(stderr, LIB_FILE_OPENING_ERROR);
			promptForFile(file);
		}
	} while(*fp == NULL);
	
//Minor output formatting adjustment
	if(argc == 1) printf("\n");
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
		printf(LIB_ENTER_FILE);
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
/// Parsing and calculations
/// ----------------------------------------
///

/// <summary>
/// Outputs a string of characters which correspond to the keyboard 
/// movement which is required to output a given string. This method
/// takes in an array of ASCII codes, corresponding to an input string,
/// and uses those values to perform a constant-time calculation to
/// output the string of characters which correspond to the input
/// movement.
/// </summary>
///
/// <param name="ascii">An array of ASCII character corresponding to a user's input string</param>
/// <param name="length">The length of the input array</param>
/// <param name="outStream">The output stream to which the keyboard movement strokes should be printed</param>
void calculatePosition(int *ascii, int length, FILE *outStream) {
	int code = 0;
	Position cur;
	int i = 0;
	Position prev;
	prev.x = 0;
	prev.y = 0;
	
//Does this line actually contain anything?
	if(length == 0) {
		fprintf(outStream, "%s\n", LIB_NO_CHARACTERS);
		return;
	}
	
	for(i = 0; i < length; ++i) {
	//Convert A - Z to the correct position in the keyboard matrix
		if(ascii[i] >= 65 && ascii[i] <= 90) {
			if(i != 0) fprintf(outStream, ",");
			
			code = ascii[i] - 65; // Align the ASCII codes to the keyboard matrix
			cur.x = code % X;     // Column
			cur.y = code / Y;     // Row
	//Convert 0 - 9 to the correct position in the keyboard matrix
		} else if (ascii[i] >= 48 && ascii[i] <= 57) {
			if(i != 0) fprintf(outStream, ",");
			
			code = ascii[i] - 48 + 26; // Align the ASCII codes to the keyboard matrix
			cur.x = code % X;          // Column
			cur.y = code / Y;          // Row
	//Take into account a space
		} else if (ascii[i] == 32) {
			if(i != 0) fprintf(outStream, ",");
			fprintf(outStream, "S");
			continue;
	//Ignore all characters not on the keyboard
		} else {
			continue;
		}
		
	//Calculate the offset of the new character from that of the previous character
	//Up or down
		if((cur.y - prev.y) < 0) {
			fprintf(outStream, "%s", up[abs(cur.y - prev.y) - 1]);     // -1 aligns to 0-based array
		} else if((cur.y - prev.y) > 0) {
			fprintf(outStream, "%s", down[(cur.y - prev.y) - 1]);
		}
	
	//Left or right
		if((cur.x - prev.x) < 0) {
			fprintf(outStream, "%s", left[abs(cur.x - prev.x) - 1]);
		} else if((cur.x - prev.x) > 0) {
			fprintf(outStream, "%s", right[(cur.x - prev.x) - 1]);
		}
		
		prev = cur;
		fprintf(outStream, "#");
	}
	
	fprintf(outStream, "\n");
}

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
			fprintf(stderr, LIB_WARNING_UPPERCASE, line[i]);
		} else if (c == '\n' || c == '\r') {
			//New line, we're at the end
			--(*length);
			return;
		} else if (!(code == 32 || (code >= 48 && code <= 57) || (code >= 65 && code <= 90))) {
			fprintf(stderr, LIB_WARNING_CHARACTER_NOT_PRESENT, c);
			
			code = -1;
		}
		
	//Save the output
		ascii[i] = code;
	}
}