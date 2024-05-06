using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Services;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static GoplayasiaBlazor.Models.Constants.Settings;
using static System.Net.Mime.MediaTypeNames;

namespace GoPlayAsiaWebApp.Goplay.Main.Signup;

public partial class Signup
{
    public string showpass = "password";
    public string showpassicon = "bi bi-eye-fill";
    public string showpassConfirm = "password";
    public string showpassiconConfirm = "bi bi-eye-fill";

    #region Injected Services 
    [Inject] IConstantService iConstantService { get; set; }
    [Inject] IAccountService _accountService { get; set; }
    [Inject] SignUpViewModel _signupViewModel { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }

    [Inject] IMapper _mapper { get; set; }
    [Inject] IToastService toastService { get; set; }
    #endregion


    #region Local Variables

    private EditContext? editContext;
    private bool step1 { get; set; } = false;
    private bool step2 { get; set; } = true;


    private bool submitPress = false;
    private bool hideMobile = true;
    private bool refWarning = true;
    private bool agreePolicy = true;
    private bool disableSubmit = true;
    private bool disbaleLogin = true;
    private bool isrefkeyenabled = true;
    private bool isChecked { get; set; } = false;
    private bool refDisable = true;
    private bool refIsChecked = false;
    private string msgMobile = "";
    private string submitClass = "disSubmit";
    private string refClass = "referralKey";
    private string refValue { get; set; }
    private string refOldvalue = "";


    private string _termsAndConditionsUrl;
    public string TermsAndConditionsUrl
    {
        get => _termsAndConditionsUrl;
        set
        {
            _termsAndConditionsUrl = value;
        }
    }
    private string _privacyPolicyUrl;
    public string PrivacyPolicyUrl
    {
        get => _privacyPolicyUrl;
        set
        {
            _privacyPolicyUrl = value;
        }
    }


    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public string refkey { get; set; } = "";


    public IModalReference refpopuLoadingpModal { get; set; }
    private ElementReference txtUsername;
    private ElementReference txtMobile;


    string CurrUrl;
    private CancellationTokenSource cancelation;
    protected override void OnParametersSet()
    {
        //CurrUrl = _navigationManager.Uri.ToString();
    }

    #endregion


    #region Events

    private async Task ValidateStep1()
    {
        bool verMobile = await _signupViewModel.VerifyMobileReg(_signupViewModel.SignupDTO.MobileNumber);
        if (!verMobile)
        {
            submitPress = false;
            return;
        }

        refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
        bool valStep1 = await _signupViewModel.Step1Validation();
        if (valStep1)
        {
            step1 = true;
            step2 = false;
            txtUsername.FocusAsync();
        }
        refpopuLoadingpModal.Close();
    }
    public async Task Register()
    {
        if (submitPress)
        {
            return;
        }
        submitPress = true;

        await _signupViewModel.PlayerRegistration();
        submitPress = false;
    }

    #endregion


    #region Local Methods

    #region STEP 1 
    private async Task onchange_MobileNumber(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        string valMobile = await _signupViewModel.ValidateMobile(value);
        msgMobile = valMobile;

        if (value.Length == 0)
        {
            #region DISABLE Sign Up button
            disableSubmit = true;
            submitClass = "disSubmit";
            #endregion
        }
        else
        {
            #region ENABLE Sign Up button
            disableSubmit = false;
            submitClass = "";
            #endregion
        }

        if (valMobile != "Ok")
        {
            #region DISABLE Sign Up button
            hideMobile = false;

            disableSubmit = true;
            submitClass = "disSubmit";
            #endregion
        }
        else
        {
            #region GET Previous Referral Key
            _signupViewModel.SignupDTO.ReferralKey = string.Empty;

            var resRefkey = await _signupViewModel.GetReferralKey(value);
            if (!string.IsNullOrEmpty(resRefkey.ReferralKey))
            {
                _signupViewModel.SignupDTO.ReferralKey = resRefkey.ReferralKey;
            }
            hideMobile = true;
            refWarning = true;
            #endregion
        }

        if (!agreePolicy)
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
        if (!refWarning)
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
    }
    private async Task onchange_ReferralKey(ChangeEventArgs e)
    {
        refValue = (string)e.Value;
        if (refValue is not null)
        {
            var referralKeyChecker = await _accountService.ReferralKeyChecker(refValue);
            if (!referralKeyChecker)
            {
                if (refValue == "" || refValue is null)
                {
                    refWarning = true;

                    disableSubmit = false;
                    submitClass = "";
                }
                else
                {
                    refWarning = false;

                    disableSubmit = true;
                    submitClass = "disSubmit";
                }
            }
            else
            {
                refWarning = true;

                disableSubmit = false;
                submitClass = "";
            }
        }
        if (msgMobile != "Ok")
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
        if (!agreePolicy)
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
        refOldvalue = refValue;
    }
    private void focusout_ReferralKey()
    {
        if (_signupViewModel.SignupDTO.ReferralKey == null || _signupViewModel.SignupDTO.ReferralKey == "")
        {
            refIsChecked = false;
            refClass = "referralKey";
            refDisable = true;
        }
    }
    private async Task agreeCheckboxChanged(ChangeEventArgs e)
    {
        bool value = (bool)e.Value;
        if (value)
        {
            disableSubmit = false;
            submitClass = "";
        }
        else
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
        if (msgMobile != "Ok")
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
        if (!refWarning)
        {
            disableSubmit = true;
            submitClass = "disSubmit";
        }
    }
    #endregion

