#pragma checksum "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "912712aee87ccdddee1040ce5c474edaa60a1ece"
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
#line 4 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
using CurlingSimulator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/pick-format")]
    public partial class PickFormat : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1 class=\"page-title pick-format-title\">Pick Tournament Format</h1>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "format-container");
            __builder.AddMarkupContent(3, "\r\n    ");
            __builder.OpenElement(4, "button");
            __builder.AddAttribute(5, "class", "format-button");
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 13 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
                                              e => NavigateToPickTeams(TournamentType.Worlds, Gender.Female)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(7, "\r\n        <span class=\"format-button-logo world-women-logo\"></span> \r\n        ");
            __builder.AddMarkupContent(8, "<span class=\"format-button-text\">Women\'s Worlds</span>\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n    ");
            __builder.OpenElement(10, "button");
            __builder.AddAttribute(11, "class", "format-button");
            __builder.AddAttribute(12, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 17 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
                                              e => NavigateToPickTeams(TournamentType.Worlds, Gender.Male)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(13, "\r\n        <span class=\"format-button-logo world-men-logo\"></span> \r\n        ");
            __builder.AddMarkupContent(14, "<span class=\"format-button-text\">Men\'s Worlds</span>\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n    ");
            __builder.OpenElement(16, "button");
            __builder.AddAttribute(17, "class", "format-button");
            __builder.AddAttribute(18, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 21 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
                                              e => NavigateToPickTeams(TournamentType.CanadaNational, Gender.Female)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(19, "\r\n        <span class=\"format-button-logo scotties-logo\"></span> \r\n        ");
            __builder.AddMarkupContent(20, "<span class=\"format-button-text\">Scotties</span>\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n    ");
            __builder.OpenElement(22, "button");
            __builder.AddAttribute(23, "class", "format-button");
            __builder.AddAttribute(24, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 25 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
                                              e => NavigateToPickTeams(TournamentType.CanadaNational, Gender.Male)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(25, "\r\n        <span class=\"format-button-logo brier-logo\"></span> \r\n        ");
            __builder.AddMarkupContent(26, "<span class=\"format-button-text\">Brier</span>\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "C:\Users\brthi\Projects\curling-simulator\Pages\PickFormat.razor"
        

    protected override void OnInitialized()
    {
        PageState.SetState(LoadingState.Finished, null);
    }

    private async void NavigateToPickTeams(TournamentType tournamentType, Gender gender) 
    {
        PageState.SetState(LoadingState.Loading, "Loading teams...");
        await Task.Delay(1000);
        
        SimulationContainer.TournamentType = TournamentType.Worlds;
        SimulationContainer.Gender = gender;
        SimulationContainer.Rankings = null;
        SimulationContainer.RestartOrInitTournament();
        await localStorage.SetItemAsync("SimulationContainer", JsonConvert.SerializeObject(SimulationContainer)); 
        NavigationManager.NavigateTo("pick-teams");

    } 

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PageState PageState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private WorldsTournament TournamentObject { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SimulationContainer SimulationContainer { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
