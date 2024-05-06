using AutoMapper;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.ViewModels;

public class DiceViewModel : BaseViewModel
{
    #region INJECTED
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region LOCAL VARIABLES & PROPERTIES

    #region GAME CONFIGURATION
    private int _allDiceLimit = 3;
    public string tokenDiv = "tokenhide";
    public string _gameGif = "";
    public string _tokenAnimation = "";
    public string _cssSingleColorNumberBtn = "";
    public string _cssSingleColorNumberBtnDiv = "hide";
    private bool _isBettingButtonDisabled = false;

    public int AllDice_Limit
    {
        get => _allDiceLimit;
        set
        {
            _allDiceLimit = value;
            RaisePropertyChanged(() => AllDice_Limit);
        }
    }
    public string Game_Gif
    {
        get => _gameGif;
        set
        {
            _gameGif = value;
            RaisePropertyChanged(() => Game_Gif);
        }
    }
    public string Token_Animation
    {
        get => _tokenAnimation;
        set
        {
            _tokenAnimation = value;
            RaisePropertyChanged(() => Token_Animation);
        }
    }
    public string CSS_SingleColor_NumberBtn
    {
        get => _cssSingleColorNumberBtn;
        set
        {
            _cssSingleColorNumberBtn = value;
            RaisePropertyChanged(() => CSS_SingleColor_NumberBtn);
        }
    }
    public string CSS_SingleColor_NumberBtn_Div
    {
        get => _cssSingleColorNumberBtnDiv;
        set
        {
            _cssSingleColorNumberBtnDiv = value;
            RaisePropertyChanged(() => CSS_SingleColor_NumberBtn_Div);
        }
    }
    public bool IsBetting_Button_Disabled
    {
        get => _isBettingButtonDisabled;
        set
        {
            _isBettingButtonDisabled = value;
            RaisePropertyChanged(() => IsBetting_Button_Disabled);
        }
    }
    #endregion

    #region MULTIPLIER
    private decimal _allDiceMultiplier;
    private decimal _singleDiceMultiplier;
    private decimal _smallBigMultiplier;
    private decimal _oddEvenMultiplier;
    private decimal _numberMultiplier;
    private string _allDiceMultiplierDisplay;
    private string _singleDiceMultiplierDisplay;
    private string _smallBigMultiplierDisplay;
    private string _oddEvenMultiplierDisplay;
    private string _numberMultiplierDisplay;

    public decimal AllDice_Multiplier
    {
        get => _allDiceMultiplier;
        set
        {
            _allDiceMultiplier = value;
            RaisePropertyChanged(() => AllDice_Multiplier);
        }
    }
    public decimal SingleDice_Multiplier
    {
        get => _singleDiceMultiplier;
        set
        {
            _singleDiceMultiplier = value;
            RaisePropertyChanged(() => SingleDice_Multiplier);
        }
    }
    public decimal SmallBig_Multiplier
    {
        get => _smallBigMultiplier;
        set
        {
            _smallBigMultiplier = value;
            RaisePropertyChanged(() => SmallBig_Multiplier);
        }
    }
    public decimal OddEven_Multiplier
    {
        get => _oddEvenMultiplier;
        set
        {
            _oddEvenMultiplier = value;
            RaisePropertyChanged(() => OddEven_Multiplier);
        }
    }
    public decimal Number_Multiplier
    {
        get => _numberMultiplier;
        set
        {
            _numberMultiplier = value;
            RaisePropertyChanged(() => Number_Multiplier);
        }
    }
    public string AllDice_Multiplier_Display
    {
        get => _allDiceMultiplierDisplay;
        set
        {
            _allDiceMultiplierDisplay = value;
            RaisePropertyChanged(() => AllDice_Multiplier_Display);
        }
    }
    public string SingleDice_Multiplier_Display
    {
        get => _singleDiceMultiplierDisplay;
        set
        {
            _singleDiceMultiplierDisplay = value;
            RaisePropertyChanged(() => SingleDice_Multiplier_Display);
        }
    }
    public string SmallBig_Multiplier_Display
    {
        get => _smallBigMultiplierDisplay;
        set
        {
            _smallBigMultiplierDisplay = value;
            RaisePropertyChanged(() => SmallBig_Multiplier_Display);
        }
    }
    public string OddEven_Multiplier_Display
    {
        get => _oddEvenMultiplierDisplay;
        set
        {
            _oddEvenMultiplierDisplay = value;
            RaisePropertyChanged(() => OddEven_Multiplier_Display);
        }
    }
    public string Number_Multiplier_Display
    {
        get => _numberMultiplierDisplay;
        set
        {
            _numberMultiplierDisplay = value;
            RaisePropertyChanged(() => Number_Multiplier_Display);
        }
    }
    #endregion

    #region TOTAL BETS
    private string _allDiceTotalBets;
    private string _diceOddTotalBets;
    private string _diceEvenTotalBets;
    private string _diceSmallTotalBets;
    private string _diceBigTotalBets;
    private string _firstDiceTotalBets;
    private string _secondDiceTotalBets;
    private string _thirdDiceTotalBets;
    private string _diceNumberTotalBets;

