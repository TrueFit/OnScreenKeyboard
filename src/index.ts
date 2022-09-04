/**
 * src/index.ts
 *
 * Main driver that sets up the input and output
 * streams and initializes path finder
 */
import { createReadStream } from 'node:fs';
import { exit } from 'node:process';
import { Readable, Writable } from 'node:stream';

import PathFinder from './path-finder';

// initialize the input stream to get search terms from, this
// can be any stream that implements Readable
const istream: Readable = createReadStream('input.txt');
// const istream: Readable = process.stdin;

// handle any errors with opening input stream
istream.on('error', (err) => {
    console.error(err);
    exit(1);
});

// initialize the output stream to print resulting cursor paths to
const ostream: Writable = process.stdout;

// handle any errors with opening output stream
ostream.on('error', (err) => {
    console.error(err);
    exit(1);
});

// define the layout of the keyboard
const layout = [
    ['A', 'B', 'C', 'D', 'E', 'F'],
    ['G', 'H', 'I', 'J', 'K', 'L'],
    ['M', 'N', 'O', 'P', 'Q', 'R'],
    ['S', 'T', 'U', 'V', 'W', 'X'],
    ['Y', 'Z', '1', '2', '3', '4'],
    ['5', '6', '7', '8', '9', '0']
];

// initialize and run the path finder
const pathFinder = new PathFinder(layout, istream, ostream);
pathFinder.computeCursorPaths().then(() => {
    exit(0);
});
