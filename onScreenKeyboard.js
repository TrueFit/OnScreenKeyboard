/**
 * Will process a set of newLine seperated search terms contained within a file passed using setFile method.
 *
 * @param type {method} callback A callback method used to handle output.
 * @param type {method} error A callback method used to handle error message.
 */
function OnScreenKeyboard(callback, error) {
	
	/**
	 * Object containing all the x and y cooridinates for each character on the keyboard.
	 */
    this.keyboard = {
		"a" : { "x" : 0, "y" : 0 },
		"b" : { "x" : 1, "y" : 0 },
		"c" : { "x" : 2, "y" : 0 },
		"d" : { "x" : 3, "y" : 0 },
		"e" : { "x" : 4, "y" : 0 },
		"f" : { "x" : 5, "y" : 0 },
		"g" : { "x" : 0, "y" : 1 },
		"h" : { "x" : 1, "y" : 1 },
		"i" : { "x" : 2, "y" : 1 },
		"j" : { "x" : 3, "y" : 1 },
		"k" : { "x" : 4, "y" : 1 },
		"l" : { "x" : 5, "y" : 1 },
		"m" : { "x" : 0, "y" : 2 },
		"n" : { "x" : 1, "y" : 2 },
		"o" : { "x" : 2, "y" : 2 },
		"p" : { "x" : 3, "y" : 2 },
		"q" : { "x" : 4, "y" : 2 },
		"r" : { "x" : 5, "y" : 2 },
		"s" : { "x" : 0, "y" : 3 },
		"t" : { "x" : 1, "y" : 3 },
		"u" : { "x" : 2, "y" : 3 },
		"v" : { "x" : 3, "y" : 3 },
		"w" : { "x" : 4, "y" : 3 },
		"x" : { "x" : 5, "y" : 3 },
		"y" : { "x" : 0, "y" : 4 },
		"z" : { "x" : 1, "y" : 4 },
		"1" : { "x" : 2, "y" : 4 },
		"2" : { "x" : 3, "y" : 4 },
		"3" : { "x" : 4, "y" : 4 },
		"4" : { "x" : 5, "y" : 4 },
		"5" : { "x" : 0, "y" : 5 },
		"6" : { "x" : 1, "y" : 5 },
		"7" : { "x" : 2, "y" : 5 },
		"8" : { "x" : 3, "y" : 5 },
		"9" : { "x" : 4, "y" : 5 },
		"0" : { "x" : 5, "y" : 5 }
	};

	/**
	 * A set of x and y cooridinates for the position of the cursor on the keyboard.
	 */
    this.cursor = { "x" : 0, "y" : 0 };

	/**
	 * String conataining the formatted keyboard commands for output.
	 */
    this.output = "";
    
    this.callback = callback;
    this.error = error;
}

(function() {

	/**
	 * Sends file contents to be processed if they are of a valid type, otherwise will call error method.
	 *
	 * @param type {String} file Path to file containing newLine seperated search terms.
	 */
	this.setFile =  function(file) {
		var textType = /text.*/
		var self = this;
	    if (file.type.match(textType)) {
	        var reader = new FileReader();

	        reader.onload = function(e) {
	            self.processInput(reader.result, self.callback);
	        }

	        reader.readAsText(file);    
	    } else {
	        error("File not supported!");
	    }
	};

	/**
	 * Processes keyboard commands and call a provided callback method with resulting output.
	 *
	 * @param type {String} input Contents of selected file.
	 */
	this.processInput = function(input) {
		for(var i = 0; i < input.length; i++) {
			var character = input.charAt(i);
			var position = this.keyboard[character.toLowerCase()];
			if (position) {
				this.processY(position.y);
				this.processX(position.x);
				this.processSelect();
			} else {
				this.processNewLine();
			}
		}
		this.output = this.output.split("").join(",");
		this.callback(this.output);
	};

	/**
	 * Computes horizontal position of cursor and adds necessary L and R commands to output string.
	 *
	 * @param type {String} x Horizontal position of character on keyboard.
	 */
	this.processX = function(x) {
		var currentX = this.cursor.x;
		var xmove = currentX - x;
		var direction = xmove > 0 ? 'L' : 'R';
		this.output += direction.repeat(Math.abs(xmove));
		this.cursor.x = x;
	};

	/**
	 * Computes vertical position of cursor and adds necessary U and D commands to output string.
	 *
	 * @param type {String} y vertical position of character on keyboard.
	 */
	this.processY = function(y) {
		var currentY = this.cursor.y;
		var ymove = currentY - y;
		var direction = ymove > 0 ? 'U' : 'D';
		this.output += direction.repeat(Math.abs(ymove));
		this.cursor.y = y;
	};

	/**
	 * Adds a space command to output string.
	 */
	this.processNewLine = function() {
		this.output += "S";
	};

	/**
	 * Adds a select command to output string.
	 */
	this.processSelect = function() {
		this.output += "#";
	};

}).call(OnScreenKeyboard.prototype);
