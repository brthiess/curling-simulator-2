using System.Collections.Generic;
namespace CurlingSimulator
{
    public class TeamRanking    {
        public int rank { get; set; } 
        public string firstName { get; set; } 

        public string lastName { get; set; } 
        public string location { get; set; } 
        public string image { get; set;}
        public bool selected {get;set;} = false;
    }

    public class Rankings    {
        public List<TeamRanking> TeamRankings { get; set; } 
    }
}