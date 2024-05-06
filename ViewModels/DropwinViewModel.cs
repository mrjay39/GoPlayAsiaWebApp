using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.ViewModels;

public class DropwinViewModel : BaseViewModel
{
    private int _multiplier_f2;
    private int _multiplier_f3;
    private int _multiplier_all4;

    private bool _isEnabled_First2;
    private bool _isEnabled_First3;
    private bool _isEnabled_All4;
    private int _baseValue;
    private int _sampleWin_F2;
    private int _sampleWin_F3;
    private int _sampleWin_All4;
    private string _totalBets_F2;
    private string _totalBets_F3;
    private string _totalBets_All4;
    private string _ratioDisplay_F2;
    private string _ratioDisplay_F3;
    private string _ratioDisplay_All4;
    private bool _isGameCancelled;
    private bool _isChipsVisible;
    private bool _isBettingEnabled;
    private string _gameResult;
    private int _selectedChipAmount = 0;
    private decimal _totalAccumulatedBets;
    private string _totalAccumulatedBetsString;
    private ObservableCollection<BetModel> _userBets_F2;
    private ObservableCollection<BetModel> _userBets_F3;
    private ObservableCollection<BetModel> _userBets_All4;
    private ObservableCollection<BetModel> _userPrevBets_F2;
    private ObservableCollection<BetModel> _userPrevBets_F3;
    private ObservableCollection<BetModel> _userPrevBets_All4;
    private int _betCount_f2;
    private int _betCount_f3;
    private int _betCount_all4;
    private int _charLimit = 0;
    private bool _isPlaceBetEnabled;
    private string _betValueDisplay;
    private int _betTypeId = 0;
    public bool _isDuplicateBet;
    private string _duplicateBetvalue = "You already placed a bet ";

    private GameChipModel _gameChips_F2;
    private GameChipModel _gameChips_F3;
    private GameChipModel _gameChips_All4;
    private string _betMultiplier;
    public string BetMultiplier
    {
        get => _betMultiplier;
        set
        {
            _betMultiplier = value;
            RaisePropertyChanged(() => BetMultiplier);
        }
    }

    public int BetCount_f2
    {
        get => _betCount_f2;
        set
        {
            _betCount_f2 = value;
            RaisePropertyChanged(() => BetCount_f2);
        }
    }
    public int BetCount_f3
    {
        get => _betCount_f3;
        set
        {
            _betCount_f3 = value;
            RaisePropertyChanged(() => BetCount_f3);
        }
    }
    public int BetCount_all4
    {
        get => _betCount_all4;
        set
        {
            _betCount_all4 = value;
            RaisePropertyChanged(() => BetCount_all4);
        }
    }


    public GameChipModel GameChips_F2
    {
        get => _gameChips_F2;
        set
        {
            _gameChips_F2 = value;
            RaisePropertyChanged(() => GameChips_F2);
        }
    }

    public GameChipModel GameChips_F3
    {
        get => _gameChips_F3;
        set
        {
            _gameChips_F3 = value;
            RaisePropertyChanged(() => GameChips_F3);
        }
    }

    public GameChipModel GameChips_All4
    {
        get => _gameChips_All4;
        set
        {
            _gameChips_All4 = value;
            RaisePropertyChanged(() => GameChips_All4);
        }
    }

    public bool IsEnabled_First2
    {
        get => _isEnabled_First2;
        set
        {
            _isEnabled_First2 = value;
            RaisePropertyChanged(() => IsEnabled_First2);
        }
    }

    public bool IsEnabled_First3
    {
        get => _isEnabled_First3;
        set
        {
            _isEnabled_First3 = value;
            RaisePropertyChanged(() => IsEnabled_First3);
        }
    }

    public bool IsEnabled_All4
    {
        get => _isEnabled_All4;
        set
        {
            _isEnabled_All4 = value;
            RaisePropertyChanged(() => IsEnabled_All4);
        }
    }

