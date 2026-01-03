Enums and structs are used to store the state of each cell on the user and computer grids, containing data about the boat type, and cell state. Lines 9 - 30

2D arrays are used to store the data of each grid - the user and computer having a grid of my struct to store their boats and the user having an aditional char 2D array, used as a target tracker. Initialised on lines 43 - 45 (Methods used to initialse them on lines 545 - 577)

A custom menu is used to help the user select their choice without having them be able to select anything that is not wanted. Lines 728 - 758

A switch statement is used, using the result of the menu, to select which option to use rather than using an else if chain. Lines 51 - 71

Binary writer / reader is used to save the progress. Lines 422 - 563

Turn based game loop is used to keep the game running until someone wins, swapping between user and computer turns each cycle. Lines 126 - 170

A win check method is used to count sunk cells on both grids and decide if the user or computer has won. Lines 106 - 125

Random numbers are used for the computer’s shooting and boat placement so the game isn’t predictable every time. Lines 35 - 41, 801 - 911

The computer turn has validation to stop it shooting the same place twice by re-rolling coords until it finds an empty cell. Lines 171 - 181

An “adjacent shot” system is used so if the computer hits, it then tries nearby cells instead of going fully random again. Lines 182 - 223

Directional arrays (row/col offsets) are used to simplify moving around the grid for both shooting logic and boat placement. Lines 197 - 213, 232 - 247, 830 - 853

A method is used to check if the computer hit and then update hit/miss/sunk states, including sinking full multi-cell boats using boat IDs. Lines 280 - 330

The user turn uses a cursor selector instead of typing coordinates, making it harder to input invalid shots. Lines 331 - 351, 621 - 727

Arrow key movement is used with bounds checking so the cursor can’t go off the grid. Lines 668 - 713

A “find next empty cell” system is used so the cursor skips over already-shot cells and only lands on valid targets. Lines 729 - 804

A convert-to-char method is used so the grid display is based on cell data, keeping UI separate from game logic. Lines 806 - 836

A display grid method is used to print the user grid neatly every turn. Lines 838 - 858

Instructions are loaded from a text file, with a check to show an error if the file is missing. Lines 760 - 789

Async / await is used for a loading animation so the computer placing boats feels smoother. Lines 779 - 800, 801 - 815

Computer boat placement is split into separate methods per boat type to keep it organised and easy to change. Lines 801 - 911

A placement validation method is used to stop submarines/carriers going out of bounds or overlapping existing boats. Lines 830 - 853

User boat placement is interactive, letting the user move a highlighted boat around and press R to rotate it. Lines 947 - 1025

Overlap checks are used during user placement to stop placing boats on top of each other. Lines 926 - 946

Boat IDs are used so multi-cell ships are treated as one boat, letting hits be counted and sunk properly across all its cells. Lines 25 - 33, 280 - 330, 352 - 421, 912 - 1073
