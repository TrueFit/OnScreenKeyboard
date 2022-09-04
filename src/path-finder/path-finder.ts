/**
 * src/path-finder/path-finder.ts
 * 
 * Class that represents the cursor path finder, input can be
 * any Readable stream (socket, file, stdin), with search terms
 * separated by a newline. Output will be the path the cursor
 * should follow, along with spaces and select key presses, one
 * per line, in the same order as the input was read in
 */

import { exit } from "node:process";
import { Readable, Writable } from "node:stream";

import Keyboard from "../keyboard";


class PathFinder {
    private keyboard: Keyboard;
    private istream: Readable;
    private ostream: Writable;

    constructor(keyboardLayout: string[][], istream: Readable, ostream: Writable) {
        this.keyboard = new Keyboard(keyboardLayout);
        this.ostream = ostream;
        this.istream = istream;
    }

    // returns a promise that resolves after all data has been read in, 
    // processed, and had its output streamed to the output stream
    computeCursorPaths() : Promise<boolean> {
        return new Promise<boolean>((resolve) => {
            this.readInput().then((searchTerms) => {
                for (const term of searchTerms) {
                    const result = this.getPath(term);
                    this.printPath(result);
                }

                resolve(true);
            });
        });
    }

    // returns a promise that resolves after all search terms have been read
    // from the input stream
    readInput() : Promise<string[]> {
        return new Promise<string[]>((resolve) => {
            // read in data until end of file is reached, or CTL+D in command line
            const rawInputChunks: string[] = [];
            this.istream.on('readable', () => {
                let chunk;
                while (null !== (chunk = this.istream.read())) {
                    rawInputChunks.push(chunk);
                }
            });

            // when all data is read in, split up into search terms
            this.istream.on('end', () => {
                // chunks are not guaranteed to be in line form, so combine them 
                // and then split by newline characters
                const rawInput = rawInputChunks.join('');
                const searchTerms = rawInput.split('\n');

                resolve(searchTerms);
            });
        });
    }

    // computes the cursor path for one search term
    getPath(term: string) : string {
        // initialize cursor's initial position
        let currChar = 'A';
        let currPos = this.keyboard.getKeyPosition(currChar);

        // make sure start key was found
        if (!currPos) {
            console.error("Error: invalid start key: " + currChar);
            exit(1);
        }

        // iterate over the characters in the search term and add moves to list
        const moves: string[] = [];
        for (let i = 0; i < term.length; i++) {
            // check if curr character is a space, if so, add it and move on
            if (term[i] === ' ') {
                moves.push('S');
                continue;
            }

            // get destination position
            const destPos = this.keyboard.getKeyPosition(term[i]);

            // make sure key exists in keyboard
            if (!destPos) {
                console.error("Error: invalid key in term: " + term[i]);
                exit(1);
            }

            // calculate row and col difference
            const dy = destPos.row - currPos.row;
            const dx = destPos.col - currPos.col;

            // determine which characters are needed to make the moves
            let rowChar: 'U' | 'D';
            let colChar: 'L' | 'R';
            if (dy > 0) {
                rowChar = 'D';
            } else {
                rowChar = 'U';
            }

            if (dx > 0) {
                colChar = 'R';
            } else {
                colChar = 'L';
            }

            // add moves to move cursor from currPos to destPos
            for (let rowI = 0; rowI < Math.abs(dy); rowI++) {
                moves.push(rowChar);
            }
            for (let colI = 0; colI < Math.abs(dx); colI++) {
                moves.push(colChar);
            }

            // add selection character
            moves.push('#');

            // update curr variables
            currChar = term[i];
            currPos = destPos;
        }
        return moves.join(',');
    }

    // prints a path to the output stream
    printPath(path: string) {
        this.ostream.write(path + '\n');
    }
}

export default PathFinder;