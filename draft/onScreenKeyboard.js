const keyboardArea = document.getElementById("keyboard");
const defaultKeyboard = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"]
const defaultSize = 6;
const defaultName = "Default";

/**@TODO Implement a Keyboard Class */
class Keyboard {
  /**@constructor
   * @param {number} size Defines a keyboard length on screen
   * @param {object} keylist Array/Dictionary of letter keys (eg. 'a','A','b')
   * @param {string} name Name of new keyboard
   */
  constructor(size = defaultSize, keyList = defaultKeyboard, name = defaultName) {
    this.size = size;
    this.keyList = keyList;
    this.name = name
  }

  create() {
    /**@todo come up with method to create a keyboard */
  }

  addKey(item) {
    this.keyList.push(item);
  }
}

/**Allows to create a new keyboard
 * @param {Array.<string>} keyboard keyboard keys
 * @param {number} size Maximum column size for keyboard, major constraint for the algorithm to work
 * @returns {Array.<string[]>} Keyboard layout as a 2D array
 */
function createKeyboard(keyboard = defaultKeyboard, size = defaultSize) {
  const kb = [];

  //Quick solution when the key list size is not a multiplier to the set size
  //Not ideal but handles out of range issues.
  while (keyboard.length % defaultSize != 0) {
    keyboard.push('');
  }

  let idx = size;
  while (idx <= keyboard.length) {
    kb.push([keyboard.slice(idx - size, idx)]);
    idx += size;
  }

  return kb;
}

/**Creates front-end layout 
 * @param {Array<Array<string>>} keyboard Keyboard layout
 * @param {HTMLElement} kbArea Target area for the new Keyboard
 * @param {string} name Keyboard's name
 */
function createKeyboardLayout(keyboard, kbArea = keyboardArea, name = defaultName) {
  //Title
  const title = document.createElement("h2");
  title.textContent = `${name} Keyboard`;
  kbArea.appendChild(title);

  for (const buttons of keyboard) {
    const buttonRow = createButtonRow(buttons, defaultName);
    kbArea.appendChild(buttonRow);
  }

  //Footer
  const spaceBar = document.createElement("button");
  spaceBar.className = "col btn btn-outline-dark btn-lg keyButton mb-5";
  spaceBar.innerHTML = `Spacebar`;
  kbArea.appendChild(spaceBar);

}

/**Creates a keyboard row in the frontent
 * @param {Array<string>} buttons Array of keyboard buttons
 * @returns {HTMLDivElement} Container element representing a row of buttons
 */
function createButtonRow(buttons) {
  const row = document.createElement("div");
  row.className = "kbRow row m-2";

  for (const button of buttons[0]) {
    const col = createButtonColumns(button);
    row.appendChild(col);
  }

  return row;
}

/**Creates a keyboard button in the frontent
 * @param {string} button 
 * @returns {HTMLButtonElement} Button HTML Element
 */
function createButtonColumns(button) {
  const col = document.createElement("button");
  col.className = "col btn btn-outline-dark btn-lg keyButton";
  col.setAttribute("type", "button");
  col.innerText = `${button}`

  //If Button is a filler, disable it for visual effects.
  if (col.innerText === '') {
    col.style.backgroundColor = "#ccc"
    col.disabled = true;
  }

  return col;
}

/**Main Search Algorithm, returns a string of directions comma separated
 * @param {Array.<string[]>} keyboard
 * @param {number} size
 * @param {string} searchText search term(s)
 * @returns {string} Directions (e.g. "#,S,U")
 */
function onScreenKeyboardSearch(keyboard, size = defaultSize, searchText) {
  //If keyboard or search term aren't available, return an empty string
  if (keyboard == null || keyboard.length === 0 || searchText === null || searchText === undefined) return "";
  const result = [];
  let positionY = 0; //allows us to look up and down
  let positionX = 0; //allow us to look left and right

  for (const char of searchText) {
    const charIndex = keyboard.indexOf(char);

    let relPositionY = Math.floor(charIndex / size);
    let relPositionX = charIndex % size;

    if (charIndex < 0) {
      result.push('S');
      continue;
    }

    const deltapositionY = relPositionY - positionY;
    for (let i = 0; i < Math.abs(deltapositionY); i++) {
      deltapositionY > 0 ? result.push("D") : result.push("U");
    }

    const deltapositionX = relPositionX - positionX;
    for (let i = 0; i < Math.abs(deltapositionX); i++) {
      deltapositionX > 0 ? result.push("R") : result.push("L");
    }

    result.push('#');
    positionY = relPositionY;
    positionX = relPositionX;
  }

  return result.join(",");
}

/**Fetches the search text from an input, input toggled by a checkbox
 * @param {Array.<string[]>} keyboard
 * @param {HTMLElement} inputMode boolean check for input mode (simple or file)
 */
