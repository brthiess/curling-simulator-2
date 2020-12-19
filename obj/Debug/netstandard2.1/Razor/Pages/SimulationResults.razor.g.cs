#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8812662f79918bf2a0a0385983af3d1fe65df7fd"
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
            __builder.AddMarkupContent(5, "<thead>\r\n        <tr>\r\n            <th>Team</th>\r\n            <th>W</th>\r\n            <th>L</th>\r\n            <th>LSD</th>\r\n        </tr>\r\n    </thead>\r\n");
#nullable restore
#line 18 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 foreach (var team in State.Tournament.GetTeamsSortedByRoundRobin())
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
            __builder.AddAttribute(13, "src", 
#nullable restore
#line 21 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                team.Image

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddContent(14, " ");
            __builder.AddContent(15, 
#nullable restore
#line 21 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                               team.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.OpenElement(17, "td");
            __builder.AddContent(18, 
#nullable restore
#line 22 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             team.RoundRobinRecord.Wins

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n        ");
            __builder.OpenElement(20, "td");
            __builder.AddContent(21, 
#nullable restore
#line 23 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             team.RoundRobinRecord.Losses

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n        ");
            __builder.OpenElement(23, "td");
            __builder.AddContent(24, 
#nullable restore
#line 24 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
              Math.Round(team.LsdTotal)

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(25, "cm ");
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n");
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}     

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n\r\n");
            __builder.AddMarkupContent(29, "<h3 class=\"playoffs-title results-title\">Playoffs</h3>\r\n");
#nullable restore
#line 30 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 if (State.Tournament.TournamentType == TournamentType.Worlds){
 

#line default
#line hidden
#nullable disable
            __builder.AddContent(30, "    ");
            __builder.OpenElement(31, "div");
            __builder.AddAttribute(32, "class", "quarter-final-round");
            __builder.AddMarkupContent(33, "\r\n");
#nullable restore
#line 33 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
         foreach (var game in GetRoundOneGames())
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(34, "            ");
            __builder.OpenElement(35, "div");
            __builder.AddAttribute(36, "class", "game");
            __builder.AddMarkupContent(37, "\r\n                ");
            __builder.OpenElement(38, "div");
            __builder.AddAttribute(39, "class", "home-team team-name");
            __builder.AddMarkupContent(40, "\r\n                    ");
            __builder.AddContent(41, 
#nullable restore
#line 37 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(42, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n                ");
            __builder.OpenElement(44, "div");
            __builder.AddAttribute(45, "class", "away-team team-name");
            __builder.AddMarkupContent(46, "\r\n                    ");
            __builder.AddContent(47, 
#nullable restore
#line 40 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(48, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n");
#nullable restore
#line 43 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(51, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(52, "\r\n    ");
            __builder.OpenElement(53, "div");
            __builder.AddAttribute(54, "class", "semi-final-round");
            __builder.AddMarkupContent(55, "\r\n");
#nullable restore
#line 46 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
         foreach (var game in GetRoundTwoGames())
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(56, "            ");
            __builder.OpenElement(57, "div");
            __builder.AddAttribute(58, "class", "game");
            __builder.AddMarkupContent(59, "\r\n                ");
            __builder.OpenElement(60, "div");
            __builder.AddAttribute(61, "class", "home-team team-name");
            __builder.AddMarkupContent(62, "\r\n                    ");
            __builder.AddContent(63, 
#nullable restore
#line 50 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(64, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n                ");
            __builder.OpenElement(66, "div");
            __builder.AddAttribute(67, "class", "away-team team-name");
            __builder.AddMarkupContent(68, "\r\n                    ");
            __builder.AddContent(69, 
#nullable restore
#line 53 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(70, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(71, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(72, "\r\n");
#nullable restore
#line 56 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(73, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n    ");
            __builder.OpenElement(75, "div");
            __builder.AddAttribute(76, "class", "final-round");
            __builder.AddMarkupContent(77, "\r\n");
#nullable restore
#line 59 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
         foreach (var game in GetFinal())
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(78, "            ");
            __builder.OpenElement(79, "div");
            __builder.AddAttribute(80, "class", "game");
            __builder.AddMarkupContent(81, "\r\n                ");
            __builder.OpenElement(82, "div");
            __builder.AddAttribute(83, "class", "home-team team-name");
            __builder.AddMarkupContent(84, "\r\n                    ");
            __builder.AddContent(85, 
#nullable restore
#line 63 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(86, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n                ");
            __builder.OpenElement(88, "div");
            __builder.AddAttribute(89, "class", "away-team team-name");
            __builder.AddMarkupContent(90, "\r\n                    ");
            __builder.AddContent(91, 
#nullable restore
#line 66 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(92, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(93, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(94, "\r\n");
#nullable restore
#line 69 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(95, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(96, "\r\n    ");
            __builder.OpenElement(97, "div");
            __builder.AddAttribute(98, "class", "bronze-round");
            __builder.AddMarkupContent(99, "\r\n");
#nullable restore
#line 72 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
         foreach (var game in GetFinal())
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(100, "            ");
            __builder.OpenElement(101, "div");
            __builder.AddAttribute(102, "class", "game");
            __builder.AddMarkupContent(103, "\r\n                ");
            __builder.OpenElement(104, "div");
            __builder.AddAttribute(105, "class", "home-team team-name");
            __builder.AddMarkupContent(106, "\r\n                    ");
            __builder.AddContent(107, 
#nullable restore
#line 76 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(108, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(109, "\r\n                ");
            __builder.OpenElement(110, "div");
            __builder.AddAttribute(111, "class", "away-team team-name");
            __builder.AddMarkupContent(112, "\r\n                    ");
            __builder.AddContent(113, 
#nullable restore
#line 79 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(114, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(115, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(116, "\r\n");
#nullable restore
#line 82 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(117, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(118, "\r\n");
#nullable restore
#line 84 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 87 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 

        public List<Game> GetRoundOneGames()
        {
            var quartersGames = State.Tournament.PlayoffResults.ListOfGameRounds[PlayoffRoundType.Quarters];
            return quartersGames;
        }

        public List<Game> GetRoundTwoGames()
        {
            var semisGames = State.Tournament.PlayoffResults.ListOfGameRounds[PlayoffRoundType.Semis];
            return semisGames;
        }

        public List<Game> GetFinal()
        {
            var finalGame = State.Tournament.PlayoffResults.ListOfGameRounds[PlayoffRoundType.Finals];
            return finalGame;
        }

        public List<Game> GetBronze()
        {
            var bronzeGame = State.Tournament.PlayoffResults.ListOfGameRounds[PlayoffRoundType.Bronze];
            return bronzeGame;
        }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SimulationContainer State { get; set; }
    }
}
#pragma warning restore 1591
