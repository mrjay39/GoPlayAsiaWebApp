using AutoMapper;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.ViewModels;

public class Lucky9ViewModel : BaseViewModel
{
    #region Local Variables & Properties

    [Inject] protected IJSRuntime JsRuntime { get; set; }

    #region GAME CONFIGURATION
    private bool _showTotalBets;
    private bool _isFixedPlayerEnabled;
    private bool _isFixedBankerEnabled;
    private bool _isRunndingOddsPlayerEnabled;
    private bool _isRunningOddsBankerEnabled;
    private bool _isDrawEnabled;
    private bool _isFixedPriceCancelled;
    private bool _isRunningOddsCancelled;
    private bool _isbuttonDisabled = false;
    private int _currentGameType = 0;
    private int _betType = 0;
    private string _tokenAnimation = "";
    private string _tokenDiv = "tokenhide";
    private string _l9gif = "";


    public bool ShowTotalBets
    {
        get => _showTotalBets;
        set
        {
            _showTotalBets = value;
            RaisePropertyChanged(() => ShowTotalBets);
        }
    }
    public bool IsFixedPlayerEnabled
    {
        get => _isFixedPlayerEnabled;
        set
        {
            _isFixedPlayerEnabled = value;
            RaisePropertyChanged(() => IsFixedPlayerEnabled);
        }
    }
    public bool IsFixedBankerEnabled
    {
        get => _isFixedBankerEnabled;
        set
        {
            _isFixedBankerEnabled = value;
            RaisePropertyChanged(() => IsFixedBankerEnabled);
        }
    }
    public bool IsRunndingOddsPlayerEnabled
    {
        get => _isRunndingOddsPlayerEnabled;
        set
        {
            _isRunndingOddsPlayerEnabled = value;
            RaisePropertyChanged(() => IsRunndingOddsPlayerEnabled);
        }
    }
    public bool IsRunningOddsBankerEnabled
    {
        get => _isRunningOddsBankerEnabled;
        set
        {
            _isRunningOddsBankerEnabled = value;
            RaisePropertyChanged(() => IsRunningOddsBankerEnabled);
        }
    }
    public bool IsDrawEnabled
    {
        get => _isDrawEnabled;
        set
        {
            _isDrawEnabled = value;
            RaisePropertyChanged(() => IsDrawEnabled);
        }
    }
    public bool IsFixedPrizeCancelled
    {
        get => _isFixedPriceCancelled;
        set
        {
            _isFixedPriceCancelled = value;
            RaisePropertyChanged(() => _isFixedPriceCancelled);
        }
    }
    public bool IsRunningOddsCancelled
    {
        get => _isRunningOddsCancelled;
        set
        {
            _isRunningOddsCancelled = value;
            RaisePropertyChanged(() => IsRunningOddsCancelled);
        }
    }
    public bool IsbuttonDisabled
    {
        get => _isbuttonDisabled;
        set
        {
            _isbuttonDisabled = value;
            RaisePropertyChanged(() => IsbuttonDisabled);
        }
    }
    public int CurrentGameType
    {
        get => _currentGameType;
        set
        {
            _currentGameType = value;
            RaisePropertyChanged(() => CurrentGameType);
        }
    }
    public int BetType
    {
        get => _betType;
        set
        {
            _betType = value;
            RaisePropertyChanged(() => BetType);
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
    public string TokenDiv
    {
        get => _tokenDiv;
        set
        {
            _tokenDiv = value;
            RaisePropertyChanged(() => TokenDiv);
        }
    }
    public string L9gif
    {
        get => _l9gif;
        set
        {
            _l9gif = value;
            RaisePropertyChanged(() => L9gif);
        }
    }

    #endregion

    #region USER BETS

    #region MAIN BETS

    #region REGULAR
    private string _userTotalBet_FixedPrizePlayer = "0";
    private string _userTotalBet_FixedPrizeBanker = "0";


    public string UserTotalBet_FixedPrizePlayer
    {
        get => _userTotalBet_FixedPrizePlayer;
        set
        {
            _userTotalBet_FixedPrizePlayer = value;
            RaisePropertyChanged(() => UserTotalBet_FixedPrizePlayer);
        }
    }
    public string UserTotalBet_FixedPrizeBanker
    {
        get => _userTotalBet_FixedPrizeBanker;
        set
        {
            _userTotalBet_FixedPrizeBanker = value;
            RaisePropertyChanged(() => UserTotalBet_FixedPrizeBanker);
        }
    }
    #endregion

    #region RUNNING ODDS
    private string _userTotalBet_OddsPlayer = "0";
    private string _userTotalBet_OddsBanker = "0";


    public string UserTotalBet_OddsPlayer
    {
        get => _userTotalBet_OddsPlayer;
        set
        {
            _userTotalBet_OddsPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_OddsPlayer);
        }
    }
    public string UserTotalBet_OddsBanker
    {
        get => _userTotalBet_OddsBanker;
        set
        {
            _userTotalBet_OddsBanker = value;
            RaisePropertyChanged(() => UserTotalBet_OddsBanker);
        }
    }
    #endregion

    #endregion

    #region SIDE BETS

    #region DRAW
    private string _userTotalBet_Draw = "0";
    public string UserTotalBet_Draw
    {
        get => _userTotalBet_Draw;
        set
        {
            _userTotalBet_Draw = value;
            RaisePropertyChanged(() => UserTotalBet_Draw);
        }
    }
    #endregion

    #region TARGET
    private string _userTotalBet_TargetPlayer = "0";
    private string _userTotalBet_TargetBanker = "0";


    public string UserTotalBet_TargetPlayer
    {
        get => _userTotalBet_TargetPlayer;
        set
        {
            _userTotalBet_TargetPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_TargetPlayer);
        }
    }
    public string UserTotalBet_TargetBanker
    {
        get => _userTotalBet_TargetBanker;
        set
        {
            _userTotalBet_TargetBanker = value;
            RaisePropertyChanged(() => UserTotalBet_TargetBanker);
        }
    }
    #endregion

    #region SUITS
    private string _userTotalBet_SuitsPlayer = "0";
    private string _userTotalBet_SuitsBanker = "0";