    public string AllDice_Total_Bets
    {
        get => _allDiceTotalBets;
        set
        {
            _allDiceTotalBets = value;
            RaisePropertyChanged(() => AllDice_Total_Bets);
        }
    }
    public string DiceOdd_Total_Bets
    {
        get => _diceOddTotalBets;
        set
        {
            _diceOddTotalBets = value;
            RaisePropertyChanged(() => DiceOdd_Total_Bets);
        }
    }
    public string DiceEven_Total_Bets
    {
        get => _diceEvenTotalBets;
        set
        {
            _diceEvenTotalBets = value;
            RaisePropertyChanged(() => DiceEven_Total_Bets);
        }
    }
    public string DiceSmall_Total_Bets
    {
        get => _diceSmallTotalBets;
        set
        {
            _diceSmallTotalBets = value;
            RaisePropertyChanged(() => DiceSmall_Total_Bets);
        }
    }
    public string DiceBig_Total_Bets
    {
        get => _diceBigTotalBets;
        set
        {
            _diceBigTotalBets = value;
            RaisePropertyChanged(() => DiceBig_Total_Bets);
        }
    }
    public string FirstDice_Total_Bets
    {
        get => _firstDiceTotalBets;
        set
        {
            _firstDiceTotalBets = value;
            RaisePropertyChanged(() => FirstDice_Total_Bets);
        }
    }
    public string SecondDice_Total_Bets
    {
        get => _secondDiceTotalBets;
        set
        {
            _secondDiceTotalBets = value;
            RaisePropertyChanged(() => SecondDice_Total_Bets);
        }
    }
    public string ThirdDice_Total_Bets
    {
        get => _thirdDiceTotalBets;
        set
        {
            _thirdDiceTotalBets = value;
            RaisePropertyChanged(() => ThirdDice_Total_Bets);
        }
    }
    public string Dice_Number_Total_Bets
    {
        get => _diceNumberTotalBets;
        set
        {
            _diceNumberTotalBets = value;
            RaisePropertyChanged(() => Dice_Number_Total_Bets);
        }
    }
    #endregion

    #region USER TOTAL BETS
    private string _userAllDiceTotalBers;
    private string _userDiceOddTotalBets;
    private string _userDiceEvenTotalBets;
    private string _userDiceSmallTotalBets;
    private string _userDiceBigTotalBets;
    private string _userFirstDiceTotalBets;
    private string _userSecondDiceTotalBets;
    private string _userThirdDiceTotalBets;
    private string _userNumber1TotalBets;
    private string _userNumber2TotalBets;
    private string _userNumber3TotalBets;
    private string _userNumber4TotalBets;
    private string _userNumber5TotalBets;
    private string _userNumber6TotalBets;

    public string User_AllDice_Total_Bets
    {
        get => _userAllDiceTotalBers;
        set
        {
            _userAllDiceTotalBers = value;
            RaisePropertyChanged(() => User_AllDice_Total_Bets);
        }
    }
    public string User_DiceOdd_Total_Bets
    {
        get => _userDiceOddTotalBets;
        set
        {
            _userDiceOddTotalBets = value;
            RaisePropertyChanged(() => User_DiceOdd_Total_Bets);
        }
    }
    public string User_DiceEven_Total_Bets
    {
        get => _userDiceEvenTotalBets;
        set
        {
            _userDiceEvenTotalBets = value;
            RaisePropertyChanged(() => User_DiceEven_Total_Bets);
        }
    }
    public string User_DiceSmall_Total_Bets
    {
        get => _userDiceSmallTotalBets;
        set
        {
            _userDiceSmallTotalBets = value;
            RaisePropertyChanged(() => User_DiceSmall_Total_Bets);
        }
    }
    public string User_DiceBig_Total_Bets
    {
        get => _userDiceBigTotalBets;
        set
        {
            _userDiceBigTotalBets = value;
            RaisePropertyChanged(() => User_DiceBig_Total_Bets);
        }
    }
    public string User_FirstDice_Total_Bets
    {
        get => _userFirstDiceTotalBets;
        set
        {
            _userFirstDiceTotalBets = value;
            RaisePropertyChanged(() => User_FirstDice_Total_Bets);
        }
    }
    public string User_SecondDice_Total_Bets
    {
        get => _userSecondDiceTotalBets;
        set
        {
            _userSecondDiceTotalBets = value;
            RaisePropertyChanged(() => User_SecondDice_Total_Bets);
        }
    }
    public string User_ThirdDice_Total_Bets
    {
        get => _userThirdDiceTotalBets;
        set
        {
            _userThirdDiceTotalBets = value;
            RaisePropertyChanged(() => User_ThirdDice_Total_Bets);
        }
    }
    public string User_Number1_Total_Bets
    {
        get => _userNumber1TotalBets;
        set
        {
            _userNumber1TotalBets = value;
            RaisePropertyChanged(() => User_Number1_Total_Bets);
        }
    }
    public string User_Number2_Total_Bets
    {
        get => _userNumber2TotalBets;
        set
        {
            _userNumber2TotalBets = value;
            RaisePropertyChanged(() => User_Number2_Total_Bets);
        }
    }
    public string User_Number3_Total_Bets
    {
        get => _userNumber3TotalBets;
        set
        {
            _userNumber3TotalBets = value;
            RaisePropertyChanged(() => User_Number3_Total_Bets);
        }
    }
    public string User_Number4_Total_Bets
    {
        get => _userNumber4TotalBets;
        set
        {
            _userNumber4TotalBets = value;
            RaisePropertyChanged(() => User_Number4_Total_Bets);
        }
    }
    public string User_Number5_Total_Bets
    {
        get => _userNumber5TotalBets;
        set
        {
            _userNumber5TotalBets = value;
            RaisePropertyChanged(() => User_Number5_Total_Bets);
        }
    }
    public string User_Number6_Total_Bets
    {
        get => _userNumber6TotalBets;
        set
        {
            _userNumber6TotalBets = value;
            RaisePropertyChanged(() => User_Number6_Total_Bets);
        }
    }
    #endregion

