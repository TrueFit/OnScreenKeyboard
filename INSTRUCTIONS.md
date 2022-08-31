# OnScreen Keyboard (Solution)
-------------------------------
(./src/assets/querty.webp)
## Introduction
Welcome to the OnScreen Keyboard solution submitted by Adrian Salazar. This is a submission for the coding challenge provided by TrueFit.io

## Approach
I started with a draft solution using vanilla JavaScript and basic HTML and CSS (Bootstrap 5 as the main framework). Once the proof of concept was completed, I then focused on converting it all to a React App. The React App uses an NodeJS package called json-server as part of the backend as I was afraid there would be too many constraints for folks to run.

### Algorithm
OnScreenKeyboard algorithm can successfully generate a list of directions based on a few assumptions - we can represent the keyboard as a fully connected graph, which is also a perfect square; the number of rows is the same as columns, thus using the variable size as the n number of rows would help us traverse at a linear time complexity (O(n)). The second assumption is that our input is a single dimension array or that we are able to flatten it to a single dimension. The final assumption is that our keyboards will always have unique values aside from the empty entries.

Simplest way to traverse is by determining how many steps should we move vertically followed by how many steps horizontally. This can be calculated by the formulas:
- delta_x = (x_index % size) - current_x_index
- delta_y = (x_index / size) - current_y_index

delta_x allows us to know how many times we should move to the left or right since it is easier to treat our coordinates similar to a cartesian map (e.g A= (0,0) and G = (0, 1)).
curr_x_index is extracted by simply using keyboardList.indexOf(character), thus making the graph extremely flexible to where it would not matter if the letter A was placed as the first one or as the 34th entry, indexOf would allow me to locate its coordinates with ease.
Finally once the delta variables were determined, I allowed the algorithm to add to a stack called 'results' the vertical and horizontal steps, what direction they were facing and how many times that ws applied. For example, A=>H would be D,R#. Vertical movement takes priority in order for the algorithm to return the correct format.
Finally, if the character was not present on the onscreen keyboard, the algorithm would simply ignore the entry. Now, depending on user requirements, it may be preferrable to handle these cases more gracefully like a warning message letting the user know the key was not valid, but since we are simulating more of a speech to text input, it would make sense to ignore special characters.

## Architecture
In order to create a visual example using React, I heavily used reducer hooks with actions defined in a context file called KeyboardContext. Reducers would allow me to not only dynamically request an output thru a textbox, but I could actually switch to use a file uploader instead and allow myself to select pre-existing keyboards. There is room for expanding this application by also adding the ability to add actions to manipulate the database directly.
I separated the database in a folder called 'backend', at the moment of writing, the backend consists only of a singular json file.
For the application, I used two folders: 'draft' and 'frontend', draft are the remains of the first implementation using vanilla tools.

in the frontend, React allowed to create a robust solution where i could create custom routes, hooks, helper functions, constant variables for actions in the context provider, and many components. Feel free to reach each and let me know if there are any places to improve.



## Running the code
- On VSCode (or using gitBash and/or other code editor) open two terminals on the root folder (/OnScreenKeyboard/)
- On the first terminal, use the following commands to start the backend:
  - cd .. 
  - cd backend/ 
  - json-server --watch db.json (note, you may need to run npm install -g json-server)
- On the second terminal, run the following commands:
  - npm install
  - npm start
  - This should allow you to install node_modules that were not included in the pull request as well as setting the local environment to use a different port from the database
