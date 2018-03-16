import string

class Keypad:

  def __init__(self):
    self.letters = list(string.ascii_uppercase)
    self.numbers = self.swap_zero(list(string.digits))
    self.letters_and_numbers = self.letters + self.numbers
    self.keypad = list()

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
    print(self.keypad)
    for index in range(len(self.keypad)):
      if character in self.keypad[index]:
        x = index
        y = self.keypad[index].index(character)
        coordinate = (x, y)
        return coordinate

k = Keypad()
k.generate()
k.get_coordinates('0')