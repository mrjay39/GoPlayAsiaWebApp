using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Main.Login;

public partial class ForgotPassword
{
    #region Injected Services
    [Inject] ForgotPasswordViewModel _forgotpasswordViewModel { get; set; }
    #endregion


    #region Local Variables
    [Parameter]
    public string message { get; set; } = "Input the Mobile Number that you have used to register with us and we will send you an One-Time Password";

    private bool hideOptButton = true;
    private bool hideOptEmail = true;
    private bool hideOptMobile = false;

    private bool hideVerification = true;
    private bool hidePassword = true;
    private bool otppass = false;
    private bool emailpass = false;

    #endregion


    #region Local Methods
    private async Task optEmail()
    {
        message = "Input the Email Address that you have used to register with us and we will send you verification code";
        hideOptButton = true;
        hideOptEmail = false;
    }

    private async Task optMobile()
    {
        message = "Input the Mobile Number that you have used to register with us and we will send you an One-Time Password";
        hideOptButton = true;
        hideOptMobile = false;
    }

    private async Task SubmitOTP()
    {
        bool result = await _forgotpasswordViewModel.SubmitSendToMobileNumber();
        if (result)
        {
            message = "Input the one-time password sent to <br />" + _forgotpasswordViewModel.MobileNumber;
            hideOptMobile = true;
            hideOptEmail = true;
            hideVerification = false;
        }
    }

    private async Task SubmitCode()
    {
        bool result = await _forgotpasswordViewModel.SubmitSendToEmailAddress();
        if (result)
        {
            message = "Input verification code sent to <br />" + _forgotpasswordViewModel.EmailAddress;
            hideOptEmail = true;
            hideVerification = false;
        }
    }

    private async Task RequestNewCode()
    {
        await _forgotpasswordViewModel.RequestNewCode();
    }

    private async Task VerifyCode()
    {
        bool result = await _forgotpasswordViewModel.SubmitVerifyCode();
        if (result)
        {
            if (otppass && emailpass)
            {
                message = "Enter your new password";
                hideVerification = true;
                hidePassword = false;
            }
            else if (!otppass)
            {
                //hideOptEmail = false;
                //hideOptMobile = true;
                //hideVerification = true;
                //message = "Input the Email Address that you have used to register with us and we will send you verification code";
                //_forgotpasswordViewModel.ReferenceNo = "";

                //otppass = true;

                message = "Enter your new password";
                hideVerification = true;
                hidePassword = false;
            }
            else if (!emailpass)
            {
                message = "Enter your new password";
                hideVerification = true;
                hidePassword = false;

                emailpass = true;
            }
        }
    }

    private async Task SubmitNewPassword()
    {
        await _forgotpasswordViewModel.SubmitResetPassword();
    }
    #endregion


    #region Lifecylce Method
    #endregion@endre
}