    #region SAMPLER WINNABLE AMOUNT
    private string _sampleWinAllDice;
    private string _sampleWinDiceSmall;
    private string _sampleWinDiceBig;
    private string _sampleWinDiceOdd;
    private string _sampleWinDiceEven;
    private string _sampleWinFirstDice;
    private string _sampleWinSecondDice;
    private string _sampleWinThirdDice;

    public string Sample_Win_AllDice
    {
        get => _sampleWinAllDice;
        set
        {
            _sampleWinAllDice = value;
            RaisePropertyChanged(() => Sample_Win_AllDice);
        }
    }
    public string Sample_Win_DiceSmall
    {
        get => _sampleWinDiceSmall;
        set
        {
            _sampleWinDiceSmall = value;
            RaisePropertyChanged(() => Sample_Win_DiceSmall);
        }
    }
    public string Sample_Win_DiceBig
    {
        get => _sampleWinDiceBig;
        set
        {
            _sampleWinDiceBig = value;
            RaisePropertyChanged(() => Sample_Win_DiceBig);
        }
    }
    public string Sample_Win_DiceOdd
    {
        get => _sampleWinDiceOdd;
        set
        {
            _sampleWinDiceOdd = value;
            RaisePropertyChanged(() => Sample_Win_DiceOdd);
        }
    }
    public string Sample_Win_DiceEven
    {
        get => _sampleWinDiceEven;
        set
        {
            _sampleWinDiceEven = value;
            RaisePropertyChanged(() => Sample_Win_DiceEven);
        }
    }
    public string Sample_Win_FirstDice
    {
        get => _sampleWinFirstDice;
        set
        {
            _sampleWinFirstDice = value;
            RaisePropertyChanged(() => Sample_Win_FirstDice);
        }
    }
    public string Sample_Win_SecondDice
    {
        get => _sampleWinSecondDice;
        set
        {
            _sampleWinSecondDice = value;
            RaisePropertyChanged(() => Sample_Win_SecondDice);
        }
    }
    public string Sample_Win_ThirdDice
    {
        get => _sampleWinThirdDice;
        set
        {
            _sampleWinThirdDice = value;
            RaisePropertyChanged(() => Sample_Win_ThirdDice);
        }
    }
    #endregion

