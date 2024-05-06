using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Helpers;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class ForgotPasswordViewModel
{
    #region Local Variable & Properties
    IToastService _toastService;
    IModalService _popupModal;
    IAccountService _accountService;
    public NavigationManager _navigationManager;

    public string EmailAddress { get; set; }
    public string MobileNumber { get; set; }
    public string Code { get; set; }
    public string ReferenceNo { get; set; }
    public string JWTToken { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
    #endregion

    #region Lifecycle methods
    public ForgotPasswordViewModel(IToastService toastService, IModalService popupModal, IAccountService accountService, NavigationManager navigationManager)
    {
        _toastService = toastService;
        _popupModal = popupModal;
        _accountService = accountService;
        _navigationManager = navigationManager;
    }
    #endregion

    #region Local Methods
    public async Task<bool> SubmitSendToEmailAddress()
    {
        if (string.IsNullOrEmpty(EmailAddress) || !await ValidatorHelper.ValidateEmail(EmailAddress))
        {
            _toastService.ShowError("Email Address cannot be empty, cannot contain spaces and must be valid");
            return false;
        }
        var popupRes = _popupModal.Show<PopupLoading>("");
        ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
        {
            EmailAddress = EmailAddress
        };
        var code = await _accountService.ForgotPasswordInitialization(paramsModel);
        popupRes.Close();
        if (code == null)
        {
            _toastService.ShowError("An error occured while attempting to request verification code");
            return false;
        }
        if (!code.Success || string.IsNullOrEmpty(code.Code))
        {
            _toastService.ShowError("An error occured while attempting to request verification code");
            return false;
        }
        return true;
    }

    public async Task<bool> SubmitSendToMobileNumber()
    {
        if (string.IsNullOrEmpty(MobileNumber) || MobileNumber.Length != 11)
        {
            _toastService.ShowError("Mobile Number cannot be empty, cannot containt spaces and must contain 11 characters");
            return false;
        }
        var popupRes = _popupModal.Show<PopupLoading>("");
        ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
        {
            MobileNumber = MobileNumber
        };
        var referenceNo = await _accountService.ForgotPasswordInitialization(paramsModel);
        popupRes.Close();
        if (referenceNo == null)
        {
            _toastService.ShowError("An error occured while attempting to request OTP");
            return false;
        }
        if (!referenceNo.Success || string.IsNullOrEmpty(referenceNo.ReferenceNo))
        {
            _toastService.ShowError("An error occured while attempting to request OTP");
            return false;
        }
        ReferenceNo = referenceNo.ReferenceNo;
        return true;
    }

    public async Task RequestNewCode()
    {
        if (!string.IsNullOrEmpty(EmailAddress))
        {
            var popupRes = _popupModal.Show<PopupLoading>("");
            ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
            {
                EmailAddress = EmailAddress
            };
            var code = await _accountService.ForgotPasswordInitialization(paramsModel);
            popupRes.Close();
            if (code == null)
            {
                _toastService.ShowError("An error occured while attempting to request verification code");
                return;
            }
            if (!code.Success || string.IsNullOrEmpty(code.Code))
            {
                _toastService.ShowError("An error occured while attempting to request verification code");
                return;
            }
        }
        else
        {
            var popupRes = _popupModal.Show<PopupLoading>("");
            ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
            {
                MobileNumber = MobileNumber
            };
            var referenceNo = await _accountService.ForgotPasswordInitialization(paramsModel);
            popupRes.Close();
            if (referenceNo == null)
            {
                _toastService.ShowError("An error occured while attempting to request OTP");
                return;
            }
            if (!referenceNo.Success || string.IsNullOrEmpty(referenceNo.ReferenceNo))
            {
                _toastService.ShowError("An error occured while attempting to request OTP");
                return;
            }
            ReferenceNo = referenceNo.ReferenceNo;
        }
    }

    public async Task<bool> SubmitVerifyCode()
    {
        if (string.IsNullOrEmpty(Code) || Code.Length != 6)
        {
            if (!string.IsNullOrEmpty(EmailAddress))
                _toastService.ShowError("Verification Code cannot be empty and must contain exactly 6 characters");
            else
                _toastService.ShowError("One-Time Password cannot be empty and must contain exactly 6 characters");
            return false;
        }
        if (!string.IsNullOrEmpty(ReferenceNo))
        {
            var popupRes = _popupModal.Show<PopupLoading>("");
            ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
            {
                MobileNumber = MobileNumber,
                ReferenceNo = ReferenceNo,
                Code = Code
            };
            var verifyOTP = await _accountService.VerifyForgotPasswordCode(paramsModel);
            popupRes.Close();
            if (verifyOTP == null)
            {
                _toastService.ShowError("An error occured while attempting to verify OTP");
                return false;
            }
            if (!verifyOTP.Success || string.IsNullOrEmpty(verifyOTP.JWTToken))
            {
                _toastService.ShowError("An error occured while attempting to verify OTP");
                return false;
            }
            JWTToken = verifyOTP.JWTToken;
        }
        else
        {
            var popupRes = _popupModal.Show<PopupLoading>("");
            ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
            {
                EmailAddress = EmailAddress,
                Code = Code
            };
            var verifyCode = await _accountService.VerifyForgotPasswordCode(paramsModel);
            popupRes.Close();
            if (verifyCode == null)
            {
                _toastService.ShowError("An error occured while attempting to verify code");
                return false;
            }
            if (!verifyCode.Success || string.IsNullOrEmpty(verifyCode.JWTToken))
            {
                _toastService.ShowError("An error occured while attempting to verify code");
                return false;
            }
            JWTToken = verifyCode.JWTToken;
        }
        return true;
    }

    public async Task SubmitResetPassword()
    {
        var valPassRes = await ValidatorHelper.ValidatePassword(NewPassword, ConfirmNewPassword);
        if (valPassRes != 0)
        {
            _toastService.ShowError("Password inputs cannot be empty, cannot contain spaces, must match and must be at least 8 characters");
            return;
        }
        var popupRes = _popupModal.Show<PopupLoading>("");
        ForgotPasswordParamsDTO paramsModel = new ForgotPasswordParamsDTO()
        {
            EmailAddress = EmailAddress is null ? "" : EmailAddress,
            MobileNumber = MobileNumber,
            Password = NewPassword,
            JWTToken = JWTToken
        };
        var resetPassword = await _accountService.ResetPassword(paramsModel);
        popupRes.Close();
        if (resetPassword == null)
        {
            _toastService.ShowError("An error occured while attempting to reset password");
            return;
        }
        if (!resetPassword.Success)
        {
            _toastService.ShowError("An error occured while attempting to reset password");
            return;
        }
        _toastService.ShowSuccess("Successfully reset password. Try logging in now.");
        _navigationManager.NavigateTo("/");
    }
    #endregion
}
