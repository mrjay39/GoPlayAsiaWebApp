using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.ObjectModel;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

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
    public ObservableCollection<UserBetHistoryResultDTO> FilteredGameRounds { get; set; }
    public List<UserBetHistoryResultDTO> VisibleGameRounds { get; set; } = new List<UserBetHistoryResultDTO>();
    public bool ShowSeeMoreButton { get; set; } = false;
    public bool ShowAllRows { get; set; } = false;
    public int DisplayCnt { get; set; } = 20;

    public string Category { get; set; }
    public DateTime SelDate { get; set; }

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
            UserId = _currentUser.Id,
            Date = SelDate,
            TimeFrom = TimeSpan.Zero,
            TimeTo = new TimeSpan(23, 59, 59, 999)
        };
        var gameRounds = await _reportService.GetGameHistoryWithUserBets(paramsModel, _currentUser.Token);
        if (gameRounds != null && gameRounds.GameRounds != null && gameRounds.GameRounds.Count > 0)
            GameRounds = new ObservableCollection<UserBetHistoryResultDTO>(gameRounds.GameRounds);
        else
            GameRounds = new ObservableCollection<UserBetHistoryResultDTO>();
        FilterGameRounds();
    }

    private void FilterGameRounds()
    {
        if (GameRounds != null)
        {
            if (string.IsNullOrEmpty(Category))
            {
                FilteredGameRounds = new ObservableCollection<UserBetHistoryResultDTO>(GameRounds);
            }
            else
            {
                var filteredList = GameRounds.Where(gr => !String.IsNullOrEmpty(gr.GameCategory) && gr.GameCategory.ToUpper() == Category.ToUpper()).ToList();
                FilteredGameRounds = new ObservableCollection<UserBetHistoryResultDTO>(filteredList);
            }

            if (FilteredGameRounds.Count > 20 && !ShowAllRows)
            {
                ShowSeeMoreButton = true;
            }
            else
            {
                ShowSeeMoreButton = false;
            }

            UpdateVisibleGameRounds();
        }
    }

    public void UpdateVisibleGameRounds()
    {
        if (ShowAllRows)
        {
            VisibleGameRounds = FilteredGameRounds.ToList();
        }
        else
        {
            VisibleGameRounds = FilteredGameRounds.Take(DisplayCnt).ToList();
        }
    }

    public async Task SetCategory(string param)
    {
        Category = param;
        FilterGameRounds();
        await CallInvoke();
    }

    #endregion
}
