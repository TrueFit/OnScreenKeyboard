const KeyboardButton = ({kb}) =>{
  return(
    <button className="col btn btn-outline-dark btn-lg keyButton"
    type="button">
      {kb}
    </button>
  )
}

export default KeyboardButton;