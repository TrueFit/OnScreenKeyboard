import { createContext, useReducer } from 'react';
import ACTIONS from '../constants/Keyboard';

export const KeyboardContext = createContext();

export const keyboardReducer = (state, action)=>{
  switch(action.type){
    case ACTIONS.SET_KEYBOARD:
      return {
        ...state, keyboard: action.payload
      }
    case ACTIONS.SET_KEYBOARD_LIST:
      return{
        ...state, kbList: action.payload
      }
    case ACTIONS.INPUT_TEXT_FIELD:
      return{
        ...state, kbInput: action.payload
      }
    case ACTIONS.OUTPUT_TEXT:
      return{
        ...state, kbOutput: action.payload
      }
    default:
      return state;
  }
}

export const KeyboardContextProvider = ({children}) =>{
  const [state, dispatch] = useReducer(keyboardReducer, {
    keyboard: {
      id: "default",
      title: "Default Keyboard",
      buttons:["A", "B", "C", "D", "E", "F", 
               "G","H", "I", "J", "K", "L", 
               "M","N", "O", "P", "Q", "R", 
               "S","T", "U", "V", "W", "X", 
               "Y","Z", "1", "2", "3", "4", 
               "5", "6", "7", "8", "9", "0"],
      size:4
    },
    kbList: null,
    kbInput: "",
    kbOutput: ""
  });

  return(
    <KeyboardContext.Provider value={{state, dispatch}}>
      {children}
    </KeyboardContext.Provider>
  )
}