using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public class CanadaNationalTournament : Tournament
	{
		public CanadaNationalTournament():base(){}

		public override TournamentType TournamentType {get;} = TournamentType.CanadaNational;

        public override int NumberOfTeams => 13;

        

		public override void Run()
		{
			DoQualifyingRound();
			DoChampionshipRound();
			DoPlayoffs();
		}

		private void DoQualifyingRound()
		{
			foreach (var division in Divisions)
			{
				var teams = division.Value;
				for(var i = 0; i < teams.Count; i++)
				{
					for (var j = i + 1; j < teams.Count; j++)
					{
						Team.PlayGame(teams[j], teams[i], RoundType.Qualifying);
					}
				}
			}
			SetFinalRankingsForNonQualifyingTeams();
		}

		private void DoChampionshipRound()
		{
			var championshipTeams = GetChampionshipTeams();
			championshipTeams.ForEach(x => x.MadeChampionshipRound = true);
			for(var i = 0; i < championshipTeams.Count; i++)
			{
				for (var j = i + 1; j < championshipTeams.Count; j++)
				{
					if (championshipTeams[j].DivisionNumber != championshipTeams[i].DivisionNumber)
					{
						Team.PlayGame(championshipTeams[j], championshipTeams[i], RoundType.Championship);
					}
				}
			}
			SetFinalRankingsForNonPlayoffTeams(championshipTeams);
		}

		public List<Team> GetChampionshipTeams()
		{
			var championshipTeams = new List<Team>();
			foreach(var division in Divisions)
			{
				championshipTeams.Concat(GetQualifyingTeamsInDivision(division.Value));
			}
			List<Team> teamsSorted = championshipTeams.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return teamsSorted;
		}

		private void SetFinalRankingsForNonQualifyingTeams()
		{
			var teams = GetNonQualifyingTeams();
			for (var i = 0; i < teams.Count; i++)
			{
				teams[i].FinalRanking = i + 9;
			}
		}

		private void SetFinalRankingsForNonPlayoffTeams(List<Team> championshipTeams)
		{
			var teams = GetNonPlayoffTeams(championshipTeams);
			for (var i = 0; i < teams.Count; i++)
			{
				teams[i].FinalRanking = i + 5;
			}
		}

		private void DoPlayoffs()
		{
			List<Team> teams = GetTop4Teams();
			teams.ForEach(x => x.MadePlayoffs = true);
			
			var oneTwoGame = Team.PlayGame(teams[0], teams[1], RoundType.Playoff, true);
			var threeFourGame = Team.PlayGame(teams[2], teams[3], RoundType.Playoff, true);

			var semiFinal = Team.PlayGame(oneTwoGame.LosingTeam, threeFourGame.WinningTeam, RoundType.Playoff, true);

			var finalsResult = Team.PlayGame(oneTwoGame.WinningTeam, semiFinal.WinningTeam, RoundType.Playoff, true);

			finalsResult.WinningTeam.FinalRanking = 1;
			finalsResult.LosingTeam.FinalRanking = 2;
			semiFinal.LosingTeam.FinalRanking = 3;
			threeFourGame.LosingTeam.FinalRanking = 4;

			var playoffResults = new System.Collections.Generic.Dictionary<PlayoffRoundType, List<Game>>();
			playoffResults.Add(PlayoffRoundType.ThreeFourGame, new List<Game>(new Game[] {threeFourGame}));
			playoffResults.Add(PlayoffRoundType.OneTwoGame, new List<Game>(new Game[] {oneTwoGame}));
			playoffResults.Add(PlayoffRoundType.Semis, new List<Game>(new Game[] {semiFinal}));
			playoffResults.Add(PlayoffRoundType.Finals, new List<Game>(new Game[] {finalsResult}));
			this.PlayoffResults = new PlayoffResults(playoffResults);
		}

		private List<Team> GetTop4Teams()
		{
			List<Team> TeamsSorted = Teams.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return TeamsSorted.GetRange(0, 4);
		}

		private List<Team> GetNonPlayoffTeams(List<Team> championshipTeams)
		{
			List<Team> TeamsSorted = championshipTeams.OrderByDescending(o => o.RoundRobinRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return TeamsSorted.GetRange(4, TeamsSorted.Count() - 4);
		}

		private List<Team> GetQualifyingTeamsInDivision()
		{
			var qualifyingTeams = new List<Team>();
			foreach(var division in Divisions)
			{
				qualifyingTeams.Concat(GetQualifyingTeamsInDivision(division.Value));
			}
			List<Team> teamsSorted = qualifyingTeams.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return teamsSorted;
		}

		private List<Team> GetNonQualifyingTeams()
		{
			var nonQualifyingTeams = new List<Team>();
			foreach(var division in Divisions)
			{
				nonQualifyingTeams.Concat(GetNonQualifyingTeamsInDivision(division.Value));
			}
			List<Team> teamsSorted = nonQualifyingTeams.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return teamsSorted;
		}

		private List<Team> GetNonQualifyingTeamsInDivision(List<Team> division)
		{
			List<Team> teamsSorted = division.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return teamsSorted.GetRange(4, teamsSorted.Count() - 4);
		}

		private List<Team> GetQualifyingTeamsInDivision(List<Team> division)
		{
			List<Team> teamsSorted = division.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
			return teamsSorted.GetRange(0, 4);
		}
	}
}