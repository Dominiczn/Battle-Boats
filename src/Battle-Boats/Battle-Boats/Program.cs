using System;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Battle_Boats
{
    enum BoatType
    {
        Destroyer, //1 Cell
        Submarine, //2 Cells
        Carrier, //3 Cells
        None
    }
 
    enum CellState
    {
        Empty,
        Hit,
        Miss,
        Sunk
    }

    struct Cell
    {
        public BoatType Boat;
        public CellState State;
        public int BoatId; //-1 means no boat
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //For displaying the instructions
            Random random = new Random();

            int rows = 8;
            int cols = 8;
            int menuSelection = 0;

            Cell[,] computerGrid = InitialiseBoatGrid(rows, cols);
            Cell[,] userGrid = InitialiseBoatGrid(rows, cols);
            char[,] targetGrid = InitialiseTargetGrid(rows, cols);



            DisplayMenu(ref menuSelection);

            switch (menuSelection)
            {
                case 0:
                    //Start game
                    await NewGame(userGrid, computerGrid, targetGrid, rows, cols, random);
                    break;

                case 1:
                    //Resume game
                    break;

                case 2:
                    Console.WriteLine(DisplayInstructions());
                    break;

                case 3:
                    //Quit
                    break;
            }
        }

        static async Task NewGame(Cell[,] userGrid, Cell[,] computerGrid, char[,] targetGrid, int rows, int cols, Random random)
        {
            string text = "Select a cell to shoot at\n";
            PlaceUserBoats(userGrid, rows, cols);

            await PlaceComputerBoats(computerGrid, rows, cols, random);
            SaveGameProgress(userGrid, computerGrid, targetGrid, rows, cols);

            UserShootAtBoat(userGrid, computerGrid, targetGrid, rows, cols, ref text);

            //UserShootAtBoat(userGrid, computerGrid, targetGrid, rows, cols, ref text);

        }

        static void ComputerShootAtBoat(Cell[,] userGrid, Cell[,] computerGrid, char[,] targetTracker, int rows, int cols, ref string text, Random random)
        {
            int randomRow = random.Next(rows);
            int randomCol = random.Next(cols);

            if (userGrid[randomRow, randomCol].BoatId != -1)
            {
                int boatIdCounter = 0
            } 
        }

        static void UserShootAtBoat(Cell[,] userGrid, Cell[,] computerGrid, char[,] targetTracker, int rows, int cols, ref string text)
        {
            (int rowSelection, int colSelection) coordinates = TraverseTargetGrid(targetTracker, computerGrid, userGrid, rows, cols, text);

            int rowSelection = coordinates.rowSelection;
            int colSelection = coordinates.colSelection;
            
            if (computerGrid[rowSelection, colSelection].BoatId != -1)
            {
                int boatIdCounter = 0;
                computerGrid[rowSelection, colSelection].State = CellState.Hit;
                
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (computerGrid[row, col].BoatId == computerGrid[rowSelection, colSelection].BoatId && computerGrid[row, col].State == CellState.Hit)
                        {
                            boatIdCounter++;
                        }
                    }
                }


                if (computerGrid[rowSelection, colSelection].Boat == BoatType.Destroyer && boatIdCounter == 1)
                {
                    computerGrid[rowSelection, colSelection].State = CellState.Sunk;
                    text = "You sunk a ship!\n";
                }

                else { text = "You hit a ship!\n"; }
            }

            else
            {
                computerGrid[rowSelection, colSelection].State = CellState.Miss;
                text = "You missed!\n";
            }
        }

        /*
        static void DidBoatGetHit(Cell[,] grid, int rowHit, int colHit, string sunkText, string hitText, string missText, int rows, int cols)
        {
            if (grid[rowHit, colHit].BoatId != -1)
            {
                int boatIdCounter = 0;
                grid[rowHit, colHit].State = CellState.Hit;

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (grid[row, col].BoatId == grid[rowHit, colHit].BoatId && grid[row, col].State == CellState.Hit)
                        {
                            boatIdCounter++;
                        }
                    }
                }


                if (grid[rowHit, colHit].Boat == BoatType.Destroyer && boatIdCounter == 1)
                {
                    grid[rowHit, colHit].State = CellState.Sunk;
                    sunkText = "You sunk a ship!\n";
                }

                else { hitText = "You hit a ship!\n"; }
            }

            else
            {
                grid[rowHit, colHit].State = CellState.Miss;
                missText = "You missed!\n";
            }
        }

        */

        static void SaveGameProgress(Cell[,] userGrid, Cell[,] computerGrid, char[,] targetGrid, int rows, int cols)
        {
            string directoryLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BattleBoatsDominic");
            Directory.CreateDirectory(directoryLocation);
            string saveFileLocation = Path.Combine(directoryLocation, "BattleBoatsSaveData.bin");

            using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileLocation, FileMode.Create, FileAccess.Write)))
            {
                WriteCellGrid(writer, userGrid, rows, cols);
                WriteCellGrid(writer, computerGrid, rows, cols);
                WriteCharGrid(writer, targetGrid, rows, cols);
            }
        }

        static (Cell[,] userGrid, Cell[,] computerGrid, char[,] targetGrid) LoadGameProgress()
        {
            string directoryLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BattleBoatsDominic");
            Directory.CreateDirectory(directoryLocation);
            string saveFileLocation = Path.Combine(directoryLocation, "BattleBoatsSaveData.bin");

            if (!File.Exists(saveFileLocation))
            {
                Console.WriteLine($"\nOops, your save data couldn't be found \nExpected at {saveFileLocation}");
            }

            using (BinaryReader reader = new BinaryReader(File.Open(saveFileLocation, FileMode.Open, FileAccess.Read)))
            {
                Cell[,] userGrid = ReadCellGrid(reader);
                Cell[,] computerGrid = ReadCellGrid(reader);
                char[,] targetGrid = ReadCharGrid(reader);

                return (userGrid, computerGrid, targetGrid);
            }
        }

        static void WriteCellGrid(BinaryWriter writer, Cell[,] grid, int rows, int cols)
        {
            writer.Write(rows);
            writer.Write(cols);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Cell cell = grid[row, col];
                    writer.Write((byte)cell.Boat);
                    writer.Write((byte)cell.State);
                    writer.Write((byte)cell.BoatId);
                }
            }
        }


        static Cell[,] ReadCellGrid(BinaryReader reader)
        {
            int rows = reader.ReadInt32();
            int cols = reader.ReadInt32();

            Cell[,] grid = new Cell[rows, cols];
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    byte boat = reader.ReadByte();
                    byte state = reader.ReadByte();
                    byte boatId = reader.ReadByte();

                    grid[row, col] = new Cell
                    {
                        Boat = (BoatType)boat,
                        State = (CellState)state,
                        BoatId = boatId
                    };
                }
            }

            return grid;
        }

        static void WriteCharGrid(BinaryWriter writer, char[,] grid, int rows, int cols)
        {
            writer.Write(rows);
            writer.Write(cols);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols ; col++)
                {
                    writer.Write(grid[row,col]);
                }
            }
        }

        static char[,] ReadCharGrid(BinaryReader reader)
        {
            int rows = reader.ReadInt32();
            int cols = reader.ReadInt32();

            char[,] grid = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols ; col++)
                {
                    grid[row, col] = reader.ReadChar();
                }
            }

            return grid;
        }

        static Cell[,] InitialiseBoatGrid(int rows, int cols)
        {
            Cell[,] grid = new Cell[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col] = new Cell
                    {
                        Boat = BoatType.None,
                        State = CellState.Empty,
                        BoatId = -1
                    };
                }
            }

            return grid;

        }
        static char[,] InitialiseTargetGrid(int rows, int cols)
        {
            char[,] grid = new char[rows, cols];

            for (int row = 0; row < grid.GetLength(0);  row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = '~';
                }
            }

            return grid;
        }

        static void DisplayTargetGrid(char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write($"{grid[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        static (int rowSelection, int colSelection) TraverseTargetGrid(char[,] targetGrid, Cell[,] computerGrid, Cell[,] userGrid, int rows, int cols, string text)
        {
            ConsoleKey key = ConsoleKey.None;
            int rowSelection = 0;
            int colSelection = 0;

            do
            {
                Console.Clear();
                Console.WriteLine(text);
                Console.WriteLine("Target tracker:");

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (row == rowSelection && col== colSelection)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ResetColor();
                        }

                        Console.Write($"{targetGrid[row, col]} ");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Your grid:");
                DisplayCellGrid(userGrid, rows, cols);

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && rowSelection > 0 && computerGrid[rowSelection - 1, colSelection].State == CellState.Empty) { rowSelection--; }
                else if (key == ConsoleKey.DownArrow && rowSelection < rows - 1 && computerGrid[rowSelection + 1, colSelection].State == CellState.Empty) { rowSelection++; }
                else if (key == ConsoleKey.LeftArrow && colSelection > 0 && computerGrid[rowSelection, colSelection - 1].State == CellState.Empty) { colSelection--; }
                else if (key == ConsoleKey.RightArrow && colSelection < cols - 1 && computerGrid[rowSelection, colSelection + 1].State == CellState.Empty) { colSelection++; }
            }
            while (key != ConsoleKey.Enter);

            return (rowSelection, colSelection);
        }

        static char ConvertCellToChar(Cell cell)
        {
            if (cell.Boat == BoatType.None)
            {
                return '~';
            }

            else if (cell.Boat == BoatType.Destroyer)
            {
                return 'D';
            }

            else if (cell.Boat == BoatType.Submarine)
            {
                return 'S';
            }

            else if (cell.Boat == BoatType.Carrier)
            {
                return 'C';
            }

            return '#';
        }

        static void DisplayCellGrid(Cell[,] grid, int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write($"{ConvertCellToChar(grid[row,col])} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n\n\n");
        }

        static void DisplayIdGrid(Cell[,] grid, int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write($"{grid[row, col].Boat} ");
                }
                Console.WriteLine();
            }
        }

        static void DisplayMenu(ref int selection)
        {
            string[] options = ["NEW GAME", "RESUME GAME", "READ INSTRUCTIONS", "QUIT"];
            ConsoleKey key = ConsoleKey.None;

            do
            {
                Console.Clear();
                Console.WriteLine("Pick an option: ");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.WriteLine($" {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selection > 0) { selection--; }
                else if (key == ConsoleKey.DownArrow && selection < options.Length - 1) { selection++; }
            }
            while (key != ConsoleKey.Enter);
        }

        static string DisplayInstructions()
        {
            string instructionsFileLocation = Path.Combine(AppContext.BaseDirectory, "GameInstructions.txt");

            if (!File.Exists(instructionsFileLocation))
            {
                return $"\nMissing GameInstrucions.txt \nExpected at {instructionsFileLocation}\n";
            }

            else
            {
                string instructions = File.ReadAllText(instructionsFileLocation);
                return instructions;
            }
        }

        static async Task LoadingAnimation()
        {
            string loading = "Loading.";

            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("Computer is placing boats\n\n");
                Console.WriteLine(loading);
                await Task.Delay(500);
                loading += ".";
            }
            Console.Clear();
            Console.WriteLine("Computer is placing boats\n\n");
            Console.WriteLine("Complete!");
            await Task.Delay(500);
        }

        static async Task PlaceComputerBoats(Cell[,] grid, int rows, int cols, Random random)
        {
            int computerBoatCounter = 0; //0 will be the first boat's Id

            await LoadingAnimation();

            PlaceComputerDestroyer(grid, rows, cols, ref computerBoatCounter, random);
            PlaceComputerDestroyer(grid, rows, cols, ref computerBoatCounter, random);

            PlaceComputerSubmarine(grid, rows, cols, ref computerBoatCounter, random);
            PlaceComputerSubmarine(grid, rows, cols, ref computerBoatCounter, random);

            PlaceComputerCarrier(grid, rows, cols, ref computerBoatCounter, random);
        }

        static void PlaceComputerDestroyer(Cell[,] grid, int rows, int cols, ref int computerBoatCounter, Random random)
        {
            int boatRow = random.Next(rows);
            int boatCol = random.Next(cols);

            while (grid[boatRow, boatCol].Boat != BoatType.None)
            {
                boatRow = random.Next(rows);
                boatCol = random.Next(cols);
            }

            grid[boatRow, boatCol] = new Cell { Boat = BoatType.Destroyer, BoatId = computerBoatCounter++ };
        }

        static bool IsBoatPlacementValid(Cell[,] grid, int rows, int cols, int startRow, int startCol, int direction, int length)
        {
            int[] rowCoordinate = { -1, 0, 1, 0 }; //up, right, down, left
            int[] colCoordinate = { 0, 1, 0, -1 };

            for (int i = 0; i < length; i++)
            {
                int newBoatRow = startRow + (rowCoordinate[direction] * i);
                int newBoatCol = startCol + (colCoordinate[direction] * i);

                if (newBoatRow >= rows || newBoatRow < 0 || newBoatCol >= cols || newBoatCol < 0)
                {
                    return false;
                }

                if (grid[newBoatRow, newBoatCol].Boat != BoatType.None)
                {
                    return false;
                }
            }

            return true;
        }

        static void PlaceComputerSubmarine(Cell[,] grid, int rows, int cols, ref int computerBoatCounter, Random random)
        {
            int boatRow = random.Next(rows);
            int boatCol = random.Next(cols);
            int direction = random.Next(4); //0 = up, 1 = right, 2 = down, 3 = left

            bool isValid = IsBoatPlacementValid(grid, rows, cols, boatRow, boatCol, direction, 2);
            
            while(!isValid)
            {
                boatRow = random.Next(rows);
                boatCol = random.Next(cols);
                direction = random.Next(4);

                isValid = IsBoatPlacementValid(grid, rows, cols, boatRow, boatCol, direction, 2);
            }

            int[] rowCoordinate = { -1, 0, 1, 0 }; //up, right, down, left
            int[] colCoordinate = { 0, 1, 0, -1 };

            int endBoatRow = boatRow + rowCoordinate[direction];
            int endBoatCol = boatCol + colCoordinate[direction];

            grid[boatRow, boatCol] = new Cell { Boat = BoatType.Submarine, BoatId = computerBoatCounter };
            grid[endBoatRow, endBoatCol] = new Cell { Boat = BoatType.Submarine, BoatId = computerBoatCounter++ };
        }

        static void PlaceComputerCarrier(Cell[,] grid, int rows, int cols, ref int computerBoatCounter, Random random)
        {
            int boatRow = random.Next(rows);
            int boatCol = random.Next(cols);
            int direction = random.Next(4); //0 = up, 1 = right, 2 = down, 3 = left

            bool isValid = IsBoatPlacementValid(grid, rows, cols, boatRow, boatCol, direction, 3);

            while (!isValid)
            {
                boatRow = random.Next(rows);
                boatCol = random.Next(cols);
                direction = random.Next(4);

                isValid = IsBoatPlacementValid(grid, rows, cols, boatRow, boatCol, direction, 3);
            }

            int[] rowCoordinate = { -1, 0, 1, 0 }; //up, right, down, left
            int[] colCoordinate = { 0, 1, 0, -1 };

            int middleBoatRow = boatRow + rowCoordinate[direction];
            int middleBoatCol = boatCol + colCoordinate[direction];

            int endBoatRow = boatRow + (rowCoordinate[direction] * 2);
            int endBoatCol = boatCol + (colCoordinate[direction] * 2);

            grid[boatRow, boatCol] = new Cell { Boat = BoatType.Carrier, BoatId = computerBoatCounter };
            grid[middleBoatRow, middleBoatCol] = new Cell { Boat = BoatType.Carrier, BoatId = computerBoatCounter };
            grid[endBoatRow, endBoatCol] = new Cell { Boat = BoatType.Carrier, BoatId = computerBoatCounter++ };
        }

        static void PlaceUserBoats(Cell[,] grid, int rows, int cols)
        {
            int userBoatCounter = 0; //This will be the Id for the first boat

            PlaceUserDestroyer(grid, rows, cols, ref userBoatCounter);
            PlaceUserDestroyer(grid, rows, cols, ref userBoatCounter);

            PlaceUserSubmarine(grid, rows, cols, ref userBoatCounter);
            PlaceUserSubmarine(grid, rows, cols, ref userBoatCounter);

            PlaceUserCarrier(grid, rows, cols, ref userBoatCounter);

        }

        static (int rowSelection, int colSelection) TraverseUserGrid(Cell[,] grid, int rows, int cols, string text)
        {
            ConsoleKey key = ConsoleKey.None;
            int rowSelection = 0;
            int colSelection = 0;

            do
            {
                Console.Clear();
                Console.WriteLine(text);
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (row == rowSelection && col == colSelection)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        else
                        {
                            Console.ResetColor();
                        }
                        Console.Write($"{ConvertCellToChar(grid[row, col])} ");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && rowSelection > 0) { rowSelection--; }
                else if (key == ConsoleKey.DownArrow && rowSelection < rows - 1) { rowSelection++; }
                else if (key == ConsoleKey.RightArrow && colSelection <  cols - 1) { colSelection++; }
                else if (key == ConsoleKey.LeftArrow && colSelection > 0) { colSelection--; }
            }
            while (key != ConsoleKey.Enter);

            return (rowSelection, colSelection);
        }

        static (int rowSelection, int colSelection) OverlapChecks(Cell[,] grid, int rows, int cols, bool isHorizontal, int rowSelection, int colSelection, int boatLength)
        {
            bool overlap = grid[rowSelection, colSelection].BoatId > -1 || (boatLength >= 2 && (isHorizontal ? grid[rowSelection, colSelection + 1].BoatId > -1 : grid[rowSelection + 1, colSelection].BoatId > -1)) || (boatLength == 3 && (isHorizontal ? grid[rowSelection, colSelection + 2].BoatId > -1 : grid[rowSelection + 2, colSelection].BoatId > -1));
            while (overlap)
            {
                if (rowSelection >= rows - 1)
                {
                    if (colSelection >= cols - 1) { rowSelection--; colSelection--; }
                    else {  rowSelection--; }
                }
                else if (boatLength == 2 && rowSelection + 2 >= rows - 1) { rowSelection--; colSelection++; }
                else if (boatLength == 3 && rowSelection + 3 >= rows - 1) { rowSelection--; colSelection++; }

                else { rowSelection++; }

                overlap = grid[rowSelection, colSelection].BoatId > -1 || (boatLength >= 2 && (isHorizontal ? grid[rowSelection, colSelection + 1].BoatId > -1 : grid[rowSelection + 1, colSelection].BoatId > -1)) || (boatLength == 3 && (isHorizontal ? grid[rowSelection, colSelection + 2].BoatId > -1 : grid[rowSelection + 2, colSelection].BoatId > -1));
            }

            return (rowSelection, colSelection);
        }

        //Come back to this, the collision detection shit doesnt work and i really want to move on rn, also i think the rotation is broken but idrk and i dont care to do anything about it rn
        static (int r1, int c1, int r2, int c2, int r3, int c3) TraverseGridToPlaceUserBoat(Cell[,] grid, int rows, int cols, string text, int boatLength)
        {
            ConsoleKey key = ConsoleKey.None;

            bool isHorizontal = false;
            bool isFlipped = false;

            int rowSelection = 0;
            int colSelection = 0;

            int row1, col1, row2, col2, row3, col3;

            do
            {

                (int rowSelection,int colSelection) newSelections = OverlapChecks(grid, rows, cols, isHorizontal, rowSelection, colSelection, boatLength);

                rowSelection = newSelections.rowSelection;
                colSelection = newSelections.colSelection;

                Console.Clear();
                Console.WriteLine(text);

                row1 = rowSelection;
                col1 = colSelection;

                row2 = rowSelection + 1;
                col2 = colSelection;

                row3 = rowSelection + 2;
                col3 = colSelection;

                if (isFlipped)
                {
                    if (!isHorizontal)
                    {
                        isHorizontal = true;

                        row2 = rowSelection;
                        col2 = colSelection + 1;

                        row3 = rowSelection;
                        col3 = colSelection + 2;
                    }

                    else if (isHorizontal)
                    {
                        isHorizontal = false;

                        row2 = rowSelection + 1;
                        col2 = colSelection;

                        row3 = rowSelection + 2;
                        col3 = colSelection;
                    }
                }

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {

                        bool highlight = (row == row1 && col == col1) || (boatLength >= 2 && row == row2 && col == col2) || (boatLength >= 3 && row == row3 && col == col3); 

                        if (highlight) { Console.ForegroundColor = ConsoleColor.Green; }
                        else { Console.ResetColor(); }

                        Console.Write($"{ConvertCellToChar(grid[row, col])} ");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && rowSelection > 0 ) { rowSelection--;}
                else if (key == ConsoleKey.DownArrow && rowSelection < rows - 1 && (isHorizontal || rowSelection + (boatLength - 1) < rows - 1)) { rowSelection++;}
                else if (key == ConsoleKey.RightArrow && colSelection < cols - 1 && (!isHorizontal || colSelection + (boatLength - 1) < cols - 1)) { colSelection++;}
                else if (key == ConsoleKey.LeftArrow && colSelection > 0) { colSelection--; }
                if (key == ConsoleKey.R && (rowSelection <= rows - boatLength && colSelection <= cols - boatLength)) { isFlipped = !isFlipped; }

            }
            while (key != ConsoleKey.Enter);

            return (row1, col1, row2, col2, row3, col3);
        }

        static void PlaceUserDestroyer(Cell[,] grid, int rows, int cols, ref int userBoatCounter)
        {
            string text = "Place a Destroyer: \n";
            (int r1, int c1, int r2, int c2, int r3, int c3) coordinates = TraverseGridToPlaceUserBoat(grid, rows, cols, text, 1);

            int row1 = coordinates.r1;
            int col1 = coordinates.c1;

            grid[row1, col1] = new Cell { Boat = BoatType.Destroyer, BoatId = userBoatCounter++ };
        }

        static void PlaceUserSubmarine(Cell[,] grid, int rows, int cols, ref int userBoatCounter)
        {
            string text = "Place a Submarine: \n";
            (int r1, int c1, int r2, int c2, int r3, int c3) coordinates = TraverseGridToPlaceUserBoat(grid, rows, cols, text, 2);

            int row1 = coordinates.r1;
            int col1 = coordinates.c1;

            int row2 = coordinates.r2;
            int col2 = coordinates.c2;

            grid[row1, col1] = new Cell { Boat = BoatType.Submarine, BoatId = userBoatCounter };
            grid[row2, col2] = new Cell { Boat = BoatType.Submarine, BoatId = userBoatCounter++ };
        }

        static void PlaceUserCarrier(Cell[,] grid, int rows, int cols, ref int userBoatCounter)
        {
            string text = "Place a Carrier: \n";
            (int r1, int c1, int r2, int c2, int r3, int c3) coordinates = TraverseGridToPlaceUserBoat(grid, rows, cols, text, 3);

            int row1 = coordinates.r1;
            int col1 = coordinates.c1;

            int row2 = coordinates.r2;
            int col2 = coordinates.c2;

            int row3 = coordinates.r3;
            int col3 = coordinates.c3;

            grid[row1, col1] = new Cell { Boat = BoatType.Carrier, BoatId = userBoatCounter };
            grid[row2, col2] = new Cell { Boat = BoatType.Carrier, BoatId = userBoatCounter };
            grid[row3, col3] = new Cell { Boat = BoatType.Carrier, BoatId = userBoatCounter++ };
        }
    }
}
     