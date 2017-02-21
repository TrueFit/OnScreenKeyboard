/*
 * OnScreenKeyboard
 * Will Stumpf
 *
 * This program documents the path needed to select a specific string.
 */

const fs = require('fs')
const grid = [
  [ 'A', 'B', 'C', 'D', 'E', 'F' ],
  [ 'G', 'H', 'I', 'J', 'K', 'L' ],
  [ 'M', 'N', 'O', 'P', 'Q', 'R' ],
  [ 'S', 'T', 'U', 'V', 'W', 'X' ],
  [ 'Y', 'Z', '1', '2', '3', '4' ],
  [ '5', '6', '7', '8', '9', '0' ]
]
const ACTION = {
  UP: 'U',
  DOWN: 'D',
  LEFT: 'L',
  RIGHT: 'R',
  SPACE: 'S',
  SELECT: '#'
}

const DEFAULT_FILE = 'input.txt'
const ROW = 0
const COL = 1
const UNKNOWN = 'UNKNOWN'
const DEBUG = false

let response = []
let position = [0, 0]

/*
 * getPath
 * main function to get the path
 */
function getPath(input)
{
  // populate data from file or text input
  let data = selectInput(input)

  // loop through each line and apply the function to store the response
  data.forEach(function(line)
  {
    // ignore any line with no characters
    if (!line.length)
    {
      return;
    }

    // show user the line to search
    console.log(line, "\n")

    // get each character
    for (var i = 0; i < line.length; i++)
    {
      if (line[i] === ' ')
      {
        if (DEBUG)
        {
          console.log('space')
        }

        response.push('S')
      }
      else
      {
        let location = getIndex(line[i])
        if (DEBUG)
        {
          console.log(line[i], location)
        }

        if (location !== UNKNOWN)
        {
          translateAndSelect(location)
        }
      }

      if (DEBUG)
      {
        console.log("\n")
      }
    }

    // output
    console.log(response.join(), "\n\n")

    // reset current position for next word
    position = [0, 0]
    response = []

  });
}

/*
 * getIndex
 * gets the coordinates of the character we want to locate
 *
 * @param val - the value we want the index of
 */
function getIndex(val)
{
  for (var i = 0; i < grid.length; i++)
  { // row
    for (var j = 0; j < grid[i].length; j++)
    { // column
      if (grid[i][j] === val.toUpperCase())
      {
        return [i, j]
      }
    }
  }

  // can't find the value, return unknown to skip character
  return 'UNKNOWN'
}

/*
 * translateAndSelect
 * This function finds the path for desired selection
 *
 * @params stop_position - desired item position
 */
function translateAndSelect(stop_position)
{
  if (DEBUG)
  {
    console.log('translate from', position, 'to', stop_position)
  }

  moveCursor(0, ROW, stop_position)
  moveCursor(1, COL, stop_position)

  // check to see if we are there, if so, select
  if (position[0] == stop_position[0] && position[1] == stop_position[1])
  {
    response.push(ACTION.SELECT)
  }
}

/*
 * moveCursor
 *
 * given the index, axis, stop position increment the indexes
 * and record the directions until we reach the desired position
 *
 * @params index - x or y coords
 *         axis - the axis we are on
 *         stop - desired location
 */
function moveCursor(index, axis, stop)
{
  var difference = (position[index] - stop[index])
  var direction = getDirection(difference, axis)

  while (position[index] != stop[index])
  {
    response.push(direction)

    if (direction == ACTION.UP || direction == ACTION.LEFT)
    {
      position[index]--
    }
    else
    {
      position[index]++
    }
  }
}


/*
 * getDirection
 *
 * calculates the direction needed to go
 *
 * @params value - the amount we need to move
 *         axis - the axis we are on
 */
function getDirection(value, axis)
{
  if (axis == ROW)
  {
    return value < 0 ? ACTION.DOWN : ACTION.UP
  }
  else
  {
    return value < 0 ? ACTION.RIGHT : ACTION.LEFT
  }
}


/*
 * selectInput
 *
 * Takes in an argument from the user, if it is a file load it,
 * if its a string, parse it. if none given, use default.
 *
 * @params input - args from CLI
 */
function selectInput(input)
{
  if (input.length)
  {
    try
    {
      fs.statSync('./' + input).isFile() === true
      return (fs.readFileSync('./' + input, 'utf8')).split('\n')
    }
    catch (e)
    {
      if (e.code === 'ENOENT')
      {
        // file doesnt exist but there are args
        // lets take these and put them as a quick entry
        return input
      }
      else
      {
        console.log('error')
      }
    }
  }
  else
  {
    // no file/string defined so use default
    return (fs.readFileSync('./' + DEFAULT_FILE, 'utf8')).split('\n')
  }
}

module.exports = getPath
