#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\SimulationResults.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8812662f79918bf2a0a0385983af3d1fe65df7fd"
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