    public string UserTotalBet_SuitsPlayer
    {
        get => _userTotalBet_SuitsPlayer;
        set
        {
            _userTotalBet_SuitsPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_SuitsPlayer);
        }
    }
    public string UserTotalBet_SuitsBanker
    {
        get => _userTotalBet_SuitsBanker;
        set
        {
            _userTotalBet_SuitsBanker = value;
            RaisePropertyChanged(() => UserTotalBet_SuitsBanker);
        }
    }
    #endregion

    #region COLOR
    private string _userTotalBet_ColorRedPlayer = "0";
    private string _userTotalBet_ColorBlackPlayer = "0";
    private string _userTotalBet_ColorRedBanker = "0";
    private string _userTotalBet_ColorBlackBanker = "0";


    public string UserTotalBet_ColorRedPlayer
    {
        get => _userTotalBet_ColorRedPlayer;
        set
        {
            _userTotalBet_ColorRedPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_ColorRedPlayer);
        }
    }
    public string UserTotalBet_ColorBlackPlayer
    {
        get => _userTotalBet_ColorBlackPlayer;
        set
        {
            _userTotalBet_ColorBlackPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_ColorBlackPlayer);
        }
    }
    public string UserTotalBet_ColorRedBanker
    {
        get => _userTotalBet_ColorRedBanker;
        set
        {
            _userTotalBet_ColorRedBanker = value;
            RaisePropertyChanged(() => UserTotalBet_ColorRedBanker);
        }
    }
    public string UserTotalBet_ColorBlackBanker
    {
        get => _userTotalBet_ColorBlackBanker;
        set
        {
            _userTotalBet_ColorBlackBanker = value;
            RaisePropertyChanged(() => UserTotalBet_ColorBlackBanker);
        }
    }
    #endregion

    #region PAIR
    private string _userTotalBet_PairPlayer = "0";
    private string _userTotalBet_PairBanker = "0";


    public string UserTotalBet_PairPlayer
    {
        get => _userTotalBet_PairPlayer;
        set
        {
            _userTotalBet_PairPlayer = value;
            RaisePropertyChanged(() => UserTotalBet_PairPlayer);
        }
    }
    public string UserTotalBet_PairBanker
    {
        get => _userTotalBet_PairBanker;
        set
        {
            _userTotalBet_PairBanker = value;
            RaisePropertyChanged(() => UserTotalBet_PairBanker);
        }
    }
    #endregion

    #endregion

    #endregion

    #region BETS

    #region MAIN BETS

    #region REGULAR
    private string _fixedPricePlayerTotalBets;
    private string _fixedPriceBankerTotalBets;


    public string FixedPricePlayerTotalBets
    {
        get => _fixedPricePlayerTotalBets;
        set
        {
            _fixedPricePlayerTotalBets = value;
            RaisePropertyChanged(() => _fixedPricePlayerTotalBets);
        }
    }
    public string FixedPriceBankerTotalBets
    {
        get => _fixedPriceBankerTotalBets;
        set
        {
            _fixedPriceBankerTotalBets = value;
            RaisePropertyChanged(() => _fixedPriceBankerTotalBets);
        }
    }
    #endregion

    #region RUNNING ODDS
    private string _runningOddsPlayerTotalBets;
    private string _runningOddBankerTotalBets;


    public string RunningOddsPlayerTotalBets
    {
        get => _runningOddsPlayerTotalBets;
        set
        {
            _runningOddsPlayerTotalBets = value;
            RaisePropertyChanged(() => _runningOddsPlayerTotalBets);
        }
    }
    public string RunningOddBankerTotalBets
    {
        get => _runningOddBankerTotalBets;
        set
        {
            _runningOddBankerTotalBets = value;
            RaisePropertyChanged(() => _runningOddBankerTotalBets);
        }
    }
    #endregion

    #endregion

    #region SIDE BETS

    #region DRAW
    #endregion

    #region TARGET
    private string _targetPlayerTotalBets;
    private string _targetBankerTotalBets;


    public string TargetPlayerTotalBets
    {
        get => _targetPlayerTotalBets;
        set
        {
            _targetPlayerTotalBets = value;
            RaisePropertyChanged(() => _targetPlayerTotalBets);
        }
    }
    public string TargetBankerTotalBets
    {
        get => _targetBankerTotalBets;
        set
        {
            _targetBankerTotalBets = value;
            RaisePropertyChanged(() => _targetBankerTotalBets);
        }
    }
    #endregion

    #region SUITS
    private string _suitsPlayerTotalBets;
    private string _suitsBankerTotalBets;


    public string SuitsPlayerTotalBets
    {
        get => _suitsPlayerTotalBets;
        set
        {
            _suitsPlayerTotalBets = value;
            RaisePropertyChanged(() => _suitsPlayerTotalBets);
        }
    }
    public string SuitsBankerTotalBets
    {
        get => _suitsBankerTotalBets;
        set
        {
            _suitsBankerTotalBets = value;
            RaisePropertyChanged(() => _suitsBankerTotalBets);
        }
    }
    #endregion

    #region COLOR
    private string _colorRedPlayerTotalBets;
    private string _colorBlackPlayerTotalBets;
    private string _colorRedBankerTotalBets;
    private string _colorBlackBankerTotalBets;


    public string ColorRedPlayerTotalBets
    {
        get => _colorRedPlayerTotalBets;
        set
        {
            _colorRedPlayerTotalBets = value;
            RaisePropertyChanged(() => _colorRedPlayerTotalBets);
        }
    }
    public string ColorBlackPlayerTotalBets
    {
        get => _colorBlackPlayerTotalBets;
        set
        {
            _colorBlackPlayerTotalBets = value;
            RaisePropertyChanged(() => _colorBlackPlayerTotalBets);
        }
    }
    public string ColorRedBankerTotalBets
    {
        get => _colorRedBankerTotalBets;
        set
        {
            _colorRedBankerTotalBets = value;
            RaisePropertyChanged(() => _colorRedBankerTotalBets);
        }
    }
    public string ColorBlackBankerTotalBets
    {
        get => _colorBlackBankerTotalBets;
        set
        {
            _colorBlackBankerTotalBets = value;
            RaisePropertyChanged(() => _colorBlackBankerTotalBets);
        }
    }
    #endregion

    #region PAIR
    private string _pairPlayerTotalBets;
    private string _pairBankerTotalBets;


    public string PairPlayerTotalBets
    {
        get => _pairPlayerTotalBets;
        set
        {
            _pairPlayerTotalBets = value;
            RaisePropertyChanged(() => _pairPlayerTotalBets);
        }
    }
    public string PairBankerTotalBets
    {
        get => _pairBankerTotalBets;
        set
        {
            _pairBankerTotalBets = value;
            RaisePropertyChanged(() => _pairBankerTotalBets);
        }
    }
    #endregion

    #endregion

    #endregion

    #region MULTIPLIER

    #region REGULAR
    private string _fixedPricePlayerMultiplier;
    private string _fixedPriceBankerMultipler;


    public string FixedPricePlayerMultiplier
    {
        get => _fixedPricePlayerMultiplier;
        set
        {
            _fixedPricePlayerMultiplier = value;
            RaisePropertyChanged(() => FixedPricePlayerMultiplier);
        }
    }
    public string FixedPriceBankerMultipler
    {
        get => _fixedPriceBankerMultipler;
        set
        {
            _fixedPriceBankerMultipler = value;
            RaisePropertyChanged(() => FixedPriceBankerMultipler);
        }
    }
    #endregion

    #region RUNNING ODDS
    private string _runningOddsPlayerMultiplier;
    private string _runningOddBankerMultiplier;


    public string RunningOddsPlayerMultiplier
    {
        get => _runningOddsPlayerMultiplier;
        set
        {
            _runningOddsPlayerMultiplier = value;
            RaisePropertyChanged(() => RunningOddsPlayerMultiplier);
        }
    }
    public string RunningOddBankerMultiplier
    {
        get => _runningOddBankerMultiplier;
        set
        {
            _runningOddBankerMultiplier = value;
            RaisePropertyChanged(() => RunningOddBankerMultiplier);
        }
    }
    #endregion

    #endregion

    #region CARDS
    private int CardValue1;
    private int CardValue2;
    private int CardValue3;
    private int CardValue4;
    private int CardValue5;
    private int CardValue6;


    public string PlayerCardValue
    {
        get; set;
    }
    public string BankerCardValue
    {
        get; set;
    }
    #endregion

    #region SAMPLE DISPLAY
    private string _sampleBetDisplay;
    public string SampleBetDisplay
    {
        get => _sampleBetDisplay;
        set
        {
            _sampleBetDisplay = value;
            RaisePropertyChanged(() => SampleBetDisplay);
        }
    }

    #region REGULAR
    private string _sampleWinFixedPrizePlayer = "0";
    private string _sampleWinFixedPrizeBanker = "0";


    public string SampleWinFixedPrizePlayer
    {
        get => _sampleWinFixedPrizePlayer;
        set
        {
            _sampleWinFixedPrizePlayer = value;
            RaisePropertyChanged(() => SampleWinFixedPrizePlayer);
        }
    }
    public string SampleWinFixedPrizeBanker
    {
        get => _sampleWinFixedPrizeBanker;
        set
        {
            _sampleWinFixedPrizeBanker = value;
            RaisePropertyChanged(() => SampleWinFixedPrizeBanker);
        }
    }
    #endregion

    #region RUNNING ODDS
    private string _sampleWinOddsBanker = "0";
    private string _sampleWinOddsPlayer = "0";


    public string SampleWinOddsPlayer
    {
        get => _sampleWinOddsPlayer;
        set
        {
            _sampleWinOddsPlayer = value;
            RaisePropertyChanged(() => SampleWinOddsPlayer);
        }
    }
    public string SampleWinOddsBanker
    {
        get => _sampleWinOddsBanker;
        set
        {
            _sampleWinOddsBanker = value;
            RaisePropertyChanged(() => SampleWinOddsBanker);
        }
    }
    #endregion

    #region DRAW
    private string _sampleWinDraw = "0";
    public string SampleWinDraw
    {
        get => _sampleWinDraw;
        set
        {
            _sampleWinDraw = value;
            RaisePropertyChanged(() => SampleWinDraw);
        }
    }
    #endregion

    #region TARGET
    private string _sampleWinTargetPlayer = "0";
    private string _sampleWinTargetBanker = "0";


    public string SampleWinTargetPlayer
    {
        get => _sampleWinTargetPlayer;
        set
        {
            _sampleWinTargetPlayer = value;
            RaisePropertyChanged(() => SampleWinTargetPlayer);
        }
    }
    public string SampleWinTargetBanker
    {
        get => _sampleWinTargetBanker;
        set
        {
            _sampleWinTargetBanker = value;
            RaisePropertyChanged(() => SampleWinTargetBanker);
        }
    }
    #endregion

    #region SUITS
    private string _sampleWinSuitsPlayer = "0";
    private string _sampleWinSuitsBanker = "0";


    public string SampleWinSuitsPlayer
    {
        get => _sampleWinSuitsPlayer;
        set
        {
            _sampleWinSuitsPlayer = value;
            RaisePropertyChanged(() => SampleWinSuitsPlayer);
        }
    }
    public string SampleWinSuitsBanker
    {
        get => _sampleWinSuitsBanker;
        set
        {
            _sampleWinSuitsBanker = value;
            RaisePropertyChanged(() => SampleWinSuitsBanker);
        }
    }
    #endregion

    #region COLOR
    private string _sampleWinColorRedPlayer = "0";
    private string _sampleWinColorBlackPlayer = "0";
    private string _sampleWinColorRedBanker = "0";
    private string _sampleWinColorBlackBanker = "0";


    public string SampleWinColorRedPlayer
    {
        get => _sampleWinColorRedPlayer;
        set
        {
            _sampleWinColorRedPlayer = value;
            RaisePropertyChanged(() => SampleWinColorRedPlayer);
        }
    }
    public string SampleWinColorBlackPlayer
    {
        get => _sampleWinColorBlackPlayer;
        set
        {
            _sampleWinColorBlackPlayer = value;
            RaisePropertyChanged(() => SampleWinColorBlackPlayer);
        }
    }
    public string SampleWinColorRedBanker
    {
        get => _sampleWinColorRedBanker;
        set
        {
            _sampleWinColorRedBanker = value;
            RaisePropertyChanged(() => SampleWinColorRedBanker);
        }
    }
    public string SampleWinColorBlackBanker
    {
        get => _sampleWinColorBlackBanker;
        set
        {
            _sampleWinColorBlackBanker = value;
            RaisePropertyChanged(() => SampleWinColorBlackBanker);
        }
    }
    #endregion

    #region PAIR
    private string _sampleWinPairPlayer = "0";
    private string _sampleWinPairBanker = "0";


    public string SampleWinPairPlayer
    {
        get => _sampleWinPairPlayer;
        set
        {
            _sampleWinPairPlayer = value;
            RaisePropertyChanged(() => SampleWinPairPlayer);
        }
    }
    public string SampleWinPairBanker
    {
        get => _sampleWinPairBanker;
        set
        {
            _sampleWinPairBanker = value;
            RaisePropertyChanged(() => SampleWinPairBanker);
        }
    }
    #endregion

    #endregion

    #region PAYOUT
    private bool _isPayoutWon_Player;
    private bool _isPayoutWon_Banker;
    private bool _isOddsWon_Player;
    private bool _isOddsWon_Banker;
    private bool _isDrawWon;


    public bool IsPayoutWon_Player
    {
        get => _isPayoutWon_Player;
        set
        {
            _isPayoutWon_Player = value;
            RaisePropertyChanged(() => IsPayoutWon_Player);
        }
    }
    public bool IsPayoutWon_Banker
    {
        get => _isPayoutWon_Banker;
        set
        {
            _isPayoutWon_Banker = value;
            RaisePropertyChanged(() => IsPayoutWon_Banker);
        }
    }
    public bool IsOddsWon_Player
    {
        get => _isOddsWon_Player;
        set
        {
            _isOddsWon_Player = value;
            RaisePropertyChanged(() => IsOddsWon_Player);
        }
    }
    public bool IsOddsWon_Banker
    {
        get => _isOddsWon_Banker;
        set
        {
            _isOddsWon_Banker = value;
            RaisePropertyChanged(() => IsOddsWon_Banker);
        }
    }
    public bool IsDrawWon
    {
        get => _isDrawWon;
        set
        {
            _isDrawWon = value;
            RaisePropertyChanged(() => IsDrawWon);
        }
    }
    #endregion

    #region TRENDS
    public static int payoutRowLimit = 10;
    public static int oddsRowLimit = 5;


    private ObservableCollection<L9GameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _oddsTrendsDisplay;
    private ObservableCollection<TrendsDisplayModel> _payoutTrendsDisplay;


    public ObservableCollection<L9GameRoundModel> Trends
    {
        get => _trends;
        set
        {
            _trends = value;
            RaisePropertyChanged(() => Trends);
        }
    }
    public ObservableCollection<TrendsDisplayModel> OddsTrendsDisplay
    {
        get => _oddsTrendsDisplay;
        set
        {
            _oddsTrendsDisplay = value;
            RaisePropertyChanged(() => OddsTrendsDisplay);
        }
    }
    public ObservableCollection<TrendsDisplayModel> PayoutTrendsDisplay
    {
        get => _payoutTrendsDisplay;
        set
        {
            _payoutTrendsDisplay = value;
            RaisePropertyChanged(() => PayoutTrendsDisplay);
        }
    }


    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<L9GameRoundModel> CurrentList { get; set; }
    }
    #endregion

    #endregion


    #region Life cycle methods
    public Lucky9ViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService,
        IGameSettingService igameSettingsService, IToastService toastService, IAccountService iaccountService,
        NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider, IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _igameSettingService = igameSettingsService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _mapper = mapper;

        StreamId = Constants.StreamIDLucky9;
        GametypeId = (int)GameTypes.Lucky_9;
        ShowTotalBets = _config.GetValue<bool>("showtotalbets");
        BetsDisplayDelay = _config.GetValue<int>("BetsDisplayDelay");
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
    #endregion

    #region Game Methods
    public async Task ShowDivToken()
    {
        TokenDiv = "block";
        await CallInvoke();
    }
    public async Task HideDivToken()
    {
        TokenDiv = "none";
        await CallInvoke();
    }


    public async Task UpdateDrawBetAmount(int GameType, decimal Amount)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        if (GameType == GametypeId)
        {
            int pDelay = 0;
            if (Token_Animation != "")
            {
                pDelay = 800;
            }

            Task.Delay(pDelay).ContinueWith(async (t) =>
            {
                decimal DrawMultiplierValue = L9Draw.FixedPriceMultiplier != null ? Convert.ToDecimal(L9Draw.FixedPriceMultiplier) : 0;
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_Draw) ? decimal.Parse(UserTotalBet_Draw) + Amount : Amount;
                UserTotalBet_Draw = totalBets.ToString("#,##0");
                SampleWinDraw = totalBets != 0 ? (totalBets * DrawMultiplierValue).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateFixed_Player_BetAmount(int GameType, decimal Amount)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        if (GameType == GametypeId)
        {
            int pDelay = 0;
            if (Token_Animation != "")
            {
                pDelay = 800;
            }



            Task.Delay(pDelay).ContinueWith(async (t) =>
            {
                decimal FixedPriceMultiplier = Convert.ToDecimal(GameSetting.FixedPriceMultiplier);
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrizePlayer) ? decimal.Parse(UserTotalBet_FixedPrizePlayer) + Amount : Amount;
                UserTotalBet_FixedPrizePlayer = totalBets.ToString("#,##0");
                SampleWinFixedPrizePlayer = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateFixed_Banker_BetAmount(int GameType, decimal Amount)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        if (GameType == GametypeId)
        {
            int pDelay = 0;
            if (Token_Animation != "")
            {
                pDelay = 800;
            }

            Task.Delay(pDelay).ContinueWith(async (t) =>
            {
                decimal FixedPriceMultiplier = Convert.ToDecimal(GameSetting.FixedPriceMultiplier);
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrizeBanker) ? decimal.Parse(UserTotalBet_FixedPrizeBanker) + Amount : Amount;
                UserTotalBet_FixedPrizeBanker = totalBets.ToString("#,##0");
                SampleWinFixedPrizeBanker = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateRunningOdds_Player_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_OddsPlayer) ? decimal.Parse(UserTotalBet_OddsPlayer) + Amount : Amount;
            UserTotalBet_OddsPlayer = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_OddsPlayer);

            await UpdateOddsValue();
        }
    }
    public async Task UpdateRunningOdds_Banker_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_OddsBanker) ? decimal.Parse(UserTotalBet_OddsBanker) + Amount : Amount;
            UserTotalBet_OddsBanker = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_OddsBanker);

            await UpdateOddsValue();
        }
    }
    public async Task UpdateOddsValue()
    {
        decimal leftPercentage = decimal.Parse(RunningOddsPlayerMultiplier.Replace("%", ""));
        decimal rightPercentage = decimal.Parse(RunningOddBankerMultiplier.Replace("%", ""));

        decimal userOddsPlayerTotalBets = !string.IsNullOrEmpty(UserTotalBet_OddsPlayer) ? decimal.Parse(UserTotalBet_OddsPlayer) : 0;
        SampleWinOddsPlayer = userOddsPlayerTotalBets != 0 ? (userOddsPlayerTotalBets * (leftPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOddsPlayer);

        decimal userOddsBankerTotalBets = !string.IsNullOrEmpty(UserTotalBet_OddsBanker) ? decimal.Parse(UserTotalBet_OddsBanker) : 0;
        SampleWinOddsBanker = userOddsBankerTotalBets != 0 ? (userOddsBankerTotalBets * (rightPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOddsBanker);
        await CallInvoke();
    }
    public async Task GetCurrentRoundBets()
    {
        if (L9GameRound is null) return;
        TotalBets = 0;
        if (L9GameRound.RoundStatus != (int)RoundStatus.Cancelled)
        {
            var tempRoundBets = await _igameRoundService.L9GetBets(_icurrentUser.Id, GametypeId, null);
            if (tempRoundBets != null)
            {
                L9UserBets = new ObservableCollection<L9BetModel>(tempRoundBets);
                foreach (var item in L9UserBets)
                {
                    if (item.GameVariantID == 0)
                    {
                        if (item.BetValue == "DRAW")
                        {
                            item.WinableAmount = item.BetAmount * GameSetting.PayoutMultiplier != null ? (Convert.ToDecimal(GameSetting.PayoutMultiplier) * Convert.ToDecimal(item.BetAmount)).ToString("N2") : "0";
                        }
                        else
                        {
                            item.WinableAmount = item.BetAmount * GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * Convert.ToDecimal(item.BetAmount)).ToString("N2") : "0";
                        }
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9Target)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Target.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9Suits)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Suits.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9Color)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Color.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9Pair)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Pair.FixedPriceMultiplier).ToString("N2");
                    }
                    TotalBets += Convert.ToInt32(item.BetAmount.Value);
                }
                await CallInvoke();
            }
        }
        else
        {
            L9UserBets = new ObservableCollection<L9BetModel>();
        }
    }


    public async Task AssignGameCardResult()
    {
        GameResult.LeftWinningTarget = L9GameRound.LeftWinningTarget;
        GameResult.RightWinningTarget = L9GameRound.RightWinningTarget;
        GameResult.LeftWinningSuits = L9GameRound.LeftWinningSuits;
        GameResult.RightWinningSuits = L9GameRound.RightWinningSuits;
        GameResult.LeftWinningColor = L9GameRound.LeftWinningColor;
        GameResult.RightWinningColor = L9GameRound.RightWinningColor;
        GameResult.LeftWinningPair = L9GameRound.LeftWinningPair;
        GameResult.RightWinningPair = L9GameRound.RightWinningPair;
        GameResult.WinningResult = L9GameRound.WinningResult;
        GameResult.TrioResult = L9GameRound.TrioResult;
        GameResult.ValACard1 = L9GameRound.ValACard1;
        GameResult.ValACard2 = L9GameRound.ValACard2;
        GameResult.ValACard3 = L9GameRound.ValACard3;
        GameResult.ValBCard1 = L9GameRound.ValBCard1;
        GameResult.ValBCard2 = L9GameRound.ValBCard2;
        GameResult.ValBCard3 = L9GameRound.ValBCard3;
        await ComputeCardValues();

    }
    private async Task ComputeCardValues()
    {
        CardValue1 = 0;
        CardValue2 = 0;
        CardValue3 = 0;
        CardValue4 = 0;
        CardValue5 = 0;
        CardValue6 = 0;
        if (GameResult.ValACard1 != null)
            CardValue1 = GetCardValues(GameResult.ValACard1);
        if (GameResult.ValACard2 != null)
            CardValue2 = GetCardValues(GameResult.ValACard2);
        if (GameResult.ValACard3 != null)
            CardValue3 = GetCardValues(GameResult.ValACard3);
        if (GameResult.ValBCard1 != null)
            CardValue4 = GetCardValues(GameResult.ValBCard1);
        if (GameResult.ValBCard2 != null)
            CardValue5 = GetCardValues(GameResult.ValBCard2);
        if (GameResult.ValBCard3 != null)
            CardValue6 = GetCardValues(GameResult.ValBCard3);

        int playercard = 0;
        playercard = CardValue1 + CardValue2 + CardValue3;
        if (playercard > 20)
        {
            playercard = playercard - 20;
        }
        else if (playercard > 10)
        {
            playercard = playercard - 10;
        }
        else if (playercard == 10)
        {
            playercard = 0;
        }
        PlayerCardValue = playercard.ToString();

        int bankercard = 0;
        bankercard = CardValue4 + CardValue5 + CardValue6;
        if (bankercard > 20)
        {
            bankercard = bankercard - 20;
        }
        else if (bankercard > 10)
        {
            bankercard = bankercard - 10;
        }
        else if (bankercard == 10)
        {
            bankercard = 0;
        }
        BankerCardValue = bankercard.ToString();

    }
    public int GetCardValues(string Card)
    {
        string[] CardNumber = Card.Split("-");
        int CardValue = 0;
        if (CardNumber.Length >= 3)
        {
            switch (CardNumber[0])
            {
                case "A":
                    CardValue = 1;
                    break;
                case "K":
                case "Q":
                case "10":
                case "J":
                    CardValue = 0;
                    break;
                default:
                    CardValue = int.Parse(CardNumber[0]);
                    break;
            }

        }
        return CardValue;

    }



    public async Task GetGameRound()
    {
        L9GameRound = await _igameRoundService.L9GetRound(GametypeId);
        if (L9GameRound == null)
        {
            _toastService.ShowError("No round details found.");
            await GetGameSetting();
            return;
        }
        else if (L9GameRound != null)
        {
            if (L9GameRound.RoundNumber > 0)
            {
                RoundNumber = L9GameRound.RoundNumber;
            }

            FixedPricePlayerTotalBets = L9GameRound.FixedLeftBet;
            FixedPriceBankerTotalBets = L9GameRound.FixedRightBet;
            RunningOddsPlayerTotalBets = L9GameRound.OddsLeftBet;
            RunningOddBankerTotalBets = L9GameRound.OddsRightBet;
            DrawTotalBets = L9GameRound.DrawBet;

            RunningOddsPlayerMultiplier = L9GameRound.OddsLeftPercentage;
            RunningOddBankerMultiplier = L9GameRound.OddsRightPercentage;

            //new
            TargetPlayerTotalBets = L9GameRound.TargetLeftBet;
            TargetBankerTotalBets = L9GameRound.TargetRightBet;
            SuitsPlayerTotalBets = L9GameRound.SuitsLeftBet;
            SuitsBankerTotalBets = L9GameRound.SuitsRightBet;
            ColorRedPlayerTotalBets = L9GameRound.ColorRedLeftBet;
            ColorBlackPlayerTotalBets = L9GameRound.ColorBlackLeftBet;
            ColorRedBankerTotalBets = L9GameRound.ColorRedRightBet;
            ColorBlackBankerTotalBets = L9GameRound.ColorBlackRightBet;
            PairPlayerTotalBets = L9GameRound.PairLeftBet;
            PairBankerTotalBets = L9GameRound.PairRightBet;


            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            GameResultString = L9GameRound.WinningResult;
            switch (L9GameRound.RoundStatus)
            {
                case (int)RoundStatus.Open:
                    GameResult = new UpdateGameResultModel();
                    PreGameResult = new UpdateGameResultModel();
                    GameResultString = null;
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;


                    IsFixedPlayerEnabled = true;
                    IsFixedBankerEnabled = true;
                    IsRunndingOddsPlayerEnabled = true;
                    IsRunningOddsBankerEnabled = true;
                    IsDrawEnabled = true;

                    IsPayoutWon_Player = false;
                    IsPayoutWon_Banker = false;
                    IsOddsWon_Player = false;
                    IsOddsWon_Banker = false;
                    IsDrawWon = false;

                    PlayerCardValue = "";
                    BankerCardValue = "";
                    break;

                case (int)RoundStatus.Closed:

                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";

                    IsFixedPlayerEnabled = false;
                    IsFixedBankerEnabled = false;
                    IsRunndingOddsPlayerEnabled = false;
                    IsRunningOddsBankerEnabled = false;
                    IsDrawEnabled = false;
                    IsRunningOddsCancelled = L9GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = L9GameRound.FixedCancelled;

                    if (L9GameRound.WinningResult == Constants.Player)
                    {
                        if (!L9GameRound.FixedCancelled)
                        {
                            IsPayoutWon_Player = true;
                        }

                        if (!L9GameRound.RunningOddsCancelled)
                        {
                            IsOddsWon_Player = true;
                        }
                    }
                    else if (L9GameRound.WinningResult == Constants.Banker)
                    {
                        if (!L9GameRound.FixedCancelled)
                        {
                            IsPayoutWon_Banker = true;
                        }

                        if (!L9GameRound.RunningOddsCancelled)
                        {
                            IsOddsWon_Banker = true;
                        }
                    }
                    else if (L9GameRound.WinningResult == Constants.Draw)
                    {
                        IsDrawWon = true;
                    }
                    //assign winning results
                    await AssignGameCardResult();

                    break;
                case (int)RoundStatus.Paused:

                    RoundStatusString = Constants.Paused;
                    RoundStatusColor = Constants.GamePausedColor;
                    break;

                case (int)RoundStatus.Cancelled:
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";

                    IsFixedPlayerEnabled = false;
                    IsFixedBankerEnabled = false;
                    IsRunndingOddsPlayerEnabled = false;
                    IsRunningOddsBankerEnabled = false;
                    IsDrawEnabled = false;
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    IsRunningOddsCancelled = L9GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = L9GameRound.FixedCancelled;

                    break;

                case (int)RoundStatus.PendingResult:
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    IsRunningOddsCancelled = L9GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = L9GameRound.FixedCancelled;
                    IsFixedPlayerEnabled = false;
                    IsFixedBankerEnabled = false;
                    IsRunndingOddsPlayerEnabled = false;
                    IsRunningOddsBankerEnabled = false;
                    IsDrawEnabled = false;

                    //assign winning results
                    //assign winning results
                    //await AssignGameCardResult();
                    PlayerCardValue = "";
                    BankerCardValue = "";
                    break;

                default:
                    break;
            }
            await GetRoundBetSummary();
            await CallInvoke();
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
                DrawMultiplier = GameSetting.PayoutMultiplier != null ? "(x" + Convert.ToDecimal(GameSetting.PayoutMultiplier).ToString("0") + ")" : "(x)";
                FixedPricePlayerMultiplier = GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * 100).ToString("N0") + "%" : "%";
                FixedPriceBankerMultipler = GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * 100).ToString("N0") + "%" : "%";
                RunningOddsPlayerMultiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
                RunningOddBankerMultiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
                MinBet = GameSetting.MinimumBet != null ? GameSetting.MinimumBet.Value.ToString("#,##0") : "0";
                MaxBet = GameSetting.MaximumBet != null ? GameSetting.MaximumBet.Value.ToString("#,##0") : "0";
                StreamURL = GameSetting.GameStreamUrl;

            }
            await CallInvoke();

        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task SetGameChips(int GameVariantId, bool isSubGames)
    {
        if (!isSubGames)
        {
            GameChips = L9GameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.L9Target:
                    GameChips = _mapper.Map<GameChipModel>(L9TargetChips);
                    break;
                case (int)GameVariant.L9Pair:
                    GameChips = _mapper.Map<GameChipModel>(L9PairChips);
                    break;
                case (int)GameVariant.L9Color:
                    GameChips = _mapper.Map<GameChipModel>(L9ColorChips);
                    break;
                case (int)GameVariant.L9Suits:
                    GameChips = _mapper.Map<GameChipModel>(L9SuitsChips);
                    break;
                case (int)GameVariant.L9Draw:
                    GameChips = _mapper.Map<GameChipModel>(L9DrawChips);
                    break;
            }

        }
        await CallInvoke();
    }
    public async Task GetGameVariantChips()
    {
        var tempGameVariantChips = await _igameSettingService.GetGameVariantChips(GametypeId);
        if (tempGameVariantChips == null)
        {
            _toastService.ShowInfo("No game variant chips found.");
            return;
        }

        foreach (var item in tempGameVariantChips)
        {
            if (item.GameVariantId == (int)GameVariant.L9Target)
            {
                L9TargetChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.L9Suits)
            {
                L9SuitsChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.L9Color)
            {
                L9ColorChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.L9Pair)
            {
                L9PairChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.L9Draw)
            {
                L9DrawChips = item;
            }
        }

        //GameChips = tempGameChips;
        await CallInvoke();

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
            if (item.GameVariantId == (int)GameVariant.L9Target)
            {
                L9TargetChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9Suits)
            {
                L9SuitsChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9Color)
            {
                L9ColorChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9Pair)
            {
                L9PairChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9Draw)
            {
                L9DrawChips = item;
                chipsList.Add(item.Chip1);
            }
        }
        MinChip = chipsList.Min();

        await CallInvoke();
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
                    if (item.GameVariantID == (int)GameVariant.L9Target)
                    {
                        L9Target = item;

                    }
                    if (item.GameVariantID == (int)GameVariant.L9Suits)
                    {
                        L9Suits = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9Color)
                    {
                        L9Color = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9Pair)
                    {
                        L9Pair = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9Draw)
                    {
                        L9Draw = item;
                    }
                }
            }
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async void NotifyGameRoundResult(UpdateGameResultModel result)
    {
        if (result != null && result.GameTypeId == GametypeId)
        {
            //LoadDefaultBetDetails();
            GameResultString = result.WinningCombination;
            GameResult = result;
            await GetCurrentRoundBets();
        }
    }
    public async Task UpdateEnableOpenButton(int GameTypeId)
    {
        if (GameTypeId == GametypeId)
        {
            //await LoadDefaultBetDetails();
        }
    }
    public async Task NotifyFixedCancelled(long userId, int gameTypeId)
    {
        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsFixedPrizeCancelled = true;
            IsFixedPlayerEnabled = false;
            IsFixedBankerEnabled = false;
            await CallInvoke();
        }
    }
    public async Task NotifyOddsCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsRunningOddsCancelled = true;
            IsRunndingOddsPlayerEnabled = false;
            IsRunningOddsBankerEnabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyFixedLeftOptions(int gameTypeId, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            if (L9GameRound != null)
            {

                if (L9GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixedPlayerEnabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
            //if (IsFixedPlayerEnabled)
            //{ 
            //    IsFixedPlayerEnabled = !arg.IsDisabled;
            //}
        }

    }
    public async Task NotifyFixedRightOptions(int gameTypeId, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            if (L9GameRound != null)
            {

                if (L9GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixedBankerEnabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
        }
    }
    public async void UpdateCardResults(int gameTypeId, string CardType, string CardValue)
    {
        if (GametypeId == gameTypeId)
        {
            await Task.Delay(4000);
            switch (CardType)
            {
                case "ValACard1":
                    PreGameResult.ValACard1 = CardValue;
                    break;
                case "ValACard2":
                    PreGameResult.ValACard2 = CardValue;
                    break;
                case "ValACard3":
                    PreGameResult.ValACard3 = CardValue;
                    break;
                case "ValBCard1":
                    PreGameResult.ValBCard1 = CardValue;
                    break;
                case "ValBCard2":
                    PreGameResult.ValBCard2 = CardValue;
                    break;
                case "ValBCard3":
                    PreGameResult.ValBCard3 = CardValue;
                    break;
                default:
                    break;
            }
            await CallInvoke();

        }
    }
    #endregion

    #region Bet Methods
    public async Task SubmitBet()
    {
        try
        {
            L9BetDTO betParams = new L9BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = L9GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.BetAmount = BetAmount;
            betParams.BetValue = BetCombinationValue;
            betParams.GameVariantID = GameVariantId;
            var popupRes = popupModal.Show<PopupLoading>("");
            var tempBetResult = await _igameRoundService.L9BetOnRound(betParams);
            if (tempBetResult == null)
            {
                popupRes.Close();
                _toastService.ShowError("Unable to place bet.");
                return;
            }
            else if (tempBetResult != null && tempBetResult.Bet == null)
            {
                popupRes.Close();
                _toastService.ShowError(tempBetResult.Message);
                return;
            }
            else if (tempBetResult != null)
            {
                popupRes.Close();

                await GetCurrentRoundBets();
                TokenDiv = "none";
                IsbuttonDisabled = false;
                Token_Animation = "1s";
                BetType = 0;


                if (BetCombinationValue.ToUpper().Contains(Constants.Draw))
                {
                    await UpdateDrawBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedPlayer))
                {
                    await UpdateFixed_Player_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedBanker))
                {
                    await UpdateFixed_Banker_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsPlayer))
                {
                    await UpdateRunningOdds_Player_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsBanker))
                {
                    await UpdateRunningOdds_Banker_BetAmount(GametypeId, BetAmount);
                }
                //_toastService.ShowSuccess("Bets Confirmed ");



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
    public async Task SetBetSelectedValue(object value)
    {
        BetAmount = (int)value;

        switch (BetType)
        {
            case (int)L9MainBetTypes.Player:
            case (int)L9MainBetTypes.Banker:
                Payout = Convert.ToDecimal(BetAmount) * GameSetting.FixedPriceMultiplier.Value;
                break;
            case (int)L9MainBetTypes.Draw:
                Payout = Convert.ToDecimal(BetAmount) * GameSetting.PayoutMultiplier.Value;
                break;
            case (int)L9SubBetTypes.TwoRedPlayer:
            case (int)L9SubBetTypes.TwoRedBanker:
            case (int)L9SubBetTypes.TwoBlackPlayer:
            case (int)L9SubBetTypes.TwoBlackBanker:
                Payout = Convert.ToDecimal(BetAmount) * L9Color.FixedPriceMultiplier;
                break;
            case (int)L9SubBetTypes.Target9Player:
            case (int)L9SubBetTypes.Target9Banker:
                Payout = Convert.ToDecimal(BetAmount) * L9Target.FixedPriceMultiplier;
                break;
            case (int)L9SubBetTypes.AnypairPlayer:
            case (int)L9SubBetTypes.AnyPairBanker:
                Payout = Convert.ToDecimal(BetAmount) * L9Pair.FixedPriceMultiplier;
                break;
        }
    }
    public async Task<bool> ValidateBet(string betOptionSelected, string betCombinationValue)
    {
        bool isvalid = true;

        var isCrossBetting = L9IsCrossBetting(betCombinationValue, L9UserBets).Result;
        if (isCrossBetting)
        {
            _toastService.ShowError("Cross betting not allowed!");
            isvalid = false;
        }

        //check if divisible by 10
        var result = BetAmount % 10;
        if (result > 0)
        {
            _toastService.ShowError("Bet amount should be divisible by 10.");
            isvalid = false;
        }
        // check for min max bet and above credit
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
                case (int)GameVariant.L9Target:
                    if (BetAmount < L9Target.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > L9Target.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.L9Pair:
                    if (BetAmount < L9Pair.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > L9Pair.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.L9Color:
                    if (BetAmount < L9Color.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > L9Color.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.L9Suits:
                    if (BetAmount < L9Suits.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > L9Suits.MaximumBet)
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
        if (L9UserBets != null && L9UserBets.Count > 0)
        {
            var duplicateBets = L9UserBets.Where(x => x.BetValue.ToLower().Contains(BetCombinationValue.ToLower())).ToList();
            if (duplicateBets != null && duplicateBets.Count > 0)
            {
                result = true;
            }
        }
        return result;
    }
    public async Task L9GetPreviousBets()
    {
        long UserId = _icurrentUser.Id;
        var tempRoundBets = await _igameRoundService.L9GetPrevGameBets(UserId, 5);
        if (tempRoundBets != null)
        {
            L9PreviousBets = new ObservableCollection<L9BetModel>(tempRoundBets);
            CallInvoke();
        }
    }

    public async Task GetRoundBetSummary()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {
            var roundBetDetail = await _igameRoundService.L9GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
            if (roundBetDetail != null)
            {
                int pDelay = 0;
                if (Token_Animation != "")
                {
                    pDelay = 800;
                }

                Task.Delay(pDelay).ContinueWith(async (t) =>
                {
                    UserTotalBet_FixedPrizePlayer = !string.IsNullOrEmpty(roundBetDetail.LeftFixedBet) ? roundBetDetail.LeftFixedBet : "0";
                    SampleWinFixedPrizePlayer = roundBetDetail.LeftFixedWinnings;

                    UserTotalBet_FixedPrizeBanker = !string.IsNullOrEmpty(roundBetDetail.RightFixedBet) ? roundBetDetail.RightFixedBet : "0";
                    SampleWinFixedPrizeBanker = roundBetDetail.RightFixedWinnings;

                    UserTotalBet_OddsPlayer = !string.IsNullOrEmpty(roundBetDetail.LeftOddsBet) ? roundBetDetail.LeftOddsBet : "0";
                    SampleWinOddsPlayer = roundBetDetail.LeftOddsWinnings;

                    UserTotalBet_OddsBanker = !string.IsNullOrEmpty(roundBetDetail.RightOddsBet) ? roundBetDetail.RightOddsBet : "0";
                    SampleWinOddsBanker = roundBetDetail.RightOddsWinnings;

                    UserTotalBet_Draw = !string.IsNullOrEmpty(roundBetDetail.DrawBet) ? roundBetDetail.DrawBet : "0";
                    SampleWinDraw = roundBetDetail.DrawWinnings;

                    #region NEW VARIANT
                    UserTotalBet_TargetPlayer = !string.IsNullOrEmpty(roundBetDetail.TargetLeftBet) ? roundBetDetail.TargetLeftBet : "0";
                    SampleWinTargetPlayer = roundBetDetail.TargetLeftBetWinnings;

                    UserTotalBet_TargetBanker = !string.IsNullOrEmpty(roundBetDetail.TargetRightBet) ? roundBetDetail.TargetRightBet : "0";
                    SampleWinTargetBanker = roundBetDetail.TargetRightBetWinnings;

                    UserTotalBet_SuitsPlayer = !string.IsNullOrEmpty(roundBetDetail.SuitsLeftBet) ? roundBetDetail.SuitsLeftBet : "0";
                    SampleWinSuitsPlayer = roundBetDetail.SuitsLeftBetWinnings;

                    UserTotalBet_SuitsBanker = !string.IsNullOrEmpty(roundBetDetail.SuitsRightBet) ? roundBetDetail.SuitsRightBet : "0";
                    SampleWinSuitsBanker = roundBetDetail.SuitsRightBetWinnings;

                    UserTotalBet_ColorRedPlayer = !string.IsNullOrEmpty(roundBetDetail.ColorRedLeftBet) ? roundBetDetail.ColorRedLeftBet : "0";
                    SampleWinColorRedPlayer = roundBetDetail.ColorRedLeftBetWinnings;

                    UserTotalBet_ColorBlackPlayer = !string.IsNullOrEmpty(roundBetDetail.ColorBlackLeftBet) ? roundBetDetail.ColorBlackLeftBet : "0";
                    SampleWinColorBlackPlayer = roundBetDetail.ColorBlackLeftBetWinnings;

                    UserTotalBet_ColorRedBanker = !string.IsNullOrEmpty(roundBetDetail.ColorRedRightBet) ? roundBetDetail.ColorRedRightBet : "0";
                    SampleWinColorRedBanker = roundBetDetail.ColorRedRightBetWinnings;

                    UserTotalBet_ColorBlackBanker = !string.IsNullOrEmpty(roundBetDetail.ColorBlackRightBet) ? roundBetDetail.ColorBlackRightBet : "0";
                    SampleWinColorBlackBanker = roundBetDetail.ColorBlackRightBetWinnings;

                    UserTotalBet_PairPlayer = !string.IsNullOrEmpty(roundBetDetail.PairLeftBet) ? roundBetDetail.PairLeftBet : "0";
                    SampleWinPairPlayer = roundBetDetail.PairLeftBetWinnings;

                    UserTotalBet_PairBanker = !string.IsNullOrEmpty(roundBetDetail.PairRightBet) ? roundBetDetail.PairRightBet : "0";
                    SampleWinPairBanker = roundBetDetail.PairRightBetWinnings;
                    #endregion

                    Token_Animation = "";
                    BetAmount = 0;
                    CurrentGameType = 0;
                }, cancellationToken);
            }


        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    #endregion

    #region Trends
    public async Task GetTrends()
    {

        //var tempTrends = await _igameRoundService.GetTrends(GametypeId);
        var tempTrends = await _igameRoundService.L9GetTrends();
        if (tempTrends != null)
        {
            await SetPayoutTrendsDisplay(tempTrends);
            await SetOddsTrendsDisplay(tempTrends);
            await CallInvoke();
        }
    }
    public async Task SetOddsTrendsDisplay(List<L9GameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<L9GameRoundModel> CurrentList = new ObservableCollection<L9GameRoundModel>();
            TrendsDisplayModel Current = new TrendsDisplayModel();
            int counter = 0;
            int rowCounter = 0;
            int indexCounter = 0;

            foreach (var t in tempTrends.OrderBy(x => x.Id).ToList())
            {
                counter++;
                rowCounter++;
                if (rowCounter != oddsRowLimit)
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
                        Current.CurrentList = new ObservableCollection<L9GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<L9GameRoundModel>();

                        rowCounter = 0;
                        counter = 0;
                    }
                }
                else if (rowCounter == oddsRowLimit)
                {
                    CurrentList.Add(t);
                    Current = new TrendsDisplayModel();
                    indexCounter++;
                    Current.ColumnIndex = indexCounter;
                    Current.CurrentList = new ObservableCollection<L9GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<L9GameRoundModel>();

                    rowCounter = 0;
                }
            }

            if (resultHolder != null)
            {
                //OddsTrendsDisplay = resultHolder;
                OddsTrendsDisplay = new ObservableCollection<TrendsDisplayModel>(resultHolder.OrderByDescending(x => x.ColumnIndex));
            }
        }

    }
    public async Task SetPayoutTrendsDisplay(List<L9GameRoundModel> tempTrends)
    {
        Trends = new ObservableCollection<L9GameRoundModel>(tempTrends.OrderBy(x => x.Id));
        var tempTrendList = new List<L9GameRoundModel>();
        var tempTrendListHolder = new TrendsDisplayModel();
        var tempTrendsForDisplay = new List<TrendsDisplayModel>();
        L9GameRoundModel current = new L9GameRoundModel();
        int count = 0;
        int indexCounter = 0;
        L9GameRoundModel lastItem = new L9GameRoundModel();
        L9GameRoundModel lastNonDrawGame = new L9GameRoundModel();
        lastItem = Trends.LastOrDefault();


        foreach (var t in Trends)
        {
            count++;
            if (current.Id == 0)
            {
                current = t;
                tempTrendList.Add(current);

                if (lastItem.Id == t.Id)
                {
                    indexCounter++;
                    tempTrendListHolder.ColumnIndex = indexCounter;
                    tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                    tempTrendsForDisplay.Add(tempTrendListHolder);

                    tempTrendList = new List<L9GameRoundModel>();
                    tempTrendListHolder = new TrendsDisplayModel();

                    count = 0;
                }
            }
            else
            {
                if ((current.Trends_PayoutDisplay == Constants.Cancelled || current.Trends_PayoutDisplay == Constants.Draw) && string.IsNullOrEmpty(lastNonDrawGame.Trends_PayoutDisplay))
                {
                    current = t;
                    tempTrendList.Add(current);

                    if (tempTrendList.Count == payoutRowLimit)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                }
                else if ((t.Trends_PayoutDisplay == Constants.Cancelled || t.Trends_PayoutDisplay == Constants.Draw) && string.IsNullOrEmpty(lastNonDrawGame.Trends_PayoutDisplay))
                {
                    current = t;
                    tempTrendList.Add(current);

                    if (tempTrendList.Count == payoutRowLimit)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                }
                else if ((t.Trends_PayoutDisplay == Constants.Cancelled || t.Trends_PayoutDisplay == Constants.Draw) && lastNonDrawGame.Trends_PayoutDisplay == t.Trends_PayoutDisplay)
                {
                    current = t;
                    tempTrendList.Add(current);

                    if (tempTrendList.Count == payoutRowLimit)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                }
                else if ((current.Trends_PayoutDisplay != Constants.Cancelled || current.Trends_PayoutDisplay != Constants.Draw) && (t.Trends_PayoutDisplay == Constants.Cancelled || t.Trends_PayoutDisplay == Constants.Draw))
                {
                    current = t;
                    tempTrendList.Add(current);

                    if (tempTrendList.Count == payoutRowLimit)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                }
                else if (lastNonDrawGame.Trends_PayoutDisplay == t.Trends_PayoutDisplay)
                {
                    current = t;
                    tempTrendList.Add(current);

                    if (tempTrendList.Count == payoutRowLimit)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                }
                else
                {
                    if (tempTrendList.Count > payoutRowLimit)
                    {
                        var excessList = tempTrendList.OrderBy(x => x.Id).Take(payoutRowLimit).ToList();
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(excessList);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        var remaining = tempTrendList.OrderBy(x => x.Id).Skip(payoutRowLimit).ToList();
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(remaining);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }
                    else
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }

                    if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<L9GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<L9GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();
                    }
                    count = 0;
                }

            }

            if (t.Trends_PayoutDisplay != Constants.Cancelled && t.Trends_PayoutDisplay != Constants.Draw)
            {
                lastNonDrawGame = t;
            }
        }

        if (tempTrendsForDisplay != null)
        {
            PayoutTrendsDisplay = new ObservableCollection<TrendsDisplayModel>(tempTrendsForDisplay.OrderByDescending(x => x.ColumnIndex));
        }
    }
    public override async void UpdateGameTimer(int gametypeId, string value)
    {
        try
        {

            if (GametypeId == gametypeId && L9GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                // Console.WriteLine(int.Parse(RoundTimer.Replace(":", "").TrimStart(new Char[] { '0' })));
                if (L9GameRound.RoundStatus == (int)RoundStatus.Open && timer < 10)
                {
                    ShowFlashing = "timerFlasher";
                }
                else
                {
                    ShowFlashing = "timerNotFlashing";
                }
                if (L9GameRound.RoundStatus == (int)RoundStatus.Closed && AwaitingGameRound == false)
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
    #endregion

    #region SignalR Methods
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
                    HubConnection.Remove(Constants.UpdateBetValues);
                    HubConnection.Remove(Constants.UpdateGameOdds);
                    HubConnection.Remove(Constants.UpdateTrends);
                    HubConnection.Remove(Constants.NotifyGameRoundResult);
                    HubConnection.Remove(Constants.UpdateEnableOpenButton);
                    HubConnection.Remove(Constants.NotifyFixedCancelled);
                    HubConnection.Remove(Constants.NotifyOddsCancelled);
                    HubConnection.Remove(Constants.NotifyFixedLeftOptions);
                    HubConnection.Remove(Constants.NotifyFixedRightOptions);
                    HubConnection.Remove(Constants.UpdateCardResults);


                    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<BetUpdatesModel>(Constants.UpdateBetValues, UpdateBetValues);
                    HubConnection.On<int, decimal, decimal>(Constants.UpdateGameOdds, UpdateGameOdds);
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);

                    HubConnection.On<int>(Constants.UpdateEnableOpenButton, UpdateEnableOpenButton);
                    HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);
                    HubConnection.On<long, int>(Constants.NotifyOddsCancelled, NotifyOddsCancelled);

                    HubConnection.On<int, bool>(Constants.NotifyFixedLeftOptions, NotifyFixedLeftOptions);
                    HubConnection.On<int, bool>(Constants.NotifyFixedRightOptions, NotifyFixedRightOptions);
                    HubConnection.On<int, string, string>(Constants.UpdateCardResults, UpdateCardResults);
                    //HubConnection.On<int, int, decimal>(Constants.NotifyNumberBets, NotifyNumberBets); not l9
                    //HubConnection.On<int, int, bool>(Constants.NotifyNumberOptions, NotifyNumberOptions); not l9
                    //HubConnection.On<int>(Constants.NotifyEnableAllNumbers, NotifyEnableAllNumbers); not l9


                }

            }
        }
        catch (Exception)
        {

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
    private async Task LoadDefaultBetDetails()
    {
        var sampleBet = 0;
        decimal FixedPriceMultiplier = Convert.ToDecimal(GameSetting.FixedPriceMultiplier);
        decimal RunningOddsPercentage = Convert.ToDecimal(GameSetting.RunningOddsPercentage) / 100;
        decimal DrawMultiplierValue = Convert.ToDecimal(L9Draw.FixedPriceMultiplier);
        SampleBetDisplay = sampleBet.ToString("0");
        SampleWinFixedPrizePlayer = (sampleBet * FixedPriceMultiplier).ToString("0");
        SampleWinFixedPrizeBanker = (sampleBet * FixedPriceMultiplier).ToString("0"); ;
        SampleWinOddsPlayer = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinOddsBanker = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinDraw = (sampleBet * DrawMultiplierValue).ToString("0");

        //if (GameRound.RoundStatus == (int)RoundStatus.Closed || GameRound.RoundStatus == (int)RoundStatus.Cancelled)
        //{ 
        FixedPricePlayerTotalBets = "0";
        FixedPriceBankerTotalBets = "0";
        RunningOddsPlayerTotalBets = "0";
        RunningOddBankerTotalBets = "0";

        UserTotalBet_Draw = "0";
        UserTotalBet_FixedPrizePlayer = "0";
        UserTotalBet_FixedPrizeBanker = "0";
        UserTotalBet_OddsPlayer = "0";
        UserTotalBet_OddsBanker = "0";

        RunningOddsPlayerMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
        RunningOddBankerMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";

        //IsFixedPlayerEnabled = false;
        //IsFixedBankerEnabled = false;
        //IsFixedPrizeCancelled = false;
        //IsRunningOddsCancelled = false;
        //}


    }
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus, long gameRoundId)
    {
        if (GametypeId == gametypeId)
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

            if (gameStatus != (int)RoundStatus.Open)
            {
                TickerMessage = "";
                IsFixedPlayerEnabled = false;
                IsFixedBankerEnabled = false;
                IsRunndingOddsPlayerEnabled = false;
                IsRunningOddsBankerEnabled = false;
                IsDrawEnabled = false;
                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    L9gif = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    await LoadDefaultBetDetails();
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    L9gif = "/img/animation/test-closebet.gif";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                }
                ShowFlashing = "timerNotFlashing";
                //MessagingCenter.Send(this, Constants.CloseBettingPopups, string.Empty);

                BetAmount = 0;
                Payout = 0;
                TokenDiv = "tokenhide";
                IsbuttonDisabled = false;
            }
            else if (gameStatus == (int)RoundStatus.Open)
            {
                TotalBets = 0;
                IsFixedPlayerEnabled = true;
                IsFixedBankerEnabled = true;
                IsRunndingOddsPlayerEnabled = true;
                IsRunningOddsBankerEnabled = true;
                IsDrawEnabled = true;
                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;
                await LoadDefaultBetDetails();
                L9UserBets = new ObservableCollection<L9BetModel>();

                L9gif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            await CallInvoke();

        }

    }
    public async Task UpdateBetValues(BetUpdatesModel paramsModel)
    {
        if (L9GameRound is null) return;
        if (paramsModel.GameTypeId == GametypeId)
        {
            if (L9GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                //if (disableUpdate == true) return;
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {

                    //Console.WriteLine(DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds);
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    DrawTotalBets = paramsModel.DrawBetValue.ToString("#,##0");
                    FixedPricePlayerTotalBets = paramsModel.FixedLeftBetValue.ToString("#,##0");
                    FixedPriceBankerTotalBets = paramsModel.FixedRightBetValue.ToString("#,##0");
                    RunningOddsPlayerTotalBets = paramsModel.RunningLeftBetValue.ToString("#,##0");
                    RunningOddBankerTotalBets = paramsModel.RunningRightBetValue.ToString("#,##0");
                    RunningOddsPlayerMultiplier = paramsModel.LeftPercentage != 0 ? paramsModel.LeftPercentage.ToString("#.#0") + "%" : "0%";
                    RunningOddBankerMultiplier = paramsModel.RightPercentage != 0 ? paramsModel.RightPercentage.ToString("#.#0") + "%" : "0%";
                    await UpdateOddsValue();
                    await CallInvoke();
                }
            }
        }
    }
    public async Task UpdateGameOdds(int _gameTypeId, decimal firstOddvalue, decimal secondOddValue)
    {
        if (_gameTypeId == GametypeId)
        {
            //if (disableUpdate == true) return;
            if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
            {
                lastBetUpdateReg = DateTime.Now.TimeOfDay;
                RunningOddsPlayerMultiplier = firstOddvalue != 0 ? firstOddvalue.ToString("#.#0") + "%" : "0%";
                RunningOddBankerMultiplier = secondOddValue != 0 ? secondOddValue.ToString("#.#0") + "%" : "0%";
            }
            await UpdateOddsValue();
            await CallInvoke();
        }
    }
    public async Task NotifySignalRReconnection()
    {
        await GetRoundBetSummary();
        await GetCurrentRoundBets();
    }
    #endregion
}