# On Screen Keyboard

## My Solution
The solution I have constructed is TypeScript+Node based. To run the project from the source code:

1. Install Node and npm on your machine. This project was tested on Node v16.17.0.
2. Clone this repository
```sh 
git clone https://github.com/oalexander-dev/OnScreenKeyboard.git
```
3. Install the dependencies
```sh
cd ./OnScreenKeyboard
npm install 
```
4. Add one search term per line to the `input.txt` file in the root of this project
5. Run the following command to compile and run the program
```sh 
npm start
```


To run the tests for this project, follow steps 1-3 above, then run the following command 
```sh 
npm run test
```


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
