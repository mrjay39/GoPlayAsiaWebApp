using Blazored.Modal.Services;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.Withdraw;

public partial class Withdraw
{
    public string showpass = "password";
    public string showpassicon = "bi bi-eye-fill";
    public void ShowPassword()
    {
        if (showpass == "password")
        {
            showpass = "text";
            showpassicon = "bi bi-eye-slash-fill";
        }
        else
        {
            showpass = "password";
            showpassicon = "bi bi-eye-fill";
        }
    }

    #region Injected Services
    [Inject] MainCreditViewModel _mainCreditsViewModel { get; set; }
    [Parameter]
    public UserDTO UserInfo { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }


    public string PgHeader { get; set; } = "";
    private string paymentGatewayPaymentItem { get; set; }


    private bool hidePGWithdrawGCash = true;
    private bool hidePGOtherBanks = true;
    private bool hidePGWithdrawUB = true;
    private bool hideWithdrawCredit = true;
    private bool hideWithdrawGoplay = true;
    private int isOpened = 0;

    public UBBanks SelectedBank { get; private set; }





    private bool hideWithdrawGCash = false;
    private bool hideWithdrawPaymentGateway = true;
    private bool hideWithdrawUB = true;
    private bool hideWithdrawCreditbtn = false;
    private bool hideWithdrawGoplaybtn = false;
    private bool hideWithdrawPaymentGatewaybtn = false;

    public IEnumerable<string> PaymentList = new List<string>()
     {
        "GCASH"
     };
    public IEnumerable<string> PaymentListPG = new List<string>()
     {
        "SELECT TYPE OF PAYMENT",
        "UNION BANK",
        "GCASH",
        "OTHER BANKS"
     };
    private string paymentItem { get; set; }


    [Parameter]
    public string TypeOfPayment
    {
        get { return paymentItem; }
        set
        {
            if (paymentItem != value)
            {
                paymentItem = value;
                if (ItemChanged.HasDelegate)
                {
                    ItemChanged.InvokeAsync(value);
                }
            }
        }
    }

    [Parameter]
    public EventCallback<string> ItemChanged { get; set; }
    #endregion


    #region Local Methods
    private async Task UpdateState()
    {
        StateHasChanged();
    }
    public async Task SelPaymentGateway(string SelGateway)
    {
        //await CashoutTutorial();
        await _mainCreditsViewModel.Clear();
        _mainCreditsViewModel.WithdrawRequestUBParamsDtl.AccountNumber = string.Empty;
        paymentGatewayPaymentItem = SelGateway;

        PgHeader = SelGateway;
        hidePGWithdrawGCash = true;
        hidePGOtherBanks = true;
        hidePGWithdrawUB = true;
        hideWithdrawCredit = true;

        if (paymentGatewayPaymentItem == "GCASH")
        {
            if (isOpened == 1)
            {
                hidePGWithdrawGCash = true;
                isOpened = 0;
            }
            else
            {
                hidePGWithdrawGCash = false;
                isOpened = 1;
            }
        }
        else if (paymentGatewayPaymentItem == "OTHER BANKS")
        {
            PgHeader = "INSTAPAY";
            if (isOpened == 2)
            {
                hidePGOtherBanks = true;
                isOpened = 0;
            }
            else
            {
                hidePGOtherBanks = false;
                isOpened = 2;
            }
        }
        else if (paymentGatewayPaymentItem == "UNION BANK")
        {
            if (isOpened == 3)
            {
                hidePGWithdrawUB = true;
                isOpened = 0;
            }
            else
            {
                hidePGWithdrawUB = false;
                isOpened = 3;
            }
        }
        else if (paymentGatewayPaymentItem == "OTC")
        {
            isOpened = 0;
            PgHeader = "OVER THE COUNTER";
            hideWithdrawCredit = false;
        }
    }

