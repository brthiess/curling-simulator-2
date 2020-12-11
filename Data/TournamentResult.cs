using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CurlingSimulator
{
	public class TournamentResult
	{

		public List<Team> Teams;

		public TournamentResult(Tournament tournament)
		{
			Teams = new List<Team>();
			SetTeams(tournament.teams);
		}

		private void SetTeams(List<Team> teams)
		{
			foreach(var team in teams)
			{
				var newTeam = new Team(team.Name, team.TourRecord, team.TourRanking);
				newTeam.RoundRobinRecord = team.RoundRobinRecord;
				newTeam.FinalRanking = team.FinalRanking;
				this.Teams.Add(newTeam);
			}
		}

		public List<Team> GetTeamsSortedByRoundRobin()
		{
			return Teams.OrderByDescending(o => o.RoundRobinRecord.Wins).ToList();
		}

		public List<Team> GetTeamsSortedByPlayoffResults()
		{
			return  Teams.OrderBy(o => o.FinalRanking).ToList();
		}
	}
}