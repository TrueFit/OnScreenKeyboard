CC = gcc
FILES = header/constants.h src/lib.c main.c
OUT = main

# Output a program, with default options and no debugging
all:
	$(CC) $(FILES) -Wall -o $(OUT)
	
# Output a program which is ready for GDB debugging
debug:
	$(CC) $(FILES) -g -Wall -o $(OUT)

# Compiles and runs a program, without a check for compilation success
run:
	$(CC) $(FILES) -Wall -o $(OUT)
	./$(OUT)