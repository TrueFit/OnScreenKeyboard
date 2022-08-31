import {useKeyboardContext} from '../hooks/useKeyboardContext';
import onScreenKeyboardSearch from '../helpers/onScreenKeyboardSearch';
import ACTIONS from '../constants/Keyboard';

const InputFileReader = ()=>{
  const {state,dispatch} = useKeyboardContext();

  /**Add Text to Input */
  const readTextFile = (e) => {
    e.preventDefault();
    
    const reader = new FileReader();
    reader.addEventListener('load', event => {
      const lines = event.target.result.split(/\n/);
      dispatch({type:ACTIONS.INPUT_TEXT_FIELD, payload: lines});

      const results = [];
      for (const line of lines) {
        results.push(onScreenKeyboardSearch(state.keyboard.buttons,state.keyboard.size,line.toUpperCase()));
      }
      
      dispatch({
        type: ACTIONS.OUTPUT_TEXT,
        payload: results.join(",*,")
      });
    });
    reader.readAsText(e.target.files[0]);
  }
  
  return(
    <div className="mb-3 kb-file-selector" id="kb-file-selector">
      <label htmlFor="file-selector" className="form-label">
        Upload Text to read
      </label>
      <input className="form-control" 
      type="file" id="file-selector" accept=".txt" onChange={readTextFile}/>
      
    </div>
  )
}

export default InputFileReader;