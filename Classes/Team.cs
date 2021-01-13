using System;
using System.Collections.Generic;
using System.Text;

namespace CurlingSimulator
{
	public class Team
	{
		public string Name { get; set; }
		private double Rating { get; set; }

		public Record RoundRobinRecord {get;set;}

		public Record QualifyingRoundRecord {get;set;}

		public Record ChampionshipRoundRecord {get;set;}

		public Record TourRecord { get; set; }

		public int? TourRanking { get; set; }

		public int FinalRanking { get; set; }

		public double LsdTotal {get;set;} 

		public string Location {get;set;}

		public string Image {get;set;}

		public bool MadePlayoffs {get;set;}

		public bool MadeChampionshipRound {get;set;}

		public int DivisionNumber {get;set;}

		public Team(){}

		public Team(string name, int divisionNumber, Record tourRecord = null, int? tourRanking = null, string location = null, string image = null)
		{
			Location = location;
			Image = image;
			TourRecord = tourRecord;
			TourRanking = tourRanking;
			DivisionNumber = divisionNumber;
			if (tourRecord != null)
			{
				Rating = GetRatingFromRecord(tourRecord);
			}
			else if (tourRanking.HasValue)
			{
				Rating = GetRatingFromRanking(tourRanking.Value, RegressionType.Inverse);
			}
			else
			{
				throw new Exception("Record and Ranking are both null in call to Team constructor");
			}
			if (Rating < 0 || Rating >= 1)
			{
				throw new Exception("Rating is not within a valid range (0,1).  Rating is: " + Rating);
			}

			this.Name = name;
			this.QualifyingRoundRecord = new Record();
			this.RoundRobinRecord = new Record();
			this.ChampionshipRoundRecord = new Record();
			this.LsdTotal = 0;
		}

		public static double GetRatingFromRanking(int ranking, RegressionType regressionType)
		{
			double rating = 0;
			if (regressionType == RegressionType.Logarithmic)
			{
				rating = (94.6740233 + -14.68314639 * Math.Log(ranking + Math.E)) / 100;
			}
			else if (regressionType == RegressionType.Exponential)
			{
				rating = 65.54*Math.Pow(Math.E, -0.0093221 * ranking) / 100;
			}
			else if (regressionType == RegressionType.ArcTan) 
			{
				rating = (-25 * Math.Atan(0.1 * ranking - 1.5) + 55) / 100;
			}
			else if (regressionType == RegressionType.Inverse)
			{
				rating = (-1833.15 / (-1.05558 * ranking - 23.703) + 12.144) / 100;
			}
			else if (regressionType == RegressionType.Linear)
			{
				rating = (-0.3134148880054439 * ranking +  64.4390619317387) / 100;
			}
			else if (regressionType == RegressionType.None)
			{
				return 0.5;
			}
			
			if (rating < 0.01)
			{
				rating = 0.01;
			}
			return rating;
		}

		private double GetRatingFromRecord(Record record)
		{
			return (double) record.Wins / (record.Wins + record.Losses);
		}

