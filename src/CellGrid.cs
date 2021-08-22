using System;
using System.Collections.Generic;
using System.Text;

namespace ConewaysGameOfLife
{
    class GameOfLife
    {
		private struct CellPosition
		{
			public int row, column;

			public CellPosition(int row, int column)
			{
				this.row = row;
				this.column = column;
			}
		}

		private static CellPosition[,] cellPositions = new CellPosition[3, 3]
		{
			{ new CellPosition( -1, -1 ), new CellPosition( -1, 0 ),  new CellPosition( -1, 1 ) },
			{ new CellPosition( 0, -1 ),  new CellPosition( 0, 0 ),   new CellPosition( 0, 1 ) },
			{ new CellPosition( 1, -1 ),  new CellPosition( 1, 0 ),   new CellPosition( 1, 1 ) }
		};

		public Cell[,] cells;

		public GameOfLife(string[] startGrid, char occupiedSymbol, char emptySymbol)
		{
			try
            {
				int cellRows = startGrid.Length;
				int cellColumns = startGrid[0].Length;

				cells = new Cell[cellRows, cellColumns];

				for (int row = 0; row != cellRows; row++)
				{
					for (int columns = 0; columns != cellColumns; columns++)
					{
						cells[row, columns] = new Cell();
						if (startGrid[row][columns] == occupiedSymbol)
						{
							cells[row, columns].occupied = true;
						}
						else
						{
							cells[row, columns].occupied = false;
						}
					}
				}
			}
			catch
            {
				Console.WriteLine("Invalid starting grid, unable to start the game.");
            }
		}

		private void CalculateCellNeighboors()
		{
			for (int row = 0; row != cells.GetLength(0); row++)
			{
				for (int column = 0; column != cells.GetLength(1); column++)
				{
					foreach (CellPosition cellPosition in cellPositions)
					{
						if (cellPosition.column == 0 && cellPosition.row == 0) continue;

						bool rowInRange = row + cellPosition.row < cells.GetLength(0)
							&& row + cellPosition.row >= 0;
						bool columnInRange = column + cellPosition.column < cells.GetLength(1)
							&& column + cellPosition.column >= 0;

						if (rowInRange && columnInRange)
						{
							if (cells[row + cellPosition.row, column + cellPosition.column].occupied)
							{
								cells[row, column].neighboors++;
							}
						}
					}
				}
			}
		}

		public static implicit operator string(GameOfLife cellGrid)
		{
			string gridString = "";
			for (int l = 0; l != cellGrid.cells.GetLength(0); l++)
			{
				for (int w = 0; w != cellGrid.cells.GetLength(1); w++)
				{
					gridString += cellGrid.cells[l, w].CellSymbol;
				}
				if (l < cellGrid.cells.GetLength(0) - 1)
				{
					gridString += "\n";
				}
			}
			return gridString;
		}

		public void Update()
		{
			CalculateCellNeighboors();
			foreach (Cell cell in cells)
			{
				cell.Update();
				cell.neighboors = 0;
			}
		}
	}
}
