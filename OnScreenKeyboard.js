// This program takes a new-line-delimited series of strings and uses 
//   them to generate paths to navigate an on-screen keyboard.
 
const keyboard = [
                  ['A','B','C','D','E','F'],
                  ['G','H','I','J','K','L'],
                  ['M','N','O','P','Q','R'],
                  ['S','T','U','V','W','X'],
                  ['Y','Z','1','2','3','4'],
                  ['5','6','7','8','9','0']
                 ];

const inputFromVoice = (str, keyboard) => {
  const inputSequence = str.toUpperCase();
  let selector = [0,0];
  let outputSequence = []; 
  let xChangeChar, xDiff, yChangeChar, yDiff;

  for(let char of inputSequence) {
    if(char === ' ') {
      outputSequence.push('S');
      continue;
    }

    for(let arr of keyboard) {
      arr.findIndex(el => {
        if(el === char) {
          if(keyboard.indexOf(arr) - selector[0] > 0) {
            xChangeChar = 'D';
            xDiff = keyboard.indexOf(arr) - selector[0];
          } else if (keyboard.indexOf(arr) - selector[0] < 0) {
            xChangeChar = 'U';
            xDiff = (keyboard.indexOf(arr) - selector[0]) * -1;
          } else {
            xDiff = 0;
          };

          if(arr.indexOf(el) - selector[1] > 0) {
            yChangeChar = 'R';
            yDiff = arr.indexOf(el) - selector[1];
          } else if (arr.indexOf(el) - selector[1] < 0) {
            yChangeChar = 'L';
            yDiff = (arr.indexOf(el) - selector[1]) * -1;
          } else {
            yDiff = 0;
          };

          outputSequence = [...outputSequence,
                            ...Array(xDiff).fill(xChangeChar),
                            ...Array(yDiff).fill(yChangeChar),
                            '#'];
          selector = [keyboard.indexOf(arr), arr.indexOf(el)];
          return true;
        }
      });
    }
  }
  return outputSequence.join(',');
}

const fs = require('fs');
data = fs.readFile('inputs.txt', 'utf8', (err, data) => {
  let seqArray = [];

  if (err) throw err;
  data = data.split(/\r?\n/);

  for(let title of data) {
    seqArray.push(inputFromVoice(title, keyboard));
  }
  console.log(seqArray);
  return seqArray;
});