    #region TRENDS
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<DiceGameRoundModel> CurrentList { get; set; }
    }
    private ObservableCollection<DiceGameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _trendsDisplay;


    public ObservableCollection<DiceGameRoundModel> Trends
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

    #region RESULT
    private string _firstDiceResult;
    private string _secondDiceResult;
    private string _thirdDiceResult;

    private string _prefirstDiceResult;
    private string _presecondDiceResult;
    private string _prethirdDiceResult;

    private string _oddEvenResult;
    private string _smallBigResult;
    private string _winningResult = "";

    public string PreFirst_Dice_Result
    {
        get => _prefirstDiceResult;
        set
        {
            _prefirstDiceResult = value;
            RaisePropertyChanged(() => PreFirst_Dice_Result);
        }
    }
    public string PreSecond_Dice_Result
    {
        get => _presecondDiceResult;
        set
        {
            _presecondDiceResult = value;
            RaisePropertyChanged(() => First_Dice_Result);
        }
    }
    public string PreThird_Dice_Result
    {
        get => _prethirdDiceResult;
        set
        {
            _prethirdDiceResult = value;
            RaisePropertyChanged(() => PreThird_Dice_Result);
        }
    }
    public string First_Dice_Result
    {
        get => _firstDiceResult;
        set
        {
            _firstDiceResult = value;
            RaisePropertyChanged(() => First_Dice_Result);
        }
    }
    public string Second_Dice_Result
    {
        get => _secondDiceResult;
        set
        {
            _secondDiceResult = value;
            RaisePropertyChanged(() => Second_Dice_Result);
        }
    }
    public string Third_Dice_Result
    {
        get => _thirdDiceResult;
        set
        {
            _thirdDiceResult = value;
            RaisePropertyChanged(() => Third_Dice_Result);
        }
    }
    public string OddEven_Dice_Result
    {
        get => _oddEvenResult;
        set
        {
            _oddEvenResult = value;
            RaisePropertyChanged(() => OddEven_Dice_Result);
        }
    }
    public string SmallBig_Dice_Result
    {
        get => _smallBigResult;
        set
        {
            _smallBigResult = value;
            RaisePropertyChanged(() => SmallBig_Dice_Result);
        }
    }
    public string Winning_Result
    {
        get => _winningResult;
        set
        {
            _winningResult = value;
            RaisePropertyChanged(() => Winning_Result);
        }
    }
    #endregion

    #region BET
    private int _currentGameType = 0;
    private int _firstDiceBetCount;
    private int _secondDiceBetCount;
    private int _thirdDiceBetCount;
    private int _allDiceBetCount;
    private ObservableCollection<DiceBetModel> _userFirstDiceBets;
    private ObservableCollection<DiceBetModel> _userSecondDiceBets;
    private ObservableCollection<DiceBetModel> _userThirdDiceBets;
    private ObservableCollection<DiceBetModel> _userAllDiceBets;
    private ObservableCollection<DiceBetModel> _userPerDiceBets;

    public int CurrentGameType
    {
        get => _currentGameType;
        set
        {
            _currentGameType = value;
            RaisePropertyChanged(() => CurrentGameType);
        }
    }
    public int FirstDice_BetCount
    {
        get => _firstDiceBetCount;
        set
        {
            _firstDiceBetCount = value;
            RaisePropertyChanged(() => FirstDice_BetCount);
        }
    }
    public int SecondDice_BetCount
    {
        get => _secondDiceBetCount;
        set
        {
            _secondDiceBetCount = value;
            RaisePropertyChanged(() => SecondDice_BetCount);
        }
    }
    public int ThirdDice_BetCount
    {
        get => _thirdDiceBetCount;
        set
        {
            _thirdDiceBetCount = value;
            RaisePropertyChanged(() => ThirdDice_BetCount);
        }
    }
    public int AllDice_BetCount
    {
        get => _allDiceBetCount;
        set
        {
            _allDiceBetCount = value;
            RaisePropertyChanged(() => AllDice_BetCount);
        }
    }
    public ObservableCollection<DiceBetModel> User_FirstDice_Bets
    {
        get => _userFirstDiceBets;
        set
        {
            _userFirstDiceBets = value;
            RaisePropertyChanged(() => User_FirstDice_Bets);
            FirstDice_BetCount = User_FirstDice_Bets.Count;
        }
    }
    public ObservableCollection<DiceBetModel> User_SecondDice_Bets
    {
        get => _userSecondDiceBets;
        set
        {
            _userSecondDiceBets = value;
            RaisePropertyChanged(() => User_SecondDice_Bets);
            SecondDice_BetCount = User_SecondDice_Bets.Count;
        }
    }
    public ObservableCollection<DiceBetModel> User_ThirdDice_Bets
    {
        get => _userThirdDiceBets;
        set
        {
            _userThirdDiceBets = value;
            RaisePropertyChanged(() => User_ThirdDice_Bets);
            ThirdDice_BetCount = User_ThirdDice_Bets.Count;
        }
    }
    public ObservableCollection<DiceBetModel> User_AllDice_Bets
    {
        get => _userAllDiceBets;
        set
        {
            _userAllDiceBets = value;
            RaisePropertyChanged(() => User_AllDice_Bets);
            AllDice_BetCount = User_AllDice_Bets.Count;
        }
    }
    public ObservableCollection<DiceBetModel> User_PerDice_Bets
    {
        get => _userPerDiceBets;
        set
        {
            _userPerDiceBets = value;
            RaisePropertyChanged(() => User_PerDice_Bets);
        }
    }
    #endregion

    #endregion

    #region LIFE CYCLE METHODS
    public DiceViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IDiceGameRoundService iDiceGameRoundService, IGameSettingService igameSettingsService, IToastService toastService, IAccountService iaccountService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider, IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _iDiceGameRoundService = iDiceGameRoundService;
        _igameSettingService = igameSettingsService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _mapper = mapper;

        StreamId = Constants.StreamIDDice;
        GametypeId = (int)GameTypes.Dice;
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
    public async void UpdateBallGamesResult(int gameTypeId, string BallValue)
    {
        if (GametypeId == gameTypeId)
        {
            if (BallValue.Contains('-'))
            {
                string[] parts = BallValue.Split('-');
                _prefirstDiceResult = parts[0];
                _presecondDiceResult = parts[1];
                _prethirdDiceResult = parts[2];
            }

            await CallInvoke();

        }
    }
    #endregion

    #region SIGNAL R METHODS
    public async Task AssignSignalRMethods()
    {
        try
        {
            if (HubConnection != null)
            {
                if (HubConnection.State == HubConnectionState.Connected)
                {
                    HubConnection.Remove(Constants.UpdateGameTimer);
                    HubConnection.Remove(Constants.UpdateEnableOperatorInput);
                    HubConnection.Remove(Constants.UpdateEnableValidatorInput);
                    HubConnection.Remove(Constants.NotifyGameRoundResult);
                    HubConnection.Remove(Constants.UpdateEnableOpenButton);
                    HubConnection.Remove(Constants.UpdateTrends);


                    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<string, string>(Constants.UpdateNotificationBadge, UpdateNotificationBadge);
                    HubConnection.On<string, int>(Constants.UpdateNotificationBadgeCount, UpdateNotificationBadgeCount);
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
            if (GametypeId == gametypeId && DiceGameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                if (DiceGameRound.RoundStatus == (int)RoundStatus.Closed && AwaitingGameRound == false)
                {
                    AwaitingGameRound = true;
                    //await GetGameRound();
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
        if (GametypeId == gametypeId)
        {
            await GetGameRound();
            if (gameStatus != (int)RoundStatus.Open)
            {
                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    Game_Gif = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = "";
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    Game_Gif = "/img/animation/test-closebet.gif";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                }

                BetAmount = 0;
                Payout = 0;
            }
            else if (gameStatus == (int)RoundStatus.Open)
            {
                TotalBets = 0;

                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;

                Game_Gif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
        }
    }
    public async Task UpdateTrends(int _gameTypeId)
    {
        if (_gameTypeId == GametypeId)
        {
            await GetTrends();
            await CallInvoke();
        }
    }
    public async Task GetTrends()
    {
        var tempTrends = await _iDiceGameRoundService.DiceGetTrends();
        if (tempTrends != null)
        {
            await SetTrendsDisplay(tempTrends);
            await CallInvoke();
        }
    }
    public async Task SetTrendsDisplay(List<DiceGameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<DiceGameRoundModel> CurrentList = new ObservableCollection<DiceGameRoundModel>();
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
                        Current.CurrentList = new ObservableCollection<DiceGameRoundModel>(CurrentList.OrderByDescending(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<DiceGameRoundModel>();

                        rowCounter = 0;
                        counter = 0;
                    }
                }
                else if (rowCounter == 5)
                {
                    CurrentList.Add(t);
                    Current = new TrendsDisplayModel();
                    Current.CurrentList = new ObservableCollection<DiceGameRoundModel>(CurrentList.OrderByDescending(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<DiceGameRoundModel>();

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

    #region GAME METHODS
    public async Task ShowDivToken()
    {
        tokenDiv = "block";
        await CallInvoke();
    }
    public async Task HideDivToken()
    {
        tokenDiv = "tokenhide";
        await CallInvoke();
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
                AllDice_Multiplier = GameSetting.FixedPriceMultiplier != null ? GameSetting.FixedPriceMultiplier.Value : 0;
                AllDice_Multiplier_Display = GameSetting.FixedPriceMultiplier != null ? "x" + Convert.ToInt32(GameSetting.FixedPriceMultiplier).ToString() : "x";
                MinBet = GameSetting.MinimumBet != null ? GameSetting.MinimumBet.Value.ToString("#,##0") : "0";
                MaxBet = GameSetting.MaximumBet != null ? GameSetting.MaximumBet.Value.ToString("#,##0") : "0";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public async Task GetGameSettingVariant()
    {
        try
        {
            var tempVariant = await _igameSettingService.GetGameSettingsVariant(GametypeId);
            if (tempVariant == null)
            {
                _toastService.ShowInfo("No game variant found.");
            }
            else if (tempVariant != null)
            {
                foreach (var item in tempVariant)
                {
                    if (item.GameVariantID == (int)GameVariant.DiceOddEven)
                    {
                        DiceOddEven = item;
                        OddEven_Multiplier = item.FixedPriceMultiplier;
                        OddEven_Multiplier_Display = (item.FixedPriceMultiplier * 100).ToString("N0") + "%";
                    }
                    if (item.GameVariantID == (int)GameVariant.DiceSmallBig)
                    {
                        DiceSmallBig = item;
                        SmallBig_Multiplier = item.FixedPriceMultiplier;
                        SmallBig_Multiplier_Display = (item.FixedPriceMultiplier * 100).ToString("N0") + "%";
                    }
                    if (item.GameVariantID == (int)GameVariant.DiceSingle)
                    {
                        DiceSingle = item;
                        SingleDice_Multiplier = item.FixedPriceMultiplier;
                        SingleDice_Multiplier_Display = "x" + Convert.ToInt32(item.FixedPriceMultiplier).ToString();
                    }
                    if (item.GameVariantID == (int)GameVariant.DiceNumber)
                    {
                        DiceNumber = item;
                        Number_Multiplier = item.FixedPriceMultiplier;
                        Number_Multiplier_Display = "x" + Convert.ToDecimal(item.FixedPriceMultiplier).ToString();
                    }
                }
            }
            await CallInvoke();
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

        List<int> chipsList = new List<int>();
        foreach (var item in tempGameVariantChips)
        {
            if (item.GameVariantId == (int)GameVariant.DiceOddEven)
            {
                DiceOddEvenChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.DiceSmallBig)
            {
                DiceSmallBigChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.DiceSingle)
            {
                DiceSingleChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.DiceNumber)
            {
                DiceNumberChips = item;
                chipsList.Add(item.Chip1);
            }
        }
        MinChip = chipsList.Min();

        await CallInvoke();
    }
    public async Task GetGameRound()
    {
        Winning_Result = string.Empty;
        DiceGameRound = await _iDiceGameRoundService.DiceGetRound(GametypeId);
        if (DiceGameRound == null)
        {
            _toastService.ShowError("No round details found.");
            await GetGameSetting();
            return;
        }
        else if (DiceGameRound != null)
        {
            if (DiceGameRound.RoundNumber > 0)
            {
                RoundNumber = DiceGameRound.RoundNumber;
            }

            AllDice_Total_Bets = DiceGameRound.AllDiceBet;
            DiceOdd_Total_Bets = DiceGameRound.OddBet;
            DiceEven_Total_Bets = DiceGameRound.EvenBet;
            DiceSmall_Total_Bets = DiceGameRound.SmallBet;
            DiceBig_Total_Bets = DiceGameRound.BigBet;
            FirstDice_Total_Bets = DiceGameRound.FirstDiceBet;
            SecondDice_Total_Bets = DiceGameRound.SecondDiceBet;
            ThirdDice_Total_Bets = DiceGameRound.ThirdDiceBet;
            Dice_Number_Total_Bets = DiceGameRound.NumberBet;

            CSS_SingleColor_NumberBtn_Div = "hide";

            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            GameResultString = DiceGameRound.WinningResult;
            switch (DiceGameRound.RoundStatus)
            {
                case (int)RoundStatus.Open:
                    GameResult = new UpdateGameResultModel();
                    GameResultString = null;
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;

                    First_Dice_Result = DiceGameRound.FirstDiceResult;
                    Second_Dice_Result = DiceGameRound.SecondDiceResult;
                    Third_Dice_Result = DiceGameRound.ThirdDiceResult;
                    OddEven_Dice_Result = DiceGameRound.OddEvenResult;
                    SmallBig_Dice_Result = DiceGameRound.SmallBigResult;
                    _prefirstDiceResult = string.Empty;
                    _presecondDiceResult = string.Empty;
                    _prethirdDiceResult = string.Empty;

                    Winning_Result = string.Empty;
                    break;

                case (int)RoundStatus.Closed:
                    RoundTimer = ""; // 00:00

                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    First_Dice_Result = DiceGameRound.FirstDiceResult;
                    Second_Dice_Result = DiceGameRound.SecondDiceResult;
                    Third_Dice_Result = DiceGameRound.ThirdDiceResult;
                    OddEven_Dice_Result = DiceGameRound.OddEvenResult;
                    SmallBig_Dice_Result = DiceGameRound.SmallBigResult;
                    Winning_Result = DiceGameRound.WinningResult;
                    break;
                case (int)RoundStatus.Paused:
                    RoundStatusString = Constants.Paused;
                    RoundStatusColor = Constants.GamePausedColor;
                    break;

                case (int)RoundStatus.Cancelled:
                    RoundTimer = ""; // 00:00

                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    _prefirstDiceResult = string.Empty;
                    _presecondDiceResult = string.Empty;
                    _prethirdDiceResult = string.Empty;

                    break;

                case (int)RoundStatus.PendingResult:
                    RoundTimer = ""; // 00:00

                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    HideDivToken();
                    break;

                default:
                    break;
            }
            await GetRoundBetSummary();
            await CallInvoke();
        }
    }
    #endregion

    #region BET METHODS
    public async Task SetGameChips(int GameVariantId, bool isSubGames)
    {
        if (!isSubGames)
        {
            GameChips = DiceGameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.DiceOddEven:
                    GameChips = _mapper.Map<GameChipModel>(DiceOddEvenChips);
                    break;
                case (int)GameVariant.DiceSmallBig:
                    GameChips = _mapper.Map<GameChipModel>(DiceSmallBigChips);
                    break;
                case (int)GameVariant.DiceSingle:
                    GameChips = _mapper.Map<GameChipModel>(DiceSingleChips);
                    break;
                case (int)GameVariant.DiceNumber:
                    GameChips = _mapper.Map<GameChipModel>(DiceNumberChips);
                    break;
            }

        }
        await CallInvoke();
    }
    public async Task AddCharacter(string param)
    {
        if (param != null)
        {
            if (BetCombinationValue == null || BetCombinationValue.Length < AllDice_Limit)
            {
                BetCombinationValue = BetCombinationValue + param.ToString();
            }

            BetAmount = 0;
            IsBetting_Button_Disabled = false;

            await SetNumberColor();

            if (BetCombinationValue.Length == 3)
            {
                IsBetting_Button_Disabled = true;
                CSS_SingleColor_NumberBtn_Div = "hide";
                await ShowDivToken();
            }
        }
        await CallInvoke();
    }
    public async Task SetNumberColor()
    {
        if (BetCombinationValue.Length == 1)
        {
            CSS_SingleColor_NumberBtn = "blue";
        }
        else if (BetCombinationValue.Length == 2)
        {
            CSS_SingleColor_NumberBtn = "red";
        }
        else
        {
            CSS_SingleColor_NumberBtn = "yellow";
        }
    }
    public async Task SetBetSelectedValue(object value)
    {
        BetAmount = (int)value;

        switch (CurrentGameType)
        {
            case (int)DiceBetTypes.Small:
            case (int)DiceBetTypes.Big:
                Payout = Convert.ToDecimal(BetAmount) * DiceSmallBig.FixedPriceMultiplier;
                break;
            case (int)DiceBetTypes.Odd:
            case (int)DiceBetTypes.Even:
                Payout = Convert.ToDecimal(BetAmount) * DiceOddEven.FixedPriceMultiplier;
                break;
            case (int)DiceBetTypes.Single:
            case (int)DiceBetTypes.FirstDice:
            case (int)DiceBetTypes.SecondDice:
            case (int)DiceBetTypes.ThirdDice:
                Payout = Convert.ToDecimal(BetAmount) * DiceSingle.FixedPriceMultiplier;
                break;
            case (int)DiceBetTypes.AllDice:
            case (int)DiceBetTypes.TripleDice:
                Payout = Convert.ToDecimal(BetAmount) * GameSetting.FixedPriceMultiplier.Value;
                break;
        }
    }
    public async Task<bool> ValidateBet(string betCombinationValue)
    {
        bool isvalid = true;

        var result = BetAmount % 5;
        if (result > 0)
        {
            _toastService.ShowError("Bet amount should be divisible by 5.");
            isvalid = false;
        }

        if (GameVariantId == 0)
        {
            if (BetAmount < GameSetting.MinimumBet.Value)
            {
                _toastService.ShowError("Below Minimum Bet");
                isvalid = false;
            }
            else if (BetAmount > GameSetting.MaximumBet.Value)
            {
                _toastService.ShowError("Above Maximum Bet");
                isvalid = false;
            }
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.DiceSmallBig:
                    if (BetAmount < DiceSmallBig.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > DiceSmallBig.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.DiceOddEven:
                    if (BetAmount < DiceOddEven.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > DiceOddEven.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.DiceSingle:
                    if (BetAmount < DiceSingle.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > DiceSingle.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
            }
        }

        if (BetAmount > _icurrentUser.Credits)
        {
            popupModal.Show<PopupAddCredit>("Insufficient Credits", new ModalOptions() { Class = "op-modal", HideHeader = false });
            isvalid = false;
        }

        return isvalid;
    }
    public async Task<bool> IsDoubleBet()
    {
        bool result = false;
        if (F3UserBets != null && F3UserBets.Count > 0)
        {
            var duplicateBets = F3UserBets.Where(x => x.BetValue.ToLower().Contains(BetCombinationValue.ToLower())).ToList();
            if (duplicateBets != null && duplicateBets.Count > 0)
            {
                result = true;
            }
        }
        return result;
    }
    public async Task SubmitBet()
    {
        try
        {
            DiceBetDTO betParams = new DiceBetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = DiceGameRound.Id;
            betParams.BetAmount = BetAmount;
            betParams.UserId = _icurrentUser.Id;
            betParams.BetValue = BetCombinationValue;
            betParams.GameVariantID = GameVariantId;
            var popupRes = popupModal.Show<PopupLoading>("");
            var tempBetResult = await _iDiceGameRoundService.DiceBetOnRound(betParams);
            if (tempBetResult == null)
            {
                popupRes.Close();
                IsBetting_Button_Disabled = false;
                tokenDiv = "tokenhide";
                CurrentGameType = 0;
                BetAmount = 0;

                _toastService.ShowError("Unable to place bet.");
                return;
            }
            else if (tempBetResult != null && tempBetResult.Bet == null)
            {
                popupRes.Close();
                IsBetting_Button_Disabled = false;
                tokenDiv = "tokenhide";
                CurrentGameType = 0;
                BetAmount = 0;

                _toastService.ShowError(tempBetResult.Message);
                return;
            }
            else if (tempBetResult != null)
            {
                popupRes.Close();

                await GetCurrentRoundBets();
                tokenDiv = "tokenhide";
                IsBetting_Button_Disabled = false;
                CurrentGameType = 0;
                CSS_SingleColor_NumberBtn_Div = "hide";

                if (BetCombinationValue.ToUpper().Contains(Constants.DiceSmall))
                {
                    await UpdateDiceSmallBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.DicecBig))
                {
                    await UpdateDiceBigBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.DiceOdd))
                {
                    await UpdateDiceOddBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.DiceEven))
                {
                    await UpdateDiceEvenBetAmount(GametypeId, BetAmount);
                }

                await GetRoundBetSummary();
                await ValidateUser();
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
        TotalBets = 0;
        if (DiceGameRound.RoundStatus != (int)RoundStatus.Cancelled)
        {
            var tempRoundBets = await _iDiceGameRoundService.DiceGetBets(_icurrentUser.Id, GametypeId, null);
            if (tempRoundBets != null)
            {
                DiceUserBets = new ObservableCollection<DiceBetModel>(tempRoundBets);
                foreach (var item in DiceUserBets)
                {
                    if (item.GameVariantID == 0)
                    {
                        item.WinableAmount = item.BetAmount * GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * Convert.ToDecimal(item.BetAmount)).ToString("N2") : "0";
                    }
                    else if (item.GameVariantID == (int)GameVariant.DiceOddEven)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * DiceOddEven.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.DiceSmallBig)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * DiceSmallBig.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.DiceSingle)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * DiceSingle.FixedPriceMultiplier).ToString("N2");
                    }
                    TotalBets += Convert.ToInt32(item.BetAmount.Value);
                }
                await CallInvoke();
            }
        }
        else
        {
            DiceUserBets = new ObservableCollection<DiceBetModel>();
        }
    }
    public async Task UpdateDiceSmallBetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(User_DiceSmall_Total_Bets) ? decimal.Parse(User_DiceSmall_Total_Bets) + Amount : Amount;
            User_DiceSmall_Total_Bets = totalBets.ToString("#,##0");
            Sample_Win_DiceSmall = totalBets != 0 ? (totalBets * SmallBig_Multiplier).ToString("#,###.#0") : "0";

            BetAmount = 0;
            CurrentGameType = 0;
        }
    }
    public async Task UpdateDiceBigBetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(User_DiceBig_Total_Bets) ? decimal.Parse(User_DiceBig_Total_Bets) + Amount : Amount;
            User_DiceBig_Total_Bets = totalBets.ToString("#,##0");
            Sample_Win_DiceBig = totalBets != 0 ? (totalBets * SmallBig_Multiplier).ToString("#,###.#0") : "0";

            BetAmount = 0;
            CurrentGameType = 0;
        }
    }
    public async Task UpdateDiceOddBetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(User_DiceOdd_Total_Bets) ? decimal.Parse(User_DiceOdd_Total_Bets) + Amount : Amount;
            User_DiceOdd_Total_Bets = totalBets.ToString("#,##0");
            Sample_Win_DiceOdd = totalBets != 0 ? (totalBets * OddEven_Multiplier).ToString("#,###.#0") : "0";

            BetAmount = 0;
            CurrentGameType = 0;
        }
    }
    public async Task UpdateDiceEvenBetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(User_DiceEven_Total_Bets) ? decimal.Parse(User_DiceEven_Total_Bets) + Amount : Amount;
            User_DiceEven_Total_Bets = totalBets.ToString("#,##0");
            Sample_Win_DiceEven = totalBets != 0 ? (totalBets * OddEven_Multiplier).ToString("#,###.#0") : "0";

            BetAmount = 0;
            CurrentGameType = 0;
        }
    }
    public async Task GetRoundBetSummary()
    {
        try
        {
            var roundBetDetail = await _iDiceGameRoundService.DiceGetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
            if (roundBetDetail != null)
            {
                User_AllDice_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.AllDiceBet) ? roundBetDetail.AllDiceBet : "0";
                Sample_Win_AllDice = roundBetDetail.AllDiceWinnings;

                User_DiceSmall_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.SmallBet) ? roundBetDetail.SmallBet : "0";
                Sample_Win_DiceSmall = roundBetDetail.SmallWinnings;

                User_DiceBig_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BigBet) ? roundBetDetail.BigBet : "0";
                Sample_Win_DiceBig = roundBetDetail.BigWinnings;

                User_DiceOdd_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.OddBet) ? roundBetDetail.OddBet : "0";
                Sample_Win_DiceOdd = roundBetDetail.OddWinnings;

                User_DiceEven_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.EvenBet) ? roundBetDetail.EvenBet : "0";
                Sample_Win_DiceEven = roundBetDetail.EvenWinnings;

                User_FirstDice_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.FirstDiceBet) ? roundBetDetail.FirstDiceBet : "0";
                Sample_Win_FirstDice = roundBetDetail.FirstDiceWinnings;

                User_SecondDice_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.SecondDiceBet) ? roundBetDetail.SecondDiceBet : "0";
                Sample_Win_SecondDice = roundBetDetail.SecondDiceWinnings;

                User_ThirdDice_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.ThirdDiceBet) ? roundBetDetail.ThirdDiceBet : "0";
                Sample_Win_ThirdDice = roundBetDetail.ThirdDiceWinnings;

                User_Number1_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber1) ? roundBetDetail.BetNumber1 : "0";
                User_Number2_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber2) ? roundBetDetail.BetNumber2 : "0";
                User_Number3_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber3) ? roundBetDetail.BetNumber3 : "0";
                User_Number4_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber4) ? roundBetDetail.BetNumber4 : "0";
                User_Number5_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber5) ? roundBetDetail.BetNumber5 : "0";
                User_Number6_Total_Bets = !string.IsNullOrEmpty(roundBetDetail.BetNumber6) ? roundBetDetail.BetNumber6 : "0";


                DiceBetModel tempBet = new DiceBetModel();
                ObservableCollection<DiceBetModel> tempBetList = new ObservableCollection<DiceBetModel>();

                #region FIRST DICE
                if (roundBetDetail.FirstDiceBets != null && roundBetDetail.FirstDiceBets.Count > 0)
                {
                    tempBetList = new ObservableCollection<DiceBetModel>();
                    foreach (var b in roundBetDetail.FirstDiceBets)
                    {
                        tempBet = b;
                        if (tempBet.BetAmount != null)
                        {
                            tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * SingleDice_Multiplier).ToString("#,##0");
                        }
                        tempBetList.Add(tempBet);
                    }
                    User_FirstDice_Bets = new ObservableCollection<DiceBetModel>(tempBetList.OrderBy(x => x.BetValue));
                }
                else
                {
                    User_FirstDice_Bets = new();
                }
                #endregion

                #region SECOND DICE
                if (roundBetDetail.SecondDiceBets != null && roundBetDetail.SecondDiceBets.Count > 0)
                {
                    tempBetList = new ObservableCollection<DiceBetModel>();
                    foreach (var b in roundBetDetail.SecondDiceBets)
                    {
                        tempBet = b;
                        if (tempBet.BetAmount != null)
                        {
                            tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * SingleDice_Multiplier).ToString("#,##0");
                        }
                        tempBetList.Add(tempBet);
                    }
                    User_SecondDice_Bets = new ObservableCollection<DiceBetModel>(tempBetList.OrderBy(x => x.BetValue));
                }
                else
                {
                    User_SecondDice_Bets = new();
                }
                #endregion

                #region THIRD DICE
                if (roundBetDetail.ThirdDiceBets != null && roundBetDetail.ThirdDiceBets.Count > 0)
                {
                    tempBetList = new ObservableCollection<DiceBetModel>();
                    foreach (var b in roundBetDetail.ThirdDiceBets)
                    {
                        tempBet = b;
                        if (tempBet.BetAmount != null)
                        {
                            tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * SingleDice_Multiplier).ToString("#,##0");
                        }
                        tempBetList.Add(tempBet);
                    }
                    User_ThirdDice_Bets = new ObservableCollection<DiceBetModel>(tempBetList.OrderBy(x => x.BetValue));
                }
                else
                {
                    User_ThirdDice_Bets = new();
                }
                #endregion

                #region ALL DICE
                if (roundBetDetail.AllDiceBets != null && roundBetDetail.AllDiceBets.Count > 0)
                {
                    tempBetList = new ObservableCollection<DiceBetModel>();
                    foreach (var b in roundBetDetail.AllDiceBets)
                    {
                        tempBet = b;
                        if (tempBet.BetAmount != null)
                        {
                            tempBet.WinableAmount = (Convert.ToDecimal(tempBet.BetAmount) * AllDice_Multiplier).ToString("#,##0");
                        }
                        tempBetList.Add(tempBet);
                    }
                    User_AllDice_Bets = new ObservableCollection<DiceBetModel>(tempBetList.OrderBy(x => x.BetValue));
                }
                else
                {
                    User_AllDice_Bets = new();
                }
                #endregion

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }

            await CollateToUserBets();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    private async Task CollateToUserBets()
    {
        User_PerDice_Bets = new();
        foreach (DiceBetModel bet in User_FirstDice_Bets)
        {
            User_PerDice_Bets.Add(bet);
        }
        foreach (DiceBetModel bet in User_SecondDice_Bets)
        {
            User_PerDice_Bets.Add(bet);
        }
        foreach (DiceBetModel bet in User_ThirdDice_Bets)
        {
            User_PerDice_Bets.Add(bet);
        }
        foreach (DiceBetModel bet in User_AllDice_Bets)
        {
            User_PerDice_Bets.Add(bet);
        }
    }
    #endregion
}

