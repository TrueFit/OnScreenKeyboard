Simple OKeyboard
-----------------------------------------------------
Create and visualize keyboard traversal paths for Voice to Text searches.

Project Details
-----------------------------------------------------
The heart of the project is located within Simple's library. The library itself is broken up into 3 major parts: Keyboard implementation, Logging, and the Search service. 

The Keyboard Implementation interface allows any keyboard maker to create their own keyboard implementations. An example of this is
the TrueFit keyboard implementation. 

The Logger interface allows the user to switch out logging endpoints. This project uses a flat file implementation, but allows for database, xml, or any ohter
data storage service solution.

Lastly, the Search service harnesses the specific keyboard implementation to return the expected traversal path given a search term.

Using interfaces and a simple bridge pattern, replacing the default implemenation (TrueFit) with another keyboard implementatino becomes trivial. The use of interfaces
ensures a common set of methods, and as long as those methods are implemented correctly, the bridge pattern will allow it to be "plugged in" with minimal effort.

Simple Console App
-----------------------------------------------------
The Simple project is a console application that demonstrates the library's functionality. 

Simple Web Application
-----------------------------------------------------
The Simple Web Application demonstrates the library's functionality in a more visual way. The desired keyboard is generated
graphically within the user's browser and the path is animated using simple javascript animation techniques. 
