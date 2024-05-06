using AutoMapper;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Goplay.Main.Login;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;
using Microsoft.AspNetCore.Components.Authorization;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class Lucky4V2ViewModel : BaseViewModel
{

    #region Injected
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region Local Variables & Properties

    #region GAME CONFIGURATION
    private int _baseValue;
    private int _charLimit = 4;
    private int _selectedChipAmount = 0;
    private int _betTypeId = 0;
    private int _screenstate = 0;
    private int selectedMainOption = 0;
    private bool _isEnabled_First2;
    private bool _isEnabled_First3;
    private bool _isEnabled_All4;
    private bool _isGameCancelled;
    private bool _isBettingEnabled;
    private bool _isPlaceBetEnabled;
    private bool _isChipsDisabled;
    public bool _isDuplicateBet;
    private string _l4gif = "";
    private string _winRate;
    private string _gameResult;
    private string _betValueDisplay;
    private string _betAmountDisplay = "0";
    private string _duplicateBetvalue = "You already placed a bet ";
    public bool ShowLetterBetOptions { get; set; } = false;
    public event Action NotifyGameStatus;
    public string CurrView { get; set; } = "Current";


    public int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            RaisePropertyChanged(() => BaseValue);
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
    public int SelectedChipAmount
    {
        get => _selectedChipAmount;
        set
        {
            _selectedChipAmount = value;
            RaisePropertyChanged(() => SelectedChipAmount);
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
    public int Screenstate
    {
        get => _screenstate;
        set
        {
            _screenstate = value;
            RaisePropertyChanged(() => _screenstate);
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
    public bool IsPlaceBetEnabled
    {
        get => _isPlaceBetEnabled;
        set
        {
            _isPlaceBetEnabled = value;
            RaisePropertyChanged(() => IsPlaceBetEnabled);
        }
    }
    public bool IsChipsDisabled
    {
        get => _isChipsDisabled;
        set
        {
            _isChipsDisabled = value;
            RaisePropertyChanged(() => IsChipsDisabled);
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
    public string L4gif
    {
        get => _l4gif;
        set
        {
            _l4gif = value;
            RaisePropertyChanged(() => L4gif);
        }
    }
    public string Winrate
    {
        get => _winRate;
        set
        {
            _winRate = value;
            RaisePropertyChanged(() => Winrate);
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
    public string BetAmountDisplay
    {
        get => _betAmountDisplay;
        set
        {
            _betAmountDisplay = value;
            RaisePropertyChanged(() => BetAmountDisplay);
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
    public string DuplicateBetvalue
    {
        get => _duplicateBetvalue;
        set
        {
            _duplicateBetvalue = value;
            RaisePropertyChanged(() => DuplicateBetvalue);
        }
    }
    public string PreGameResult { get; set; } = "";
    #endregion

    #region USER BETS
    private ObservableCollection<BetModel> _userBets_F2;
    private ObservableCollection<BetModel> _userBets_F3;
    private ObservableCollection<BetModel> _userBets_All4;
    private ObservableCollection<BetModel> _userPrevBets_F2;
    private ObservableCollection<BetModel> _userPrevBets_F3;
    private ObservableCollection<BetModel> _userPrevBets_All4;


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
    #endregion

    #region BETS
    private int _betCount_f2;
    private int _betCount_f3;
    private int _betCount_all4;
    private string _totalBets_F2;
    private string _totalBets_F3;
    private string _totalBets_All4;
    private decimal _totalAccumulatedBets;
    private string _totalAccumulatedBetsString;


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
    #endregion

    #region MULTIPLIER
    private string _betMultiplier;
    private int _multiplier_f2;
    private int _multiplier_f3;
    private int _multiplier_all4;


    public string BetMultiplier
    {
        get => _betMultiplier;
        set
        {
            _betMultiplier = value;
            RaisePropertyChanged(() => BetMultiplier);
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
    #endregion

    #region GAME CHIPS 
    private GameChipModel _gameChips_F2;
    private GameChipModel _gameChips_F3;
    private GameChipModel _gameChips_All4;


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
    #endregion

    #region SAMPLE DISPLAY
    private int _sampleWin_F2;
    private int _sampleWin_F3;
    private int _sampleWin_All4;


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
    #endregion

    #region LETTERS 
    private static string letterValueHolder;
    private string _letterValue = "";

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
    #endregion

    #region RATIO
    private string _ratioDisplay_F2;
    private string _ratioDisplay_F3;
    private string _ratioDisplay_All4;


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
    #endregion

    #region LUCKY PICK
    private bool _isLuckyPick1;
    private bool _isLuckyPick3;
    private bool _isLuckyPick5;


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
    #endregion

    #region TRENDS
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
    #endregion

    #endregion

    #region Lifecycle Methods
    public Lucky4V2ViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IToastService toastService, IAccountService iaccountService, IGameSettingService igameSettingService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider, IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _igameSettingService = igameSettingService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _mapper = mapper;

        StreamId = Constants.StreamIDLucky4;
        GametypeId = (int)GameTypes.Lucky4;
        BetTypeId = (int)NonCardGameBetTypes.Normal;
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
    private void SendBallGamesResult(int gameTypeId, string results)
    {
        if (gameTypeId == GametypeId)
        {
            PreLetterValue = results;
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
                    HubConnection.Remove(Constants.UpdateGameTimer);
                    HubConnection.Remove(Constants.UpdateGameStatus);
                    HubConnection.Remove(Constants.NotifyGameRoundResult);
                    HubConnection.Remove(Constants.UpdateTrends);
                    HubConnection.Remove(Constants.UpdateBallGamesResult);

                    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    HubConnection.On<int, string>(Constants.UpdateBallGamesResult, UpdateBallGamesResult);
                }

            }
        }
        catch (Exception)
        {

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
    protected async Task UpdateGameStatus(int gametypeId, int value, long gameRoundId)
    {
        if (GametypeId == gametypeId)
        {
            //if (gameRoundId != GameRound.Id && value == (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else if (gameRoundId == GameRound.Id && value != GameRound.RoundStatus && value != (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else
            //{
            //    return;
            //}
            await GetGameRound();
            if (value == (int)RoundStatus.Open)
            {
                TickerMessage = "";
                PreGameResult = string.Empty;
                GameResult = string.Empty;
                IsBettingEnabled = true;
                UserBets_F2 = new ObservableCollection<BetModel>();
                UserBets_F3 = new ObservableCollection<BetModel>();
                UserBets_All4 = new ObservableCollection<BetModel>();
                NotifyGameStatus?.Invoke();

                L4gif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else if (value == (int)RoundStatus.PendingResult)
            {
                L4gif = "/img/animation/test-closebet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else
            {
                L4gif = "";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }

            await ResetDefaultBetAmount();
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
        if (GametypeId == gameTypeId)
        {
            await GetTrends();
            await CallInvoke();
        }
    }
    public async void UpdateBallGamesResult(int gameTypeId, string BallValue)
    {
        if (GametypeId == gameTypeId)
        {
            //await Task.Delay(4000);
            PreGameResult = BallValue;
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

                //RatioDisplay_F2 = (BaseValue * 10) + "=" + (SampleWin_F2 * 10).ToString("#,##0");
                //RatioDisplay_F3 = (BaseValue * 10) + "=" + (SampleWin_F3 * 10).ToString("#,##0");
                //RatioDisplay_All4 = (BaseValue * 10) + "=" + (SampleWin_All4 * 10).ToString("#,##0");

                RatioDisplay_F2 = "x" + GameSetting.F2ratio.Value.ToString("#,##0");
                RatioDisplay_F3 = "x" + GameSetting.F3ratio.Value.ToString("#,##0");
                RatioDisplay_All4 = "x" + GameSetting.A4ratio.Value.ToString("#,##0");
                await CallInvoke();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public async Task GetGameVariantChipsByCategory()
    {
        var tempGameVariantChips = await _igameSettingService.GetGameVariantChipsByCategory(GametypeId, _icurrentUser.CategoryId);
        if (tempGameVariantChips == null)
        {
            _toastService.ShowInfo("No game variant chips found.");
            return;
        }
        foreach (var item in tempGameVariantChips)
        {
            if (item.GameVariantId == (int)Lucky4ChipTypes.F2)
            {
                L4Pick2Chips = item;
            }
            if (item.GameVariantId == (int)Lucky4ChipTypes.F3)
            {
                L4Pick3Chips = item;
            }
            if (item.GameVariantId == (int)Lucky4ChipTypes.All4)
            {
                L4All4Chips = item;
            }
        }
        await CallInvoke();
    }
    public async Task GetGameRound()
    {
        IsGameCancelled = false;
        IsBettingEnabled = false;

        if (GametypeId > 0)
        {
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

                IsEnabled_First3 = false;
                IsEnabled_All4 = false;
                BetAmount = 0;
                BetAmountDisplay = string.Empty;
                SelectedChipAmount = 0;
                LetterValue = string.Empty;
                letterValueHolder = string.Empty;
                IsLuckyPick1 = false;
                IsLuckyPick5 = false;

                if (tempRound.GameType != null)
                {
                    if (tempRound.GameType.F2Chip != null)
                    {
                        GameChips_F2 = tempRound.GameType.F2Chip;
                        GameChips = _mapper.Map<GameChipModel>(L4Pick2Chips);
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
                    PreGameResult = string.Empty;
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
                    PreGameResult = string.Empty;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
                {
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    IsBettingEnabled = false;

                    GameResult = string.Empty;
                    PreGameResult = string.Empty;
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
                await GetTrends();
            }
        }
        await CallInvoke();
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
                    if (tempBet.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_f2).ToString("#,##0");
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
                    if (tempBet.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_f3).ToString("#,##0");
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
                    if (tempBet.BetAmount != null)
                    {
                        tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_all4).ToString("#,##0");
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
        await CollateToUserBets();
        await CallInvoke();


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
    #endregion

    #region Bet Methods
    public async Task ResetDefaultBetAmount()
    {
        IsChipsDisabled = true;
        BetAmount = 0;
        SelectedChipAmount = 0;
        IsPlaceBetEnabled = false;
        IsBettingEnabled = true;
        BetMultiplier = "";
        IsLuckyPick1 = false;
        IsLuckyPick5 = false;
        Winrate = "";
    }
    public async Task AddCharacter(string param)
    {
        if (param != null)
        {
            if (LetterValue.Length < CharLimit)
            {
                LetterValue = LetterValue + param.ToString();
                IsBettingEnabled = true;
            }

            await ResetDefaultBetAmount();

            if (LetterValue.Length == 4)
            {
                IsBettingEnabled = false;
            }

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            IsLuckyPick3 = false;
            BetTypeId = (int)NonCardGameBetTypes.Normal;

            // Set Bet Type
            switch (LetterValue.Length)
            {
                case 2:
                    await BetOptionSelected("F2");
                    break;
                case 3:
                    await BetOptionSelected("F3");
                    break;
                case 4:
                    await BetOptionSelected("ALL4");
                    break;
                default:
                    break;
            }
        }
        await CallInvoke();
    }
    public async Task RemoveLastCharacter()
    {
        await ResetDefaultBetAmount();

        if (!string.IsNullOrEmpty(LetterValue))
        {
            letterValueHolder = LetterValue;
            letterValueHolder = LetterValue.Substring(0, LetterValue.Length - 1);
            LetterValue = letterValueHolder;

            // Set Bet Type
            switch (LetterValue.Length)
            {
                case 2:
                    await BetOptionSelected("F2");
                    break;
                case 3:
                    await BetOptionSelected("F3");
                    break;
                case 4:
                    await BetOptionSelected("ALL4");
                    break;
                default:
                    IsEnabled_First2 = false;
                    IsEnabled_First3 = false;
                    IsEnabled_All4 = false;
                    break;
            }
            await CallInvoke();
        }
    }
    public async Task BetOptionSelected(object arg)
    {
        IsChipsDisabled = false;

        GameChipModel chipmodel = new GameChipModel();
        selectedMainOption = 0;
        Winrate = "";
        if (arg.ToString() == "F2")
        {
            selectedMainOption = (int)Lucky4MainBetOption.F2;
            //chipmodel = GameChips_F2;
            //GameChips = GameChips_F2;
            chipmodel = _mapper.Map<GameChipModel>(L4Pick2Chips);
            GameChips = _mapper.Map<GameChipModel>(L4Pick2Chips);

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            IsEnabled_First2 = true;
            IsEnabled_First3 = false;
            IsEnabled_All4 = false;
        }
        else if (arg.ToString() == "F3")
        {
            selectedMainOption = (int)Lucky4MainBetOption.F3;
            chipmodel = _mapper.Map<GameChipModel>(L4Pick3Chips);
            GameChips = _mapper.Map<GameChipModel>(L4Pick3Chips);

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;

            IsEnabled_First2 = false;
            IsEnabled_First3 = true;
            IsEnabled_All4 = false;
        }
        else if (arg.ToString() == "ALL4")
        {
            selectedMainOption = (int)Lucky4MainBetOption.All4;
            chipmodel = _mapper.Map<GameChipModel>(L4All4Chips);
            GameChips = _mapper.Map<GameChipModel>(L4All4Chips);

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;

            IsEnabled_First2 = false;
            IsEnabled_First3 = false;
            IsEnabled_All4 = true;
        }
        SetWinrateDisplay();
        await CallInvoke();
        await ResetDefaultAmount();
    }
    public async Task SetBetAmount(object value, string multiplier)
    {
        IsPlaceBetEnabled = true;
        BetMultiplier = multiplier;
        SelectedChipAmount = (int)value;
        BetAmount = (int)value;
        BetAmountDisplay = SelectedChipAmount.ToString("N0");

        if (!string.IsNullOrEmpty(LetterValue))
        {
            if (selectedMainOption > 0 && SelectedChipAmount > 0 && LetterValue.Length > 1)
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
            else if (IsLuckyPick3 && BetAmount > 0)
            {
                BetAmount = (int)value * 3;
                BetAmountDisplay = BetAmount.ToString("N0");
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
        SetWinrateDisplay();
        await CallInvoke();
    }
    public async Task<bool> IsDivisibleBy10()
    {
        bool isvalid = false;
        if (BetAmount > 0)
        {
            var result = BetAmount % 5;
            if (result > 0)
            {
                _toastService.ShowError("Bet amount should be divisible by 5.");
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
        else if (IsLuckyPick3)
        {
            BetValueDisplay = Constants.LuckyPickx3;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx3;
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
            else if (BetTypeId == (int)NonCardGameBetTypes.LuckyPickx3)
            {
                betParams.LuckyPickX3 = true;
                betParams.BetAmount = BetAmount / 3;
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
            }

            await ValidateUser();
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
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
                if (tempBet.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_f2).ToString("#,##0");
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
                if (tempBet.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_f3).ToString("#,##0");
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
                if (tempBet.BetAmount != null)
                {
                    tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * Multiplier_all4).ToString("#,##0");
                }
                tempBetList.Add(tempBet);
            }
            UserBets_All4 = tempBetList;
            TotalBets_All4 = tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount)).ToString("#,##0");
            TotalAccumulatedBets = TotalAccumulatedBets + tempBetList.Sum(x => Convert.ToDecimal(x.BetAmount));
        }

        TotalAccumulatedBetsString = TotalAccumulatedBets.ToString("N0");

        await ResetDefaultBetAmount();

        LetterValue = string.Empty;
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
        await CollateToUserBets();
    }
    private async Task CollateToUserBets()
    {
        UserBets = new();
        foreach (BetModel bet in UserBets_F2)
        {
            UserBets.Add(bet);
        }
        foreach (BetModel bet in UserBets_F3)
        {
            UserBets.Add(bet);
        }
        foreach (BetModel bet in UserBets_All4)
        {
            UserBets.Add(bet);
        }
    }
    private async Task SetWinrateDisplay()
    {
        if (selectedMainOption == (int)Lucky4MainBetOption.F2)
        {

            Winrate = SelectedChipAmount + " = " + string.Format("{0:0,0}", SampleWin_F2 * SelectedChipAmount);

        }
        else if (selectedMainOption == (int)Lucky4MainBetOption.F3)
        {

            Winrate = SelectedChipAmount + " = " + string.Format("{0:0,0}", SampleWin_F3 * SelectedChipAmount);


        }
        else if (selectedMainOption == (int)Lucky4MainBetOption.All4)
        {
            Winrate = SelectedChipAmount + " = " + string.Format("{0:0,0}", SampleWin_All4 * SelectedChipAmount);
        }
    }
    public async Task ResetDefaultAmount()
    {
        if (GameChips != null)
        {
            BetAmount = int.Parse(GameChips.Chip1.ToString());
            BetAmountDisplay = GameChips.Chip1.ToString();
            BetMultiplier = GameChips.Chip1Display.ToString();
            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            Winrate = "";
        }
    }
    #endregion

    #region UNUSED
    private async Task CollateToPrevUserBets()
    {
        PreviousBets = new();
        foreach (BetModel bet in UserPrevBets_F2)
        {
            PreviousBets.Add(bet);
        }
        foreach (BetModel bet in UserPrevBets_F3)
        {
            PreviousBets.Add(bet);
        }
        foreach (BetModel bet in UserPrevBets_All4)
        {
            PreviousBets.Add(bet);
        }
    }
    public async Task GetPreviousBets()
    {
        long UserId = _icurrentUser.Id;
        var tempRoundBets = await _igameRoundService.GetPrevGameBets(UserId, GametypeId, 100);
        if (tempRoundBets != null)
        {
            PreviousBets = new ObservableCollection<BetModel>(tempRoundBets);
            CallInvoke();
        }
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
        await CollateToPrevUserBets();
        await CallInvoke();
    }
    public async Task SetGameTypeForLuckyPick(object arg)
    {
        GameChipModel chipmodel = new GameChipModel();

        if (arg.ToString() == "F2")
        {
            selectedMainOption = (int)Lucky4MainBetOption.F2;
            chipmodel = GameChips_F2;
            GameChips = GameChips_F2;

        }
        else if (arg.ToString() == "F3")
        {
            selectedMainOption = (int)Lucky4MainBetOption.F3;
            chipmodel = GameChips_F3;
            GameChips = GameChips_F3;

        }
        else if (arg.ToString() == "ALL4")
        {
            selectedMainOption = (int)Lucky4MainBetOption.All4;
            chipmodel = GameChips_All4;

        }
        LetterValue = string.Empty;
        await CallInvoke();
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

            IsLuckyPick3 = false;
            IsLuckyPick5 = false;
            LetterValue = string.Empty;
            BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount : 0;
            BetAmountDisplay = BetAmount.ToString("N0");

        }
        else if (param == Constants.LuckyPickx3)
        {
            BetValueDisplay = Constants.LuckyPickx3;
            BetTypeId = (int)NonCardGameBetTypes.LuckyPickx3;

            if (!IsLuckyPick3)
            {
                IsLuckyPick3 = true;
                BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount * 3 : 0;
                BetAmountDisplay = BetAmount.ToString("N0");
            }
            else
            {
                IsLuckyPick3 = false;
                BetAmount = SelectedChipAmount > 0 ? SelectedChipAmount : 0;
                BetAmountDisplay = BetAmount.ToString("N0");
            }

            IsLuckyPick1 = false;
            IsLuckyPick5 = false;
            LetterValue = string.Empty;
        }
        else if (param == Constants.LuckyPickx5)
        {
            BetValueDisplay = Constants.LuckyPickx5;
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
            IsLuckyPick3 = false;
            LetterValue = string.Empty;
        }

        if (IsLuckyPick1 && BetAmount > 0 ||
            IsLuckyPick5 && BetAmount > 0 ||
            IsLuckyPick3 && BetAmount > 0)
        {
            IsPlaceBetEnabled = true;
        }
        else
        {
            IsPlaceBetEnabled = false;
        }
    }
    #endregion

}
