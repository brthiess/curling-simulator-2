using CurlingSimulator;
public class SimulationContainer
{
    public SimulationState State {get;set;}

    public Tournament Tournament {get;set;}

    public TournamentResult TournamentResult {get;set;}

    public void Reset(){
        State = SimulationState.NotStarted;
        Tournament = null;
        TournamentResult = null;
    }
}

public enum SimulationState{
    NotStarted,
    Running,
    Finished
}