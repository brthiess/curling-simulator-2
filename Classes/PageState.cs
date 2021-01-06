using CurlingSimulator;
using System.Collections.Generic;


public class PageState
{
    public LoadingState State {get;set;} = LoadingState.Loading;

    public string LoadingText {get;set;} = "Loading...";

    public event System.Action OnChange;

    public void SetState(LoadingState loadingState, string loadingText)
    {
        State = loadingState;
        LoadingText = loadingText;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();


}

public enum LoadingState{
    Loading,
    Finished
}

