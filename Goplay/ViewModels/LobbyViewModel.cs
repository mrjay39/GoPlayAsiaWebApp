using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.eGames;
using GoplayasiaBlazor.Models;
using GoplayasiaCore.Core.Services;
using GoplayasiaCore.Core.Services.Interface;
using GoplayasiaSharedKernel.DTOs.DTOIn;
using GoplayasiaSharedKernel.DTOs.eGames;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class LobbyViewModel : BaseViewModel
{
    public string egameUrl { get; set; } = string.Empty;

    public List<eGamesListDTO> AllGameList { get; set; }
    public List<eGamesListDTO> AllListPlayerGames { get; set; }


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
        if (icurrentUser.Username != null)
        {

            ValidateUser();
        }

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

    public async Task GetListGames()
    {
        try
        {
            if (string.IsNullOrEmpty(_icurrentUser.Token))
            {
                AllGameList = await _iegamesservice.GetGames(Constants.egamesAll, Constants.egamesAll);

            }
            else
            {
                AllGameList = await _iegamesservice.ListPlayerGames(Constants.egamesAll, Constants.egamesAll);
            }
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }


    public async Task LaunchGame(eGamesListDTO game)
    {
        try
        {
            var result = await _iegamesservice.LaunchGame(game);
            if (result != null)
                egameUrl = result.launchURL;
        }
        catch (Exception ex)
        {
            egameUrl = string.Empty;
        }
    }

    public async Task LaunchGameMode(eGamesListDTO game, string demo)
    {
        try
        {
            var result = await _iegamesservice.LaunchGameMode(game, demo);
            if (result != null)
                egameUrl = result.launchURL;
        }
        catch (Exception ex)
        {
            egameUrl = string.Empty;
        }
    }



}
