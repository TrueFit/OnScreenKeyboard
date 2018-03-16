import string
import sys


class Keypad:

    def __init__(self, user_input:str):
        self.letters = list(string.ascii_uppercase)
        self.numbers = self.swap_zero(list(string.digits))
        self.letters_and_numbers = self.letters + self.numbers
        self.keypad = self.generate()
        self.user_input = 'A' + user_input # 'A' starts the output string at (0,0)
        self.output = str()

        # Output the directions
        for index in range(len(self.user_input) - 1):
            start = self.user_input[index]
            end = self.user_input[index + 1]
            if end == ' ':
                self.output += 'S,'
                # start = self.user_input[index]
                # end = self.user_input[index + 2]
                # self.output += self.calculate_path(start, end) +'#,'
            elif start == ' ':
                start = self.user_input[index - 1]
                end = self.user_input[index + 1]
                self.output += self.calculate_path(start, end) +'#,'
            else:
                # start = self.user_input[index + 1]
                # end = self.user_input[index + 2]
                self.output += self.calculate_path(start, end) +'#,' 
        print(self.output[0:-1]) # -1 to remove trailing comma
    
    # def _input(self, user_input:str) ->  str:
    #   return user_input

    def swap_zero(self, numbers: list) -> list:
        """ Moves the 0 in a list of digits to the last index """
        numbers.append(numbers[0])
        del(numbers[0])
        return numbers

    def generate(self) -> list:
        """ Creates a multi-dimensional list that represents the keypad """
        keypad = list()
        for i in self.letters_and_numbers:
            for y in range(0, 6):
                temp_list = self.letters_and_numbers[0:6]
                keypad.append(temp_list)
                del(self.letters_and_numbers[0:6])
        return keypad

    def get_coordinates(self, character: string) -> tuple:
        """ Assigns a coordinate tuple for the character provided in the list """
        for index in range(len(self.keypad)):
            if character in self.keypad[index]:
                x = index
                y = self.keypad[index].index(character)
                coordinate = (int(x), int(y))
                return coordinate

    def calculate_path(self, start: int, finish: int) -> str:
        """ Determine the keypad movement between two coordinates """
        starting_point = self.get_coordinates(start)
        ending_point = self.get_coordinates(finish)
        if starting_point[0] >= ending_point[0]:
            x_distance = 'U,' * abs(ending_point[0] - starting_point[0])
        else:
            x_distance = 'D,' * abs(starting_point[0] - ending_point[0])
        if starting_point[1] >= ending_point[1]:
            y_distance = 'L,' * abs(ending_point[1] - starting_point[1])
        else:
            y_distance = 'R,' * abs(ending_point[1] - starting_point[1])
        return x_distance + y_distance


# Bugs and fixes
# Remove extra '#' that occurs in test string 'IT CROWD'
# Create input file
# Change loop structure based on file input
# comma delimit programmatically
