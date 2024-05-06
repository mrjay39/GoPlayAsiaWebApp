using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.ObjectModel;

namespace GoPlayAsiaWebApp.ViewModels;

public class CompBetHistoryViewModel : BaseViewModel
{
    #region Local Variable & Properties
    ICurrentUser _currentUser;
    IReportService _reportService;

    public ObservableCollection<UserBetHistoryResultDTO> _gameRounds;
    public ObservableCollection<UserBetHistoryResultDTO> GameRounds
    {
        get => _gameRounds;
        set
        {
            _gameRounds = value;
        }
    }
    #endregion

    #region Life cycle methods
    public CompBetHistoryViewModel(ICurrentUser currentUser, IReportService reportService, NavigationManager navigationManager, IAccountService iaccountService, IToastService toastService, AuthenticationStateProvider AuthenticationStateProvider)
    {
        _currentUser = currentUser;
        _reportService = reportService;
        _navigationManager = navigationManager;
        _iaccountService = iaccountService;
        _toastService = toastService;
        _AuthenticationStateProvider = AuthenticationStateProvider;
    }

    public async Task LoadData()
    {
        ReportParamsDTO paramsModel = new ReportParamsDTO()
        {
            UserId = _currentUser.Id
        };
        var gameRounds = await _reportService.GetGameHistoryWithUserBets(paramsModel, _currentUser.Token);
        if (gameRounds != null && gameRounds.GameRounds != null && gameRounds.GameRounds.Count > 0)
            GameRounds = new ObservableCollection<UserBetHistoryResultDTO>(gameRounds.GameRounds);
        else
            GameRounds = new ObservableCollection<UserBetHistoryResultDTO>();
    }
    #endregion
}