    #region PG GCASH
    private async Task WithdrawUBGcash()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        await _mainCreditsViewModel.WithdrawUBGcash();
        popupRes.Close();
    }
    #endregion

    #region PG INSTAPAY
    private async Task<IEnumerable<UBBanks>> SearchBanks(string searchText)
    {
        return _mainCreditsViewModel.InstapayBanks.Where(x => x.bank.ToLower().StartsWith(searchText.ToLower())).OrderBy(s => s.bank).ToList();
    }
    private async Task WithdrawUBInstaPay()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        _mainCreditsViewModel.SelectedBank = SelectedBank;
        await _mainCreditsViewModel.WithdrawUBInstaPay();
        popupRes.Close();
    }
    #endregion

    #region PG UNION BANK
    private async Task WithdrawUBToUB()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        await _mainCreditsViewModel.WithdrawUBToUB();
        popupRes.Close();
    }
    #endregion

    #region GPA OTC
    private async Task RequestWithdrawCredit()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        await _mainCreditsViewModel.WithdrawDirectly();
        popupRes.Close();
    }
    #endregion

    #region GPA GCASH
    private async Task RequestWithdrawGCashGoplay()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        await _mainCreditsViewModel.WithdrawGCashGoPlay();
        popupRes.Close();
    }
    #endregion

    #region UNUSED
    //public async Task showWithdrawCreditForm()
    //{
    //    await _mainCreditsViewModel.Clear();
    //    hideWithdrawPaymentGateway = true;
    //    hideWithdrawGoplay = true;

    //    if (hideWithdrawCredit)
    //    {
    //        hideWithdrawCreditbtn = false;
    //        hideWithdrawGoplaybtn = true;
    //        hideWithdrawPaymentGatewaybtn = true;

    //        hideWithdrawCredit = false;
    //        hideWithdrawGoplay = true;
    //    }
    //    else
    //    {
    //        hideWithdrawCreditbtn = false;
    //        hideWithdrawGoplaybtn = false;
    //        hideWithdrawPaymentGatewaybtn = false;

    //        hideWithdrawCredit = true;
    //        hideWithdrawGoplay = true;
    //    }
    //}
    //public async Task showWithdrawGoplayForm()
    //{
    //    hideWithdrawPaymentGateway = true;
    //    hideWithdrawCredit = true;
    //    await _mainCreditsViewModel.Clear();

    //    if (hideWithdrawGoplay)
    //    {
    //        hideWithdrawCreditbtn = true;
    //        hideWithdrawGoplaybtn = false;
    //        hideWithdrawPaymentGatewaybtn = true;

    //        hideWithdrawGoplay = false;
    //        hideWithdrawCredit = true;
    //    }
    //    else
    //    {
    //        hideWithdrawCreditbtn = false;
    //        hideWithdrawGoplaybtn = false;
    //        hideWithdrawPaymentGatewaybtn = false;

    //        hideWithdrawGoplay = true;
    //        hideWithdrawCredit = true;
    //    }
    //}
    //public async Task showTypeOfPaymentForm(ChangeEventArgs e)
    //{

    //    paymentItem = e.Value.ToString();
    //    if (paymentItem == "UNION BANK")
    //    {
    //        hideWithdrawUB = false;
    //        hideWithdrawGCash = true;
    //    }
    //    else if (paymentItem == "GCASH")
    //    {
    //        hideWithdrawUB = true;
    //        hideWithdrawGCash = false;
    //    }
    //    else
    //    {
    //        hideWithdrawUB = true;
    //        hideWithdrawGCash = true;
    //    }
    //}
    //public async Task showPaymentGatewayForm(ChangeEventArgs e)
    //{
    //    //await CashoutTutorial();
    //    _mainCreditsViewModel.WithdrawRequestUBParamsDtl.AccountNumber = String.Empty;
    //    paymentGatewayPaymentItem = e.Value.ToString();



    //    hidePGWithdrawUB = true;
    //    hidePGWithdrawGCash = true;
    //    hidePGOtherBanks = true;

    //    if (paymentGatewayPaymentItem == "UNION BANK")
    //    {
    //        hidePGWithdrawUB = false;
    //    }
    //    else if (paymentGatewayPaymentItem == "GCASH")
    //    {
    //        hidePGWithdrawGCash = false;
    //    }
    //    else if (paymentGatewayPaymentItem == "OTHER BANKS")
    //    {
    //        hidePGOtherBanks = false;
    //    }
    //}
    //public async Task showWithdrawPaymentGateway()
    //{

    //    await _mainCreditsViewModel.Clear();
    //    hideWithdrawGoplay = true;
    //    hideWithdrawCredit = true;
    //    hidePGWithdrawUB = true;
    //    hidePGWithdrawGCash = true;
    //    hidePGOtherBanks = true;

    //    if (hideWithdrawPaymentGateway)
    //    {
    //        hideWithdrawCreditbtn = true;
    //        hideWithdrawGoplaybtn = true;
    //        hideWithdrawPaymentGatewaybtn = false;

    //        hideWithdrawPaymentGateway = false;
    //    }
    //    else
    //    {
    //        hideWithdrawCreditbtn = false;
    //        hideWithdrawGoplaybtn = false;
    //        hideWithdrawPaymentGatewaybtn = false;

    //        hideWithdrawPaymentGateway = true;
    //    }
    //}
    //private async Task CashoutTutorial()
    //{
    //    var popupConfirm = popupModal.Show<UpayTut>("", new ModalOptions() { Class = "gamerules-modal", HideHeader = false });
    //    ModalResult result = await popupConfirm.Result;
    //    if (result.Data != null)
    //    {
    //        popupConfirm.Close();
    //    }
    //}
    //private async Task RequestWithdrawUBGoplay()
    //{
    //    var popupRes = popupModal.Show<PopupLoading>("");
    //    await _mainCreditsViewModel.WithdrawUBGoPlay();
    //    popupRes.Close();
    //}

    #endregion

    #endregion

    protected override async Task OnInitializedAsync()
    {
        _mainCreditsViewModel.Notify += OnNotify;
        _mainCreditsViewModel.popupModal = popupModal;
        await _mainCreditsViewModel.ConnectSignalR();
        await _mainCreditsViewModel.GetInstapayBanks();
    }

    public async Task Verification()
    {
        await _mainCreditsViewModel.Verification();
    }

    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }

    #region UNUSED
    //public async ValueTask DisposeAsync()
    //{
    //    //await _mainCreditsViewModel.DisconnectSignalR();
    //}
    //public void Dispose()
    //{
    //    _mainCreditsViewModel.Notify -= OnNotify;
    //}
    //public async Task popTutorialWithdraw()
    //{
    //    var parameters = new ModalParameters();
    //    parameters.Add("Hlink", "https://drive.google.com/file/d/1FKwD8_JkIaPiyfkdkGO_21kD23Hm-thq/preview");
    //    var popupConfirm = popupModal.Show<UpayTut>("", parameters, new ModalOptions() { Class = "gamerules-modal", HideHeader = false });
    //    ModalResult result = await popupConfirm.Result;
    //    if (result.Data != null)
    //    {
    //        popupConfirm.Close();
    //    }
    //}
    //public async Task popTutorialWithdrawotc()
    //{
    //    var parameters = new ModalParameters();
    //    parameters.Add("Hlink", "https://drive.google.com/file/d/1YET4LH7XUAYAWSippiewxMYwuT8so3If/preview");
    //    var popupConfirm = popupModal.Show<UpayTut>("", parameters, new ModalOptions() { Class = "gamerules-modal", HideHeader = false });
    //    ModalResult result = await popupConfirm.Result;
    //    if (result.Data != null)
    //    {
    //        popupConfirm.Close();
    //    }
    //}
    //public async Task popTutorialWithdrawGcash()
    //{
    //    var parameters = new ModalParameters();
    //    parameters.Add("Hlink", "https://drive.google.com/file/d/1WpM9qbH1D4Wd05FpChBl5jnayhEKOUNg/preview");
    //    var popupConfirm = popupModal.Show<UpayTut>("", parameters, new ModalOptions() { Class = "gamerules-modal", HideHeader = false });
    //    ModalResult result = await popupConfirm.Result;
    //    if (result.Data != null)
    //    {
    //        popupConfirm.Close();
    //    }
    //}
    #endregion

}
