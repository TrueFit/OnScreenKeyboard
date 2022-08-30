# OnScreenKeyboard Solution
## First Implementation
This is the first Draft of the coding challenge.
A rudimentary approach with plain frontend stack
This is mostly to get the initial idea implemented.

We will then convert this to a React App and hopefully integrate a DB and AUTH using Node.js and either MySQL or MongoDB.

Photo by Lorenzo Herrera on Unsplash.com

## Current Constraints
- Visually, keyboard display will start to break after keyboard size being 12, however, programmatically, it does not have a limit.
- File uploader only accepts TXT files
- Keyboard layout events attempts to keep the keyboard array divisible by the size
- Unrecognized characters will be considered a 'spacebar' input (I may change this to just be an ignored character)
- Currently I have not set a say to make key buttons unique, so it is possible to have two 'A' keys, this can be fixed once I move items to NodeJS


## Roadmap
### Modules
#### Site Structure
- Header
- Main
- Footer
#### Keyboard
- Keyboard container (Stateful)
- Letter Key

#### I/O
- Input Text
- Input File
- Toggle Mode
- Choose existing keyboards
- Output