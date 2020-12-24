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

		public Record TourRecord { get; set; }

		public int? TourRanking { get; set; }

		public int FinalRanking { get; set; }

		public double LsdTotal {get;set;} 

		public string Location {get;set;}

		public string Image {get;set;}

		public Team(){}

		public Team(string name, Record tourRecord = null, int? tourRanking = null, string location = null, string image = null)
		{
			Location = location;
			Image = image;
			TourRecord = tourRecord;
			TourRanking = tourRanking;
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
			if (Rating < 0 || Rating > 1)
			{
				throw new Exception("Rating is not within a valid range (0,1).  Rating is: " + Rating);
			}

			this.Name = name;
			this.RoundRobinRecord = new Record();
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
			//
			//double rating = (-9450.702849 * Math.Atan(5.4421135 * ranking + 129.2) + 14857) / 100;
			//double rating = (-23 * Math.Atan(0.07 * ranking - 1.5) + 55) / 100;
			//double rating = (-25 * Math.Atan(0.1 * ranking - 1.5) + 55) / 100;
			
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

		public static Game PlayGame(Team homeTeam, Team awayTeam, bool giveHammerAdvantageToTeamWithBetterRecord = false, bool isRoundRobinGame=true, bool isPlayoffGame=false)
		{
			var homeTeamLsd = DrawToTheButton(homeTeam, isPlayoffGame);
			var awayTeamLsd = DrawToTheButton(awayTeam, isPlayoffGame);

			double probabilityHomeBeatsAway = GetProbabilityOfHomeBeatingAway(homeTeam, awayTeam, homeTeamLsd, awayTeamLsd,  false); 
			var random = new Random();
			
			
			if (random.NextDouble() < probabilityHomeBeatsAway)
			{				
				if (isRoundRobinGame)
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
					AwayTeamWon = false	
				};
			}
			else
			{				
				if (isRoundRobinGame)
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
					AwayTeamWon = true
				};
			}
			
		}

        private static double DrawToTheButton(Team team, bool isPlayoffGame)
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
			if (!isPlayoffGame)
			{
				team.LsdTotal += lsdLength;
			}
			return lsdLength;
        }

        public static double GetProbabilityOfHomeBeatingAway(Team homeTeam, Team awayTeam, double homeTeamLsd, double awayTeamLsd,  bool giveHammerAdvantageToTeamWithBetterRecord = false)
		{
			if (homeTeam.Rating == 0)
			{
				if (awayTeam.Rating > 0)
				{
					return 0;
				}
				else
				{
					return 0.5;
				}
			}
			if (homeTeam.Rating == 1)
			{
				if (awayTeam.Rating < 1)
				{
					return 1;
				}
				else
				{
					return 0.5;
				}
			}
			var initialProbability =  ((1 - awayTeam.Rating) * homeTeam.Rating) / ((1 - awayTeam.Rating) * (homeTeam.Rating) + (1 - homeTeam.Rating) * (awayTeam.Rating));
			var homeTeamAdvantage = GetHomeTeamAdvantage(homeTeam, awayTeam, homeTeamLsd, awayTeamLsd, giveHammerAdvantageToTeamWithBetterRecord);
			return initialProbability + homeTeamAdvantage;
		}

		private static double GetHomeTeamAdvantage(Team homeTeam, Team awayTeam, double homeTeamLsd, double awayTeamLsd, bool giveHammerAdvantageToTeamWithBetterRecord) 
		{
			if (!giveHammerAdvantageToTeamWithBetterRecord) 
			{
				if (homeTeamLsd < awayTeamLsd)
				{
					return 0.05;
				}
				if (homeTeamLsd > awayTeamLsd)
				{
					return -0.05;
				}
			}
			else
			{
				if (homeTeam.RoundRobinRecord.Wins > awayTeam.RoundRobinRecord.Wins)
				{
					return 0.05;
				}
				else if (homeTeam.RoundRobinRecord.Wins < awayTeam.RoundRobinRecord.Wins) 
				{
					return -0.05;
				}
			}
			return 0;
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