/// <summary>
/// This is the driver program for the On Screen Keyboard project. It will
/// open a file, call the necessary library functions, and use them to display
/// the necessary output for the keyboard keystrokes.
/// </summary>
///
/// <author>Oliver Spryn</author>
/// <date>2014-11-15</date>
/// <project>On Screen Keyboard</project>

#include "header/constants.h"
#include "header/lib.h"

#include <stdio.h>
#include <stdlib.h>

int main(int argc, char **argv) {
//Provide usage instructions
	if(argc == 1) {
		printf(MAIN_PROGRAM_USAGE, argv[0]);
		printf(MAIN_PROGRAM_USAGE_INPUT);
		printf(MAIN_PROGRAM_USAGE_OUTPUT);
	}
	
//Should the output be routed to a file?
	FILE *out;
	
	if(argc >= 3) {
		out = fopen(argv[2], "w");
	} else { 
		out = stdout;
	}
	
//Open the file
	FILE *handle = NULL;
	openFile(&handle, argc, argv);

//Read the file and output the keystroke movements
	int codes[512];
	int length = 0;
	char line[512];
	int round = 1;
	
	printf(MAIN_WELCOME);
	
	while(fgets(line, sizeof(line), handle) != NULL) {
		int noNewLine = (line[strlen(line) - 1] == '\n' || line[strlen(line) - 1] == '\r') ? strlen(line) - 1 : strlen(line);
		
		printf("\n%s: %d\n%.*s\n", MAIN_BEGINNING_ROUND, round, noNewLine, line);
		printf("=======================================\n");
		lineToASCII(line, codes, &length);
		calculatePosition(codes, length, out);
		printf("%s\n", MAIN_DONE);
		
		++round;
	}
	
	printf("\n%s\n", MAIN_COMPLETED);
	
//Close the file
	fclose(handle);
	return 0;
}