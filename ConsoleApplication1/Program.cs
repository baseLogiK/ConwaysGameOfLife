using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        const char Alive = '*';
        const char Dead = '.';
        private static int Height;
        private static int Width;

        static void Main(string[] args)
        {
            Console.Write("Please enter the height ");
            string value = Console.ReadLine();
            Console.WriteLine(value);
            Height = int.Parse(value);
            Console.WriteLine();

            Console.Write("Please enter the width ");
            value = Console.ReadLine();
            Console.WriteLine(value);
            Width = int.Parse(value);
            Console.WriteLine();

            char[,] board = CreateRandomBoard();

            //PrintBoard(board);
            //Console.ReadLine();
            //return;

            int generation = 0;

            Console.WriteLine("Generation " + generation);
            PrintBoard(board);
            generation += 1;

            do
            {
                board = SimulateGeneration(board);
                Console.WriteLine("Generation " + generation);
                PrintBoard(board);
                //Console.ReadLine();

               
                generation = generation + 1;
            } while (!EveryonesDead(board));

            Console.ReadLine();
        }

        static char[,] SimulateGeneration(char[,] board)
        {
            for (int currentRow = 0; currentRow < Height; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Width; currentColumn++)
                {
                    bool survives = DoesSurvive(board, currentRow, currentColumn);

                    if (survives)
                    {
                        board[currentRow, currentColumn] = Alive;
                    }
                    else
                    {
                        board[currentRow, currentColumn] = Dead;
                    }
                }
            }

            return board;
        }

        static bool EveryonesDead(char[,] board)
        {
            for (int currentRow = 0; currentRow < Height; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Width; currentColumn++)
                {
                    if(board[currentRow, currentColumn] == Alive)
                        return false;
                }
            }

            return true;
        }

        static bool DoesSurvive(char[,] board, int currentRow, int currentColumn)
        {
            int numberOfAliveNeighbors = 0;

            for (int i = currentRow - 1; i <= currentRow + 1; i++)
            {
                if (i < 0) continue;
                if (i >= Height) continue;

                for (int j = currentColumn - 1; j < currentColumn + 1; j++)
                {
                    if (j < 0) continue;
                    if (j >= Width) continue;
                    if (i == currentRow && j == currentColumn) continue;

                    if (board[i, j] == Alive)
                        numberOfAliveNeighbors = numberOfAliveNeighbors + 1;
                }
            }

            bool lessThan2LiveNeighbors = false;

            if (numberOfAliveNeighbors < 2)
                lessThan2LiveNeighbors = true;

            if (lessThan2LiveNeighbors) return false;

            bool has2Or3LiveNeighbors = false;

            has2Or3LiveNeighbors = numberOfAliveNeighbors == 2
                || numberOfAliveNeighbors == 3;

            if (has2Or3LiveNeighbors)
                return true;//yay!

            bool hasMoreThan3LiveNeighbors = false;

            hasMoreThan3LiveNeighbors = numberOfAliveNeighbors > 3;

            if (hasMoreThan3LiveNeighbors)
                return false; //boo

            //Rule 4
            bool has3LiveNeighbors = false;

            has3LiveNeighbors = numberOfAliveNeighbors == 3;

            bool isCurrentCellDeadNow = false;

            isCurrentCellDeadNow = board[currentRow, currentColumn] == Dead;

            if (isCurrentCellDeadNow
                && has3LiveNeighbors)
                return true;

            return false;
            //end of isalive
        }

        static void PrintBoard(char[,] board)
        {
            for (int currentRow = 0; currentRow < board.GetLength(0); currentRow++)
            {
                for (int currentColumn = 0; currentColumn < board.GetLength(1); currentColumn++)
                {
                    Console.Write(board[currentRow, currentColumn]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static char[,] CreateRandomBoard()
        {
            char[,] board = new char[Height, Width];
            Random random = new Random();

            for (int currentRow = 0; currentRow < board.GetLength(0); currentRow++)
            {
                for (int currentColumn = 0; currentColumn < board.GetLength(1); currentColumn++)
                {
                    board[currentRow, currentColumn] = 
                        random.Next(0, 2) == 0
                        ? Dead
                        : Alive;
                }
            }

            return board;
        }
    }
}
