# Evaluation

Overall, I am happy with how my project turned out. I works as intended and meets all the requirements I initially wanted to include. I have also since added more game features, such as a loading screen while the computer places their boats to make it seem to the user that the computer was actually picking its location.
I could have done something similar with the comptuers turn during the actual game loop, however this would make the game feel more tedious since the user would have to wait every single turn, when it could be instant.


I am happy with how the menu's turned out, since it eliminates any chance of the user trying to break the coordinate selection, and also makes it easier for the user to select coordinates as its visual rather than having the user count rows and columns every turn which might lead to them miss counting. Aditionally, placing boats would be a whole new challenge if I didn't have this type of grid selection as the user would have to enter up to 3 coordinates for each boat, to place each cell which would lead to lots of validity checks of every coordinate selected and make it hard to keep track of for the user.


Looking back at my code, I could have menuSelection = DisplayMenu rather than using menuSelection by reference, which just tidies up the code and makes it look cleaner, as I don't set menuSelection to 0 and then change it anyways and it makes it more clear what DisplayMenu is doing rather than just being a line in the code, but having menuSelection by reference still works to update it in Main.


Making the validation for the selection of the users boats, along with the placing of the computers boats took much longer than expected, mainly due to me not visualising how I wanted to do it before I started coding it, so this resulted in lots of back tracking and road blocks where I felt totally stuck, however in the end I did end up having a solution, especially for the direction of the boats, that I feel proud with.


Implementing the rotation of boats when placing them for the user was a challenge as I had to think about mapping the current boats coordinates to the ones to be rotated to, along with making sure that the new coordinates are inside the grid and not overlapping any other boats. I ended up having to come back to it once I had finished the rest of the coding as it was taking me too long to do and I felt like I could be more productive just by coming back to it.


Making the save / load game parts of the code was a bit challenging as I had to learn how to use binary writer and reader, and making the functions that writes the data match the functions that reads the data took me a bit of time to understand and implement properly.


Making the functions that make the comptuer know where to go after hitting a boat took some time as I wanted the computer to play like how a human would play. Particularly the part of code that makes the computer loop back to the other side of the 3 length boat if it hit the middle, and went down off the edje of the boat took some time, however in the end I a happy with how it turned out. Admittedly the computer does just make a new random coordinate if it hits a boat and then goes the wrong way and misses, however coding this would take me too long and I would like to get this project submitted on time.
