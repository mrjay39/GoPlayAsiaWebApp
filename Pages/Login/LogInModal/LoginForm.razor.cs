using Blazored.Modal.Services;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Goplay.Main.Login;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static GoplayasiaBlazor.Models.Constants.Settings;
using System.ComponentModel;
using System.Reflection;

namespace GoPlayAsiaWebApp.Pages.Login.LogInModal;

public partial class LoginForm
{

    #region Injected Services
    [Inject]
    IAccountService _accountService { get; set; }
    [Inject]
    ICurrentUser _iCurrentUser { get; set; }
    [Inject]
    NavigationManager _navigationManager { get; set; }
    [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] IToastService toastService { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Inject] public static IJSRuntime JSRuntimePWA { get; set; }
    #endregion

    #region Local Variables
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public static IModalService popupModalpwa { get; set; }
    [CascadingParameter] public IModalService popuLoadingpModal { get; set; }
    public IModalReference refpopuLoadingpModal { get; set; }
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
    public bool IsLoading { get; set; } = true;
    public bool isCapchaVerified { get; set; } = true;
    string DeviceToken = "";

    LoginDTO loginDTO = new LoginDTO();
    private EditContext? editContext;
    public string showpass = "password";
    public string showpassicon = "bi bi-eye-fill";
    public bool alreadyloggedinshown = false;
    public bool islogginin = false;
    public string Buildnumber = string.Empty;
    private static Func<string, Task> ChangeParaContentActionAsync;
    public bool agreeToTerms { get; set; } = false;
    #endregion

