using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class BigWinViewModel : BaseViewModel
{
    #region Injected
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region Local Variables & Properties

    #region GAME CONFIGURATION
    private TimeSpan _previousTime;
    private TimeSpan _currentTime;
    private TimeSpan _allowedViewingTime;
    private int _roundNumber;
    private int _numberValue;
    int _betTypeId = 0;
    private bool _hasResult;
    private bool _isGameOpen;
    private bool _isBetEnabled;
    public bool _isBetDisabled;
    private bool _isTimerStopped;
    private bool _isLuckyPickEnabled;
    private bool _isNumberValueFocused;
    private bool _isBackToGameListingEnabled;
    private string _baseBetValue;
    private string _roundStatusColor;
    private string _numberValueInput;
    private string _roundStatusString;
    string _betOption = string.Empty;
    private string _bwgif = "";


    public TimeSpan PreviousTime
    {
        get => _previousTime;
        set
        {
            _previousTime = value;
            RaisePropertyChanged(() => PreviousTime);
        }
    }
    public TimeSpan CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            RaisePropertyChanged(() => CurrentTime);
        }
    }
    public TimeSpan AllowedViewingTime
    {
        get => _allowedViewingTime;
        set
        {
            _allowedViewingTime = value;
            RaisePropertyChanged(() => AllowedViewingTime);
        }
    }
    public int RoundNumber
    {
        get => _roundNumber;
        set
        {
            _roundNumber = value;
            RaisePropertyChanged(() => RoundNumber);
        }
    }
    public int NumberValue
    {
        get => _numberValue;
        set
        {
            _numberValue = value;
            RaisePropertyChanged(() => NumberValue);
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
    public bool HasResult
    {
        get => _hasResult;
        set
        {
            _hasResult = value;
            RaisePropertyChanged(() => HasResult);
        }
    }
    public bool IsGameOpen
    {
        get => _isGameOpen;
        set
        {
            _isGameOpen = value;
            RaisePropertyChanged(() => IsGameOpen);
        }
    }
    public bool IsBetEnabled
    {
        get => _isBetEnabled;
        set
        {
            _isBetEnabled = value;
            RaisePropertyChanged(() => IsBetEnabled);
        }
    }
    public bool IsBetDisabled
    {
        get => _isBetDisabled;
        set
        {
            _isBetDisabled = value;
            RaisePropertyChanged(() => IsBetDisabled);
        }
    }
    public bool IsTimerStopped
    {
        get => _isTimerStopped;
        set
        {
            _isTimerStopped = value;
            RaisePropertyChanged(() => IsTimerStopped);
        }
    }
    public bool IsLuckyPickEnabled
    {
        get => _isLuckyPickEnabled;
        set
        {
            _isLuckyPickEnabled = value;
            RaisePropertyChanged(() => IsLuckyPickEnabled);
        }
    }
    public bool IsNumberValueFocused
    {
        get => _isNumberValueFocused;
        set
        {
            _isNumberValueFocused = value;
            RaisePropertyChanged(() => IsNumberValueFocused);
        }
    }
    public bool IsBackToGameListingEnabled
    {
        get => _isBackToGameListingEnabled;
        set
        {
            _isBackToGameListingEnabled = value;
            RaisePropertyChanged(() => IsBackToGameListingEnabled);
        }
    }
    public string BaseBetValue
    {
        get => _baseBetValue;
        set
        {
            _baseBetValue = value;
            RaisePropertyChanged(() => BaseBetValue);
        }
    }
    public string NumberValueInput
    {
        get => _numberValueInput;
        set
        {
            _numberValueInput = value;
            RaisePropertyChanged(() => NumberValueInput);
        }
    }
    public string RoundStatusColor
    {
        get => _roundStatusColor;
        set
        {
            _roundStatusColor = value;
            RaisePropertyChanged(() => RoundStatusColor);
        }
    }
    public string RoundStatusString
    {
        get => _roundStatusString;
        set
        {
            _roundStatusString = value;
            RaisePropertyChanged(() => RoundStatusString);
        }
    }
    public string BetOption
    {
        get => _betOption;
        set
        {
            _betOption = value;
            RaisePropertyChanged(() => BetOption);
        }
    }
    public string BWgif
    {
        get => _bwgif;
        set
        {
            _bwgif = value;
            RaisePropertyChanged(() => BWgif);
        }
    }
    #endregion

    #region GAME SETTINGS
    private GameSettingModel _gameSetting;


    public GameSettingModel GameSetting
    {
        get => _gameSetting;
        set
        {
            _gameSetting = value;
            RaisePropertyChanged(() => GameSetting);
        }
    }
    #endregion

    #region LUCKY PICK
    private bool _isLuckyPick1;
    private bool _isLuckyPick3;
    private bool _isLuckyPick5;
    private bool _isLuckyPick10;


    public bool IsLuckyPick1
    {
        get => _isLuckyPick1;
        set
        {
            _isLuckyPick1 = value;
            RaisePropertyChanged(() => IsLuckyPick1);
        }
    }
    public bool IsLuckyPick3
    {
        get => _isLuckyPick3;
        set
        {
            _isLuckyPick3 = value;
            RaisePropertyChanged(() => IsLuckyPick3);
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
    public bool IsLuckyPick10
    {
        get => _isLuckyPick10;
        set
        {
            _isLuckyPick10 = value;
            RaisePropertyChanged(() => IsLuckyPick10);
        }
    }
    #endregion

    #region USER BETS
    private int _totalBets;
    int _totalBetAmount = 0;
    private ObservableCollection<BetModel> _userBets;
    private ObservableCollection<BetModel> _userprevBets;


    public int TotalBets
    {
        get => _totalBets;
        set
        {
            _totalBets = value;
            RaisePropertyChanged(() => TotalBets);
        }
    }
    public int TotalBetAmount
    {
        get => _totalBetAmount;
        set
        {
            _totalBetAmount = value;
            RaisePropertyChanged(() => _totalBetAmount);
        }
    }
    public ObservableCollection<BetModel> UserBets
    {
        get => _userBets;
        set
        {
            _userBets = value;
            RaisePropertyChanged(() => UserBets);
        }
    }
    public ObservableCollection<BetModel> UserPrevBets
    {
        get => _userprevBets;
        set
        {
            _userprevBets = value;
            RaisePropertyChanged(() => UserPrevBets);
        }
    }
    #endregion

    #region LETTER
    private static string letterValueHolder;
    private string _letterValue = "";


    public string LetterValue
    {
        get => _letterValue;
        set
        {
            _letterValue = value.ToUpper();
            RaisePropertyChanged(() => LetterValue);
            RaisePropertyChanged(() => LetterValue1);
            RaisePropertyChanged(() => LetterValue2);
            RaisePropertyChanged(() => LetterValue3);
            RaisePropertyChanged(() => LetterValue4);
            RaisePropertyChanged(() => LetterValue5);
            CallInvoke();
        }
    }
    string _PreletterValue = "";
    public string PreLetterValue
    {
        get => _PreletterValue;
        set
        {
            _PreletterValue = value.ToUpper();
            CallInvoke();
        }
    }
    public string LetterValue1
    {
        get
        {
            if (LetterValue.Length > 0)
            {
                return LetterValue.Substring(0, 1);
            }
            else
            {
                return string.Empty;
            }
        }

    }
    public string LetterValue2
    {
        get
        {
            if (LetterValue.Length > 1)
            {
                return LetterValue.Substring(1, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string LetterValue3
    {
        get
        {
            if (LetterValue.Length > 2)
            {
                return LetterValue.Substring(2, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string LetterValue4
    {
        get
        {
            if (LetterValue.Length > 3)
            {
                return LetterValue.Substring(3, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string LetterValue5
    {
        get
        {
            if (LetterValue.Length > 4)
            {
                return LetterValue.Substring(4, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    #endregion

    #region GAME RESULT
    private string _gameResult = "";


    public string GameResult
    {
        get => _gameResult;
        set
        {
            _gameResult = value;
            RaisePropertyChanged(() => GameResult);
            HasResult = !string.IsNullOrEmpty(GameResult) ? true : false;
        }
    }
    public string GameResult1
    {
        get
        {
            if (GameResult.Length > 0)
            {
                return GameResult.Substring(0, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string GameResult2
    {
        get
        {
            if (GameResult.Length > 1)
            {
                return GameResult.Substring(1, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string GameResult3
    {
        get
        {
            if (GameResult.Length > 2)
            {
                return GameResult.Substring(2, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string GameResult4
    {
        get
        {
            if (GameResult.Length > 3)
            {
                return GameResult.Substring(3, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public string GameResult5
    {
        get
        {
            if (GameResult.Length > 4)
            {
                return GameResult.Substring(4, 1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    #endregion

    #region PRIZES
    private string _jackpotPrize;
    private string _miniJackpotPrize;
    private string _consolationPrize;


    public string JackpotPrize
    {
        get => _jackpotPrize;
        set
        {
            _jackpotPrize = value;
            RaisePropertyChanged(() => JackpotPrize);
        }
    }
    public string MiniJackpot
    {
        get => _miniJackpotPrize;
        set
        {
            _miniJackpotPrize = value;
            RaisePropertyChanged(() => MiniJackpot);
        }
    }
    public string ConsolationPrize
    {
        get => _consolationPrize;
        set
        {
            _consolationPrize = value;
            RaisePropertyChanged(() => ConsolationPrize);
        }
    }
    #endregion

    #region TRENDS
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<GameRoundModel> CurrentList { get; set; }
    }
    private ObservableCollection<GameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _trendsDisplay;


    public ObservableCollection<GameRoundModel> Trends
    {
        get => _trends;
        set
        {
            _trends = value;
            RaisePropertyChanged(() => Trends);
        }
    }
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay
    {
        get => _trendsDisplay;
        set
        {
            _trendsDisplay = value;
            RaisePropertyChanged(() => TrendsDisplay);
        }
    }
    #endregion

    #region PREV GAME ROUND
    private GameRoundModel _prevGameRound;


    public GameRoundModel PrevGameRound
    {
        get => _prevGameRound;
        set
        {
            _prevGameRound = value;
            RaisePropertyChanged(() => PrevGameRound);
        }
    }
    #endregion

    public string ticketRows = "";
    private static int fixPrize = 0;
    private static int tapCounter = 0;
    public ElementReference txtbetnumber;
    public string CurrView { get; set; } = "Current";

    #endregion

    #region Lifecycle Methods
    public BigWinViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IToastService toastService, IAccountService iaccountService, IGameSettingService igameSettingService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _igameSettingService = igameSettingService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        StreamId = Constants.StreamIDBigWin;
        GametypeId = (int)GameTypes.BigWin;

        ValidateUser();
    }
    #endregion

    #region Security
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
    #endregion

    #region SignalR Methods
    private void UpdateBallGamesResult(int gameTypeId, string results)
    {
        if (gameTypeId == GametypeId)
        {
            PreLetterValue = results;
            CallInvoke();
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
                    {
                        HubConnection.Remove(Constants.UpdateGameTimer);
                        HubConnection.Remove(Constants.UpdateGameStatus);
                        HubConnection.Remove(Constants.NotifyGameRoundResult);
                        HubConnection.Remove(Constants.UpdateTrends);

                        HubConnection.On<int, string>(Constants.UpdateBallGamesResult, UpdateBallGamesResult);
                        HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                        HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                        HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                        HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    }
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    public override async void UpdateGameTimer(int gametypeId, string value)
    {
        try
        {
            if (GametypeId == gametypeId && GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                if (GameRound.RoundStatus == (int)RoundStatus.Open && timer < 10)
                {
                    ShowFlashing = "timerFlasher";
                }
                else
                {
                    ShowFlashing = "timerNotFlashing";
                }
                if (GameRound.RoundStatus == (int)RoundStatus.Closed && AwaitingGameRound == false)
                {
                    AwaitingGameRound = true;
                    await GetGameRound();
                    AwaitingGameRound = false;
                }
                CallInvoke();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus, long gameRoundId)
    {
        if (gametypeId == GametypeId)
        {
            await GetGameRound();
            //if (gameRoundId != GameRound.Id && gameStatus == (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else if (gameRoundId == GameRound.Id && gameStatus != GameRound.RoundStatus && gameStatus != (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else
            //{
            //    return;
            //}

            BetAmount = int.Parse(BaseBetValue);

            if (gameStatus == (int)RoundStatus.Open)
            {
                TickerMessage = "";
                IsGameOpen = true;
                IsLuckyPickEnabled = true;

                BWgif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else if (gameStatus == (int)RoundStatus.PendingResult)
            {
                BWgif = "/img/animation/test-closebet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick10 = false;
                IsLuckyPick5 = false;

                BWgif = "";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            await CallInvoke();
        }

    }
    public async void NotifyGameRoundResult(UpdateGameResultModel paramsModel)
    {
        if (paramsModel.GameTypeId == GametypeId)
        {
            GameResult = paramsModel.WinningCombination;
            await GetCurrentRoundBets();
            CallInvoke();
        }
    }
    public async Task UpdateTrends(int gameTypeId)
    {
        if (gameTypeId == GametypeId)
        {
            await GetTrends();
            await CallInvoke();
        }
    }
    #endregion

    #region Game Methods
    public async Task GetGameSetting()
    {
        try
        {
            var tempRoundSettings = await _igameSettingService.GetGameSettings(GametypeId);
            if (tempRoundSettings == null)
            {
                _toastService.ShowError("No round details found.");
            }
            else if (tempRoundSettings != null)
            {
                GameSetting = tempRoundSettings;
                BaseBetValue = tempRoundSettings.FixValue != null ? Convert.ToDecimal(tempRoundSettings.FixValue).ToString("0") : "0";
                JackpotPrize = tempRoundSettings.Jackpot != null ? Convert.ToDecimal(tempRoundSettings.Jackpot).ToString("#,###.#0") : "0";
                MiniJackpot = tempRoundSettings.MiniJackpot != null ? Convert.ToDecimal(tempRoundSettings.MiniJackpot).ToString("#,###.#0") : "0";
                ConsolationPrize = tempRoundSettings.Consolation != null ? Convert.ToDecimal(tempRoundSettings.Consolation).ToString("#,##0") : "0";
                fixPrize = tempRoundSettings.FixValue != null ? Convert.ToInt32(tempRoundSettings.FixValue) : 0;
                BetAmount = int.Parse(BaseBetValue);
                await CallInvoke();
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task GetGameRound()
    {
        if (GametypeId > 0)
        {
            IsBetEnabled = false;
            LetterValue = string.Empty;
            NumberValue = 0;
            NumberValueInput = string.Empty;
            RoundTimer = ""; // 00:00
            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            IsBackToGameListingEnabled = true;

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
                if (!GameRound.JackpotWinner)
                {
                    JackpotPrize = tempRound.JackpotPrize != null ? Convert.ToDecimal(tempRound.JackpotPrize).ToString("#,###.#0") : "0";
                }
                if (!GameRound.MiniJackpotWinner)
                {
                    MiniJackpot = tempRound.MiniJackpotPrize != null ? Convert.ToDecimal(tempRound.MiniJackpotPrize).ToString("#,###.#0") : "0";
                }
                await GetCurrentRoundBets();
                await GetPrevRoundBets();

                if (GameRound.RoundNumber > 0)
                {
                    RoundNumber = GameRound.RoundNumber;
                }
                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;
                    IsGameOpen = true;
                    IsLuckyPickEnabled = true;
                    GameResult = string.Empty;
                    PreLetterValue = string.Empty;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Closed)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;

                    IsGameOpen = false;
                    IsLuckyPickEnabled = false;

                    GameResult = tempRound.WinningResult;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Paused)
                {
                    RoundStatusString = Constants.Paused;
                    RoundStatusColor = Constants.GamePausedColor;
                    IsBackToGameListingEnabled = false;
                    IsGameOpen = false;
                    IsLuckyPickEnabled = false;
                    GameResult = string.Empty;
                    PreLetterValue = string.Empty;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
                {
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;

                    IsGameOpen = false;
                    IsLuckyPickEnabled = false;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.PendingResult)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    IsGameOpen = false;
                    IsLuckyPickEnabled = false;
                }

                await GetTrends();

            }
        }
        await CallInvoke();
    }
    public async Task GetCurrentRoundBets()
    {
        long UserId = _icurrentUser.Id;
        if (GameRound == null) return;
        var tempRoundBets = await _igameRoundService.GetBets(UserId, GametypeId, GameRound.Id);
        if (tempRoundBets != null)
        {
            if (GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                UserBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderByDescending(x => x.BetStatus).ThenBy(x => x.BetValue));
                var LoseUserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderByDescending(x => x.Id));
                foreach (var item in LoseUserPrevBets)
                {
                    UserBets.Add(item);
                }
            }
            else
            {
                UserBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderByDescending(x => x.BetStatus).ThenBy(x => x.BetValue));
                var LoseUserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderBy(x => x.BetValue));
                foreach (var item in LoseUserPrevBets)
                {
                    UserBets.Add(item);
                }
            }
            TotalBets = (int)UserBets.Sum(x => Convert.ToDecimal(x.BetAmount));
            await GetTotalBetsCSS();
            await CallInvoke();
        }
    }
    public async Task GetPrevRoundBets()
    {
        long UserId = _icurrentUser.Id;
        if (GameRound == null) return;
        var tempRoundBets = await _igameRoundService.GetPrevGameBets(UserId, GametypeId, 100);
        if (tempRoundBets != null)
        {
            UserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.OrderByDescending(x => x.GameRound.RoundNumber).ThenByDescending(x => x.Winnings).ThenBy(x => x.BetValue));
        }
    }
    private async Task GetTotalBetsCSS()
    {
        if (UserBets.ToList().Count > 5)
        {
            ticketRows = "1fr 1fr 1fr 1fr 1fr";
        }
        else
        {
            ticketRows = "";
            for (int i = 0; i < UserBets.ToList().Count; i++)
            {
                ticketRows = ticketRows + " 1fr ";
            }
        }
    }
    public async Task GetTrends()
    {
        if (GametypeId > 0)
        {
            var tempTrends = await _igameRoundService.GetTrends(GametypeId);
            if (tempTrends != null)
            {
                PrevGameRound = tempTrends.First();
                await SetTrendsDisplay(tempTrends);
                await CallInvoke();
            }
        }
    }
    public async Task SetTrendsDisplay(List<GameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<GameRoundModel> CurrentList = new ObservableCollection<GameRoundModel>();
            TrendsDisplayModel Current = new TrendsDisplayModel();
            int counter = 0;
            int rowCounter = 0;

            foreach (var t in tempTrends.OrderByDescending(x => x.Id).ToList())
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
                    Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<GameRoundModel>();

                    rowCounter = 0;
                }
            }

            if (resultHolder != null)
            {
                TrendsDisplay = resultHolder;
            }
        }
    }
    #endregion

    #region Bet Methods
    public async Task AddCharacter(string param)
    {
        if (param != null)
        {
            LetterValue = LetterValue + param.ToString();
        }
        if (LetterValue.Length == 5)
        {
            IsBetEnabled = true;
        }
        else
        {
            IsBetEnabled = false;
        }
        await CallInvoke();
    }
    public async Task<bool> BetOptionSelected(object arg = null)
    {
        bool results = false;

        var creditValue = Convert.ToDecimal(_icurrentUser.Credits);
        if (!IsBetEnabled)
        {
            results = false;
        }

        if (arg.ToString() == Constants.NormalPick)
        {
            BetOption = LetterValue;
            BetTypeId = (int)NonCardGameBetTypes.Normal;
            TotalBetAmount = fixPrize;
        }
        else if (arg.ToString() == Constants.LuckyPick)
        {
            BetOption = Constants.LuckyPick;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPick;
            TotalBetAmount = fixPrize;
        }
        else if (arg.ToString() == Constants.LuckyPickx5)
        {
            BetOption = Constants.LuckyPickx5;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;
            TotalBetAmount = fixPrize * 5;
        }
        else if (arg.ToString() == Constants.LuckyPickx10)
        {
            BetOption = Constants.LuckyPickx10;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx10;
            TotalBetAmount = fixPrize * 10;
        }
        else if (arg.ToString() == Constants.LuckyPickx3)
        {
            BetOption = Constants.LuckyPickx10;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx3;
            TotalBetAmount = fixPrize * 3;
        }

        if (TotalBetAmount > creditValue)
        {
            _toastService.ShowError("Insufficient Credits....");
            results = false;
        }
        else if (TotalBetAmount <= creditValue)
        {
            results = true;
        }

        return results;
    }
    public async Task GenerateLuckyPick1()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random random = new Random();
        char[] randomArray = new char[5];

        for (int i = 0; i < 5; i++)
        {
            randomArray[i] = chars[random.Next(chars.Length)];
        }
        IsBetEnabled = true;
        IsLuckyPick1 = true;
        LetterValue = new string(randomArray);
    }
    public async Task SubmitBet()
    {
        try
        {

            BetDTO betParams = new BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.LuckyPickCharacters = 0;
            if (BetTypeId == (int)NonCardGameBetTypes.Normal)
            {
                betParams.BetValue = BetOption;
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
            else if (BetTypeId == (int)NonCardGameBetTypes.LuckyPickx3)
            {
                betParams.LuckyPickX3 = true;
                betParams.BetAmount = BetAmount / 3;
            }


            var tempBetResult = await _igameRoundService.BetOnRound(betParams);
            if (tempBetResult == null)
            {
                _toastService.ShowError("Unable to place bet.");
                await ReEnableSubmitButton();
            }
            else if (tempBetResult != null && tempBetResult.Success == false)
            {

                _toastService.ShowError(tempBetResult.Message);
                await ReEnableSubmitButton();
            }
            else if (tempBetResult != null && tempBetResult.Bets != null)
            {

                //_toastService.ShowSuccess(tempBetResult.Message);
                //_toastService.ShowSuccess("Bets Confirmed");
                await UpdateGigaDrawBets(GametypeId, tempBetResult.Bets != null && tempBetResult.Bets.Count > 0 ? tempBetResult.Bets : null);

            }
            BetAmount = int.Parse(BaseBetValue);

            IsLuckyPick1 = false;
            IsLuckyPick10 = false;
            IsLuckyPick5 = false;
            IsLuckyPick3 = false;
            IsBetEnabled = false;
            await ValidateUser();
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    private async Task ReEnableSubmitButton()
    {
        if (GameRound != null && GameRound.RoundStatus == (int)RoundStatus.Open)
        {
            if (!string.IsNullOrEmpty(LetterValue) && !string.IsNullOrEmpty(NumberValueInput))
            {
                IsBetEnabled = true;
            }

            tapCounter = 0;
        }
        else
        {
            IsBetEnabled = false;
            tapCounter = 0;
        }
    }
    public async Task UpdateGigaDrawBets(int GameType, List<BetModel> Bets)
    {
        if (GameType == GametypeId)
        {
            if (Bets != null && Bets.Count > 0)
            {
                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    UserBets = new ObservableCollection<BetModel>(Bets.OrderByDescending(x => x.Id));
                }
                else
                {
                    UserBets = new ObservableCollection<BetModel>();
                }

                await GetTotalBetsCSS();
                TotalBets = (int)UserBets.Sum(x => Convert.ToDecimal(x.BetAmount));
                tapCounter = 0;
            }

            LetterValue = string.Empty;
            NumberValue = 0;
            NumberValueInput = string.Empty;
        }
    }
    #endregion

    #region UNUSED
    public async Task ValidateLetterInput()
    {
        if (!string.IsNullOrEmpty(LetterValue) && letterValueHolder != LetterValue)
        {
            string tempValue = string.Empty;
            int validCounter = 0;
            var letterArr = LetterValue.ToCharArray();
            if (letterArr != null)
            {
                if (letterArr.Length > 5)
                {
                    _toastService.ShowError("Bet letters cannot exceed 5 combinations");
                    LetterValue = LetterValue.Substring(0, 5);
                    return;
                }
                foreach (var a in letterArr)
                {
                    var isValid = Regex.IsMatch(a.ToString(), "^[a-zA-Z]+$");
                    if (isValid)
                    {
                        tempValue = tempValue + a.ToString();
                        validCounter++;
                    }
                }

                if (validCounter != letterArr.Length)
                {
                    LetterValue = tempValue;
                }
            }
            letterValueHolder = LetterValue;

            if (LetterValue.Length == 5)
            {
                IsBetEnabled = true;

                //IsNumberValueFocused = true;
                //await txtbetnumber.FocusAsync();
                //if (!String.IsNullOrEmpty(NumberValueInput))
                //{
                //    IsBetEnabled = true; 

                //}

                //MessagingCenter.Send(this, Constants.SetNumberEntryFocus, true);
            }
            else
            {
                IsBetEnabled = false;
                //MessagingCenter.Send(this, Constants.SetNumberEntryFocus, false);
            }
        }
        else
        {
            //IsBetEnabled = false;
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
                isvalid = false;
            }
            else
            {
                isvalid = true;
            }
        }
        return isvalid;
    }
    private async Task UpdateGigaDrawBets(List<BetModel> Bets)
    {
        if (Bets != null && Bets.Count > 0)
        {
            UserBets = new ObservableCollection<BetModel>(Bets.OrderByDescending(x => x.Id));
            TotalBets = (int)UserBets.Sum(x => Convert.ToDecimal(x.BetAmount));
            tapCounter = 0;
        }

        LetterValue = string.Empty;
        NumberValue = 0;
        NumberValueInput = string.Empty;
    }
    public async Task SetLuckyPickBet(string param)
    {
        if (param == Constants.LuckyPick)
        {
            BetTypeId = (int)NonCardGameBetTypes.LuckyPick;

            if (!IsLuckyPick1)
            {
                IsLuckyPick1 = true;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                IsLuckyPick3 = false;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize;

                IsBetDisabled = false;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                IsLuckyPick3 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;

                IsBetDisabled = true;
            }


        }
        else if (param == Constants.LuckyPickx3)
        {

            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx3;

            if (!IsLuckyPick3)
            {
                IsLuckyPick1 = false;
                IsLuckyPick3 = true;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize * 5;
                IsBetDisabled = false;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick3 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;
                IsBetDisabled = true;
            }
        }
        else if (param == Constants.LuckyPickx5)
        {

            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;

            if (!IsLuckyPick5)
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = true;
                IsLuckyPick3 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize * 5;
                IsBetDisabled = false;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick3 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;
                IsBetDisabled = true;
            }
        }
        else if (param == Constants.LuckyPickx10)
        {

            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx10;

            if (!IsLuckyPick10)
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick3 = false;
                IsLuckyPick10 = true;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize * 10;
                IsBetDisabled = false;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick3 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;
                IsBetDisabled = true;
            }
        }
        if (IsLuckyPick1 && BetAmount > 0 ||
            IsLuckyPick5 && BetAmount > 0 ||
            IsLuckyPick10 && BetAmount > 0 ||
            IsLuckyPick3 && BetAmount > 0)
        {
            IsBetEnabled = true;
            IsBetDisabled = false;
        }
        else
        {
            IsBetEnabled = false;
            IsBetDisabled = true;
        }
    }
    #endregion

}
