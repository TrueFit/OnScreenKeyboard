import { useContext } from "react";
import { KeyboardContext } from "../context/KeyboardContext";

export const useKeyboardContext = () => {
  const context = useContext(KeyboardContext)

  if(!context) {
    throw Error('useKeyboardContext must be used inside a KeyboardContextProvider')
  }

  return context;
}