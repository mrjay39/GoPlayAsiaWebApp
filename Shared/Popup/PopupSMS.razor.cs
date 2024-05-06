using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Shared.Popup;

public partial class PopupSMS
{
    #region Local Variables
    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public string? MobileNumber { get; set; }
    public string ReferenceNo { get; private set; } = "";
    private string OTP { get; set; } = "";
    public bool errorOTP { get; set; } = false;
    public string errorOTMMSG { get; set; } = "";
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
        var validated = await _accountService.VerifyOTP(ReferenceNo, OTP);
        if (validated)
        {
            errorOTP = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok(true));
        }
        else
        {
            errorOTMMSG = "Invalid OTP Code entered, please try again...;";
            errorOTP = false;
        }
        popupRes.Close();
    }
    public async Task CancelVerification()
    {
        await BlazoredModal.CloseAsync(ModalResult.Ok(false));
    }
    public async Task RequestNewCode()
    {
        var popupRes = popupModal.Show<PopupLoading>("");


        var referenceNo = await _accountService.RequestOTP(MobileNumber);
        popupRes.Close();
        if (referenceNo == null || string.IsNullOrEmpty(referenceNo.ReferenceNo))
        {
            errorOTMMSG = "An error occured while sending OTP. Request for a new one";
            errorOTP = false;
            return;
        }
        ReferenceNo = referenceNo.ReferenceNo;

    }
    #endregion

}
