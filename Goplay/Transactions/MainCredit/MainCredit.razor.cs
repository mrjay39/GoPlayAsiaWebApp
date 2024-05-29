using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.MainCredit;

public partial class MainCredit
{
    #region Injected Services
    [Inject] MainCreditViewModel _mainCreditsViewModel { get; set; }
    [Inject] ICurrentUser _currentUser { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    public string activeaddcredit { get; set; }
    public string activetransaction { get; set; }
    public string activewithdraw { get; set; }
    #endregion

    #region Local Methods
    private async Task getUserInfo(int? tran)
    {
        activetransaction = "";
        activewithdraw = "";
        activeaddcredit = "";
        if (tran == 1)
        {
            activeaddcredit = "active";
        }else if (tran == 2)
        {
            activewithdraw = "active";
        }
        await _mainCreditsViewModel.GetUserInfo();

    }
    private async Task GetTransactionsByUserId()
    {
        activetransaction = "active";
        activewithdraw = "";
        activeaddcredit = "";
        await _mainCreditsViewModel.GetTransactionsByUserId();

    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var currentRoute = MyNavigationManager.Uri.ToLower().ToString().Replace(MyNavigationManager.BaseUri, "");
            if (currentRoute.ToLower().Contains("transaction"))
            {
                activetransaction = "active";
            }
            else if (currentRoute.ToLower().Contains("withdraw"))
            {
                activewithdraw = "active";
                await _mainCreditsViewModel.GetUserInfo();
            }
            else
            {
                activeaddcredit = "active";
            }
            StateHasChanged();
        }

    }

    protected override async Task OnInitializedAsync()
    {
        _mainCreditsViewModel.popupModal = popupModal;
        _mainCreditsViewModel.Notify += OnNotify;
        var popupRes = popupModal.Show<PopupLoading>("");
        await _mainCreditsViewModel.GetActiveVouchers();
        await _mainCreditsViewModel.GetUserInfo();
        await _mainCreditsViewModel.ConnectSignalR();
        popupRes.Close();
        if (_mainCreditsViewModel._icurrentUser.Status != 1)
        {
            _mainCreditsViewModel._navigationManager.NavigateTo("lobby");
        }
        _mainCreditsViewModel.Notify += OnNotify;
    }
    #endregion
    public async ValueTask DisposeAsync()
    {
        //await _mainCreditsViewModel.DisconnectSignalR();
    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public void Dispose()
    {
        _mainCreditsViewModel.Notify -= OnNotify;
    }


}