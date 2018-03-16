from keypad import *

if __name__ == '__main__':
  with open('text.txt') as f:
    for line in f:
      read_data = line
      Keypad(read_data.strip())
  f.close()