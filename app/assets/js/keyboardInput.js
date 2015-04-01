$(document).ready(function () {
   
        
   
});


function getAllPaths(){
    var allLinesString = document.getElementById('fileDisplayArea').innerText;
    var arrayOfLines = allLinesString.split("\n");
    

    var arrayOfOutputs = [];
    
        // arrayOfOutputs.push(currentLineOutput);
      

    $.each(arrayOfLines, function(index, currentLine){
        var currentLineOutput = getPath(currentLine);
        addRow(currentLine, currentLineOutput);

    })




    // var pathDisplayArea = document.getElementById('pathDisplayArea');
    // pathDisplayArea.innerText = result;
}

function addRow(input, output){
    var displayTable = $('#display-table-rows');
    displayTable.append($('<tr>')
    .append($('<td>').text(input))
    .append($('<td>').text(output))
    )
}

function getPath(input){
    var lowerCaseInput = input.toLowerCase();
    var charArray = lowerCaseInput.split('');

    var prevRow = 1;
    var prevCol = 1;

    var result = "";

    //Iterate through all characters of the input
    for (i = 0; i < charArray.length; i++) {
        //get the current row and col vals

        var currentRow = getRow(charArray[i]);
        var currentCol = getColumn(charArray[i]);

        //Case 1: Space
        if (currentCol == 0 || currentRow == 0) {
            result = result.concat("S,");
            //Case 2: Already on correct choice
        } else if (currentCol == prevCol && currentRow == prevRow) {
            result = result.concat("#,");
            //Case 3: Must move cursor
        } else {
            var colDif = currentCol - prevCol;
            var rowDif = currentRow - prevRow;


            //move down
            while (rowDif > 0) {
                result = result.concat("D,");
                rowDif--;
            }

            //move up
            while (rowDif < 0) {
                result = result.concat("U,");
                rowDif++;
            }

            //move left
            while (colDif < 0) {
                result = result.concat("L,");
                colDif++;
            }

            //move right
            while (colDif > 0) {
                result = result.concat("R,");
                colDif--;
            }

            //hit select for this character
            result = result.concat("#,");
            
        //set prevRow and prevCol to current 
        prevCol = currentCol;
        prevRow = currentRow;
    }
}
    //chop off the final comma
    result = result.substring(0, result.length - 1);


    return result;
}





//Helper Methods

function getColumn(c) {
    var result = 0;
    switch (c) {
        case 'a':
        case 'g':
        case 'm':
        case 's':
        case 'y':
        case '5':
        result = 1;
        break;
        case 'b':
        case 'h':
        case 'n':
        case 't':
        case 'z':
        case '6':
        result = 2;
        break;
        case 'c':
        case 'i':
        case 'o':
        case 'u':
        case '1':
        case '7':
        result = 3;
        break;
        case 'd':
        case 'j':
        case 'p':
        case 'v':
        case '2':
        case '8':
        result = 4;
        break;
        case 'e':
        case 'k':
        case 'q':
        case 'w':
        case '3':
        case '9':
        result = 5;
        break;
        case 'f':
        case 'l':
        case 'r':
        case 'x':
        case '4':
        case '0':
        result = 6;
        break;
        default:
            result = 0; //for a space
        }
        return result;
    }

      function getRow(c) {
        var result = 0;
        switch (c) {
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            result = 1;
            break;
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            result = 2;
            break;
            case 'm':
            case 'n':
            case 'o':
            case 'p':
            case 'q':
            case 'r':
            result = 3;
            break;
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            result = 4;
            break;
            case 'y':
            case 'z':
            case '1':
            case '2':
            case '3':
            case '4':
            result = 5;
            break;
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '0':
            result = 6;
            break;
            default:
            result = 0; //for a space
        }
        return result;
    }