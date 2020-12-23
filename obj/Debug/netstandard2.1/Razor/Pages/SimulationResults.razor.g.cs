#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d16cbb4004f13b5e9e29fadc8d7c961372e88d6"
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
#line 2 "C:\Users\brthi\Projects\curling-simulator\_Imports.razor"
using System.Net.Http.Json;

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
#line 3 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
using CurlingSimulator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/simulation-results")]
    public partial class SimulationResults : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1 class=\"page-title simulation-results-title\">Simulation Results</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<h3 class=\"round-robin-title results-title\">Round Robin</h3>\r\n");
            __builder.OpenElement(2, "table");
            __builder.AddAttribute(3, "class", "round-robin-table");
            __builder.AddMarkupContent(4, "\r\n    ");
            __builder.AddMarkupContent(5, "<thead>\r\n        <tr>\r\n            <th>Team</th>\r\n            <th>W</th>\r\n            <th>L</th>\r\n            <th>LSD</th>\r\n        </tr>\r\n    </thead>\r\n\r\n");
#nullable restore
#line 21 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 foreach (var team in SimulationContainer?.Tournament?.GetTeamsSortedByRoundRobin() ?? new List<Team>())
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(6, "    ");
            __builder.OpenElement(7, "tr");
            __builder.AddMarkupContent(8, "\r\n        ");
            __builder.OpenElement(9, "td");
            __builder.AddAttribute(10, "class", "round-robin-table-team-name");
            __builder.OpenElement(11, "img");
            __builder.AddAttribute(12, "class", "round-robin-table-team-image");
            __builder.AddAttribute(13, "src", "/images/locations/" + (
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                                  team.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddContent(14, " ");
            __builder.AddContent(15, 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                                                 team.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.OpenElement(17, "td");
            __builder.AddAttribute(18, "class", "round-robin-table-wins round-robin-table-record");
            __builder.AddContent(19, 
#nullable restore
#line 25 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                     team.RoundRobinRecord.Wins

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n        ");
            __builder.OpenElement(21, "td");
            __builder.AddAttribute(22, "class", "round-robin-table-losses round-robin-table-record");
            __builder.AddContent(23, 
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                        team.RoundRobinRecord.Losses

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n        ");
            __builder.OpenElement(25, "td");
            __builder.AddAttribute(26, "class", "round-robin-table-lsd-amount");
            __builder.AddContent(27, 
#nullable restore
#line 27 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                   Math.Round(team.LsdTotal)

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(28, "cm ");
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n");
#nullable restore
#line 29 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}     

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n\r\n");
            __builder.AddMarkupContent(32, "<h3 class=\"playoffs-title results-title\">Playoffs</h3>\r\n");
#nullable restore
#line 33 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 if (SimulationContainer?.Tournament?.TournamentType == TournamentType.Worlds){
 

#line default
#line hidden
#nullable disable
            __builder.AddContent(33, "    ");
            __builder.OpenElement(34, "div");
            __builder.AddAttribute(35, "class", "world-playoffs");
            __builder.AddMarkupContent(36, "\r\n        ");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "class", "quarter-final-round");
            __builder.AddMarkupContent(39, "\r\n");
#nullable restore
#line 37 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetRoundOneGames())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(40, "                ");
            __builder.OpenElement(41, "div");
            __builder.AddAttribute(42, "class", "game");
            __builder.AddMarkupContent(43, "\r\n                    ");
            __builder.OpenElement(44, "div");
            __builder.AddAttribute(45, "class", "home-team team-name");
            __builder.AddMarkupContent(46, "\r\n                        ");
            __builder.AddContent(47, 
#nullable restore
#line 41 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(48, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n                    ");
            __builder.OpenElement(50, "div");
            __builder.AddAttribute(51, "class", "away-team team-name");
            __builder.AddMarkupContent(52, "\r\n                        ");
            __builder.AddContent(53, 
#nullable restore
#line 44 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(54, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(55, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n");
#nullable restore
#line 47 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(57, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(58, "\r\n        ");
            __builder.OpenElement(59, "div");
            __builder.AddAttribute(60, "class", "semi-final-round");
            __builder.AddMarkupContent(61, "\r\n");
#nullable restore
#line 50 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetRoundTwoGames())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(62, "                ");
            __builder.OpenElement(63, "div");
            __builder.AddAttribute(64, "class", "game");
            __builder.AddMarkupContent(65, "\r\n                    ");
            __builder.OpenElement(66, "div");
            __builder.AddAttribute(67, "class", "home-team team-name");
            __builder.AddMarkupContent(68, "\r\n                        ");
            __builder.AddContent(69, 
#nullable restore
#line 54 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(70, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(71, "\r\n                    ");
            __builder.OpenElement(72, "div");
            __builder.AddAttribute(73, "class", "away-team team-name");
            __builder.AddMarkupContent(74, "\r\n                        ");
            __builder.AddContent(75, 
#nullable restore
#line 57 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(76, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(77, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(78, "\r\n");
#nullable restore
#line 60 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(79, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(80, "\r\n        ");
            __builder.OpenElement(81, "div");
            __builder.AddAttribute(82, "class", "final-round");
            __builder.AddMarkupContent(83, "\r\n");
#nullable restore
#line 63 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetBronze())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(84, "                ");
            __builder.OpenElement(85, "div");
            __builder.AddAttribute(86, "class", "game");
            __builder.AddMarkupContent(87, "\r\n                    ");
            __builder.OpenElement(88, "div");
            __builder.AddAttribute(89, "class", "home-team team-name");
            __builder.AddMarkupContent(90, "\r\n                        ");
            __builder.AddContent(91, 
#nullable restore
#line 67 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(92, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(93, "\r\n                    ");
            __builder.OpenElement(94, "div");
            __builder.AddAttribute(95, "class", "away-team team-name");
            __builder.AddMarkupContent(96, "\r\n                        ");
            __builder.AddContent(97, 
#nullable restore
#line 70 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(98, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(99, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(100, "\r\n");
#nullable restore
#line 73 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(101, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(102, "\r\n        ");
            __builder.OpenElement(103, "div");
            __builder.AddAttribute(104, "class", "bronze-round");
            __builder.AddMarkupContent(105, "\r\n");
#nullable restore
#line 76 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetFinal())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(106, "                ");
            __builder.OpenElement(107, "div");
            __builder.AddAttribute(108, "class", "game");
            __builder.AddMarkupContent(109, "\r\n                    ");
            __builder.OpenElement(110, "div");
            __builder.AddAttribute(111, "class", "home-team team-name");
            __builder.AddMarkupContent(112, "\r\n                        ");
            __builder.AddContent(113, 
#nullable restore
#line 80 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(114, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(115, "\r\n                    ");
            __builder.OpenElement(116, "div");
            __builder.AddAttribute(117, "class", "away-team team-name");
            __builder.AddMarkupContent(118, "\r\n                        ");
            __builder.AddContent(119, 
#nullable restore
#line 83 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                         game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(120, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(121, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(122, "\r\n");
#nullable restore
#line 86 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(123, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(124, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(125, "\r\n");
#nullable restore
#line 89 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 92 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 

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
            SimulationContainer.Rankings = await Http.GetFromJsonAsync<List<TeamRanking>>("/json/rankings.json");
        }
        
    }

    public List<Game> GetRoundOneGames()
    {
        var quartersGames = SimulationContainer?.Tournament?.PlayoffResults?.ListOfGameRounds?[PlayoffRoundType.Quarters] ?? new List<Game>();
        return quartersGames;
    }

    public List<Game> GetRoundTwoGames()
    {
        var semisGames = SimulationContainer?.Tournament?.PlayoffResults?.ListOfGameRounds?[PlayoffRoundType.Semis] ?? new List<Game>();
        return semisGames;
    }

    public List<Game> GetFinal()
    {
        var finalGame = SimulationContainer?.Tournament?.PlayoffResults?.ListOfGameRounds?[PlayoffRoundType.Finals] ?? new List<Game>();
        return finalGame;
    }

    public List<Game> GetBronze()
    {
        var bronzeGame = SimulationContainer?.Tournament?.PlayoffResults?.ListOfGameRounds?[PlayoffRoundType.Bronze] ?? new List<Game>();
        return bronzeGame;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SimulationContainer SimulationContainer { get; set; }
    }
}
#pragma warning restore 1591
