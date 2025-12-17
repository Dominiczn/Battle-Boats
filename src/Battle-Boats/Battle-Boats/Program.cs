using System.Text;

namespace Battle_Boats
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            char[,] playerBoats = InitialiseGrid();
            char[,] targetTracker = InitialiseGrid();
            char[,] computerBoats = InitialiseGrid();
            int menuSelection = 0;

            DisplayMenu(ref menuSelection);

            switch (menuSelection)
            {
                case 0:
                    //Start new game
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

        static char[,] InitialiseGrid()
        {
            char[,] grid = new char[8, 8];

            for (int row = 0; row < grid.GetLength(0);  row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = '~';
                }
            }

            return grid;
        }

        static void DisplayGrid(char[,] grid)
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

    }
}
