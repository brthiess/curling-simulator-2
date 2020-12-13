using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public class PlayoffResults
	{

		public List<Team> Teams;

        public Dictionary<PlayoffRoundType, List<Game>> ListOfGameRounds {get; private set;}

		public PlayoffResults(Dictionary<PlayoffRoundType, List<Game>> listOfGameRounds)
		{
			ListOfGameRounds = listOfGameRounds;
        }
	}

    public enum PlayoffRoundType 
    {
        Quarters,
        Semis,
        Finals,
        Bronze,
        Sixteens
    }
}