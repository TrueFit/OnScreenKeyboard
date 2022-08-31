import {useKeyboardContext} from '../hooks/useKeyboardContext';
import onScreenKeyboardSearch from '../helpers/onScreenKeyboardSearch';
import ACTIONS from '../constants/Keyboard';


const InputTextForm = ({kbInput})=>{
  const {state, dispatch} = useKeyboardContext();

  const handleButtonChange = () =>{
    dispatch({
      type:ACTIONS.OUTPUT_TEXT, 
      payload: onScreenKeyboardSearch(state.keyboard.buttons,
      state.keyboard.size,state.kbInput.toUpperCase()) 
    });
  }

  return(
    <div className="input-group mb-3 pt-4" id="kb-simple-text">
      <input type="text" className="form-control" placeholder="IT Crowd" aria-label="Search Term" 
      value={kbInput} onChange={(e) => dispatch({type: ACTIONS.INPUT_TEXT_FIELD, payload: e.target.value})}
        aria-describedby="kb-search" id="kb-search-text"/>
      <button className="btn btn-dark text-light" 
        type="button" id="kb-search"
        onClick={handleButtonChange}>
        <span className="fa fa-microphone"></span>
        <span className="sr-only"></span>
      </button> 
    </div>
  )
}

export default InputTextForm;