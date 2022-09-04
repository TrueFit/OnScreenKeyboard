/**
 * src/keyboard/keyboard.test.ts
 *
 * Unit tests for the keyboard component
 */
import Keyboard from './keyboard';

const layout = [
    ['A', 'B', 'C', 'D', 'E', 'F'],
    ['G', 'H', 'I', 'J', 'K', 'L'],
    ['M', 'N', 'O', 'P', 'Q', 'R'],
    ['S', 'T', 'U', 'V', 'W', 'X'],
    ['Y', 'Z', '1', '2', '3', '4'],
    ['5', '6', '7', '8', '9', '0']
];

describe('testing keyboard class', () => {
    const kb = new Keyboard(layout);

    // typical cases
    test('should return correct position for typical alphabetic values', () => {
        const pos = kb.getKeyPosition('I');
        expect(pos?.row).toBe(1);
        expect(pos?.col).toBe(2);
    });

    test('should return correct position for typical numeric values', () => {
        const pos = kb.getKeyPosition('2');
        expect(pos?.row).toBe(4);
        expect(pos?.col).toBe(3);
    });

    // edge cases
    test('should return correct position for corner values', () => {
        let pos = kb.getKeyPosition('A');
        expect(pos?.row).toBe(0);
        expect(pos?.col).toBe(0);

        pos = kb.getKeyPosition('F');
        expect(pos?.row).toBe(0);
        expect(pos?.col).toBe(5);

        pos = kb.getKeyPosition('5');
        expect(pos?.row).toBe(5);
        expect(pos?.col).toBe(0);

        pos = kb.getKeyPosition('0');
        expect(pos?.row).toBe(5);
        expect(pos?.col).toBe(5);
    });
});
