/**Main Search Algorithm, returns a string of directions comma separated
 * @param {string[]} keyboard An array of elements that can be considered buttons for a keyboard
 * @param {number} size Determines the graph dimensions (nxn)
 * @param {string} searchText search term(s)
 * @returns {string} Directions (e.g. "D,D,#,S,R,R,U#")
 */
const onScreenKeyboardSearch = (keyboard, size = 6, searchText)=>{
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
      if(char===' '){
        result.push('S');
      }
      //We can add logic to special characters if we want to
      
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

export default onScreenKeyboardSearch;