function fetchKeyboardSearchText(keyboard, inputMode) {
  const kbSearch = document.getElementById("kb-search");
  const kbSimpleInput = document.getElementById("kb-simple-text");
  const kbFileSelector = document.getElementById("kb-file-selector");

  /**@TODO Fix Toggle */
  let isIt = true;
  //Check input Mode and proceed from there
  if (isIt) {
    inputMode.setAttribute("checked", "checked");
    kbSimpleInput.style.display = 'flex';
    kbFileSelector.style.display = 'none';

    kbSearch.addEventListener("click", () => {
      const searchText = document.getElementById("kb-search-text").value.toUpperCase();

      const result = onScreenKeyboardSearch(keyboard, defaultSize, searchText);

      if (result) {
        createDirectionOutput(result, document.getElementById("kb-output"), searchText);
        animateKeyboardSearch(result, document.querySelectorAll(".keyButton"), defaultSize);
      }
    });
  } else {
    inputMode.removeAttribute("checked");
    kbSimpleInput.style.display = 'none';
    kbFileSelector.style.display = 'block';
    readTextFile(keyboard);
  }

}

/**Add Text to Input */
function readTextFile(keyboard) {
  const status = document.getElementById('status');
  const output = document.getElementById('fileInput');
  const results = [];
  if (window.FileList && window.File && window.FileReader) {
    document.getElementById('file-selector').addEventListener('change', event => {
      output.innerHTML = '';
      status.textContent = '';
      const file = event.target.files[0];
      if (!file.type) {
        status.textContent = 'Error: The File.type property does not appear to be supported on this browser.';
        return;
      }
      if (!file.type.match('text.*')) {
        status.textContent = 'Error: The selected file does not appear to be a text.'
        return;
      }
      const reader = new FileReader();
      reader.addEventListener('load', event => {
        const lines = event.target.result.split(/\n/);
        for (const line of lines) {
          const item = document.createElement("p");
          item.innerHTML = `${line}`;
          output.appendChild(item);

          const result = onScreenKeyboardSearch(keyboard, defaultSize, line.toUpperCase());

          if (result) {
            createDirectionOutput(result, document.getElementById("kb-output"), line);

            /**@TODO fix the animation to wait until the previous calls finishes its animation */
            //animateKeyboardSearch(result, document.querySelectorAll(".keyButton"), defaultSize);
          }
          //results.push([line,result]);

        }
      });
      reader.readAsText(file);
    });
  }
}


/**Creates the Ouptut of a search term in the frontend
 * @param {string} result output directions
 * @param {HTMLElement} output HTML Element where results will be printed
 */
function createDirectionOutput(result, outputArea, text = "") {
  const output = document.createElement("p");
  output.className = "d-block fs-5";
  output.innerHTML = `<pre class="bg-dark light-text text-center text-wrap pt-2">${text}
  ${result}</pre>`;

  outputArea.appendChild(output);
}

/**Helper function used to slow down iterations */
function slowIterate(millisecs) {
  return new Promise(resolve => setTimeout(resolve, millisecs));
}

/**Set the animation 
 * @param {string} result string of results
 * @param {Array.<HTMLELement>} buttons
 * @param {number} size keyboard size
*/
async function animateKeyboardSearch(result, buttons, size = defaultSize) {
  //Always start at the first Key
  let idx = 0;
  buttons[idx].className += " press";
  await slowIterate(100);

  /**U = index - size 
   * D = index + size
   * L = index - 1
   * R = index + 1
   */
  for (let r of result) {
    if (idx < 0) continue; //Safekeep in case results return something funky
    await slowIterate(200);
    switch (r) {
      case 'U':
        buttons[idx].classList.remove('press');
        idx -= size;
        buttons[idx].className += " press"
        break;
      case 'D':
        buttons[idx].classList.remove('press');
        idx += size;
        buttons[idx].className += " press"
        break;
      case 'L':
        buttons[idx].classList.remove('press');
        idx--;
        buttons[idx].className += " press"
        break;
      case 'R':
        buttons[idx].classList.remove('press');
        idx++;
        buttons[idx].className += " press"
        break;
      case '#':
        buttons[idx].classList.remove('press');
        buttons[idx].className += " select";
        document.getElementById("selected").play();
        await slowIterate(300);
        buttons[idx].classList.remove('select');
        break;
      case 'S':
        buttons[buttons.length - 1].className += " space";
        document.getElementById("spacePress").play();
        await slowIterate(300);
        buttons[buttons.length - 1].classList.remove('space');
        break;

      default:
        break;
    }

  }
}

/**Initialize Everything */
function initAlgorithm() {
  //Keyboard can be changed here
  const kb = createKeyboard(defaultKeyboard, defaultSize);
  createKeyboardLayout(kb, keyboardArea, defaultName);

  //Toggle the use between file uploader and simple text
  const inputMode = document.getElementById("inputMode");
  inputMode.addEventListener('change', fetchKeyboardSearchText(defaultKeyboard, inputMode));
  //readTextFile();

  //Test Case - QWERTY Keyboard
  // const qwerty = createKeyboard("QWERTYUIOPASDFGHJKLZXCVBNM".split(''),10);
  // createKeyboardLayout(qwerty, keyboardArea, "Secondary");
  // fetchKeyboardSearchText("QWERTYUIOPASDFGHJKLZXCVBNM".split(''));
}

initAlgorithm();