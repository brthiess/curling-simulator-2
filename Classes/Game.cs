using System;
using System.Collections.Generic;
using System.Text;

namespace CurlingSimulator
{
	public class Game
	{
        public Team HomeTeam {get;set;}

        public Team AwayTeam {get;set;}

        public Team WinningTeam {get;set;}

        public Team LosingTeam {get;set;}

        public int Team1Score{get;set;}

        public int Team2Score{get;set;}

        public Game(){}
    }
}