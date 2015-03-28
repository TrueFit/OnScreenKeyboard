
// ProcessLine.js
// Contains 2 functions to process a line of text and output the result:
//  ProcessLine: input the line of text and the remote layouy and output the routing
//  findPosision: helper function to search the remote layout array for the row,col key of the target character.


//Find Char position in the array
function findPosition(itemText, remoteArray) {
    for (var i = 0; i < remoteArray.length; i++) {
        if (remoteArray[i].item === itemText) {
            var x = remoteArray[i].col;
            var y = remoteArray[i].row;

            return [x, y];
        }
    }
};

function processLine(strLine, aryRemote) {
    //setup Remote

    //Store Starting Coordinates (seeded to 0,0 or A with each new row of text)
    var startXAxisPos = 0;
    var startYAxisPos = 0;
    var strOutput = "";

    //Loop the line for each character
    for (var x = 0; x < strLine.length; x++) {
        try {
            //alert(line[x]);

            if (strLine[x] == " ") //it's a space
            {
                strOutput += 'S';
               
            }
            else {

                //Grab the next Char and upper case it to match the valid values on the remote.
                var character = strLine[x].toUpperCase();

                //Get the target chars position in the array
                var targetCharPosition = findPosition(character, aryRemote);

                //Store Target Coordinates returned from the function
                var targetXAxisPos = targetCharPosition[0];
                var targetYAxisPos = targetCharPosition[1];
               
                //Calc y-Axis Difference for horizontal movement. Should be a function.
                if (targetYAxisPos > startYAxisPos) //going down!
                {
                    //write a for loop to add to the movement string
                    for (var yd = targetYAxisPos - startYAxisPos; yd > 0; yd--) {
                        strOutput += 'D,';
                    }
                }
                if (startYAxisPos > targetYAxisPos) //going up!
                {
                    //write a for loop to add to the movement string
                    for (var yu = startYAxisPos - targetYAxisPos  ; yu > 0; yu--) {
                        strOutput += 'U,';
                    }
                }

                //Calc x-Axis Difference for horizontal movement. Should be a Function
                if (targetXAxisPos > startXAxisPos) //we're moving right 
                {
                    //add rights to the movement string
                    for (var xr = targetXAxisPos - startXAxisPos; xr > 0; xr--) {
                        strOutput += 'R,';
                    }
                }
                else if (startXAxisPos > targetXAxisPos) //we're moving left
                {
                    //add lefts to the movement string
                    for (var xl = startXAxisPos - targetXAxisPos; xl > 0; xl--) {
                        strOutput += 'L,';
                    }
                }


                //Destination Achieved - Select Character at Position
                strOutput += '#,';
              
            }
            //set new starting point to move from
            startXAxisPos = targetXAxisPos;
            startYAxisPos = targetYAxisPos;


        }
        catch (err) {
        }

    };

    return strOutput;
}






