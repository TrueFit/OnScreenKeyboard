const keyboardArea = document.getElementById("keyboard");
const directions = ["U","D","L","R","S","#"]
const defaultKeyboard = ["A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","1","2","3","4","5","6","7","8","9","0"]
const defaultSize = 6;
const defaultName = "Default";

/**@TODO Implement a Keyboard Class */
class Keyboard{
  /**@constructor
   * @param {number} size Defines a keyboard length on screen
   * @param {object} keylist Array/Dictionary of letter keys (eg. 'a','A','b')
   * @param {string} name Name of new keyboard
   */
  constructor(size = defaultSize, keyList = defaultKeyboard, name=defaultName){
    this.size = size;
    this.keyList = keyList;
    this.name = name
  }
  
  create(){
    /**@todo come up with method to create a keyboard */
  }

  addKey(item){
    this.keyList.push(item);
  }
}

/**Allows to create a new keyboard
 * @param {Array.<string>} keyboard keyboard keys
 * @param {number} size Maximum column size for keyboard, major constraint for the algorithm to work
 * @returns {Array.<string[]>} Keyboard layout as a 2D array
 */
function createKeyboard(keyboard=defaultKeyboard, size=defaultSize){
  const kb = [];

  //Quick solution when the key list size is not a multiplier to the set size
  //Not ideal but handles out of range issues.
  while(keyboard.length % defaultSize != 0){
    keyboard.push('');
  }

  let idx=size;
  while(idx <= keyboard.length){
    kb.push([keyboard.slice(idx-size,idx)]);
    idx += size;
  }
  
  return kb;
}

/**Creates front-end layout 
 * @param {Array<Array<string>>} keyboard Keyboard layout
 * @param {HTMLElement} kbArea Target area for the new Keyboard
 * @param {string} name Keyboard's name
*/
function createKeyboardLayout(keyboard, kbArea = keyboardArea, name = defaultName){
  //Title
  const title = document.createElement("h2");
  title.textContent = `${name} Keyboard`;
  kbArea.appendChild(title);

  for(const buttons of keyboard){
    const buttonRow = createButtonRow(buttons,defaultName);
    kbArea.appendChild(buttonRow);
  }

  //Footer
  const spaceBar = document.createElement("button");
  spaceBar.className = "col btn btn-outline-dark btn-lg keyButton mb-5";
  spaceBar.innerHTML = `Space`;
  kbArea.appendChild(
    spaceBar);

}

/**Creates a keyboard row in the frontent
 * @param {Array<string>} buttons Array of keyboard buttons
 * @returns {HTMLDivElement} Container element representing a row of buttons
 */
function createButtonRow(buttons){
  const row = document.createElement("div");
  row.className = "kbRow row m-2"; 

  for(const button of buttons[0]){
    const col = createButtonColumns(button);
    row.appendChild(col);
  }

  return row;
}

/**Creates a keyboard button in the frontent
 * @param {string} button 
 */
function createButtonColumns(button) {
  const col = document.createElement("button");
  col.className = "col btn btn-outline-dark btn-lg keyButton";
  col.setAttribute("type","button");
  col.innerText = `${button}`

  //If Button is a filler, disable it for visual effects.
  if(col.innerText === ''){
    col.style.backgroundColor ="#ccc"
    col.disabled = true;
  }

  return col;
}

function initAlgorithm(){
  //Keyboard can be changed here
  const kb = createKeyboard();
  createKeyboardLayout(kb, keyboardArea, defaultName);

  // Test Case - QWERTY Keyboard
  // const qwerty = createKeyboard("QWERTYUIOPASDFGHJKLZXCVBNM".split(''),10);
  // createKeyboardLayout(qwerty, keyboardArea, "Secondary");
}

initAlgorithm();
