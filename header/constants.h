#ifndef CONSTANTS_H
#define CONSTANTS_H

/// <summary>
/// Contains definitions for constant values which are used throughout
/// this project.
/// </summary>
///
/// <author>Oliver Spryn</author>
/// <date>2014-11-15</date>
/// <project>On Screen Keyboard</project>

///
/// Strings used throughout the program
/// ----------------------------------------
///

#define LIB_ENTER_FILE "Please enter the input file path here: "
#define LIB_FILE_OPENING_ERROR "The specified file does not exist or is not accessible.\n"
#define LIB_NO_CHARACTERS "<line contains no characters>"
#define LIB_NO_INPUT_FILE "An input file was not specified.\n"
#define LIB_WARNING_CHARACTER_NOT_PRESENT "Warning: \"%c\" is not present on the keyboard, ignoring.\n"
#define LIB_WARNING_UPPERCASE "Warning: \"%c\" was converted to uppercase.\n"

#define MAIN_BEGINNING_ROUND "Beginning round"
#define MAIN_COMPLETED "File completed"
#define MAIN_DONE "Done"
#define MAIN_PROGRAM_USAGE "Usage: %s [input file [output file]]\n"
#define MAIN_PROGRAM_USAGE_INPUT " - You will be prompted for an input file, if one is not specified, or is missing.\n"
#define MAIN_PROGRAM_USAGE_OUTPUT " - If an output file is not provided, output will be printed to the terminal.\n   NOTE: File output will override the existing contents of the text file.\n\n"
#define MAIN_WELCOME "On-screen Keyboard Keystroke Movement:\n"

///
/// C definitions
/// ----------------------------------------
///

//Bring the NULL onboard with C, if needed
#ifndef NULL
#define NULL ((void *) 0)
#endif

//Bring the boolean onboard with C
typedef int bool;
#define false 0;
#define true 1;

///
/// Input keyboard definition
/// ----------------------------------------
///

//Define the "keyboard" matrix
#define X 6
#define Y 6

//ASCII codes: (0 - 9) => (48 - 57), and (A - Z) => (65 - 90)
//Explictly written out for clarity
char keyboard[X][Y];

//Pre-define the strings of possible movements, a space for time trade-off
char down[5][11];
char left[5][11];
char right[5][11];
char up[5][11];

#endif