import React from 'react';

const DisplayShows = ({ shows, calculate }) => {
  let displayShows = shows.map((show, i) => {
    return <option key={i} value={show.id}>{show.title}</option>;
  });
  
  return (
    <select onChange={calculate}>
      <option value="">Select a show</option>
      {displayShows}
    </select>
  );                  
};

export default DisplayShows;