import React, { Component } from 'react';
import axios from 'axios';
import DisplayShows from './DisplayShows';
import DisplayInfo from './DisplayInfo';
import hashTable from '../hashtable';

class App extends Component {
    constructor (props){
        super (props);

        this.loadData = this.loadData.bind(this);        
        this.calculate = this.calculate.bind(this);
        this.getDistance = this.getDistance.bind(this);
        this.getCoordinates = this.getCoordinates.bind(this);
        this.clearState = this.clearState.bind(this);

        this.state = {
            data: [],            
            letters: [],  
            lastPosition: undefined,          
            directions: [],        
            coordinates: [],                            
            hashTable                       
        };
    }
    loadData (){
      axios.get('../data/shows.json')
            .then(response => this.setState({ data: response.data }))
            .catch(err => console.log(err.toString()));            
    }
    getCoordinates (character){         
        let row = this.state.hashTable[character]["row"];
        let col = this.state.hashTable[character]["col"];

        let coord = this.state.coordinates;
        coord.push([row, col]);

        let theLetters = this.state.letters;
        theLetters.push(character);

        this.setState({ 
            letters: theLetters,
            coordinates: coord
        }, 
        () => console.log(this.state.coordinates));

        // testing
        console.log(this.state.letters);
    }
    calculate (e){
      // clear previous selection state
      this.clearState();

      if (e.target.value != ''){        
        let characters = this.state.data[e.target.value - 1].title.split("");

        characters.map(c => this.getCoordinates(c));        

        this.getDistance(); 
      }                    
    }
    clearState() {
        this.setState({                    
          directions: [],          
          coordinates: [],
          letters: [],
          lastPosition: undefined
        });
    }
    getDistance (){
        let { coordinates, hashTable, letters, lastPosition } = this.state;        

        let distance = [];

        if (letters[0] == 'A'){
            distance = [...distance, '#'];
            console.log("#");         
        }  

        // testing
        let last;
        let current;

        letters.forEach(function (val, index){
            last = letters[index - 1];
            current = letters[index];
        });

        // testing
        //for (let y = 0; y < letters.length - 1; y++){
            // testing
            //last = letters[y];
            
            /*let matches = [];
            let match = Object.keys(hashTable)
                .filter(k => k == letters[y])
                .map(k => matches = [...matches, k]);*/

            

            //alert(matches);            
        //}

        // testing        
        this.setState({ lastPosition: last });
        console.log(lastPosition);

        for (let i = 0; i < coordinates.length - 1; i++){
            let row1 = parseInt(coordinates[i].slice(0, 1));
            let row2 = parseInt(coordinates[i + 1].slice(0, 1));
            let col1 = parseInt(coordinates[i].slice(1));
            let col2 = parseInt(coordinates[i + 1].slice(1));

            let rowDiff = row2 - row1;
            let colDiff = col2 - col1;

            if (letters[i] == " "){
                distance = [...distance, 'S'];
                console.log('S');
                alert('S');
            }
            else {

            //alert('row: ' + rowDiff + ' - col: ' + colDiff);           

            //if (row1 == row2){                
                for (let j = 0; j < colDiff; j++){
                    //if (Math.sign(colDiff)){
                    if (colDiff > 0){
                        distance = [...distance, 'R'];
                        console.log('R');
                    }
                    else {
                        distance = [...distance, 'L'];
                        console.log('L');
                    }  

                    // testing
                    if (j == colDiff - 1){
                        distance = [...distance, "#"];
                        console.log("#");
                    }                  
                }   
            //}
            //else {                
                for (let x = 0; x < rowDiff; x++){
                    //if (Math.sign(rowDiff)){
                    if (rowDiff > 0){
                        distance = [...distance, 'D'];
                        console.log('D');
                    }
                    else {
                        distance = [...distance, 'U'];
                        console.log('U');
                    }  

                    // testing
                    if (x == rowDiff - 1){
                        distance = [...distance, "#"];
                        console.log("#");
                    }                                     
                }                

                /*if (coordinates == [row1, col1]){
                    distance = [...distance, '#'];
                    console.log('#');
                }*/                
            //}
            }
        }    

        this.setState({ directions: distance });
    }    
    componentDidMount (){        
      this.loadData();      
    }
    render() {
        const { 
          data,          
          directions,            
        } = this.state;        

        return (
            <div>
              <DisplayShows 
                shows={data} 
                calculate={this.calculate} 
              />
              <br />
              <DisplayInfo 
                directions={directions}                 
              />
            </div>
        );
    }
}

export default App;
