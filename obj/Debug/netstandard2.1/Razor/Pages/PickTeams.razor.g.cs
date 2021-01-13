#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0a71802f87dc21c5fc019dd9544a7674151c3bab"
// <auto-generated/>
#pragma warning disable 1591
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
#nullable restore
#line 12 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
 if(SimulationContainer == null || SimulationContainer.State == SimulationState.Uninitialized){return;}

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "\r\n\r\n");
            __builder.AddMarkupContent(1, "<h1 class=\"page-title pick-teams-title\">Pick Teams</h1>\r\n\r\n");
            __builder.OpenElement(2, "p");
            __builder.AddAttribute(3, "class", "pick-teams-text");
            __builder.AddContent(4, "Pick ");
            __builder.AddContent(5, 
#nullable restore
#line 17 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                 SimulationContainer?.Tournament?.NumberOfTeams

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\r\n\r\n");
            __builder.OpenElement(7, "ul");
            __builder.AddAttribute(8, "class", "teams-list");
            __builder.AddMarkupContent(9, "\r\n");
#nullable restore
#line 20 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
     if (SimulationContainer?.Rankings != null){
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
         foreach (var team in SimulationContainer.Rankings)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(10, "            ");
            __builder.OpenElement(11, "li");
            __builder.AddAttribute(12, "class", "team-list-item" + " " + (
#nullable restore
#line 23 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                        PickedEnoughTeams() ? "disabled" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(13, "\r\n                ");
            __builder.OpenElement(14, "input");
            __builder.AddAttribute(15, "type", "checkbox");
            __builder.AddAttribute(16, "id", 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                            team.id

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(17, "class", "team-list-item-checkbox");
            __builder.AddAttribute(18, "disabled", 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                                 PickedEnoughTeams() && !team.selected ? "disabled" : null

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(19, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                                                                                                          e => UpdateSelectedTeamsCount(team, e.Value)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(20, "checked", 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                                                                                                                                                                    team.selected ? "checked" : null

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n                <div class=\"team-list-item-background\"></div>\r\n                ");
            __builder.OpenElement(22, "label");
            __builder.AddAttribute(23, "class", "team-list-item-label" + " " + (
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                     PickedEnoughTeams() && !team.selected ? "disabled" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "for", 
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                                                     team.id

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n                ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "team-list-item-location");
            __builder.AddMarkupContent(28, "\r\n                    ");
            __builder.OpenElement(29, "img");
            __builder.AddAttribute(30, "src", "/images/locations/" + (
#nullable restore
#line 28 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                  team.countryImage

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n                ");
            __builder.OpenElement(33, "div");
            __builder.AddAttribute(34, "class", "team-list-item-name");
            __builder.AddMarkupContent(35, "\r\n                    Team ");
            __builder.AddContent(36, 
#nullable restore
#line 31 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                          team.lastName

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(37, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n                <div class=\"team-list-item-fake-checkbox\"></div>\r\n                \r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n");
#nullable restore
#line 36 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
         
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n\r\n");
            __builder.OpenElement(41, "button");
            __builder.AddAttribute(42, "class", "done-button" + " continue-button" + " " + (
#nullable restore
#line 40 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                             PickedEnoughTeams() ? "ready" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(43, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 40 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                            NavigateToSimulationResults

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(44, 
#nullable restore
#line 40 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
                                                                                                                          SimulateButtonText

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 42 "C:\Users\brthi\Projects\curling-simulator\Pages\PickTeams.razor"
        

    int selectedTeamsCount {get;set;} = 0;

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
        selectedTeamsCount = GetNumberOfSelectedTeams(false);
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
            team.selected = true;
            selectedTeamsCount++;
        }
        else 
        {
            team.selected = false;
            selectedTeamsCount--;
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
            return selectedTeamsCount;
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
                SimulationContainer.Tournament.AddTeam(new Team(team.lastName, 1, null, team.rank, team.location, team.countryImage), 1);
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
