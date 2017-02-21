/* Driver file for Truefit coding challenge
 * Will Stumpf
 *
 * Use CLI to run, leave arg blank for default file.
 * node driver.js <filename/string>
 *
 * node -v
 * v7.0.0
 */

var getPath = require('./path')

// call getPath to init program passing in the arguments from the command line
getPath(process.argv.slice(2))
