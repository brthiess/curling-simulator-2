using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public abstract class TournamentFactory
    {
        public static Tournament GetTournament(TournamentType tournamentType)
        {
            if (tournamentType == TournamentType.Worlds)
            {
                return new WorldsTournament();
            }
            return new WorldsTournament();
        }
    }
}