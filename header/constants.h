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
char keyboard[X][Y] = {
	{ 'A', 'B', 'C', 'D', 'E', 'F' }, // 65, 66, 67, 68, 69, 70
	{ 'G', 'H', 'I', 'J', 'K', 'L' }, // 71, 72, 73, 74, 75, 76
	{ 'M', 'N', 'O', 'P', 'Q', 'R' }, // 77, 78, 79, 80, 81, 82
	{ 'S', 'T', 'U', 'V', 'W', 'X' }, // 83, 84, 85, 86, 87, 88
	{ 'Y', 'Z', '0', '1', '2', '3' }, // 89, 90, 48, 49, 50, 51
	{ '4', '5', '6', '7', '8', '9' }  // 52, 53, 54, 55, 56, 57
};

#endif