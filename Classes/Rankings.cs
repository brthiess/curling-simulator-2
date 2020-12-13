using System.Collections.Generic;
namespace CurlingSimulator
{
    public class TeamRanking    {
        public string rank { get; set; } 
        public string name { get; set; } 
        public string location { get; set; } 
    }

    public class Rankings    {
        public List<TeamRanking> TeamRankings { get; set; } 
    }
}