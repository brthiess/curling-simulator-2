using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public class WorldsTournament : Tournament
	{

		public override TournamentType TournamentType
		{
			get 
			{
				return TournamentType.Worlds;
			}
		}
		
		public override TournamentResult Run()
		{
			ResetTeams();
			DoRoundRobin();
			DoPlayoffs();
			return new TournamentResult(this);
		}

		private void DoRoundRobin()
		{
			for(var i = 0; i < teams.Count; i++)
			{
				for (var j = i + 1; j < teams.Count; j++)
				{
					Team.PlayGame(teams[j], teams[i]);
				}
			}
			SetFinalRankingsForNonPlayoffTeams();
		}

		private void SetFinalRankingsForNonPlayoffTeams()
		{
			var teams = GetAllTeamsButTop6();
			for (var i = 0; i < teams.Count; i++)
			{
				teams[i].FinalRanking = i + 7;
			}
		}

		private void DoPlayoffs()
		{
			List<Team> teams = GetTop6Teams();
			
			var quarterFinalResult1 = Team.PlayGame(teams[2], teams[5], true, false);
			var quarterFinalResult2 = Team.PlayGame(teams[3], teams[4], true, false);

			var semiFinalResult1 = Team.PlayGame(teams[0], quarterFinalResult1.WinningTeam, true, false);
			var semiFinalResult2 = Team.PlayGame(teams[1], quarterFinalResult2.WinningTeam, true, false);

			var finalsResult = Team.PlayGame(semiFinalResult1.WinningTeam, semiFinalResult2.WinningTeam, true, false);
			var bronzeMedalResult = Team.PlayGame(semiFinalResult1.LosingTeam, semiFinalResult2.LosingTeam, true, false);

			finalsResult.WinningTeam.FinalRanking = 1;
			finalsResult.LosingTeam.FinalRanking = 2;
			bronzeMedalResult.WinningTeam.FinalRanking = 3;
			bronzeMedalResult.LosingTeam.FinalRanking = 4;
			quarterFinalResult1.LosingTeam.FinalRanking = 5;
			quarterFinalResult2.LosingTeam.FinalRanking = 6;

			
			var playoffResults = new System.Collections.Generic.Dictionary<string, List<Game>>();
			playoffResults.Add("Quarter Finals", new List<Game>(new Game[] {quarterFinalResult1, quarterFinalResult2}));
		}

		private List<Team> GetTop6Teams()
		{
			List<Team> TeamsSorted = teams.OrderByDescending(o => o.RoundRobinRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return TeamsSorted.GetRange(0, 6);
		}

		private List<Team> GetAllTeamsButTop6()
		{
			List<Team> TeamsSorted = teams.OrderByDescending(o => o.RoundRobinRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return TeamsSorted.GetRange(6, TeamsSorted.Count() - 6);
		}
	}
}