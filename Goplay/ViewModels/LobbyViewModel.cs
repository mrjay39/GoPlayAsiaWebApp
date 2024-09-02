using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.eGames;
using GoplayasiaBlazor.Models;
using GoplayasiaCore.Core.Services;
using GoplayasiaCore.Core.Services.Interface;
using GoplayasiaSharedKernel.DTOs.DTOIn;
using GoplayasiaSharedKernel.DTOs.eGames;
using GoplayasiaSharedKernel.Models.eGames;
using GoPlayAsiaWebApp.Goplay.Games.Lucky9;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Globalization;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class LobbyViewModel : BaseViewModel
{
    public string egameUrl { get; set; } = string.Empty;

    public List<eGamesListDTO> OriginalAllGameList { get; set; }
    public List<eGamesListDTO> AllGameList { get; set; }
    public List<eGamesListDTO> AllListPlayerGames { get; set; }
    public List<GameProviders> gameProviders { get; private set; }

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

    private void AddGameToList(string description, string gameId, string filename, string imageUrl, string category)
    {
        var game = new eGamesListDTO
        {
            Description = description,
            Provider = Constants.GoPlayAsia,
            GameId = gameId,
            Filename = filename,
            ImageUrl = imageUrl,
            Category = category
        };
        AllGameList.Add(game);
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
            AllGameList.OrderBy(x => x.Provider).ThenBy(x => x.Rank);
            OriginalAllGameList = new List<eGamesListDTO>(AllGameList);
            //get all list of provders with slots
            GameProviders newGameProvider = new GameProviders();
            gameProviders = new List<GameProviders>();

            AddGameToList(Constants.L9Game, Constants.L9Game, Constants.L9Img, Constants.L9Img, Constants.egamesLive);
            AddGameToList(Constants.F3Game, Constants.F3Game, Constants.F3Img, Constants.F3Img, Constants.egamesLive);
            AddGameToList(Constants.HTGame, Constants.HTGame, Constants.HTImg, Constants.HTImg, Constants.egamesLive);
            AddGameToList(Constants.G12Game, Constants.G12Game, Constants.G12Img, Constants.G12Img, Constants.egamesLive);

            var distinctProviders = AllGameList.Select(x => x.Provider).Distinct().ToList();

            newGameProvider.Name = "All";
            newGameProvider.ImageUrl = "provider-all.png";
            newGameProvider.SortKey = 1;

            newGameProvider.LiveCount = AllGameList.Count(x => x.Category == Constants.egamesLive);
            newGameProvider.ArcadeCount = AllGameList.Count(x => x.Category == Constants.egamesRng);
            newGameProvider.SlotsCount = AllGameList.Count(x => x.Category == Constants.egamesSlots);
            newGameProvider.CasinoCount = AllGameList.Count(x => x.Category == Constants.egamesCasino);
            newGameProvider.FishingCount = AllGameList.Count(x => x.Category == Constants.egamesFishing);
            gameProviders.Add(newGameProvider);


            foreach (var provider in distinctProviders)
            {
                newGameProvider = new GameProviders();
                newGameProvider.Name = provider;
                switch (provider)
                {
                    case Constants.GoPlayAsia:
                        newGameProvider.ImageUrl = "goPlay.png";
                        newGameProvider.SortKey = 1;
                        newGameProvider.LiveCount = 6;
                        gameProviders.Add(newGameProvider);
                        return;
                    case Constants.Jili:
                        newGameProvider.ImageUrl = "jili.png";
                        newGameProvider.SortKey = 2;
                        break;
                    case Constants.RedTiger:
                        newGameProvider.ImageUrl = "redTiger.png";
                        newGameProvider.SortKey = 7;
                        break;
                    case Constants.BigTimeGaming:
                        newGameProvider.ImageUrl = "bigTime.png";
                        newGameProvider.SortKey = 6;
                        break;
                    case Constants.NetEntExt:
                        newGameProvider.ImageUrl = "netent_extended.png";
                        newGameProvider.SortKey = 9;
                        break;
                    case Constants.NetEnt:
                        newGameProvider.ImageUrl = "netent.png";
                        newGameProvider.SortKey = 8;
                        break;
                    case Constants.Pragmatic:
                        newGameProvider.ImageUrl = "pragmatic.png";
                        newGameProvider.SortKey = 3;
                        break;
                    case Constants.Evolution:
                        newGameProvider.ImageUrl = "evolution.png";
                        newGameProvider.SortKey = 4;
                        break;
                    case Constants.NoLimitCity:
                        newGameProvider.ImageUrl = "nolimit.png";
                        newGameProvider.SortKey = 5;
                        break;
                }

                newGameProvider.LiveCount = AllGameList.Count(x => x.Category == Constants.egamesLive && x.Provider == provider);
                newGameProvider.ArcadeCount = AllGameList.Count(x => x.Category == Constants.egamesRng && x.Provider == provider);
                newGameProvider.SlotsCount = AllGameList.Count(x => x.Category == Constants.egamesSlots && x.Provider == provider);
                newGameProvider.CasinoCount = AllGameList.Count(x => x.Category == Constants.egamesCasino && x.Provider == provider);
                newGameProvider.FishingCount = AllGameList.Count(x => x.Category == Constants.egamesFishing && x.Provider == provider);
                gameProviders.Add(newGameProvider);
            }
        
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }

    public async Task FilterGames(string searchKey)
    {
        AllGameList = new List<eGamesListDTO>(OriginalAllGameList);
        if (!string.IsNullOrEmpty(searchKey))
        {
            AllGameList = AllGameList.Where(item =>
                          item.Description.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                          item.Provider.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                          item.Category.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
        await CallInvoke();
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
