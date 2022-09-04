/**
 * src/path-finder/path-finder.test.ts
 * 
 * Unit tests for the path finder component
 */

import { createReadStream } from 'fs';

import PathFinder from './path-finder';


const layout = [
    ['A', 'B', 'C', 'D', 'E', 'F'],
    ['G', 'H', 'I', 'J', 'K', 'L'],
    ['M', 'N', 'O', 'P', 'Q', 'R'],
    ['S', 'T', 'U', 'V', 'W', 'X'],
    ['Y', 'Z', '1', '2', '3', '4'],
    ['5', '6', '7', '8', '9', '0']
];

describe('testing path finder class', () => {
    test('should get correct result for simple single search term', async () => {
        // TypeScript does not like the empty mock implementation, but it is necessary
        // to keep path output out of the testing output
        // @ts-ignore
        const logSpy = jest.spyOn(process.stdout, 'write').mockImplementation(() => {});
        const pathFinder = new PathFinder(layout, createReadStream('test-inputs/test1.txt'), process.stdout);
        await pathFinder.computeCursorPaths();

        const expected = 'D,D,D,R,#,U,U,U,R,R,R,#,D,D,D,L,L,L,L,#,R,#\n'

        expect(logSpy).toHaveBeenCalledWith(expected);

        logSpy.mockClear();
    });

    test('should get correct result for multiple search terms', async () => {
        // @ts-ignore
        const logSpy = jest.spyOn(process.stdout, 'write').mockImplementation(() => {});
        const pathFinder = new PathFinder(layout, createReadStream('test-inputs/test2.txt'), process.stdout);
        await pathFinder.computeCursorPaths();

        const expected1 = 'D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#\n';
        const expected2 = '#,R,#,R,#,S,D,D,D,D,#,R,#,R,#\n';

        expect(logSpy).toHaveBeenCalledWith(expected1);
        expect(logSpy).toHaveBeenCalledWith(expected2);

        logSpy.mockClear();
    });

    test('should get correct result for corner keys', async () => {
        // @ts-ignore
        const logSpy = jest.spyOn(process.stdout, 'write').mockImplementation(() => {});
        const pathFinder = new PathFinder(layout, createReadStream('test-inputs/test3.txt'), process.stdout);
        await pathFinder.computeCursorPaths();

        const expected = '#,R,R,R,R,R,#,D,D,D,D,D,#,L,L,L,L,L,#\n';

        expect(logSpy).toHaveBeenCalledWith(expected);

        logSpy.mockClear();
    });

    test('should get correct result for spaces only', async () => {
        // @ts-ignore
        const logSpy = jest.spyOn(process.stdout, 'write').mockImplementation(() => {});
        const pathFinder = new PathFinder(layout, createReadStream('test-inputs/test4.txt'), process.stdout);
        await pathFinder.computeCursorPaths();

        const expected = 'S,S,S,S\n';

        expect(logSpy).toHaveBeenCalledWith(expected);

        logSpy.mockClear();
    });
});
