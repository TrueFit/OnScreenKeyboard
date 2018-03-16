import string
import sys

class Keypad:

  def __init__(self):
    self.letters = list(string.ascii_uppercase)
    self.numbers = self.swap_zero(list(string.digits))
    self.letters_and_numbers = self.letters + self.numbers
    self.keypad = list()
    self.output = str()

  def swap_zero(self, numbers: list) -> list:
    """ Moves the 0 in a list of digits to the last index """
    numbers.append(numbers[0])
    del(numbers[0])
    return numbers

  def generate(self) -> list:
    """ Creates a multi-dimensional list that represents the keypad """
    for i in self.letters_and_numbers:
      for y in range(0, 6):
        temp_list = self.letters_and_numbers[0:6]
        self.keypad.append(temp_list)
        del(self.letters_and_numbers[0:6])
    return self.keypad
  
  def get_coordinates(self, character:string) -> tuple:
    """ Assigns a coordinate tuple for the character provided in the list """
    for index in range(len(self.keypad)): # Switch to try/catch block later
      if character in self.keypad[index]:
        x = index
        y = self.keypad[index].index(character)
        coordinate = (int(x), int(y))
        return coordinate
  
  def calculate_path(self, start:str, finish:str) -> str:
    """ Determine the keypad movement between two coordinates """
    starting_point = self.get_coordinates(start)
    ending_point = self.get_coordinates(finish)
    if starting_point[0] > ending_point[0]:
      x_distance = 'U,' * abs(ending_point[0] - starting_point[0])
    else:
      x_distance = 'D,' * abs(starting_point[0] - ending_point[0])
    if starting_point[1] > ending_point[1]:
      y_distance = 'L,' * abs(ending_point[1] - starting_point[1])
    else:
      y_distance = 'R,' * abs(ending_point[1] - starting_point[1])
    return x_distance + y_distance + '#,'

k = Keypad()
k.generate()
test_string = 'A' + "IT CROWD"

for index in range(len(test_string) -1):
  start = test_string[index]
  end = test_string[index + 1]
  if end == ' ':
    k.output += 'S,'
    end = test_string[index + 2]
    k.output += k.calculate_path(start, end)
  elif start == ' ':
    start = test_string[index + 1]
    k.output += k.calculate_path(start, end)
  else:
    k.output += k.calculate_path(start, end)
print(k.output[0:-1])


# Bugs and fixes
# Remove extra '#' that occurs in test string 'IT CROWD'
# Create input file
# Change loop structure based on file input
  
    