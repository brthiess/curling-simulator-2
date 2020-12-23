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

    public void SetState(SimulationContainer simulationContainer){
        this.State = simulationContainer.State;
        this.Tournament = simulationContainer.Tournament;
        this.TournamentType = simulationContainer.TournamentType;
        this.Rankings = simulationContainer.Rankings;
    }

}

public enum SimulationState{
    Uninitialized,
    Initialized,
    Running,
    Finished
}