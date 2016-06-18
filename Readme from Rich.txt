Hello,

The following is my solution to the OnScreenKeyboard problem I received from Truefit.
I received the problem on the evening of Tuesday, June 14th and I am submitting the pull 
request at noon on Saturday, June 18th.
Thank you in advance for the time you spend reviewing my code.

The solution is written in C#/WPF and the VS2015 project files are included.  
Please note that .NET Framework 4.5.2 is required to run the binaries.

Input files can be found at:  $\OnScreenKeyboard\inputs
(the keyboard as defined in the example is stored in 'basickeyboard.xml')
An advanced QWERTY keyboard is in 'advancedqwertykeyboard.xml'
'voicecommands.txt' is the flat file of acceptable voice commands

Precompiled binaries can be found at: $\OnScreenKeyboard\release binaries
You want to run: $\OnScreenKeyboard\release binaries\Truefit.OnScreenKeyboard.exe

Operation should be fairly straightforward, but here is a primer:
- Load the overlay keyboard definition and the voice commands flat file by browsing to each path
  and clicking both the loads
- Loading the voice commands flat file will populate the remote control (on the right) with commands
  Clicking 'Execute' will move the remote cursor through the characters and generate a path
- You may also use the remote manually with the buttons on top
- 'Text Output' is the actual text that is selected by the remote interactions
- 'Optimal Path Output' is how the remote gets to each character from the last SELECTED character 
  **this is the CSV output requested by the original assignment**

Edge Cycling
- As a little bonus, I added the ability to "edge cycle" (I have no idea what the real term for this is, so I made this :) )
  Edge cycling just means that, rather than stopping on the sides of the keyboard, it will jump to the opposite side an cycle
  For example, if you have a 5x5 keyboard and are at column 1, row 1 and you try to go left, normally that is restricted.
  With edge cycling, you jump to column 5.  This is the way most onscreen keyboards work (for efficiency).
  You can turn on edge cycling with the "Cycle Edges" checkedit (it always defaults to off as it can effect the path).
  
If you have any issues spinning up the demo or sources, please contact me at:
richard.kisic@gmail.com
724-454-2685

Thanks again!
Best,
Rich  