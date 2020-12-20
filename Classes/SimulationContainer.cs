using CurlingSimulator;
using System.Collections.Generic;

public class SimulationContainer
{
    public SimulationState State {get;set;} = SimulationState.Uninitialized;

    public Tournament Tournament {get;set;}

    public TournamentType TournamentType {get;set;}

    public List<TeamRanking> Rankings;

    public SimulationContainer(){}
    public void RestartOrInitTournament()
    {
        State = SimulationState.Initialized;
        Tournament = TournamentFactory.GetTournament(TournamentType);
    }

}

public enum SimulationState{
    Uninitialized,
    Initialized,
    Running,
    Finished
}