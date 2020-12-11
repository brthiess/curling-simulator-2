using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public class PlayoffResult
	{

		public List<Team> Teams;

        public Dictionary<string, List<Game>> ListOfGameRounds {get; private set;}
        
		public PlayoffResult(Dictionary<string, List<Game>> listOfGameRounds)
		{
			ListOfGameRounds = listOfGameRounds;
        }
	}
}