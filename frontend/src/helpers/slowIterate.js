/**Helper function used to slow down iterations */
const slowIterate = (millisecs) =>{
  return new Promise(resolve => setTimeout(resolve, millisecs));
}

export default slowIterate;