import { useEffect } from 'react';
import Section from '../components/Section';
import Keyboard from '../components/Keyboard';
import {useKeyboardContext} from '../hooks/useKeyboardContext';
import ACTIONS from '../constants/Keyboard';

const KeyboardList = () =>{
  const {state, dispatch} = useKeyboardContext();

  useEffect(() => {
    const fetchWorkouts = async () => {
      const response = await fetch('http://localhost:3000/keyboards')
      const json = await response.json()
      console.log(json);
      if (response.ok) {
        dispatch({type: ACTIONS.SET_KEYBOARD_LIST, payload: json})
      }
    }
    fetchWorkouts()
  }, [dispatch])


  return(
    <main className='container mt-5'>
      <div className="row g-2">
        <Section id="kb-list" colSize="8">
          <h2>Available Keyboards</h2>
          <hr/>
          {state.kbList && state.kbList.map(kb =>{
            return (<Keyboard keyboard={kb} key={kb.id}></Keyboard>)
          })}
        </Section>
      </div>
    </main>
  )
}

export default KeyboardList;