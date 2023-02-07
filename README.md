## Zach Albert Comments
Hello and thanks for checking out my take home test for the on screen keyboard.

My project is a c# console application that I created in Visual Studio. The applicaiton takes 1 argument on startup.  The argument is the InputFile you would like the program to process.  The output is currently just a textFile that writes to the local directory

Feel free to run the application from the exe in the release folder or debug the application from visual studio
1. To run the exe of the application:  navigate to OnScreenKeyboard\OnScreenKeyboard\bin\Release\net5.0.  Run the exe with an argument that is the path to your Input File.  
    1. For Example:   .\OnScreenKeyboard.exe "C:\Users\Administrator\Desktop\TruFitCodingTakeHome\OnScreenKeyboard\OnScreenKeyboard\InputFile.txt"

Things to note/areas of interest:
1. The program starts and we initalize the keyboard and the keyboard processor.  The processor uses an Interface that the keyboard inherets.  The purpose of this interface is remove the dependency from the processor(processor only cares that there is a keyboard, not the specific keyboard implementation).  This will allow us in the future to create new keyboard classes that have different layouts.  Then on start up we could choose which keyboard we want and the processor would handle it without code changes
2. The tests are very minimal, but cover happy path, and some edge cases
3. You can see some of the runtime/user tests that I did in the InputFile.txt that I included



# On Screen Keyboard

## The Problem

On screen keyboards are the bane of DVR users. To help alleviate the pain, one local company is asking you to implement part of a voice to text search for their DVR by developing an algorithm to script the on screen keyboard.
The keyboard is laid out as follows:

```
ABCDEF
GHIJKL
MNOPQR
STUVWX
YZ1234
567890
```

Please write a program which scripts the path of the cursor on the keyboard. The program should:

1. Accept a flat file as input.
   1. Each new line will contain a search term
2. Output the path for the DVR to execute for each line
   1. Assume the cursor will always start on the A
   2. Use the following characters to make up the path
      1. U = up
      2. D = down
      3. L = left
      4. R = right
      5. S = space
      6. \# = select
3. Comma delimit the result

## Sample Input

IT Crowd

## Sample Output

D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#

## The Fine Print

Please use whatever technology and techniques you feel are applicable to solve the problem. We suggest that you approach this exercise as if this code was part of a larger system. The end result should be representative of your abilities and style.

Please fork this repository. When you have completed your solution, please issue a pull request to notify us that you are ready.

Have fun.

## Things To Consider

Here are a couple of thoughts about the domain that could influence your response:

- There is no guarantee that the keyboard layout will continue to be alphanumeric. How might you plan for this in your code?
- What if the interface to get the string changed from a file to stream?
