using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Models;
using GoplayasiaCore.Core.Services;
using GoplayasiaCore.Core.Services.Interface;
using GoplayasiaSharedKernel.DTOs.DTOIn;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class LobbyViewModel : BaseViewModel
{
    public List<EvolutionGamesDTO> GameList = new List<EvolutionGamesDTO>();

    public LobbyViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, NavigationManager navigationManager, IAccountService iaccountService, IToastService toastService, AuthenticationStateProvider AuthenticationStateProvider, IConstantService constantService, IEGamesService iegamesservice)

    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _navigationManager = navigationManager;
        _iaccountService = iaccountService;
        _toastService = toastService;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _constantService = constantService;
        _iegamesservice = iegamesservice;
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

    public async Task GetListGames(string gameProvider, string gameType)
    {
        try
        {
            GameList = await _iegamesservice.GetGames(gameProvider, gameType);
            if (GameList == null)
            {
                _toastService.ShowError("No games found.");
                return;
            }
            else if (GameList != null)
            {
				await CallInvoke();
            }

		}
		catch (Exception ex)
		{
			//Console.WriteLine(ex);
		}
	}

}
