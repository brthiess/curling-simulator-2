#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "809d57355d6a689e4b43081aada974215b8497b8"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace curling_simulator_2.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using curling_simulator_2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using curling_simulator_2.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
using CurlingSimulator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/pick-teams")]
    public partial class PickTeams : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 45 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
        

    int SelectedTeamsCount {get;set;} = 0;
    int CurrentDivision {get;set;} = 1;
    int CurrentDivisionBeingSelectedFor {get;set;} = 1;

    private string SimulateButtonText 
    {
        get 
        {
            int numberOfSelectedTeams = GetNumberOfSelectedTeams();
            var simulateButtonText = "Simulate";
            if (numberOfSelectedTeams < SimulationContainer?.Tournament?.NumberOfTeams)
            {
                simulateButtonText = numberOfSelectedTeams + "/" + SimulationContainer.Tournament.NumberOfTeams + " Teams";
            } 
            return simulateButtonText;
        }
    }

    private string SimulateButtonSubText 
    {
        get 
        {
            return "Pool " + CurrentDivisionBeingSelectedFor;
        }
    }

    private string PickTeamsText
    {
        get 
        {
            return "Pick " + SimulationContainer?.Tournament?.NumberOfTeams;
        }
    }
    protected override async Task OnInitializedAsync()
    {
        if (SimulationContainer.State == SimulationState.Uninitialized)
        {
            var simulationContainerString = await localStorage.GetItemAsStringAsync("SimulationContainer");
            var simulationContainerClone = JsonConvert.DeserializeObject<SimulationContainer>(simulationContainerString);
            SimulationContainer.SetState(simulationContainerClone);
        }
        if (SimulationContainer != null && SimulationContainer.Rankings == null)
        {
            SimulationContainer.Rankings = await Http.GetFromJsonAsync<List<TeamRanking>>(GetJSONPath(SimulationContainer));
        }
        SelectedTeamsCount = GetNumberOfSelectedTeams(false);
        await Task.Delay(100);
        PageState.SetState(LoadingState.Finished, null);
    }

    private string GetJSONPath(SimulationContainer simulationContainer)
    {
        if (simulationContainer.TournamentType == TournamentType.Worlds)
        {
            if (simulationContainer.Gender == Gender.Male)
            {
                return "json/rankings-worlds-men.json";
            }
            else if (simulationContainer.Gender == Gender.Female)
            {
                return "json/rankings-worlds-women.json";
            }
        }
        return "json/rankings.json";
    }

    private void UpdateSelectedTeamsCount(TeamRanking team, object checkedValue)
    {

        if ((bool)checkedValue)
        {
            SelectedTeamsCount++;
            CurrentDivision = (int) Math.Ceiling(((double)SelectedTeamsCount / (double) SimulationContainer.Tournament.NumberOfTeamsPerDivision));
            CurrentDivisionBeingSelectedFor = (int) Math.Ceiling(((double)(SelectedTeamsCount + 1) / (double) SimulationContainer.Tournament.NumberOfTeamsPerDivision));
            team.selected = true;
            team.division = CurrentDivision;
        }
        else 
        {
            SelectedTeamsCount--;
            team.division = null;
            team.selected = false;
        }
    }
    

    private async void NavigateToSimulationResults() 
    {
        if (GetNumberOfSelectedTeams() != SimulationContainer.Tournament.NumberOfTeams)
        {
            return;
        }
        PageState.SetState(LoadingState.Loading, "Simulating tournament...");
        await Task.Delay(4000);
        RunTournament();
        await localStorage.SetItemAsync("SimulationContainer", JsonConvert.SerializeObject(SimulationContainer));
        NavigationManager.NavigateTo("simulation-results"); 
    }

    private int GetNumberOfSelectedTeams(bool useCache = true)
    {
        if (useCache)
        {
            return SelectedTeamsCount;
        }

        int numberOfSelectedTeams = 0;
        foreach(var ranking in SimulationContainer.Rankings)
        {
            if (ranking.selected)
            {
                numberOfSelectedTeams++;
            }
        }
        return numberOfSelectedTeams;
    }

    private bool PickedEnoughTeams(){
        return GetNumberOfSelectedTeams() == SimulationContainer?.Tournament?.NumberOfTeams;
    }

    protected void RunTournament()
    {
        if (SimulationContainer.State != SimulationState.Initialized)
        {
            SimulationContainer.RestartOrInitTournament();
        }
        SimulationContainer.State = SimulationState.Running;
        foreach(var team in SimulationContainer.Rankings)
        {
            if (team.selected)
            {
                SimulationContainer.Tournament.AddTeam(new Team(team.lastName, team.division.HasValue ? team.division.Value : 1, null, team.rank, team.location, team.countryImage));
            }
        }


        SimulationContainer.Tournament.Run();

        SimulationContainer.State = SimulationState.Finished;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PageState PageState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SimulationContainer SimulationContainer { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
