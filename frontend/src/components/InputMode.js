const InputMode = ({isOn, handleToggle})=>{
  return(
    <div className="form-check form-switch">
      <input className="form-check-input checked" 
      type="checkbox" role="switch" 
      id="inputMode" onChange={handleToggle} checked={isOn}/>
      <label className="form-check-label" htmlFor="inputMode">
        <strong>Input Mode: {isOn ? `Simple Text`: `File Reader`}</strong>
      </label>
    </div>
  )
}

export default InputMode;