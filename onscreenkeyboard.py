#!/usr/bin/env python

# This script inputs a file containing lines of strings. It outputs the
# path required to type each line on the DVR keyboard.
# Written by Ben Lind, 11/23/14

def DVRKeyPath(inputString):
  "Returns the path required to type the passed string on the DVR keyboard"
  
  keyMap = [['A','B','C','D','E','F'],
          ['G','H','I','J','K','L'],
          ['M','N','O','P','Q','R'],
          ['S','T','U','V','W','X'],
          ['Y','Z','1','2','3','4'],
          ['5','6','7','8','9','0']]
  horizPos = 0 # init position marker to A
  vertPos = 0
  moves = [] # moves to take for each char
  
  for char in inputString.upper(): # loop through each char
    if char == ' ':
      moves.append('S')
    elif char.isalnum(): # only worry about alphanumeric chars
      for i,charGroup in enumerate(keyMap): # loop through keyMap char lines
        if char in charGroup:
          horizDiff = charGroup.index(char) - horizPos # horiz dist to new char
          vertDiff = i - vertPos # vert dist to new char
          
          if vertDiff < 0: # new char is above old char
            moves.extend(['U'] * abs(vertDiff))
          elif vertDiff > 0:
            moves.extend(['D'] * abs(vertDiff))
          
          if horizDiff < 0: # new char is left of old char
            moves.extend(['L'] * abs(horizDiff))
          elif horizDiff > 0:
            moves.extend(['R'] * abs(horizDiff))
          
          # set new starting position
          horizPos = charGroup.index(char)
          vertPos = i
          
          break # character found, go to next read character
      moves.append('#')
      
  return ','.join(moves)

with open("onscreenkeyboard.txt") as commands:
  for line in commands:
    print DVRKeyPath(line)
