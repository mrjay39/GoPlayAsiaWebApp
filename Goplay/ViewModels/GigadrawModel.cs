using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class GigadrawModel : BaseViewModel
{
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<GameRoundModel> CurrentList { get; set; }
    }


    #region Local Variable
    private TimeSpan _previousTime;
    private TimeSpan _currentTime;

    private static string letterValueHolder;
    private static int fixPrize = 0;
    public ElementReference txtbetnumber;


    private GameSettingModel _gameSetting;

    private string _roundStatusString;
    private string _roundStatusColor;
    private int _roundNumber;

    private bool _isTimerStopped;
    private TimeSpan _allowedViewingTime;

    private bool _isBackToGameListingEnabled;

    private bool _isGameOpen;
    private bool _isNumberValueFocused;
    private bool _isLuckyPickEnabled;
    private bool _isBetEnabled;
    private string _letterValue;
    private string _numberValueInput;
    private int _numberValue;
    private string _baseBetValue;
    private string _jackpotPrize;
    private string _consolationPrize;
    private int _totalBets;
    private ObservableCollection<BetModel> _userBets;
    private ObservableCollection<GameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _trendsDisplay;
    private string _gameResult;
    private bool _hasResult;
    private static int tapCounter = 0;
    string _betOption = string.Empty;
    int _betTypeId = 0;
    int _totalBetAmount = 0;
    public string ticketRows = "";
    private ObservableCollection<BetModel> _userprevBets;
    private GameRoundModel _prevGameRound;

    #region Isluckypick
    private bool _isLuckyPick1;
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
    #endregion
    #region Properties


    public ObservableCollection<BetModel> UserPrevBets
    {
        get => _userprevBets;
        set
        {
            _userprevBets = value;
            RaisePropertyChanged(() => UserPrevBets);
        }
    }
    public GameRoundModel PrevGameRound
    {
        get => _prevGameRound;
        set
        {
            _prevGameRound = value;
            RaisePropertyChanged(() => PrevGameRound);
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
    public int BetTypeId
    {
        get => _betTypeId;
        set
        {
            _betTypeId = value;
            RaisePropertyChanged(() => BetTypeId);
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


    public GameSettingModel GameSetting
    {
        get => _gameSetting;
        set
        {
            _gameSetting = value;
            RaisePropertyChanged(() => GameSetting);
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

    public string RoundStatusColor
    {
        get => _roundStatusColor;
        set
        {
            _roundStatusColor = value;
            RaisePropertyChanged(() => RoundStatusColor);
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

    public bool IsTimerStopped
    {
        get => _isTimerStopped;
        set
        {
            _isTimerStopped = value;
            RaisePropertyChanged(() => IsTimerStopped);
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

    public bool IsBackToGameListingEnabled
    {
        get => _isBackToGameListingEnabled;
        set
        {
            _isBackToGameListingEnabled = value;
            RaisePropertyChanged(() => IsBackToGameListingEnabled);
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

    public bool IsNumberValueFocused
    {
        get => _isNumberValueFocused;
        set
        {
            _isNumberValueFocused = value;
            RaisePropertyChanged(() => IsNumberValueFocused);
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

    public bool IsBetEnabled
    {
        get => _isBetEnabled;
        set
        {
            _isBetEnabled = value;
            RaisePropertyChanged(() => IsBetEnabled);
        }
    }

    public string LetterValue
    {
        get => _letterValue;
        set
        {
            _letterValue = value.ToUpper();
            RaisePropertyChanged(() => LetterValue);
            ValidateLetterInput();
        }
    }

    public string NumberValueInput
    {
        get => _numberValueInput;
        set
        {
            _numberValueInput = value;
            ValidateNumberInput();
            RaisePropertyChanged(() => NumberValueInput);

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

    public string BaseBetValue
    {
        get => _baseBetValue;
        set
        {
            _baseBetValue = value;
            RaisePropertyChanged(() => BaseBetValue);
        }
    }

    public string JackpotPrize
    {
        get => _jackpotPrize;
        set
        {
            _jackpotPrize = value;
            RaisePropertyChanged(() => JackpotPrize);
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
    public int TotalBets
    {
        get => _totalBets;
        set
        {
            _totalBets = value;
            RaisePropertyChanged(() => TotalBets);
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
    public bool HasResult
    {
        get => _hasResult;
        set
        {
            _hasResult = value;
            RaisePropertyChanged(() => HasResult);
        }
    }
    #endregion

    public GigadrawModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IToastService toastService, IAccountService iaccountService, IGameSettingService igameSettingService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _igameSettingService = igameSettingService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        StreamId = Constants.StreamIDGigadraw;
        GametypeId = (int)GameTypes.Giga_Draw;

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
                _toastService.ShowError("No round details found.");
            }
            else if (tempRoundSettings != null)
            {
                GameSetting = tempRoundSettings;
                BaseBetValue = tempRoundSettings.FixValue != null ? Convert.ToDecimal(tempRoundSettings.FixValue).ToString("0") : "0";
                JackpotPrize = tempRoundSettings.Jackpot != null ? Convert.ToDecimal(tempRoundSettings.Jackpot).ToString("#,###.#0") : "0";
                ConsolationPrize = tempRoundSettings.Consolation != null ? Convert.ToDecimal(tempRoundSettings.Consolation).ToString("#,##0") : "0";
                fixPrize = tempRoundSettings.FixValue != null ? Convert.ToInt32(tempRoundSettings.FixValue) : 0;

                await CallInvoke();
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task GetCurrentRoundBets()
    {
        long UserId = _icurrentUser.Id;
        if (GameRound == null) return;
        var tempRoundBets = await _igameRoundService.GetBets(UserId, GametypeId, GameRound.Id);
        if (tempRoundBets != null)
        {
            //UserBets = new ObservableCollection<BetModel>(tempRoundBets.OrderByDescending(x => x.BetStatus).ThenBy(x => x.BetValue));

            UserBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderBy(x => x.BetStatus).ThenBy(x => x.BetValue));
            var LoseUserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderBy(x => x.BetValue));
            foreach (var item in LoseUserPrevBets)
            {
                UserBets.Add(item);
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
        var tempRoundBets = await _igameRoundService.GetPrevBets(UserId, GametypeId);
        if (tempRoundBets != null)
        {
            UserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderBy(x => x.BetStatus).ThenBy(x => x.BetValue));
            var LoseUserPrevBets = new ObservableCollection<BetModel>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderBy(x => x.BetValue));
            foreach (var item in LoseUserPrevBets)
            {
                UserPrevBets.Add(item);
            }
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
    public async Task GetGameRound()
    {
        if (GametypeId > 0)
        {
            IsBetEnabled = false;
            LetterValue = string.Empty;
            NumberValue = 0;
            NumberValueInput = string.Empty;

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            IsLuckyPick10 = false;

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
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Closed)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
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
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
                {
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
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


                    //await GetRoundBetSummary();
                }

                await GetTrends();

            }
            await CallInvoke();
        }
    }
    public async Task GetTrends()
    {
        if (GametypeId > 0)
        {

            var tempTrends = await _igameRoundService.GetTrends(GametypeId);
            if (tempTrends != null)
            {
                //Trends = new ObservableCollection<GameRoundModel>(tempTrends.OrderByDescending(x=> x.Id));
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
    public async Task AssignSignalRMethods()
    {
        if (HubConnection != null)
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {

                {

                    HubConnection.Remove(Constants.UpdateGameStatus);
                    HubConnection.Remove(Constants.NotifyGameRoundResult);
                    HubConnection.Remove(Constants.UpdateTrends);

                    HubConnection.On<int, int>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    await CallInvoke();
                }

            }
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

    public async Task NotifyGameRoundResult(UpdateGameResultModel paramsModel)
    {
        if (paramsModel.GameTypeId == GametypeId)
        {
            GameResult = paramsModel.WinningCombination;
            await CallInvoke();
        }
    }
    public async Task UpdateGigaDrawBets(int GameType, List<BetModel> Bets)
    {
        if (GameType == GametypeId)
        {
            if (Bets != null && Bets.Count > 0)
            {
                UserBets = new ObservableCollection<BetModel>(Bets.OrderByDescending(x => x.Id));
                await GetTotalBetsCSS();
                TotalBets = (int)UserBets.Sum(x => Convert.ToDecimal(x.BetAmount));
                tapCounter = 0;
            }

            LetterValue = string.Empty;
            NumberValue = 0;
            NumberValueInput = string.Empty;
            BetAmount = int.Parse(BaseBetValue);
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            IsLuckyPick10 = false;

        }
    }
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus)
    {
        if (gametypeId == GametypeId)
        {
            await GetGameRound();
            if (gameStatus == (int)RoundStatus.Open)
            {
                IsGameOpen = true;
                IsLuckyPickEnabled = true;
                await CallInvoke();
            }
        }

    }
    public async Task ValidateLetterInput()
    {
        if (!string.IsNullOrEmpty(LetterValue) && letterValueHolder != LetterValue)
        {
            string tempValue = string.Empty;
            int validCounter = 0;
            var letterArr = LetterValue.ToCharArray();
            if (letterArr != null)
            {
                if (letterArr.Length > 3)
                {
                    _toastService.ShowError("Bet letters cannot exceed 4 combinations");
                    LetterValue = LetterValue.Substring(0, 3);
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

            if (LetterValue.Length == 3)
            {
                IsNumberValueFocused = true;
                if (!string.IsNullOrEmpty(NumberValueInput))
                {
                    IsBetEnabled = true;

                }
                else
                {

                    await txtbetnumber.FocusAsync();
                }

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
            IsBetEnabled = false;
        }
    }
    private async Task ValidateNumberInput()
    {
        var isValid = Regex.IsMatch(NumberValueInput.ToString(), "^[0-9]+$");
        if (isValid)
        {
            NumberValue = int.Parse(NumberValueInput);
            if (LetterValue.Length == 3 && !string.IsNullOrEmpty(NumberValueInput))
            {
                IsBetEnabled = true;
            }
        }
        else
        {
            NumberValue = 0;
            IsBetEnabled = false;
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

    public async Task<bool> BetOptionSelected(object arg = null)
    {

        bool results = false;

        var creditValue = Convert.ToDecimal(_icurrentUser.Credits);
        if (!IsBetEnabled)
        {
            results = false;
        }
        //var div10 = await IsDivisibleBy10();
        //if (div10)
        //{
        //    _toastService.ShowError("Bet amount should be divisible by 10.");
        //    results = false;
        //}

        if (arg.ToString() == Constants.NormalPick)
        {
            BetOption = string.Concat(LetterValue, NumberValue);
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

                _toastService.ShowSuccess("Bets Confirmed ");
                await UpdateGigaDrawBets(GametypeId, tempBetResult.Bets != null && tempBetResult.Bets.Count > 0 ? tempBetResult.Bets : null);

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
            BetTypeId = (int)NonCardGameBetTypes.LuckyPick;

            if (!IsLuckyPick1)
            {
                IsLuckyPick1 = true;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;

                BetAmount = fixPrize;
            }


        }
        else if (param == Constants.LuckyPickx5)
        {

            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx5;

            if (!IsLuckyPick5)
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = true;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize * 5;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;
            }
        }
        else if (param == Constants.LuckyPickx10)
        {

            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx10;

            if (!IsLuckyPick10)
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick10 = true;
                LetterValue = string.Empty;
                NumberValueInput = "";
                BetAmount = fixPrize * 10;
            }
            else
            {
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;
                IsLuckyPick10 = false;
                LetterValue = string.Empty;
                BetAmount = fixPrize;
            }
        }
        if (IsLuckyPick1 && BetAmount > 0 ||
            IsLuckyPick5 && BetAmount > 0 ||
            IsLuckyPick10 && BetAmount > 0)
        {
            IsBetEnabled = true;
        }
        else
        {
            IsBetEnabled = false;
        }
    }
}

