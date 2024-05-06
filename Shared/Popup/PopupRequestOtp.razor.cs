using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.DTOs.DTOOut;
using Blazored.Toast.Services;
using System.Diagnostics.Metrics;
using GoplayasiaBlazor.Core.Services.Interface;

namespace GoPlayAsiaWebApp.Shared.Popup;

public partial class PopupRequestOtp
{
    #region Local Variables
    [Inject] ICurrentUser _currentUser { get; set; }
    [Inject] ITransactionService _transactionService { get; set; }
    [Inject] IToastService _toastService { get; set; }

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public string Password { get; set; }
    [Parameter] public decimal Amount { get; set; }
    [Parameter] public int SelectionId { get; set; }
    [Parameter] public string? ReferralKey { get; set; }
    public string ReferenceNo { get; private set; } = "";
    private string Code { get; set; } = "";
    public bool errorCode { get; set; } = false;
    public string errorMsg { get; set; } = "";
    #endregion

    #region Lifecycle Methods

    protected override async Task OnInitializedAsync()
    {
        StartTimer();
        await RequestNewCode();
    }
    #endregion

    #region Local Methods
    public async Task ReqNewCode()
    {
        counter = 60;
        StartTimer();
        await RequestNewCode();
    }
    public async Task Validate()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        if (string.IsNullOrEmpty(Code) || Code.Length != 6)
        {
            errorMsg = "OTP cannot be empty and must be exactly 6 characters in length";
            errorCode = false;
            popupRes.Close();
            return;
        }

        var verifyOtp = await _accountService.VerifyOTP(ReferenceNo, Code);
        if (!verifyOtp)
        {
            errorMsg = "OTP is either incorrect or expired. Request for a new one";
            errorCode = false;
            popupRes.Close();
            return;
        }

        await BlazoredModal.CloseAsync(ModalResult.Ok(true));
        popupRes.Close();
    }

    public async Task CancelVerification()
    {
        await BlazoredModal.CloseAsync(ModalResult.Ok(false));
    }
    public async Task RequestNewCode()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        var referenceNo = await _accountService.RequestOTP(_currentUser.MobileNumber);
        popupRes.Close();

        if (referenceNo == null || string.IsNullOrEmpty(referenceNo.ReferenceNo))
        {
            errorMsg = "An error occured while sending OTP. Request for a new one";
            errorCode = false;
            return;
        }
        ReferenceNo = referenceNo.ReferenceNo;
    }
    #endregion

}
