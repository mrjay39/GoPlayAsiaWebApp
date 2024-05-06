using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.ViewModels;

public class LobbyViewModel : BaseViewModel
{
    public LobbyViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, NavigationManager navigationManager, IAccountService iaccountService, IToastService toastService, AuthenticationStateProvider AuthenticationStateProvider, IConstantService constantService)

    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _navigationManager = navigationManager;
        _iaccountService = iaccountService;
        _toastService = toastService;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _constantService = constantService;
        ValidateUser();

    }

    public async Task ValidateUser()
    {
        var user = await _iaccountService.GetUser();
        if (user != null)
        {

            if (user.User.DeviceToken != _icurrentUser.DeviceToken)
            {
                Logout();
                _toastService.ShowInfo("You have logged in on another device.");
            }
            _icurrentUser.Credits = user.User.Credits;
            _icurrentUser.CreditsDisp = string.Format("{0:0,0.00}", user.User.Credits);
        }
    }

}
