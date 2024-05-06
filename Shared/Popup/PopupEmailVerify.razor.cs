using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using GoplayasiaBlazor.Core.Global.Interface;

namespace GoPlayAsiaWebApp.Shared.Popup;

public partial class PopupEmailVerify
{
    #region Local Variables
    [Inject] ICurrentUser _currentUser { get; set; }

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public string? EmailAddress { get; set; }
    public string Code { get; private set; } = "";
    private string OTP { get; set; } = "";
    public bool errorCode { get; set; } = false;
    public string errorMsg { get; set; } = "";
    #endregion

    #region Lifecycle Methods

    protected override async Task OnInitializedAsync()
    {
        await RequestNewCode();
    }
    #endregion

    #region Local Methods
    public async Task ReqNewCode()
    {
        await RequestNewCode();
    }
    public async Task Validate()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        if (string.IsNullOrEmpty(Code) || Code.Length != 6)
        {
            errorCode = false;
            errorMsg = "Verification Code cannot be empty and must be exactly 6 characters in length";
            return;
        }

        var verifyEmail = await _accountService.VerifyCode(EmailAddress, Code, _currentUser.Id);

        if (!verifyEmail)
        {
            errorCode = false;
            errorMsg = "An error occured while attempting to verify email address. Please try again later";
            return;
        }
        else
        {
            errorCode = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok(true));
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


        var codeSent = await _accountService.RequestCode(EmailAddress, _currentUser.Id);
        popupRes.Close();
        if (string.IsNullOrEmpty(codeSent))
        {
            errorMsg = "An error occured while sending code. Request for a new one";
            errorCode = false;
            return;
        }
        Code = codeSent;

    }
    #endregion

}
