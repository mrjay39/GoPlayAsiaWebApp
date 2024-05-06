using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.Shared;

public partial class TopContent
{
    [Parameter]
    public string CreditsDisp { get; set; }

    protected override async Task OnInitializedAsync()
    {
        lobbyViewModel.Notify += OnNotify;
    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        lobbyViewModel.Notify -= OnNotify;
    }
}