    private async Task LocalChangeParaContentValueAsync(string func)
    {
        await JSRuntime.InvokeVoidAsync("BlazorPWA.installPWA");
    }

    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {

        editContext = new(loginDTO);
        var authState = await authenticationStateTask;
        var user = authState.User;
        _accountService._modal = popupModal;
        //    if ((bool)user.Identity?.IsAuthenticated)
        //    {
        //        _navigationManager.NavigateTo("/lobby");
        //    }
        Buildnumber = Assembly.GetExecutingAssembly().
            GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion.ToString();

        ChangeParaContentActionAsync = LocalChangeParaContentValueAsync;

        DeviceToken = await JSRuntime.InvokeAsync<string>(identifier: "identifyBrowser");

    }
    [JSInvokable("MyBlazorInstallMethod")]
    public static async Task MyBlazorInstallMethod()
    {
        //await JSRuntimePWA.InvokeVoidAsync("BlazorPWA.installPWA");
        //var popupConfirmLogout = popupModalpwa.Show<PopInstallApp>("", new ModalOptions() { Class = "op-modal" });
        //ModalResult result = await popupConfirmLogout.Result;
        //if ((bool)result.Data)
        //{
        //    await JSRuntimePWA.InvokeVoidAsync("BlazorPWA.installPWA");

        //}
        await ChangeParaContentActionAsync?.Invoke("");

    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public void CallbackOnSuccess(string response)
    {
        string EncodedResponse = response;
        bool IsCaptchaValid = ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false;

        if (IsCaptchaValid)
        {
            isCapchaVerified = false;
        }
        else
        {
            isCapchaVerified = true;
        }


    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public void CallbackOnExpired(string response)
    {
        toastService.ShowError("Capcha verification expired...");
    }

    public async ValueTask DisposeAsync()
    {

    }
    #endregion

    #region Local Methods
    async Task IdentifyBrowser()
    {
        string browser = "";
        browser = await JSRuntime.InvokeAsync<string>(identifier: "identifyWebBrowser");
        if (browser != "Mozilla Firefox" && browser != "Microsoft Edge" && browser != "Apple Safari" && browser != "Google Chrome or Chromium")
        {

            popupModal.Show<PopupBrowserWarning>(browser, new ModalOptions() { Class = "op-modal" });

        }

    }
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
    public async Task FullScreen()
    {
        try
        {
            await JSRuntime.InvokeAsync<bool>(identifier: "openFS");
        }
        catch (Exception)
        {

        }

    }
    public async Task<bool> CheckifApple()
    {
        try
        {
            var isApple = await JSRuntime.InvokeAsync<bool>(identifier: "checkifApple");
            return isApple;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task Authenticate()
    {
        try
        {
            if (!agreeToTerms)
            {
                toastService.ShowError("Please agree to the Terms of use and Privacy Policy");
                return;
            }
            var browser = await JSRuntime.InvokeAsync<string>(identifier: "identifyWebBrowser");
            await FullScreen();
            var isApple = await CheckifApple();

            //await JSRuntime.InvokeAsync<string>(identifier: "openFullscreen");
            // await JSRuntime.InvokeVoidAsync("BlazorPWA.installPWA");
            if (editContext != null && editContext.Validate() && !alreadyloggedinshown && !islogginin)
            {
                //if (isCapchaVerified == true)
                //{
                //    toastService.ShowError("Please complete capcha challenge...");
                //    return;

                //}
                islogginin = true;
                refpopuLoadingpModal = popuLoadingpModal.Show<PopupLoading>("");
                //DeviceToken = DeviceToken + Guid.NewGuid().ToString().Substring(0, 6);
                UserDTO userParams = new UserDTO() { Username = loginDTO.Username, Password = loginDTO.Password, DeviceToken = DeviceToken };
                var response = await _accountService.Login(userParams);

                if (response == null)
                {
                    refpopuLoadingpModal.Close();
                    toastService.ShowError("Login failed. Try again later");
                    islogginin = false;
                    return;
                }
                if (!response.Success)
                {
                    if (response.Message.Contains("logged in to another device"))
                    {

                        await _accountService.ForceLogoutUser(response.Id, loginDTO.Username);
                        refpopuLoadingpModal.Close();
                        editContext = new(loginDTO);
                        //return;
                        islogginin = false;
                        await Authenticate();
                        return;
                        var popupConfirmLogout = popupModal.Show<PopupConfirmLogout>("", new ModalOptions() { Class = "op-modal" });
                        alreadyloggedinshown = true;
                        ModalResult result = await popupConfirmLogout.Result;
                        alreadyloggedinshown = false;
                        if ((bool)result.Data)
                        {
                            await _accountService.ForceLogoutUser(response.Id, loginDTO.Username);
                            refpopuLoadingpModal.Close();
                            editContext = new(loginDTO);
                            //return;
                            islogginin = false;
                            await Authenticate();
                        }
                        else
                        {
                            islogginin = false;
                            refpopuLoadingpModal.Close();
                        }



                        return;
                    }
                    else if (response.User != null)
                    {
                        if ((response.User.Status == 3 || response.User.Status == 5) && response.User.RoleType == (int)RoleTypes.Player)
                        {
                            _iCurrentUser.Id = response.User.Id;
                            _iCurrentUser.Username = response.User.Username;
                            _iCurrentUser.FullName = response.User.FullName;
                            _iCurrentUser.Token = response.User.JWTToken;
                            _iCurrentUser.EmailAddress = response.User.EmailAddress;
                            _iCurrentUser.EmailValidated = response.User.EmailValidated;
                            _iCurrentUser.MobileNumber = response.User.MobileNumber;
                            _iCurrentUser.MobileNumberValidated = response.User.MobileNumberValidated;
                            _iCurrentUser.Credits = response.User.Credits;
                            _iCurrentUser.CreditsDisp = string.Format("{0:0,0.00}", response.User.Credits);
                            _iCurrentUser.RoleType = response.User.RoleType;
                            _iCurrentUser.RemainingViewTime = response.User.RemainingViewTime;
                            _iCurrentUser.ProfileImage = response.User.ProfileImageFullPath;
                            _iCurrentUser.PopupTimer = response.User.PopupTimer;
                            _iCurrentUser.IdleTimer = response.User.IdleTimer;
                            _iCurrentUser.DeviceToken = response.User.DeviceToken;
                            _iCurrentUser.Status = response.User.Status;
                            _iCurrentUser.Verified = response.User.Verified;
                            _iCurrentUser.ToppedUp = response.User.ToppedUp;
                            _iCurrentUser.TourWalletShown = false;
                            _iCurrentUser.HoldCredits = response.User.HoldCredits;
                            _iCurrentUser.isApple = isApple;
                            await _iCurrentUser.updateSessionAsync();

                            await ((CustomAuthStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated();
                            if (_iCurrentUser.ToppedUp || _iCurrentUser.Verified != 0)
                            {

                                _navigationManager.NavigateTo("/home");
                            }
                            else
                            {
                                _navigationManager.NavigateTo("/landing");
                            }

                        }
                        else
                        {
                            refpopuLoadingpModal.Close();
                            toastService.ShowError(response.Message);
                            islogginin = false;
                            return;
                        }
                    }
                    else if (!string.IsNullOrEmpty(response.Message))
                    {
                        refpopuLoadingpModal.Close();
                        toastService.ShowError(response.Message);
                        islogginin = false;
                        return;
                    }
                    //await ErrorPopup(string.Empty, "Login failed. Try again later");
                    return;
                }
                if (response.User.RoleType != (int)RoleTypes.Player)
                {
                    refpopuLoadingpModal.Close();
                    islogginin = false;
                    toastService.ShowError("Login failed. Account is not recognized as a Player");
                    return;
                }
                //await _accountService.ForceLogoutUser(response.User.Id, loginDTO.Username);
                _iCurrentUser.Id = response.User.Id;
                _iCurrentUser.Username = response.User.Username;
                _iCurrentUser.FullName = response.User.FullName;
                _iCurrentUser.Token = response.User.JWTToken;
                _iCurrentUser.EmailAddress = response.User.EmailAddress;
                _iCurrentUser.EmailValidated = response.User.EmailValidated;
                _iCurrentUser.MobileNumber = response.User.MobileNumber;
                _iCurrentUser.MobileNumberValidated = response.User.MobileNumberValidated;
                _iCurrentUser.Credits = response.User.Credits;
                _iCurrentUser.CreditsDisp = string.Format("{0:0,0.00}", response.User.Credits);
                _iCurrentUser.RoleType = response.User.RoleType;
                _iCurrentUser.RemainingViewTime = response.User.RemainingViewTime;
                _iCurrentUser.ProfileImage = response.User.ProfileImageFullPath;
                _iCurrentUser.PopupTimer = response.User.PopupTimer;
                _iCurrentUser.IdleTimer = response.User.IdleTimer;
                _iCurrentUser.DeviceToken = response.User.DeviceToken;
                _iCurrentUser.Status = response.User.Status;
                _iCurrentUser.Verified = response.User.Verified;
                _iCurrentUser.ToppedUp = response.User.ToppedUp;
                _iCurrentUser.TourWalletShown = false;
                _iCurrentUser.HoldCredits = response.User.HoldCredits;
                _iCurrentUser.isApple = isApple;
                if (!_iCurrentUser.MobileNumberValidated && 1 == 0)
                {

                    var parameters = new ModalParameters();
                    parameters.Add("MobileNumber", _iCurrentUser.MobileNumber);


                    var popupOtp = popupModal.Show<PopupSMS>("", parameters);
                    var result = await popupOtp.Result;
                    if (!(bool)result.Data)
                    {
                        refpopuLoadingpModal.Close();
                        islogginin = false;
                        return;
                    }
                    else
                    {
                        var verifyMobile = await _accountService.VerifyMobileNumber();
                        if (!verifyMobile)
                        {
                            toastService.ShowError("An error occured while attempting to verify mobile number. Please try again later");
                            refpopuLoadingpModal.Close();
                            islogginin = false;
                            return;
                        }
                    }

                }

                await _iCurrentUser.updateSessionAsync();

                await ((CustomAuthStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated();
                if (_iCurrentUser.ToppedUp || _iCurrentUser.Verified != 0)
                {

                    _navigationManager.NavigateTo("/home");
                }
                else
                {
                    _navigationManager.NavigateTo("/landing");
                }
                //_accountService._globalPopup.Close();
            }
            else
            {
                //Incorrect Missing
            }
        }
        catch (Exception ex)
        {
            toastService.ShowError("An error occured while attempting to verify mobile number. Please try again later");
            refpopuLoadingpModal.Close();
        }

    }

    #endregion
}