		public static Game PlayGame(Team homeTeam, Team awayTeam, RoundType roundType, bool giveHammerAdvantageToTeamWithBetterRecord = false)
		{
			var homeTeamLsd = DrawToTheButton(homeTeam, roundType);
			var awayTeamLsd = DrawToTheButton(awayTeam, roundType);

			bool homeHasHammer = GetHomeHasHammer(homeTeam, awayTeam, homeTeamLsd, awayTeamLsd, giveHammerAdvantageToTeamWithBetterRecord);

			double probabilityHomeBeatsAway = GetProbabilityOfHomeBeatingAway(homeTeam, awayTeam, homeHasHammer); 
			var random = new Random();
			
			
			if (random.NextDouble() < probabilityHomeBeatsAway)
			{				
				if (roundType == RoundType.Qualifying)
				{
					homeTeam.QualifyingRoundRecord.AddWin();
					awayTeam.QualifyingRoundRecord.AddLoss();
				}
				if (roundType != RoundType.Playoff)
				{
					homeTeam.RoundRobinRecord.AddWin();
					awayTeam.RoundRobinRecord.AddLoss();
				}
				return new Game(){
					WinningTeam = homeTeam,
					LosingTeam = awayTeam,
					HomeTeam = homeTeam,
					AwayTeam = awayTeam,
					HomeTeamLsd = homeTeamLsd,
					AwayTeamLsd = awayTeamLsd,
					HomeTeamWon = true,
					AwayTeamWon = false,
					HomeHammer = homeHasHammer,
					AwayHammer = !homeHasHammer
				};
			}
			else
			{				
				if (roundType == RoundType.Qualifying)
				{
					homeTeam.QualifyingRoundRecord.AddLoss();
					awayTeam.QualifyingRoundRecord.AddWin();
				}
				if (roundType != RoundType.Playoff)
				{
					homeTeam.RoundRobinRecord.AddLoss();
					awayTeam.RoundRobinRecord.AddWin();
				}
				return new Game(){
					WinningTeam = awayTeam,
					LosingTeam = homeTeam,
					HomeTeam = homeTeam,
					AwayTeam = awayTeam,
					HomeTeamLsd = homeTeamLsd,
					AwayTeamLsd = awayTeamLsd,
					HomeTeamWon = false,
					AwayTeamWon = true,
					HomeHammer = homeHasHammer,
					AwayHammer = !homeHasHammer
				};
			}	
		}

		private static bool GetHomeHasHammer(Team homeTeam, Team awayTeam, double homeTeamLsd, double awayTeamLsd,  bool giveHammerAdvantageToTeamWithBetterRecord = false)
		{
			if (homeTeam.QualifyingRoundRecord.Wins > awayTeam.QualifyingRoundRecord.Wins && giveHammerAdvantageToTeamWithBetterRecord)
			{
				return true;
			}
			else if (homeTeam.QualifyingRoundRecord.Wins < awayTeam.QualifyingRoundRecord.Wins && giveHammerAdvantageToTeamWithBetterRecord) 
			{
				return false;
			}
			else 
			{
				if (homeTeamLsd <= awayTeamLsd)
				{
					return true;
				}
				if (homeTeamLsd > awayTeamLsd)
				{
					return false;
				}
			}
			throw new Exception("Could not determine which team has hammer. " + homeTeamLsd + " " + awayTeamLsd + " " + homeTeam.Name + " " + awayTeam.Name);
		}

        private static double DrawToTheButton(Team team, RoundType roundType)
        {
			Random r = new Random();
			double lsdLength = 1000/(Math.Sqrt(team.Rating) * r.Next(1,200) * Math.Sqrt(team.Rating)) - 5.7;
			if (lsdLength < 0 )
			{
				lsdLength = 0;
			}
			else if (lsdLength > 144)
			{
				lsdLength = 144;
			}
			if (roundType != RoundType.Playoff)
			{
				team.LsdTotal += lsdLength;
			}
			return lsdLength;
        }

        public static double GetProbabilityOfHomeBeatingAway(Team homeTeam, Team awayTeam, bool homeHasHammer)
		{
			if (homeTeam.Rating == 0 || homeTeam.Rating == 1 || awayTeam.Rating == 1 || awayTeam.Rating == 0)
			{
				throw new Exception("Found team with bad rating: " + homeTeam.Rating + " | " + awayTeam.Rating);
			}

			var initialProbability =  ((1 - awayTeam.Rating) * homeTeam.Rating) / ((1 - awayTeam.Rating) * (homeTeam.Rating) + (1 - homeTeam.Rating) * (awayTeam.Rating));
			var homeTeamAdvantage = GetHomeTeamAdvantage(homeTeam, awayTeam, homeHasHammer);
			return initialProbability + homeTeamAdvantage;
		}

		private static double GetHomeTeamAdvantage(Team homeTeam, Team awayTeam, bool homeHasHammer) 
		{
			if (homeHasHammer)
			{
				return 0.05;
			}
			else 
			{
				return -0.05;
			}
		}
	}

	

	public enum RegressionType {
		Logarithmic,
		Exponential,
		ArcTan,
		Inverse,
		Linear,
		None
	}
}