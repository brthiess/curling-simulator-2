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

		//The tournament type
		public virtual TournamentType TournamentType {get;}

		public virtual int NumberOfTeams {get;}
		
		//The playoff results of the tournament
		public PlayoffResults PlayoffResults;

		public Tournament()
		{
			Teams = new List<Team>();
		}

		public void AddTeam(Team team)
		{
			Teams.Add(team);
		}

		public virtual void Run(){}

		protected void ResetTeams()
		{
			for (var i = 0; i < Teams.Count; i++)
			{
				Teams[i] = new Team(Teams[i].Name, Teams[i].TourRecord, Teams[i].TourRanking);
			}
		}

		public List<Team> GetTeamsSortedByRoundRobin()
		{
			return Teams.OrderByDescending(o => o.RoundRobinRecord.Wins).ThenByDescending(o => o.LsdTotal).ToList();
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
}