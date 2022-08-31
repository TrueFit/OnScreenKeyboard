import { useEffect } from "react";
import {useKeyboardContext} from '../hooks/useKeyboardContext';
import animateKeyboardSearch from "../helpers/animateOnScreenKeyboard";

const OutputBox = ({kbOutput}) =>{
  const {state} = useKeyboardContext();

  useEffect(()=>{
    const buttons = document.querySelectorAll(".keyButton");
    
    if(state.kbOutput){
      animateKeyboardSearch(state.kbOutput, buttons, state.keyboard.size);
    }
  }, [state]);
  return(
    <div className="card bg-success p-3 text-light kb-output" id="kb-output">
      <h2>Output</h2>
      <hr />
      
      {kbOutput ?
      <div className="d-block fs-5">
        <pre className="bg-dark light-text text-center text-wrap pt-2">
          {kbOutput}
        </pre>
      </div> :``}
        
      
    </div>
  )
}

export default OutputBox;