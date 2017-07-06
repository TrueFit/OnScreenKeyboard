var http = require('http');
var fs = require('fs');
var hashTable = require('./hashTable');


http.createServer(function (req, res){
    var allTheCoordinates = [];
    var directions = [];
    
    function getCoordinates (character){         
      let row = hashTable[character]["row"];
      let col = hashTable[character]["col"];

      return [row, col];
  }

  function checkMatch (row1, col1){
    let match = false;
    
    Object.keys(hashTable).forEach(function (val, index){
      if (parseInt(hashTable[val]["row"]) == row1 && parseInt(hashTable[val]["col"]) == col1){
        match = true;
        console.log('#');
      }
    });
    
    return match;
  }

function getDirections (row1, col1, rowDiff, colDiff){
  let moves = [];
  let select = checkMatch(row1, col1);
  
  if (select){
    moves = [...moves, '#'];
  }
  
  // rows
  for (let x = 0; x < rowDiff; x++){
    if (Math.sign(rowDiff)){
      moves = [...moves, 'D'];
      console.log('D');
    }
    else if (!Math.sign(rowDiff)){
      moves = [...moves, 'U'];
      console.log('U');
    }
  }
  
  // cols
  for (let y = 0; y < colDiff; y++){
    if (Math.sign(colDiff)){
      moves = [...moves, 'R'];
      console.log('R');
    }
    else if (!Math.sign(colDiff)){
      moves = [...moves, 'L'];
      console.log('U');
    }
  }
  
  return moves;
}


// testing - will read text file for titles
allTheCoordinates = [...allTheCoordinates, getCoordinates("A")];
allTheCoordinates = [...allTheCoordinates, getCoordinates("L")];
allTheCoordinates = [...allTheCoordinates, getCoordinates("F")];



fs.readFile('./shows.txt', function (err, data){    
    res.writeHead(200, {'Content-Type': 'application/json'});
    let characters = data.toString().split("");

    characters.map(c => {
        allTheCoordinates = [...allTheCoordinates, getCoordinates(c)];
        return res.write(data + ' ' + directions);
    });
    //res.write(data + ' ' + directions);    
    res.end();
    
    console.log(data.toString());
    if (err){
        console.log(err);
    }
    
    console.log(data.toString());    
});

// testing
/*var readable = fs.createReadStream('./shows.txt', {
    encoding: 'utf8',
    fd: null
});

readable.on('readable', function (){
    var chunk;
    while (null !== (chunk = readable.read(1))){
        //allTheCoordinates = [...allTheCoordinates, getCoordinates(chunk)];
        getCoordinates(String(chunk));
        console.log(chunk);
    }
});*/

// testing


/*shows.map(c => {
    return allTheCoordinates = [...allTheCoordinates, getCoordinates(c)];
});*/

/*for (let i = 0; i < allTheCoordinates.length - 1; i++){
  let row1 = parseInt(allTheCoordinates[i].slice(0, 1));
  let row2 = parseInt(allTheCoordinates[i + 1].slice(0, 1));
  let col1 = parseInt(allTheCoordinates[i].slice(1));
  let col2 = parseInt(allTheCoordinates[i + 1].slice(1));
  
  let rowDiff = row2 - row1;
  let colDiff = col2 - col1;
  
  directions = [...directions, ...getDirections(row1, col1, rowDiff, colDiff)];
}*/

allTheCoordinates.forEach(function (val, index){
  if (index < allTheCoordinates.length - 1){
    let row1 = parseInt(allTheCoordinates[index].slice(0, 1));
    let row2 = parseInt(allTheCoordinates[index + 1].slice(0, 1));
    let col1 = parseInt(allTheCoordinates[index].slice(1));
    let col2;

    if (index != allTheCoordinates.length){
      col2 = parseInt(allTheCoordinates[index + 1].slice(1));  
    }
    else {
      col2 = parseInt(allTheCoordinates[index].slice(1));
    }
  
  let rowDiff = row2 - row1;
  let colDiff = col2 - col1;
  
  directions = [...directions, ...getDirections(row1, col1, rowDiff, colDiff)];  
  }
});

console.log(directions);
//res.writeHead(200, {'Content-Type': 'application/json'});
//res.write(directions);
//res.end();


// testing getDirections
//getDirections(1, 4);

}).listen(3000);



