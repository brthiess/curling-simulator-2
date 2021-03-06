using System.Collections.Generic;
namespace CurlingSimulator
{
    public class TeamRanking    {
        public int rank { get; set; } 
        public int id {get;set;}
        public string firstName { get; set; } 
        public string lastName { get; set; } 
        public string location { get; set; } 
        public string image { get; set;}

        public int? division {get;set;}

        public string countryImage {get;set;}

        public string provinceImage {get;set;}
        
        public bool selected {get;set;} = false;

        public TeamRanking(){}
    }

    public class Rankings    {
        public List<TeamRanking> TeamRankings { get; set; } 

        public Rankings(){}
    }
}