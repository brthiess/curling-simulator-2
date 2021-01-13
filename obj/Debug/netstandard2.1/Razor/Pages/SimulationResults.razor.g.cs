#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94ac39751011c54761ce79f09c16e26d51f0776e"
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
#line 22 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 foreach (var team in SimulationContainer?.Tournament?.GetTeamsSortedByRoundRobin() ?? new List<Team>())
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(6, "    ");
            __builder.OpenElement(7, "tr");
            __builder.AddMarkupContent(8, "\r\n        ");
            __builder.OpenElement(9, "td");
            __builder.AddAttribute(10, "class", "round-robin-table-team-name" + " " + (
#nullable restore
#line 25 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                 team.MadePlayoffs ? "qualified" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.OpenElement(11, "img");
            __builder.AddAttribute(12, "class", "round-robin-table-team-image");
            __builder.AddAttribute(13, "src", "/images/locations/" + (
#nullable restore
#line 25 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                                                                          team.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddContent(14, " ");
            __builder.AddContent(15, 
#nullable restore
#line 25 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                                                                                         team.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.OpenElement(17, "td");
            __builder.AddAttribute(18, "class", "round-robin-table-wins" + " round-robin-table-record" + " " + (
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                     team.MadePlayoffs ? "qualified" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(19, 
#nullable restore
#line 26 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                             team.QualifyingRoundRecord.Wins

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n        ");
            __builder.OpenElement(21, "td");
            __builder.AddAttribute(22, "class", "round-robin-table-losses" + " round-robin-table-record" + " " + (
#nullable restore
#line 27 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                       team.MadePlayoffs ? "qualified" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(23, 
#nullable restore
#line 27 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                                                                team.QualifyingRoundRecord.Losses

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n        ");
            __builder.OpenElement(25, "td");
            __builder.AddAttribute(26, "class", "round-robin-table-lsd-amount" + " " + (
#nullable restore
#line 28 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                  team.MadePlayoffs ? "qualified" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(27, 
#nullable restore
#line 28 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
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
#line 30 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}     

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n\r\n");
            __builder.AddMarkupContent(32, "<h3 class=\"playoffs-title results-title\">Playoffs</h3>\r\n");
#nullable restore
#line 34 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 if (SimulationContainer?.Tournament?.TournamentType == TournamentType.Worlds){
 

#line default
#line hidden
#nullable disable
            __builder.AddContent(33, "    ");
            __builder.OpenElement(34, "div");
            __builder.AddAttribute(35, "class", "world-playoffs");
            __builder.AddMarkupContent(36, "\r\n        ");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "class", "playoff-round-container");
            __builder.AddMarkupContent(39, "\r\n            ");
            __builder.AddMarkupContent(40, "<h4 class=\"playoff-round-text\">Quarter Finals</h4>\r\n            ");
            __builder.OpenElement(41, "div");
            __builder.AddAttribute(42, "class", "quarter-final-round playoff-round");
            __builder.AddMarkupContent(43, "\r\n");
#nullable restore
#line 40 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetRoundOneGames())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(44, "                ");
            __builder.OpenElement(45, "div");
            __builder.AddAttribute(46, "class", "game");
            __builder.AddMarkupContent(47, "\r\n                    ");
            __builder.OpenElement(48, "div");
            __builder.AddAttribute(49, "class", "home-team" + " team-name" + " " + (
#nullable restore
#line 43 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.HomeTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(50, "\r\n                        ");
            __builder.OpenElement(51, "img");
            __builder.AddAttribute(52, "class", "team-image");
            __builder.AddAttribute(53, "src", "/images/locations/" + (
#nullable restore
#line 44 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.HomeTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\r\n                        ");
            __builder.OpenElement(55, "div");
            __builder.AddAttribute(56, "class", "team-name-text");
            __builder.AddContent(57, 
#nullable restore
#line 45 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(58, "\r\n                        ");
            __builder.OpenElement(59, "div");
            __builder.AddAttribute(60, "class", "team-hammer");
            __builder.AddMarkupContent(61, "\r\n");
#nullable restore
#line 47 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.HomeHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(62, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 50 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(63, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n                    ");
            __builder.OpenElement(66, "div");
            __builder.AddAttribute(67, "class", "away-team" + " team-name" + " " + (
#nullable restore
#line 53 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.AwayTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(68, "\r\n                        ");
            __builder.OpenElement(69, "img");
            __builder.AddAttribute(70, "class", "team-image");
            __builder.AddAttribute(71, "src", "/images/locations/" + (
#nullable restore
#line 54 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.AwayTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(72, "\r\n                        ");
            __builder.OpenElement(73, "div");
            __builder.AddAttribute(74, "class", "team-name-text");
            __builder.AddContent(75, 
#nullable restore
#line 55 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(76, "\r\n                        ");
            __builder.OpenElement(77, "div");
            __builder.AddAttribute(78, "class", "team-hammer");
            __builder.AddMarkupContent(79, "\r\n");
#nullable restore
#line 57 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.AwayHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(80, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 60 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(81, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(82, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(83, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(84, "\r\n");
#nullable restore
#line 64 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(85, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(86, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n        ");
            __builder.OpenElement(88, "div");
            __builder.AddAttribute(89, "class", "playoff-round-container");
            __builder.AddMarkupContent(90, "\r\n            ");
            __builder.AddMarkupContent(91, "<h4 class=\"playoff-round-text\">Semi-Finals</h4>\r\n            ");
            __builder.OpenElement(92, "div");
            __builder.AddAttribute(93, "class", "semi-final-round playoff-round");
            __builder.AddMarkupContent(94, "\r\n");
#nullable restore
#line 70 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetRoundTwoGames())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(95, "                ");
            __builder.OpenElement(96, "div");
            __builder.AddAttribute(97, "class", "game");
            __builder.AddMarkupContent(98, "\r\n                    ");
            __builder.OpenElement(99, "div");
            __builder.AddAttribute(100, "class", "home-team" + " team-name" + " " + (
#nullable restore
#line 73 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.HomeTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(101, "\r\n                        ");
            __builder.OpenElement(102, "img");
            __builder.AddAttribute(103, "class", "team-image");
            __builder.AddAttribute(104, "src", "/images/locations/" + (
#nullable restore
#line 74 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.HomeTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(105, "\r\n                        ");
            __builder.OpenElement(106, "div");
            __builder.AddAttribute(107, "class", "team-name-text");
            __builder.AddContent(108, 
#nullable restore
#line 75 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(109, "\r\n                        ");
            __builder.OpenElement(110, "div");
            __builder.AddAttribute(111, "class", "team-hammer");
            __builder.AddMarkupContent(112, "\r\n");
#nullable restore
#line 77 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.HomeHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(113, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 80 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(114, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(115, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(116, "\r\n                    ");
            __builder.OpenElement(117, "div");
            __builder.AddAttribute(118, "class", "away-team" + " team-name" + " " + (
#nullable restore
#line 83 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.AwayTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(119, "\r\n                        ");
            __builder.OpenElement(120, "img");
            __builder.AddAttribute(121, "class", "team-image");
            __builder.AddAttribute(122, "src", "/images/locations/" + (
#nullable restore
#line 84 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.AwayTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(123, "\r\n                        ");
            __builder.OpenElement(124, "div");
            __builder.AddAttribute(125, "class", "team-name-text");
            __builder.AddContent(126, 
#nullable restore
#line 85 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(127, "\r\n                        ");
            __builder.OpenElement(128, "div");
            __builder.AddAttribute(129, "class", "team-hammer");
            __builder.AddMarkupContent(130, "\r\n");
#nullable restore
#line 87 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.AwayHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(131, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 90 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(132, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(133, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(134, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(135, "\r\n");
#nullable restore
#line 94 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(136, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(137, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(138, "\r\n        ");
            __builder.OpenElement(139, "div");
            __builder.AddAttribute(140, "class", "playoff-round-container");
            __builder.AddMarkupContent(141, "\r\n            ");
            __builder.AddMarkupContent(142, "<h4 class=\"playoff-round-text\">Finals</h4>\r\n            ");
            __builder.OpenElement(143, "div");
            __builder.AddAttribute(144, "class", "final-round playoff-round");
            __builder.AddMarkupContent(145, "\r\n");
#nullable restore
#line 100 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetFinal())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(146, "                ");
            __builder.OpenElement(147, "div");
            __builder.AddAttribute(148, "class", "game");
            __builder.AddMarkupContent(149, "\r\n                    ");
            __builder.OpenElement(150, "div");
            __builder.AddAttribute(151, "class", "home-team" + " team-name" + " " + (
#nullable restore
#line 103 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.HomeTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(152, "\r\n                        ");
            __builder.OpenElement(153, "img");
            __builder.AddAttribute(154, "class", "team-image");
            __builder.AddAttribute(155, "src", "/images/locations/" + (
#nullable restore
#line 104 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.HomeTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(156, "\r\n                        ");
            __builder.OpenElement(157, "div");
            __builder.AddAttribute(158, "class", "team-name-text");
            __builder.AddContent(159, 
#nullable restore
#line 105 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(160, "\r\n                        ");
            __builder.OpenElement(161, "div");
            __builder.AddAttribute(162, "class", "team-hammer");
            __builder.AddMarkupContent(163, "\r\n");
#nullable restore
#line 107 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.HomeHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(164, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 110 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(165, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(166, "\r\n                        \r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(167, "\r\n                    ");
            __builder.OpenElement(168, "div");
            __builder.AddAttribute(169, "class", "away-team" + " team-name" + " " + (
#nullable restore
#line 114 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.AwayTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(170, "\r\n                        ");
            __builder.OpenElement(171, "img");
            __builder.AddAttribute(172, "class", "team-image");
            __builder.AddAttribute(173, "src", "/images/locations/" + (
#nullable restore
#line 115 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.AwayTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(174, "\r\n                        ");
            __builder.OpenElement(175, "div");
            __builder.AddAttribute(176, "class", "team-name-text");
            __builder.AddContent(177, 
#nullable restore
#line 116 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(178, "\r\n                        ");
            __builder.OpenElement(179, "div");
            __builder.AddAttribute(180, "class", "team-hammer");
            __builder.AddMarkupContent(181, "\r\n");
#nullable restore
#line 118 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.AwayHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(182, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 121 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(183, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(184, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(185, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(186, "\r\n");
#nullable restore
#line 125 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(187, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(188, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(189, "\r\n        ");
            __builder.OpenElement(190, "div");
            __builder.AddAttribute(191, "class", "playoff-round-container");
            __builder.AddMarkupContent(192, "\r\n            ");
            __builder.AddMarkupContent(193, "<h4 class=\"playoff-round-text\">Bronze</h4>\r\n            ");
            __builder.OpenElement(194, "div");
            __builder.AddAttribute(195, "class", "bronze-round playoff-round");
            __builder.AddMarkupContent(196, "\r\n");
#nullable restore
#line 131 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
             foreach (var game in GetBronze())
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(197, "                ");
            __builder.OpenElement(198, "div");
            __builder.AddAttribute(199, "class", "game");
            __builder.AddMarkupContent(200, "\r\n                    ");
            __builder.OpenElement(201, "div");
            __builder.AddAttribute(202, "class", "home-team" + " team-name" + " " + (
#nullable restore
#line 134 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.HomeTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(203, "\r\n                        ");
            __builder.OpenElement(204, "img");
            __builder.AddAttribute(205, "class", "team-image");
            __builder.AddAttribute(206, "src", "/images/locations/" + (
#nullable restore
#line 135 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.HomeTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(207, "\r\n                        ");
            __builder.OpenElement(208, "div");
            __builder.AddAttribute(209, "class", "team-name-text");
            __builder.AddContent(210, 
#nullable restore
#line 136 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.HomeTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(211, "\r\n                        ");
            __builder.OpenElement(212, "div");
            __builder.AddAttribute(213, "class", "team-hammer");
            __builder.AddMarkupContent(214, "\r\n");
#nullable restore
#line 138 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.HomeHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(215, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 141 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(216, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(217, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(218, "\r\n                    ");
            __builder.OpenElement(219, "div");
            __builder.AddAttribute(220, "class", "away-team" + " team-name" + " " + (
#nullable restore
#line 144 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                      game.AwayTeamWon ? "winner" : "loser"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(221, "\r\n                        ");
            __builder.OpenElement(222, "img");
            __builder.AddAttribute(223, "class", "team-image");
            __builder.AddAttribute(224, "src", "/images/locations/" + (
#nullable restore
#line 145 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                                         game.AwayTeam.Image

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(225, "\r\n                        ");
            __builder.OpenElement(226, "div");
            __builder.AddAttribute(227, "class", "team-name-text");
            __builder.AddContent(228, 
#nullable restore
#line 146 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                                                     game.AwayTeam.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(229, "\r\n                        ");
            __builder.OpenElement(230, "div");
            __builder.AddAttribute(231, "class", "team-hammer");
            __builder.AddMarkupContent(232, "\r\n");
#nullable restore
#line 148 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                             if (game.AwayHammer)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(233, "                                <img src=\"/images/hammer.png\">\r\n");
#nullable restore
#line 151 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(234, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(235, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(236, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(237, "\r\n");
#nullable restore
#line 155 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(238, "            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(239, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(240, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(241, "\r\n");
#nullable restore
#line 159 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 162 "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor"
 

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
        PageState.SetState(LoadingState.Finished, null);        
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
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PageState PageState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SimulationContainer SimulationContainer { get; set; }
    }
}
#pragma warning restore 1591
