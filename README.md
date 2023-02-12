# Turtle game

The objective of the game is to make the turtle escape a mined field reading moves that were pre set.
To run the turtle game you need two JSON files, the settings file and the moves file. The settings file contains the settings to create the game, the settings are:

- Board size, each value (X for width and Y for height) must be  1 <= N <=10000.
- Initial position, the X and Y coordinates and the facing direction of the starting point of the turtle. The direction must be N,E,S or W representing North, East, South and West.
- The X and Y coordinates of the location of the exit point.
- A list with the positions of the mines, each represented by the X and Y coordinates.

The moves files has a list of Move Sequences, and each move sequence has a list of moves. The moves are represented by the letter M (to go forward in the facing direction one square) or R (to rotate 90 degrees to the right).

A example of each file is included with the code, inside the examples folder.

Additional rules:
- Invalid moves (represented by other letters or moves that would leave the mined field) are ignored but the game keeps running.
- Extra moves (after the turtle blows up or escape the field are ignored).
