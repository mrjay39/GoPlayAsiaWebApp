using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Services.Interface;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Timer = System.Timers.Timer;

namespace GoPlayAsiaWebApp.Main.Lobby;

public partial class Lobby
{

    #region Injected Services
    [Inject] IAccountService _accountService { get; set; }
    [Inject] LobbyViewModel lobbyViewModel { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    #endregion

    #region Local Variables
    [CascadingParameter] public IModalService popupModal { get; set; }
    public IModalReference popupRef { get; set; }
    public IModalReference underreviewpopupRef { get; set; }
    private Timer timerObj;


    private bool cards { get; set; } = true;
    private bool coins { get; set; } = true;
    private bool balls { get; set; } = true;
    private bool dice { get; set; } = true;
    #endregion


    private async Task ShowCards()
    {
        cards = false;
        coins = true;
        balls = true;
        dice = true;
    }
    private async Task ShowCoins()
    {
        cards = true;
        coins = false;
        balls = true;
        dice = true;
    }
    private async Task ShowBalls()
    {
        cards = true;
        coins = true;
        balls = false;
        dice = true;
    }
    private async Task ShowDice()
    {
        cards = true;
        coins = true;
        balls = true;
        dice = false;
    }
    private async Task ShowAll()
    {
        cards = false;
        coins = false;
        balls = false;
        dice = false;
    }

    protected override async Task OnInitializedAsync()
    {
        ShowAll();
        await lobbyViewModel.ConnectSignalR();
        popupRef = popupModal.Show<PopupLoading>("");
        lobbyViewModel.popupModal = popupModal;
        lobbyViewModel.Notify += OnNotify;

        if (lobbyViewModel._icurrentUser.Status == 3 || lobbyViewModel._icurrentUser.Status == 5)
        {
            underreviewpopupRef = popupModal.Show<PopUnderReview>("", new ModalOptions() { Class = "op-modal" });
        }

        popupRef.Close();
        // Set the Timer delay.
        //await lobbyViewModel.GetCorpSettings();
        //timerObj = new Timer(interval: (double)lobbyViewModel.corporationSettings.IdleTimer * 1000);
        //timerObj.Elapsed += UpdateTimer;
        //timerObj.AutoReset = false;
        // Identify whether the user is active or inactive using onmousemove and onkeypress in JS function.
        //await JSRuntime.InvokeVoidAsync("timeOutCall", DotNetObjectReference.Create(this));


    }
    protected override void OnAfterRender(bool firstRender)
    {
    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public void Dispose()
    {
        lobbyViewModel.Notify -= OnNotify;
    }
    public async ValueTask DisposeAsync()
    {
        //await lobbyViewModel.DisconnectSignalR();
    }

    //[JSInvokable]
    //public void TimerInterval()
    //{
    //    // Resetting the Timer if the user in active state.
    //    timerObj.Stop();
    //    // Call the TimeInterval to logout when the user is inactive.
    //    timerObj.Start();
    //}

    //private void UpdateTimer(Object source, ElapsedEventArgs e)
    //{
    //     lobbyViewModel.Logout();
    //}
}
