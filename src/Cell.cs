using System;
using System.Collections.Generic;
using System.Text;

namespace ConewaysGameOfLife
{
    class Cell
    {
		private string occupiedCell = "X ";
		private string emptyCell = ". ";

		public int neighboors = 0;
		public string CellSymbol
		{
			get
			{
				if (occupied) return occupiedCell;
				else return emptyCell;
			}
		}
		public bool occupied;

		public void Update()
		{
			if (occupied)
			{
				if (neighboors < 2 || neighboors > 3)
				{
					occupied = false;
				}
				else
				{
					return;
				}
			}
			else
			{
				if (neighboors == 3)
				{
					occupied = true;
				}
			}
		}

		public Cell()
		{
			occupied = false;
		}

		public Cell(bool occupied)
		{
			this.occupied = occupied;
		}
	}
}
