/**
 * src/index.ts
 * 
 * Main driver that sets up the input and output 
 * streams and initializes path finder
 */

 import * as readline from 'node:readline';


 // initialize an IO interface with desired data source and write location
 const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
 });
 