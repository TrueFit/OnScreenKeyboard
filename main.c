#include "header/lib.h"
#include <stdio.h>
#include <stdlib.h>

int main(int argc, char **argv) {
//Open the file
	FILE *handle = NULL;
	openFile(&handle, argc, argv);

//Read the file
	int codes[512];
	int length = 0;
	char line[512];
	int round = 1;
	
	while(fgets(line, sizeof(line), handle) != NULL) {
		printf("\nBeginning round: %d\n", round);
		printf("=======================================\n");
		lineToASCII(line, codes, &length);
		printf("Done\n");
		
		++round;
	}
	
//Close the file
	fclose(handle);
	return 0;
}