/**
 * src/keyboard/keyboard.ts
 *
 * Class that represents the keyboard component of the
 * system
 */

export type Position = {
    row: number;
    col: number;
};

class Keyboard {
    // mappings from a key value to a position
    private keys: Map<string, Position>;

    constructor(layout: string[][]) {
        this.keys = new Map<string, Position>();

        // loop over layout and add each keys position
        layout.forEach((row, rowIndex) => {
            row.forEach((val, colIndex) => {
                // check for a duplicate key value
                if (this.keys.has(val)) {
                    console.warn('Duplicate key detected in layout: ' + val);
                }

                // insert the key
                this.keys.set(val.toLowerCase(), {
                    row: rowIndex,
                    col: colIndex
                });
            });
        });
    }

    // returns a position in the keyboard for a given value
    getKeyPosition(val: string): Position | null {
        const position = this.keys.get(val.toLowerCase());

        // if the key was found, return its position
        if (position) {
            return position;
        }

        // handle a missing key
        console.warn('Key not found: ' + val);
        return null;
    }
}

export default Keyboard;
