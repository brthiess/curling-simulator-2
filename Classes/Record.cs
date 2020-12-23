using System;
using System.Collections.Generic;
using System.Text;

namespace CurlingSimulator
{
	public class Record
	{
		public int Wins { get; set; }
		public int Losses { get; set; }

		public Record()
		{
			Wins = 0;
			Losses = 0;
		}

		public Record(int wins, int losses)
		{
			this.Wins = wins;
			this.Losses = losses;
		}

		public void AddWin()
		{
			Wins++;
		}
		public void AddLoss()
		{
			Losses++;
		}
	}
}