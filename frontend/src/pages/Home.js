import Section from '../components/Section';
import InputArea from '../components/InputArea';
import Keyboard from '../components/Keyboard';
import KeyboardSelect from '../components/KeyboardSelect';
import {useKeyboardContext} from '../hooks/useKeyboardContext';


const Home = () =>{
  const {state} = useKeyboardContext();
  return (
    <main id="main" className='container mt-5'>
      <div className="row g-2">
        <Section id="keyboard" colSize="8">
          
          <Keyboard keyboard={state.keyboard}></Keyboard>
        </Section>
        <Section id="kb-io" colSize="4">
          <KeyboardSelect kbList={state.kbList}></KeyboardSelect>
          <InputArea></InputArea>         
        </Section>
      </div>
    </main>
  )
}

export default Home;