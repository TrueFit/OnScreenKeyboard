import {useState} from 'react'
import InputFileReader from '../components/InputFileReader';
import InputTextForm from '../components/InputTextForm';
import InputMode from '../components/InputMode';
import OutputBox from '../components/OutputBox';
import {useKeyboardContext} from '../hooks/useKeyboardContext';
import ACTIONS from '../constants/Keyboard';

const InputArea = () =>{
  const [inputModeValue, setInputModeValue] = useState(true);
  const {state, dispatch} = useKeyboardContext();

  const toggleInputMode = () =>{
    setInputModeValue(!inputModeValue);
    dispatch({
      type: ACTIONS.INPUT_TEXT_FIELD,
      payload: ''
    });
    dispatch({
      type: ACTIONS.OUTPUT_TEXT,
      payload: ''
    })
  }

  return(
    <fieldset className="p-3 mb-3">
      <legend>Input</legend>
      <InputMode isOn={inputModeValue} 
        handleToggle={toggleInputMode}></InputMode>
        <div className="kb-input-mode">
        {inputModeValue ? <InputTextForm kbInput={state.kbInput}></InputTextForm> : <InputFileReader kbInput={state.kbInput}></InputFileReader>}     
        </div>
        <div className="kb-file-input">
          {state.kbInput}
      </div>
      <hr/>
      <OutputBox kbOutput={state.kbOutput}></OutputBox>
    </fieldset>
  )
}

export default InputArea;