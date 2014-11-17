CC = gcc
FILES = src/constants.c src/lib.c main.c
OUT = main

INFILE = in.txt
OUTFILE = out.txt

# Output the program, with default options and no debugging
all:
	@$(CC) $(FILES) -Wall -lm -o $(OUT)
	
# Output the program which is ready for GDB debugging
debug:
	@$(CC) $(FILES) -g -Wall -lm -o $(OUT)

# Compiles and runs the program with no input or output files, and without a check for compilation success
run:
	@$(CC) $(FILES) -Wall -lm -o $(OUT)
	@./$(OUT)

# Compiles and runs the program with only an input file, without a check for compilation success	
runIn:
	@$(CC) $(FILES) -Wall -lm -o $(OUT)
	@./$(OUT) $(INFILE)
	
# Compiles and runs the program with an input and an output file, without a check for compilation success	
runInOut:
	@$(CC) $(FILES) -Wall -lm -o $(OUT)
	@./$(OUT) $(INFILE) $(OUTFILE)