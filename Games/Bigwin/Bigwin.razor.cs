using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using GoPlayAsiaWebApp.Shared.Popup;

namespace GoPlayAsiaWebApp.Games.Bigwin;

public partial class Bigwin
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    public IModalReference popupRef { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            iBigWinViewModel.Notify += OnNotify;
            iBigWinViewModel.popupModal = popupModal;

            await iBigWinViewModel.ConnectSignalR();
            await iBigWinViewModel.AssignSignalRMethods();
            popupRef = popupModal.Show<PopupLoading>("");

            await iBigWinViewModel.GetGameSetting();
            await iBigWinViewModel.GetGameRound();
            await iBigWinViewModel.GetCurrentRoundBets();

            if (iBigWinViewModel.streamKey == string.Empty)
                await iBigWinViewModel.GenerateStreamKey();
            popupRef.Close();

            await iBigWinViewModel.GetTrends();
        }
        catch (Exception)
        {
            //throw;
        }
    }

    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        //iBigWinViewModel.DisconnectSignalR();
    }

    public async ValueTask DisposeAsync()
    {
        iBigWinViewModel.Notify -= OnNotify;
        //await iBigWinViewModel.DisconnectSignalR();
    }
}
