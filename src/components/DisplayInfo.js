import React from 'react';

const DisplayInfo = ({ directions }) => {
  let displayDirections = directions.map((d, i) => {
    return <span key={i}>{`${d}, `}</span>;
  });
  
  return (
    <div>
      {directions.length > 0 &&<h3>Moves:</h3>}         
      <h2>{displayDirections}</h2>
    </div>
  );
};

export default DisplayInfo;