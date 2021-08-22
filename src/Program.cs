using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;

namespace ConewaysGameOfLife
{
	class Program
    {
        const string welcomeMessage = "Welcome to Coneway's Game of Life in C#. Please answer the following questions to start the game.";
        const string loadGridQuery =
            "Would you like to load a grid from a file or create a random grid?\n" +
            "1 - Load file\n" +
            "2 - Create random grid";
        const string loadFileQuery = "Please enter the address to the file you wish to use.";
        const string occupiedSymbolQuery = "Please enter the occupied cell symbol.";
        const string emptySymbolQuery = "Please enter the empty cell symbol.";
        const string rowsQuery = "Please enter the desired number of rows. (1 - 100)";
        const string columnsQuery = "Please enter the desired number of columns. (1 - 100)";
        const string evolutionsQuery = "Please enter the number of evolutions in the game. (1 - 10000)";
        const string continueQuery = "Would you like to start a new game? (y/n)";
        const string inputFileEmpty = "Input file was empty, try again.";

        static string[] GetRandomStartGrid(int rows, int columns)
        {
            string[] startGrid = new string[rows];
            Random random = new Random();
            for (int l = 0; l != rows; l++)
            {
                string line = "";
                for (int w = 0; w != columns; w++)
                {
                    int randomVal = random.Next() % 2;
                    if (randomVal == 1)
                        line += "X";
                    else
                        line += ".";
                }
                startGrid[l] = line;
            }
            return startGrid;
        }       

        static string[] FormatInputFile(string[] inputFile, char emptySymbol)
        {
            if (inputFile.Length <= 0) return null;

            int columnCount = inputFile[0].Length;
            List<string> formattedFile = new List<string>();

            foreach (string line in inputFile)
            {
                string formattedLine = "";
                foreach (char character in line)
                {
                    if (!char.IsWhiteSpace(character)) formattedLine += character;
                }
                if (formattedLine.Length > 0)
                {
                    if (formattedLine.Length > columnCount)
                    {
                        formattedLine = formattedLine.Substring(0, columnCount);
                    }
                    else if (formattedLine.Length < columnCount)
                    {
                        int remainingSpace = columnCount - formattedLine.Length;
                        for (int i = 0; i != remainingSpace; i++)
                        {
                            formattedLine += emptySymbol;
                        }
                    }
                    formattedFile.Add(formattedLine);
                }
            }

            return formattedFile.ToArray();
        }

        static void Main(string[] args)
		{
            string filePath = "";
            char occupiedSymbol = ' ';
            char emptySymbol = ' ';
            int rows = 0;
            int columns = 0;
            int evolutions = 0;
            bool loadGameFromFile = false;
            bool continueGame = true;

            ConsoleInputHelper.State_Message(welcomeMessage);

            while (continueGame)
            {
                GameOfLife gameOfLife;

                int gridTypeChoice = ConsoleInputHelper.Ask_Integer(loadGridQuery, 1, 2);
                if (gridTypeChoice == 1)
                {
                    filePath = ConsoleInputHelper.Ask_FilePath(loadFileQuery);
                    emptySymbol = ConsoleInputHelper.Ask_Char(emptySymbolQuery);
                    occupiedSymbol = ConsoleInputHelper.Ask_Char(occupiedSymbolQuery);
                    loadGameFromFile = true;
                }
                else
                {
                    columns = ConsoleInputHelper.Ask_Integer(columnsQuery, 1, 100);
                    rows = ConsoleInputHelper.Ask_Integer(rowsQuery, 1, 100);
                    loadGameFromFile = false;
                }

                evolutions = ConsoleInputHelper.Ask_Integer(evolutionsQuery, 1, 10000);

                if (loadGameFromFile)
                {
                    string[] startGrid = FormatInputFile(File.ReadAllLines(filePath), emptySymbol);
                    if (startGrid == null)
                    {
                        ConsoleInputHelper.State_Message(inputFileEmpty);
                        continue;
                    }
                    gameOfLife = new GameOfLife(startGrid, occupiedSymbol, emptySymbol);
                }
                else
                {
                    gameOfLife = new GameOfLife(GetRandomStartGrid(rows, columns), 'X', '.');
                }

                for (int i = 0; i != evolutions; i++)
                {
                    Console.Clear();
                    Console.WriteLine(gameOfLife);
                    gameOfLife.Update();               
                    Thread.Sleep(10);
                }

                char yesNo = ConsoleInputHelper.Ask_YesNo(continueQuery);
                if (yesNo == 'Y' || yesNo == 'y')
                {
                    continueGame = true;
                    Console.Clear();
                }
                else
                {
                    continueGame = false;
                }
            }
        }
    }
}
