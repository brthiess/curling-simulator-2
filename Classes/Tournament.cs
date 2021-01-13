using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using JsonSubTypes;

namespace CurlingSimulator
{
	[JsonConverter(typeof(JsonSubtypes), "TournamentType")]
    [JsonSubtypes.KnownSubType(typeof(WorldsTournament), TournamentType.Worlds)]
	public class Tournament
	{
		//List of teams in the tournament
		public List<Team> Teams;

		public Dictionary<int, List<Team>> Divisions;

		//The tournament type
		public virtual TournamentType TournamentType {get;}

		public virtual int NumberOfTeams {get;}
		
		//The playoff results of the tournament
		public PlayoffResults PlayoffResults;

		public Tournament()
		{
			Teams = new List<Team>();
			Divisions = new Dictionary<int, List<Team>>();
		}

		public void AddTeam(Team team, int divisionNumber)
		{
			Teams.Add(team);
			if (!Divisions.ContainsKey(divisionNumber))
			{
				Divisions.Add(divisionNumber, new List<Team>());
			}
			Divisions[divisionNumber].Add(team);
		}

		public virtual void Run(){}

		protected void ResetTeams()
		{
			for (var i = 0; i < Teams.Count; i++)
			{
				Teams[i] = new Team(Teams[i].Name, Teams[i].DivisionNumber, Teams[i].TourRecord, Teams[i].TourRanking);
			}
		}

		public List<Team> GetTeamsSortedByRoundRobin()
		{
			return Teams.OrderByDescending(o => o.QualifyingRoundRecord.Wins).ThenBy(o => o.LsdTotal).ToList();
		}

		public List<Team> GetTeamsSortedByPlayoffResults()
		{
			return  Teams.OrderBy(o => o.FinalRanking).ToList();
		}
	}

	public enum TournamentType {
		Worlds,
		CanadaNational,
		Slam
	}

	public enum RoundType {
		Qualifying,
		Championship,
		Playoff
	}
}