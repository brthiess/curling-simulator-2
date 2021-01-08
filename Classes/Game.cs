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

        public double HomeTeamLsd {get;set;}

        public double AwayTeamLsd {get;set;}

        public bool HomeTeamWon {get;set;}

        public bool AwayTeamWon {get;set;}

        public bool HomeHammer {get;set;}

        public bool AwayHammer {get;set;}

        public Game(){}
    }
}