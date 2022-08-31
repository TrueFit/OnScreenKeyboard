//import KeyboardButton from "./KeyboardButton";
import {Fragment} from 'react';
// import {useKeyboardContext} from '../hooks/useKeyboardContext';
// import ACTIONS from '../constants/Keyboard';

const Keyboard = ({keyboard}) =>{
  return(
    <div className="row">
      <h2>{keyboard.title}</h2>
      {keyboard && keyboard.buttons.map((kb, index) => (
        <Fragment key={"kb-"+index}>
          {index % keyboard.size ===0 ? <div></div>:``}
          <button className={"col btn btn-outline-dark btn-lg keyButton " 
            + (kb === '' ? `disabled`:``)}
            disabled={kb===""}>
            {kb}
          </button>
        </Fragment>
        
      ))}
      <div></div>
      <button className="col btn btn-outline-dark btn-lg keyButton mb-5">
        Spacebar
      </button>
    </div>
  )
}

export default Keyboard;