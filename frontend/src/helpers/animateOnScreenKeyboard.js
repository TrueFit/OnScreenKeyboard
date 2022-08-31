import slowIterate from './slowIterate';

/**Set the animation 
 * @param {string} result string of results
 * @param {Array.<HTMLELement>} buttons
 * @param {number} size keyboard size
*/
const animateKeyboardSearch = async (result, buttons, size)=>{
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
        await slowIterate(300);
        buttons[idx].classList.remove('select');
        break;
      case 'S':
        buttons[buttons.length - 1].className += " space";
        await slowIterate(300);
        buttons[buttons.length - 1].classList.remove('space');
        break;
      case '*':
          idx=0;
          buttons[idx].className += " press";
          await slowIterate(100);
          break;

      default:
        break;
    }

  }
}

export default animateKeyboardSearch;