    #region STEP 2
    private async Task onchange_Username(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        string valUsername = await _signupViewModel.ValidateUsername(value);
        _signupViewModel.errUsername = valUsername;

        if (value.Length == 0)
        {
            _signupViewModel.errUsername = "Username is required";

            disbaleLogin = true;
            submitClass = "disSubmit";
        }
        else if (_signupViewModel.errUsername != "Ok")
        {
            disbaleLogin = true;
            submitClass = "disSubmit";
        }
        else
        {
            if (!string.IsNullOrEmpty(_signupViewModel.SignupDTO.Password) &&
            !string.IsNullOrEmpty(_signupViewModel.SignupDTO.ConfirmPassword))
            {
                disbaleLogin = false;
                submitClass = "";
            }
            else
            {
                disbaleLogin = true;
                submitClass = "disSubmit";
            }
        }
    }
    private async Task onchange_Password(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        string valPass = await _signupViewModel.ValidatePassword(value, _signupViewModel.SignupDTO.ConfirmPassword);
        //msgPassword = valPass;
        _signupViewModel.errPass = valPass;
        if (value.Length == 0)
        {
            disbaleLogin = true;
            submitClass = "disSubmit";
        }
        else
        {
            if (
            !string.IsNullOrEmpty(_signupViewModel.SignupDTO.Username) &&
            !string.IsNullOrEmpty(_signupViewModel.SignupDTO.ConfirmPassword))
            {
                disbaleLogin = false;
                submitClass = "";
            }
            else
            {
                disbaleLogin = true;
                submitClass = "disSubmit";
            }
        }
    }
    private async Task onchange_ConfirmPassword(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        string valPass = await _signupViewModel.ValidatePassword(_signupViewModel.SignupDTO.Password, value);
        //msgPassword = valPass;
        _signupViewModel.errPass = valPass;

        if (value.Length == 0)
        {
            disbaleLogin = true;
            submitClass = "disSubmit";
        }
        else if (@_signupViewModel.errPass != "Ok")
        {
            disbaleLogin = true;
            submitClass = "disSubmit";
        }
        else
        {
            if (
            !string.IsNullOrEmpty(_signupViewModel.SignupDTO.Username) &&
            !string.IsNullOrEmpty(_signupViewModel.SignupDTO.Password))
            {
                disbaleLogin = false;
                submitClass = "";
            }
            else
            {
                disbaleLogin = true;
                submitClass = "disSubmit";
            }
        }
    }
    #endregion

    public void ShowPassword(int type)
    {
        if (type == 1)
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
        else if (type == 2)
        {
            if (showpassConfirm == "password")
            {
                showpassConfirm = "text";
                showpassiconConfirm = "bi bi-eye-slash-fill";
            }
            else
            {
                showpassConfirm = "password";
                showpassiconConfirm = "bi bi-eye-fill";
            }
        }

    }
    #endregion


    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        _signupViewModel._popupModal = popupModal;
        _signupViewModel.SignupDTO = new SignupDTO();
        if (!string.IsNullOrEmpty(refkey))
        {
            refIsChecked = true;
            refClass = "";
            refDisable = false;
        }
        _signupViewModel.SignupDTO.ReferralKey = refkey;
        if (string.IsNullOrEmpty(refkey))
        {
            isrefkeyenabled = false;
        }
        _signupViewModel.SignupDTO.AddressAreSame = isChecked;
        editContext = new(_signupViewModel.SignupDTO);

        var popload = popupModal.Show<PopupLoading>("");
        var constants = await iConstantService.GetCorporationSettings();
        if (constants != null)
        {
            TermsAndConditionsUrl = constants.TermsAndConditionsUrl;
            PrivacyPolicyUrl = constants.PrivacyPolicyUrl;
        }
        CurrUrl = _navigationManager.Uri.ToString();
        if (CurrUrl.ToLower().Contains("agentsignup"))
        {
            _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.Agent;
        }
        else if (CurrUrl.ToLower().Contains("masignup"))
        {
            _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.MasterAgent;
        }
        else
        {
            _signupViewModel.SignupDTO.RoleType = (int)RoleTypes.Player;
        }
        txtMobile.FocusAsync();
        // txtUsername.FocusAsync();
        cancelation = new CancellationTokenSource();

        //await _signupViewModel.initializeSurvey();

        popload.Close();

    }
    #endregion

}
