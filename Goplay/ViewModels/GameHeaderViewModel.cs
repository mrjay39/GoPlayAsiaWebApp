using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class GameHeaderViewModel : BaseViewModel
{
    public GameHeaderViewModel(IAccountService iaccountService, NavigationManager navigationManager, ICurrentUser icurrentUser, AuthenticationStateProvider AuthenticationStateProvider, IToastService toastService)
    {
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _icurrentUser = icurrentUser;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _toastService = toastService;
    }
}
