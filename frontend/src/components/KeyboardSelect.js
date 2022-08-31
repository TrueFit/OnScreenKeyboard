import { useEffect, optionsState } from 'react';
import {useKeyboardContext} from '../hooks/useKeyboardContext';
import ACTIONS from '../constants/Keyboard';

const KeyboardSelect = () =>{
  const {state, dispatch} = useKeyboardContext();

  useEffect(() => {
    const fetchWorkouts = async () => {
      const response = await fetch('http://localhost:3000/keyboards')
      const json = await response.json()
      
      if (response.ok) {
        dispatch({type: ACTIONS.SET_KEYBOARD_LIST, payload: json})
      }
    }
    fetchWorkouts()
  }, [dispatch]);

  const handleSelectChange = (e)=>{
    const newSelect = state.kbList.find(x=>x.id===e.target.value);

    dispatch({
      type: ACTIONS.SET_KEYBOARD,
      payload: newSelect
    })
  }


  return(
    <fieldset>
      <legend>Select a Keyboard to begin</legend>
      <select className="form-select kb-select-list mb-5 p-3 fs-5" onChange={handleSelectChange} value={optionsState}>
      {state.kbList && state.kbList.map(kb =>{
        return (
          <option key={kb.id} value={kb.id}>
            {kb.title}
          </option>
          )
      })}
      </select>
    </fieldset>
    
  )
}

export default KeyboardSelect;