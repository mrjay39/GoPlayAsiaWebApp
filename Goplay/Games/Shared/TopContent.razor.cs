using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Shared;

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
