# Documented Design

//View project flowchart here: [Flowchart Diagram](enter link to diagram here)

Key:
- FUNCTION = this needs to be its own function (outside of Main)
- MAIN - this can be done inside Main
- "-" = this task needs to be done
- "x" = this task has been completed

PROGRAM DESIGN TODO LIST: 
- FUNCTION Create menu with cursor

- MAIN If NEW GAME:
  - FUNCTION Create save file if none exists
  - FUNCTION If previous game save exists with data inside: Ask user if they want to restart
  
- MAIN If LOAD GAME:
  -FUNCTION Load saved game data
 
- MAIN Create 8x8 grid for the player's boats
- MAIN Create a blank 8x8 target tracker
- MAIN Create 8x8 grid for computer's boats

- FUNCTION Initialise 8x8 grid
x FUNCTION Display the grid
- FUNCTION Place computer boats
  - Have the system wait for 1 second while it displays a loading animation (loading. -> loading.. -> loading...) to make computer selection feel realistic
- FUNCTION Let user place boats

- FUNCTION Make computer place its boats

- FUNCTION Let the user have their turn
- FUNCTION Make computer have its turn

- FUNCTION Save all data of the game to a read-only .txt file using structs after each turn **OR USE A BIN FILE**

- MAIN Use a while loop to make the game repeat until there is a winner (also save this to the save data file)


Key Elements:
