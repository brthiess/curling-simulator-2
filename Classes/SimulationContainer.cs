using CurlingSimulator;
public class SimulationContainer
{
    public SimulationState State {get;set;} = SimulationState.NotStarted;

    public Tournament Tournament {get;set;}

    public TournamentType TournamentType {get;set;}

    public void RestartTournament(){
        State = SimulationState.NotStarted;
        Tournament = TournamentFactory.GetTournament(TournamentType);
    }
}

public enum SimulationState{
    NotStarted,
    Running,
    Finished
}