    public int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            RaisePropertyChanged(() => BaseValue);
        }
    }

    public int SampleWin_F2
    {
        get => _sampleWin_F2;
        set
        {
            _sampleWin_F2 = value;
            RaisePropertyChanged(() => SampleWin_F2);
        }
    }
    public int SampleWin_F3
    {
        get => _sampleWin_F3;
        set
        {
            _sampleWin_F3 = value;
            RaisePropertyChanged(() => SampleWin_F3);
        }
    }
    public int SampleWin_All4
    {
        get => _sampleWin_All4;
        set
        {
            _sampleWin_All4 = value;
            RaisePropertyChanged(() => SampleWin_All4);
        }
    }
    public string TotalBets_F2
    {
        get => _totalBets_F2;
        set
        {
            _totalBets_F2 = value;
            RaisePropertyChanged(() => TotalBets_F2);
        }
    }
    public string TotalBets_F3
    {
        get => _totalBets_F3;
        set
        {
            _totalBets_F3 = value;
            RaisePropertyChanged(() => TotalBets_F3);
        }
    }
    public string TotalBets_All4
    {
        get => _totalBets_All4;
        set
        {
            _totalBets_All4 = value;
            RaisePropertyChanged(() => TotalBets_All4);
        }
    }

    public string RatioDisplay_F2
    {
        get => _ratioDisplay_F2;
        set
        {
            _ratioDisplay_F2 = value;
            RaisePropertyChanged(() => RatioDisplay_F2);
        }
    }

    public string RatioDisplay_F3
    {
        get => _ratioDisplay_F3;
        set
        {
            _ratioDisplay_F3 = value;
            RaisePropertyChanged(() => RatioDisplay_F3);
        }
    }

    public string RatioDisplay_All4
    {
        get => _ratioDisplay_All4;
        set
        {
            _ratioDisplay_All4 = value;
            RaisePropertyChanged(() => RatioDisplay_All4);
        }
    }

    public int Multiplier_f2
    {
        get => _multiplier_f2;
        set
        {
            _multiplier_f2 = value;
            RaisePropertyChanged(() => Multiplier_f2);
        }
    }
    public int Multiplier_f3
    {
        get => _multiplier_f3;
        set
        {
            _multiplier_f3 = value;
            RaisePropertyChanged(() => Multiplier_f3);
        }
    }
    public int Multiplier_all4
    {
        get => _multiplier_all4;
        set
        {
            _multiplier_all4 = value;
            RaisePropertyChanged(() => Multiplier_all4);
        }
    }
    public bool IsChipsVisible
    {
        get => _isChipsVisible;
        set
        {
            _isChipsVisible = value;
            RaisePropertyChanged(() => IsChipsVisible);
        }
    }
    public bool IsGameCancelled
    {
        get => _isGameCancelled;
        set
        {
            _isGameCancelled = value;
            RaisePropertyChanged(() => IsGameCancelled);
        }
    }
    public bool IsBettingEnabled
    {
        get => _isBettingEnabled;
        set
        {
            _isBettingEnabled = value;
            RaisePropertyChanged(() => IsBettingEnabled);
        }
    }
    public string GameResult
    {
        get => _gameResult;
        set
        {
            _gameResult = value;
            RaisePropertyChanged(() => GameResult);
        }
    }
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<GameRoundModel> CurrentList { get; set; }
    }
    private ObservableCollection<TrendsDisplayModel> _trendsDisplay;
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay
    {
        get => _trendsDisplay;
        set
        {
            _trendsDisplay = value;
            RaisePropertyChanged(() => TrendsDisplay);
        }
    }
    private string _betAmountDisplay = string.Empty;
    private static string letterValueHolder;
    private string _letterValue = string.Empty;
    private bool _isLuckyPick1;
    private bool _isLuckyPick5;
    public string BetAmountDisplay
    {
        get => _betAmountDisplay;
        set
        {
            _betAmountDisplay = value;
            RaisePropertyChanged(() => BetAmountDisplay);
        }
    }
    public string LetterValue
    {
        get => _letterValue;
        set
        {
            _letterValue = value.ToUpper();
            RaisePropertyChanged(() => LetterValue);
        }
    }
    public bool IsLuckyPick1
    {
        get => _isLuckyPick1;
        set
        {
            _isLuckyPick1 = value;
            RaisePropertyChanged(() => IsLuckyPick1);
        }
    }
    public bool IsLuckyPick5
    {
        get => _isLuckyPick5;
        set
        {
            _isLuckyPick5 = value;
            RaisePropertyChanged(() => IsLuckyPick5);
        }
    }
    public int SelectedChipAmount
    {
        get => _selectedChipAmount;
        set
        {
            _selectedChipAmount = value;
            RaisePropertyChanged(() => SelectedChipAmount);
        }
    }
    public decimal TotalAccumulatedBets
    {
        get => _totalAccumulatedBets;
        set
        {
            _totalAccumulatedBets = value;
            RaisePropertyChanged(() => TotalAccumulatedBets);
        }
    }
    public string TotalAccumulatedBetsString
    {
        get => _totalAccumulatedBetsString;
        set
        {
            _totalAccumulatedBetsString = value;
            RaisePropertyChanged(() => TotalAccumulatedBetsString);
        }
    }
    public ObservableCollection<BetModel> UserBets_F2
    {
        get => _userBets_F2;
        set
        {
            _userBets_F2 = value;
            RaisePropertyChanged(() => UserBets_F2);
            BetCount_f2 = UserBets_F2.Count;
        }
    }
    public ObservableCollection<BetModel> UserBets_F3
    {
        get => _userBets_F3;
        set
        {
            _userBets_F3 = value;
            RaisePropertyChanged(() => UserBets_F3);
            BetCount_f3 = UserBets_F3.Count;
        }
    }
    public ObservableCollection<BetModel> UserBets_All4
    {
        get => _userBets_All4;
        set
        {
            _userBets_All4 = value;
            RaisePropertyChanged(() => UserBets_All4);
            BetCount_all4 = UserBets_All4.Count;
        }
    }
    public ObservableCollection<BetModel> UserPrevBets_F2
    {
        get => _userPrevBets_F2;
        set
        {
            _userPrevBets_F2 = value;
            RaisePropertyChanged(() => UserPrevBets_F2);
        }
    }
    public ObservableCollection<BetModel> UserPrevBets_F3
    {
        get => _userPrevBets_F3;
        set
        {
            _userPrevBets_F3 = value;
            RaisePropertyChanged(() => UserPrevBets_F3);
        }
    }
    public ObservableCollection<BetModel> UserPrevBets_All4
    {
        get => _userPrevBets_All4;
        set
        {
            _userPrevBets_All4 = value;
            RaisePropertyChanged(() => UserPrevBets_All4);
        }
    }

    public int CharLimit
    {
        get => _charLimit;
        set
        {
            _charLimit = value;
            RaisePropertyChanged(() => CharLimit);
        }
    }
    public bool IsPlaceBetEnabled
    {
        get => _isPlaceBetEnabled;
        set
        {
            _isPlaceBetEnabled = value;
            RaisePropertyChanged(() => IsPlaceBetEnabled);
        }
    }
    public string BetValueDisplay
    {
        get => _betValueDisplay;
        set
        {
            _betValueDisplay = value;
            RaisePropertyChanged(() => BetValueDisplay);
        }
    }
    public int BetTypeId
    {
        get => _betTypeId;
        set
        {
            _betTypeId = value;
            RaisePropertyChanged(() => BetTypeId);
        }
    }

    public bool IsDuplicateBet
    {
        get => _isDuplicateBet;
        set
        {
            _isDuplicateBet = value;
            RaisePropertyChanged(() => IsDuplicateBet);
        }
    }
    public string DuplicateBetvalue
    {
        get => _duplicateBetvalue;
        set
        {
            _duplicateBetvalue = value;
            RaisePropertyChanged(() => DuplicateBetvalue);
        }
    }

    public event Action NotifyGameStatus;

    public DropwinViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IToastService toastService, IAccountService iaccountService, IGameSettingService igameSettingService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _igameSettingService = igameSettingService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;

        StreamId = Constants.StreamIDDrowin;
        GametypeId = (int)GameTypes.Drop_And_Win;

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
    public async Task GetGameSetting()
    {
        try
        {

            var tempRoundSettings = await _igameSettingService.GetGameSettings(GametypeId);
            if (tempRoundSettings == null)
            {
                _toastService.ShowInfo("No game settings found.");
            }
            else if (tempRoundSettings != null)
            {
                GameSetting = tempRoundSettings;
                Multiplier_f2 = tempRoundSettings.F2ratio != null ? (int)tempRoundSettings.F2ratio : 0;
                Multiplier_f3 = tempRoundSettings.F3ratio != null ? (int)tempRoundSettings.F3ratio : 0;
                Multiplier_all4 = tempRoundSettings.A4ratio != null ? (int)tempRoundSettings.A4ratio : 0;

                BaseValue = tempRoundSettings.BaseRatioValue != null ? (int)tempRoundSettings.BaseRatioValue : 0;
                SampleWin_F2 = tempRoundSettings.F2ratio != null ? (int)tempRoundSettings.F2ratio * BaseValue : 0;
                SampleWin_F3 = tempRoundSettings.F3ratio != null ? (int)tempRoundSettings.F3ratio * BaseValue : 0;
                SampleWin_All4 = tempRoundSettings.A4ratio != null ? (int)tempRoundSettings.A4ratio * BaseValue : 0;

                RatioDisplay_F2 = BaseValue * 20 + "=" + SampleWin_F2 * 20;
                RatioDisplay_F3 = BaseValue * 10 + "=" + SampleWin_F3 * 10;
                RatioDisplay_All4 = BaseValue * 10 + "=" + SampleWin_All4 * 10;
                await CallInvoke();
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    protected async Task UpdateGameStatus(int gametypeId, int value)
    {
        if (GametypeId == gametypeId)
        {
            if (value == (int)RoundStatus.Open)
            {
                IsBettingEnabled = true;
                UserBets_F2 = new ObservableCollection<BetModel>();
                UserBets_F3 = new ObservableCollection<BetModel>();
                UserBets_All4 = new ObservableCollection<BetModel>();
                NotifyGameStatus?.Invoke();
                //if (BetTypeId > 0)
                //{
                //    IsChipsVisible = true;
                //}

            }
            await GetGameRound();
            await CallInvoke();
        }

    }
    public void NotifyGameRoundResult(UpdateGameResultModel paramsModel)
    {
        if (paramsModel.GameTypeId == GametypeId)
        {
            GameResult = paramsModel.WinningCombination;
            CallInvoke();
        }
    }
    public async Task UpdateTrends(int gameTypeId)
    {
        if (GametypeId == gameTypeId)
        {
            await GetTrends();
            await CallInvoke();
        }
    }
    public async Task AssignSignalRMethods()
    {
        try
        {

            if (HubConnection != null)
            {

                if (HubConnection.State == HubConnectionState.Connected)
                {
                    HubConnection.Remove(Constants.UpdateGameStatus);
                    HubConnection.Remove(Constants.NotifyGameRoundResult);
                    HubConnection.Remove(Constants.UpdateTrends);

                    HubConnection.On<int, int>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                }

            }
        }
        catch (Exception)
        {

        }
    }
    public async Task GetTrends()
    {
        if (GametypeId > 0)
        {
            var tempTrends = await _igameRoundService.GetTrends(GametypeId);
            if (tempTrends != null)
            {
                //Trends = new ObservableCollection<GameRoundModel>(tempTrends.OrderByDescending(x => x.Id));
                PrevGameRound = tempTrends.First();

                await SetTrendsDisplay(tempTrends);
                await CallInvoke();
            }
        }
    }
    private async Task SetTrendsDisplay(List<GameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<GameRoundModel> CurrentList = new ObservableCollection<GameRoundModel>();
            TrendsDisplayModel Current = new TrendsDisplayModel();
            int counter = 0;
            int rowCounter = 0;
            int indexCounter = 0;

            foreach (var t in tempTrends.OrderBy(x => x.Id).ToList())
            {
                counter++;
                rowCounter++;
                if (rowCounter != 5)
                {
                    if (counter != tempTrends.Count)
                    {
                        CurrentList.Add(t);
                    }
                    else
                    {
                        CurrentList.Add(t);
                        Current = new TrendsDisplayModel();
                        indexCounter++;
                        Current.ColumnIndex = indexCounter;
                        Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<GameRoundModel>();

                        rowCounter = 0;
                        counter = 0;
                    }
                }
                else if (rowCounter == 5)
                {
                    CurrentList.Add(t);
                    Current = new TrendsDisplayModel();
                    indexCounter++;
                    Current.ColumnIndex = indexCounter;
                    Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<GameRoundModel>();

                    rowCounter = 0;
                }
            }

            if (resultHolder != null)
            {
                //TrendsDisplay = resultHolder;
                TrendsDisplay = new ObservableCollection<TrendsDisplayModel>(resultHolder.OrderByDescending(x => x.ColumnIndex));
            }
        }
    }
    public async Task GetCurrentRoundBets()
    {
        long UserId = _icurrentUser.Id;
        TotalAccumulatedBets = 0;
        //var tempRoundBets = await _gameRoundService.GetBets(UserId, GameTypeId, GameRound.Id);
        var tempRoundBets = await _igameRoundService.GetBetSummaryOnRound(UserId, GametypeId);
        if (tempRoundBets != null)
        {
            BetModel tempBet = new BetModel();
            ObservableCollection<BetModel> tempBetList = new ObservableCollection<BetModel>();
            //var f2 = tempRoundBets.Where(x => x.DropAndWinBetType == (int)DropAndWinMainBetOption.F2).OrderByDescending(x => x.Id).ToList();
            TotalAccumulatedBets = 0;
            if (tempRoundBets.F2Bets != null && tempRoundBets.F2Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.F2Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f2).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserBets_F2 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue));
                TotalBets_F2 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
                TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
            }
            else
            {
                UserBets_F2 = new();
                TotalBets_F2 = "";

            }

            //var f3 = tempRoundBets.Where(x => x.DropAndWinBetType == (int)DropAndWinMainBetOption.F3).OrderByDescending(x => x.Id).ToList();
            if (tempRoundBets.F3Bets != null && tempRoundBets.F3Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.F3Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f3).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserBets_F3 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue));
                TotalBets_F3 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
                TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
            }
            else
            {
                UserBets_F3 = new();
                TotalBets_F3 = "";

            }

            //var all4 = tempRoundBets.Where(x => x.DropAndWinBetType == (int)DropAndWinMainBetOption.All4).OrderByDescending(x => x.Id).ToList();
            if (tempRoundBets.A4Bets != null && tempRoundBets.A4Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.A4Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_all4).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserBets_All4 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue));
                TotalBets_All4 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
                TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
            }
            else
            {
                UserBets_All4 = new();
                TotalBets_All4 = "";

            }

            TotalAccumulatedBetsString = TotalAccumulatedBets.ToString("N0");
        }
        await CallInvoke();
    }
    public async Task GetPrevCurrentRoundBets()
    {
        long UserId = _icurrentUser.Id;
        var tempRoundBets = await _igameRoundService.GetPrevBetSummaryOnRound(UserId, GametypeId);
        if (tempRoundBets != null)
        {
            BetModel tempBet = new BetModel();
            ObservableCollection<BetModel> tempBetList = new ObservableCollection<BetModel>();
            if (tempRoundBets.F2Bets != null && tempRoundBets.F2Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.F2Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f2).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserPrevBets_F2 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue));
            }
            else
            {
                UserPrevBets_F2 = new();
            }

            if (tempRoundBets.F3Bets != null && tempRoundBets.F3Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.F3Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f3).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserPrevBets_F3 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue));
            }
            else
            {
                UserPrevBets_F3 = new();
            }

            //var all4 = tempRoundBets.Where(x => x.DropAndWinBetType == (int)DropAndWinMainBetOption.All4).OrderByDescending(x => x.Id).ToList();
            if (tempRoundBets.A4Bets != null && tempRoundBets.A4Bets.Count > 0)
            {
                tempBetList = new ObservableCollection<BetModel>();
                foreach (var b in tempRoundBets.A4Bets)
                {
                    tempBet = b;
                    if (b.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_all4).ToString("#,##0");
                    }
                    tempBetList.Add(tempBet);
                }
                UserPrevBets_All4 = new ObservableCollection<BetModel>(tempBetList.OrderBy(x => x.BetValue)); ;
            }
            else
            {
                UserPrevBets_All4 = new();
            }
        }
        await CallInvoke();
    }
    public async Task BetOptionSelected(object arg)
    {
        GameChipModel chipmodel = new GameChipModel();
        int selectedMainOption = 0;
        if (arg.ToString() == "F2")
        {
            selectedMainOption = (int)DropAndWinMainBetOption.F2;
            chipmodel = GameChips_F2;
            GameChips = GameChips_F2;
            CharLimit = 2;

            SelectedChipAmount = 0;
            BetAmount = 0;
            BetAmountDisplay = string.Empty;
            LetterValue = string.Empty;
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;

            IsEnabled_First2 = true;
            IsEnabled_First3 = false;
            IsEnabled_All4 = false;

            IsChipsVisible = true;
        }
        else if (arg.ToString() == "F3")
        {
            selectedMainOption = (int)DropAndWinMainBetOption.F3;
            chipmodel = GameChips_F3;
            GameChips = GameChips_F3;
            CharLimit = 3;

            SelectedChipAmount = 0;
            BetAmount = 0;
            BetAmountDisplay = string.Empty;
            LetterValue = string.Empty;
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;

            IsEnabled_First2 = false;
            IsEnabled_First3 = true;
            IsEnabled_All4 = false;

            IsChipsVisible = true;
        }
        else if (arg.ToString() == "ALL4")
        {
            selectedMainOption = (int)DropAndWinMainBetOption.All4;
            chipmodel = GameChips_All4;
            GameChips = GameChips_All4;
            CharLimit = 4;

            SelectedChipAmount = 0;
            BetAmount = 0;
            BetAmountDisplay = string.Empty;
            LetterValue = string.Empty;
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;

            IsEnabled_First2 = false;
            IsEnabled_First3 = false;
            IsEnabled_All4 = true;

            IsChipsVisible = true;
        }

        //var popup = new NonCardGameBetPopupView();
        //var context = new NonCardGameBetPopupViewModel(_connectivityHelper, _navigationHelper, _dialogHelper, _systemSettingsHelper, _gameRoundService,
        //    _gameSettingService, GameRound, GameSetting, chipmodel, GameTypeId, selectedMainOption);
        //if (context != null)
        //{
        //    popup.BindingContext = context;
        //    var scaleAnimation = new ScaleAnimation
        //    {
        //        PositionIn = MoveAnimationOptions.Right,
        //        PositionOut = MoveAnimationOptions.Left
        //    };
        //    popup.Animation = scaleAnimation;
        //    PopupNavigation.PushAsync(popup);
        //}
        await ResetDefaultAmount();
    }
    public async Task ResetDefaultAmount()
    {
        if (GameChips != null)
        {
            SelectedChipAmount = int.Parse(GameChips.Chip1.ToString());
            BetAmount = int.Parse(GameChips.Chip1.ToString());
            BetAmountDisplay = GameChips.Chip1.ToString();
            BetMultiplier = GameChips.Chip1Display.ToString();
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
        }
    }
    public async Task GetGameRound()
    {
        IsGameCancelled = false;
        if (GametypeId > 0)
        {
            IsChipsVisible = false;
            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            var tempRound = await _igameRoundService.GetRound(GametypeId);
            if (tempRound == null)
            {
                _toastService.ShowError("No round details found.");
                await GetGameSetting();
                return;
            }
            else if (tempRound != null)
            {
                GameRound = tempRound;
                IsEnabled_First2 = false;
                IsEnabled_First3 = false;
                IsEnabled_All4 = false;

                if (tempRound.GameType != null)
                {
                    if (tempRound.GameType.F2Chip != null)
                    {
                        GameChips_F2 = tempRound.GameType.F2Chip;
                        GameChips = GameChips_F2;
                    }

                    if (tempRound.GameType.F3Chip != null)
                    {
                        GameChips_F3 = tempRound.GameType.F3Chip;
                    }

                    if (tempRound.GameType.A4Chip != null)
                    {
                        GameChips_All4 = tempRound.GameType.A4Chip;
                    }
                }



                if (GameRound.RoundNumber > 0)
                {
                    RoundNumber = GameRound.RoundNumber;
                }
                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;
                    IsBettingEnabled = true;

                    GameResult = string.Empty;

                    TotalBets_F2 = string.Empty;
                    TotalBets_F3 = string.Empty;
                    TotalBets_All4 = string.Empty;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Closed)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    IsBettingEnabled = false;

                    GameResult = tempRound.WinningResult;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Paused)
                {
                    RoundStatusString = Constants.Paused;
                    RoundStatusColor = Constants.GamePausedColor;
                    IsBettingEnabled = false;

                    GameResult = string.Empty;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
                {
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    IsBettingEnabled = false;

                    GameResult = string.Empty;
                    IsGameCancelled = true;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.PendingResult)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    ShowFlashing = "timerNotFlashing";
                    RoundTimer = ""; // 00:00

                    IsBettingEnabled = false;

                }

                await GetCurrentRoundBets();
                await GetPrevCurrentRoundBets();
                await GetTrends();

                IsEnabled_First3 = false;
                IsEnabled_All4 = false;
                BetAmount = 0;
                BetAmountDisplay = string.Empty;
                SelectedChipAmount = 0;
                LetterValue = string.Empty;
                letterValueHolder = string.Empty;
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;




            }
        }
        await CallInvoke();
    }
    public async Task AddCharacter(string param)
    {
        if (param != null)
        {
            if (LetterValue.Length < CharLimit)
            {
                LetterValue = LetterValue + param.ToString();
            }

            BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount : 0;
            BetAmountDisplay = SelectedChipAmount.ToString("N0");

            if (LetterValue.Length == CharLimit && SelectedChipAmount > 0)
            {
                IsPlaceBetEnabled = true;
            }
            else
            {
                IsPlaceBetEnabled = false;
            }

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
        }
    }
    public async Task RemoveLastCharacter()
    {
        if (!string.IsNullOrEmpty(LetterValue))
        {
            letterValueHolder = LetterValue;
            letterValueHolder = LetterValue.Substring(0, LetterValue.Length - 1);
            LetterValue = letterValueHolder;

            if (LetterValue.Length == CharLimit && BetAmount > 0)
            {
                IsPlaceBetEnabled = true;
            }
            else
            {
                IsPlaceBetEnabled = false;
            }
        }
    }
    public async Task SetBetAmount(object value, string multiplier)
    {
        BetMultiplier = multiplier;
        SelectedChipAmount = (int)value;
        BetAmount = (int)value;
        BetAmountDisplay = SelectedChipAmount.ToString("N0");

        if (!string.IsNullOrEmpty(LetterValue))
        {
            if (LetterValue.Length == CharLimit && BetAmount > 0)
            {
                IsPlaceBetEnabled = true;
            }
            else
            {
                IsPlaceBetEnabled = false;
            }
        }
        else
        {
            if (IsLuckyPick1 && BetAmount > 0)
            {
                IsPlaceBetEnabled = true;
            }
            else if (IsLuckyPick5 && BetAmount > 0)
            {
                BetAmount = (int)value * 5;
                BetAmountDisplay = BetAmount.ToString("N0");
                IsPlaceBetEnabled = true;
            }
            else
            {
                IsPlaceBetEnabled = false;
            }


        }
    }
    public async Task<bool> IsDivisibleBy10()
    {
        bool isvalid = false;
        if (BetAmount > 0)
        {
            var result = BetAmount % 10;
            if (result > 0)
            {
                _toastService.ShowError("Bet amount should be divisible by 10.");
                isvalid = false;
            }
            else
            {
                isvalid = true;
            }
        }

        return isvalid;
    }
    public async Task<bool> PlaceBet()
    {
        bool results = false;

        if (IsLuckyPick1)
        {
            BetValueDisplay = Constants.LuckyPick;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPick;
        }
        else if (IsLuckyPick5)
        {
            BetValueDisplay = Constants.LuckyPickx5;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;
        }
        else
        {
            BetValueDisplay = LetterValue;
            BetTypeId = (int)NonCardGameBetTypes.Normal;
        }


        bool isValid = IsDivisibleBy10().Result;
        results = isValid;
        var creditValue = Convert.ToDecimal(_icurrentUser.Credits);
        if (isValid)
        {
            if (BetAmount > creditValue)
            {
                _toastService.ShowError("Insufficient Credits....");
                results = false;
            }
            else if (BetAmount <= creditValue && isValid)
            {
                results = true;
            }
        }
        return results;
    }
    public async Task<bool> IsDoubleBet()
    {
        bool result = false;
        if (UserBets_F2 != null && UserBets_F2.Count > 0)
        {
            var duplicateBetsF2 = UserBets_F2.Where(x => x.BetValue.ToLower().Equals(LetterValue.ToLower())).ToList();
            if (duplicateBetsF2 != null && duplicateBetsF2.Count > 0)
            {
                DuplicateBetvalue = duplicateBetsF2.FirstOrDefault().BetValue;
                result = true;
            }
        }
        if (UserBets_F3 != null && UserBets_F3.Count > 0)
        {
            var duplicateBetsF3 = UserBets_F3.Where(x => x.BetValue.ToLower().Equals(LetterValue.ToLower())).ToList();
            if (duplicateBetsF3 != null && duplicateBetsF3.Count > 0)
            {
                DuplicateBetvalue = duplicateBetsF3.FirstOrDefault().BetValue;
                result = true;
            }
        }
        if (UserBets_All4 != null && UserBets_All4.Count > 0)
        {
            var duplicateBetsA4 = UserBets_All4.Where(x => x.BetValue.ToLower().Equals(LetterValue.ToLower())).ToList();
            if (duplicateBetsA4 != null && duplicateBetsA4.Count > 0)
            {
                DuplicateBetvalue = duplicateBetsA4.FirstOrDefault().BetValue;
                result = true;
            }
        }
        return result;
    }
    public async Task UpdateGigaDrawBets(List<BetModel> F2Bets, List<BetModel> F3Bets, List<BetModel> A4Bets)
    {
        TotalAccumulatedBets = 0;
        BetModel tempBet = new BetModel();
        ObservableCollection<BetModel> tempBetList = new ObservableCollection<BetModel>();
        if (F2Bets != null)
        {
            tempBetList = new ObservableCollection<BetModel>();
            foreach (var b in F2Bets.OrderByDescending(x => x.Id))
            {
                tempBet = b;
                if (b.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f2).ToString("#,##0");
                }
                tempBetList.Add(tempBet);
            }
            UserBets_F2 = tempBetList;
            TotalBets_F2 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
            TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
        }

        if (F3Bets != null)
        {
            tempBetList = new ObservableCollection<BetModel>();
            foreach (var b in F3Bets.OrderByDescending(x => x.Id))
            {
                tempBet = b;
                if (b.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_f3).ToString("#,##0");
                }
                tempBetList.Add(tempBet);
            }
            UserBets_F3 = tempBetList;
            TotalBets_F3 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
            TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
        }

        if (A4Bets != null)
        {
            tempBetList = new ObservableCollection<BetModel>();
            foreach (var b in A4Bets.OrderByDescending(x => x.Id))
            {
                tempBet = b;
                if (b.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(b.BetAmount) * Multiplier_all4).ToString("#,##0");
                }
                tempBetList.Add(tempBet);
            }
            UserBets_All4 = tempBetList;
            TotalBets_All4 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
            TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
        }

        TotalAccumulatedBetsString = TotalAccumulatedBets.ToString("N0");

        //SelectedChipAmount = 0;
        //BetAmount = 0;
        //BetAmountDisplay = string.Empty;
        await ResetDefaultAmount();
        LetterValue = string.Empty;
        IsLuckyPick1 = false;
        IsLuckyPick5 = false;
        if (GameRound != null)
        {
            if (GameRound.RoundStatus != (int)RoundStatus.Open)
            {
                IsBettingEnabled = false;
            }
        }
        else
        {
            IsBettingEnabled = false;
        }
    }
    public async Task SubmitBet()
    {
        try
        {
            IsDuplicateBet = await IsDoubleBet();

            BetDTO betParams = new BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.LuckyPickCharacters = CharLimit;
            if (BetTypeId == (int)NonCardGameBetTypes.Normal)
            {
                betParams.BetValue = LetterValue;
                betParams.BetAmount = BetAmount;
            }
            else if (BetTypeId == (int)NonCardGameBetTypes.LuckyPick)
            {
                betParams.LuckyPick = true;
                betParams.BetAmount = BetAmount;
            }
            else if (BetTypeId == (int)NonCardGameBetTypes.LuckyPickx5)
            {
                betParams.LuckyPickX5 = true;
                betParams.BetAmount = BetAmount / 5;
            }
            else if (BetTypeId == (int)NonCardGameBetTypes.LuckyPickx10)
            {
                betParams.LuckyPickX10 = true;
                betParams.BetAmount = BetAmount / 10;
            }


            var tempBetResult = await _igameRoundService.BetOnRound(betParams);
            if (tempBetResult == null)
            {

                _toastService.ShowError("Unable to place bet.");
            }
            else if (tempBetResult != null && tempBetResult.Success == false)
            {
                _toastService.ShowError(tempBetResult.Message);
            }
            else if (tempBetResult != null && tempBetResult.Bets != null)
            {
                _toastService.ShowSuccess("Bets Confirmed ");

                List<BetModel> F2Bets = tempBetResult.F2Bets != null && tempBetResult.F2Bets.Count > 0 ? tempBetResult.F2Bets : null;
                List<BetModel> F3Bets = tempBetResult.F3Bets != null && tempBetResult.F3Bets.Count > 0 ? tempBetResult.F3Bets : null;
                List<BetModel> A4Bets = tempBetResult.A4Bets != null && tempBetResult.A4Bets.Count > 0 ? tempBetResult.A4Bets : null;
                BetMultiplier = "";
                await UpdateGigaDrawBets(F2Bets, F3Bets, A4Bets);
                //switch (GametypeId)
                //{
                //    case (int)GameTypes.Giga_Draw:
                //        model = new GenericModel()
                //        {
                //            GameType = GameTypeId,
                //            Bets = tempBetResult.Bets != null && tempBetResult.Bets.Count > 0 ? tempBetResult.Bets : null
                //        };
                //        MessagingCenter.Send(this, Constants.UpdateGigaDrawBets, model);
                //        break;
                //    case (int)GameTypes.Drop_And_Win:
                //        model = new GenericModel()
                //        {
                //            GameType = GameTypeId,
                //            F2Bets = tempBetResult.F2Bets != null && tempBetResult.F2Bets.Count > 0 ? tempBetResult.F2Bets : null,
                //            F3Bets = tempBetResult.F3Bets != null && tempBetResult.F3Bets.Count > 0 ? tempBetResult.F3Bets : null,
                //            A4Bets = tempBetResult.A4Bets != null && tempBetResult.A4Bets.Count > 0 ? tempBetResult.A4Bets : null
                //        };
                //        MessagingCenter.Send(this, Constants.UpdateGigaDrawBets, model);
                //        break;
                //}
            }

            await ValidateUser();
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }

    }
    public async Task SetLuckyPickBet(string param)
    {
        if (param == Constants.LuckyPick)
        {
            BetValueDisplay = Constants.LuckyPick;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPick;

            if (!IsLuckyPick1)
            {
                IsLuckyPick1 = true;
            }
            else
            {
                IsLuckyPick1 = false;

            }

            IsLuckyPick5 = false;
            LetterValue = string.Empty;
            BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount : 0;
            BetAmountDisplay = BetAmount.ToString("N0");

        }
        else if (param == Constants.LuckyPickx5)
        {
            BetValueDisplay = Constants.LuckyPickx5;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;

            if (!IsLuckyPick5)
            {
                IsLuckyPick5 = true;
                BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount * 5 : 0;
                BetAmountDisplay = BetAmount.ToString("N0");
            }
            else
            {
                IsLuckyPick5 = false;
                BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount : 0;
                BetAmountDisplay = BetAmount.ToString("N0");
            }

            IsLuckyPick1 = false;
            LetterValue = string.Empty;
        }

        if (IsLuckyPick1 && BetAmount > 0 ||
            IsLuckyPick5 && BetAmount > 0)
        {
            IsPlaceBetEnabled = true;
        }
        else
        {
            IsPlaceBetEnabled = false;
        }
    }
}
