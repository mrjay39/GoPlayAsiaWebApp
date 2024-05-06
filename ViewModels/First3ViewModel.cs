using AutoMapper;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Main.Login; 
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.ViewModels;

public class First3ViewModel : BaseViewModel
{
    [Inject] protected IJSRuntime JsRuntime { get; set; }

    #region Local Variables & Properties


    #region Bets
    private string _userTotalBet_FixedPrizeGold = "0";
    private string _userTotalBet_FixedPrizeSilver = "0";
    private string _userTotalBet_OddsGold = "0";
    private string _userTotalBet_OddsSilver = "0";
    private string _userTotalBet_Draw = "0";
    private int _currentGameType = 0;
    public bool showDiv = false;
    public string tokenDiv = "tokenhide";
    public string _tokenAnimation = "";
    public string f3gif = "";
    public int BetType = 0;

    public int CurrentGameType
    {
        get => _currentGameType;
        set
        {
            _currentGameType = value;
            RaisePropertyChanged(() => CurrentGameType);
        }
    }

    public string UserTotalBet_FixedPrizeGold
    {
        get => _userTotalBet_FixedPrizeGold;
        set
        {
            _userTotalBet_FixedPrizeGold = value;
            RaisePropertyChanged(() => UserTotalBet_FixedPrizeGold);
        }
    }
    public string UserTotalBet_FixedPrizeSilver
    {
        get => _userTotalBet_FixedPrizeSilver;
        set
        {
            _userTotalBet_FixedPrizeSilver = value;
            RaisePropertyChanged(() => UserTotalBet_FixedPrizeSilver);
        }
    }
    public string UserTotalBet_OddsGold
    {
        get => _userTotalBet_OddsGold;
        set
        {
            _userTotalBet_OddsGold = value;
            RaisePropertyChanged(() => UserTotalBet_OddsGold);
        }
    }
    public string UserTotalBet_OddsSilver
    {
        get => _userTotalBet_OddsSilver;
        set
        {
            _userTotalBet_OddsSilver = value;
            RaisePropertyChanged(() => UserTotalBet_OddsSilver);
        }
    }
    public string UserTotalBet_Draw
    {
        get => _userTotalBet_Draw;
        set
        {
            _userTotalBet_Draw = value;
            RaisePropertyChanged(() => UserTotalBet_Draw);
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

    //START NEW Variant
    private string _userTotalBet_Trio = "0";
    private string _userTotalBet_SuitsGold = "0";
    private string _userTotalBet_SuitsSilver = "0";
    private string _userTotalBet_ColorRedGold = "0";
    private string _userTotalBet_ColorBlackGold = "0";
    private string _userTotalBet_ColorRedSilver = "0";
    private string _userTotalBet_ColorBlackSilver = "0";
    private string _userTotalBet_PairGold = "0";
    private string _userTotalBet_PairSilver = "0";

    public string UserTotalBet_Trio
    {
        get => _userTotalBet_Trio;
        set
        {
            _userTotalBet_Trio = value;
            RaisePropertyChanged(() => UserTotalBet_Trio);
        }
    }

    public string UserTotalBet_SuitsGold
    {
        get => _userTotalBet_SuitsGold;
        set
        {
            _userTotalBet_SuitsGold = value;
            RaisePropertyChanged(() => UserTotalBet_SuitsGold);
        }
    }
    public string UserTotalBet_SuitsSilver
    {
        get => _userTotalBet_SuitsSilver;
        set
        {
            _userTotalBet_SuitsSilver = value;
            RaisePropertyChanged(() => UserTotalBet_SuitsSilver);
        }
    }
    public string UserTotalBet_ColorRedGold
    {
        get => _userTotalBet_ColorRedGold;
        set
        {
            _userTotalBet_ColorRedGold = value;
            RaisePropertyChanged(() => UserTotalBet_ColorRedGold);
        }
    }
    public string UserTotalBet_ColorBlackGold
    {
        get => _userTotalBet_ColorBlackGold;
        set
        {
            _userTotalBet_ColorBlackGold = value;
            RaisePropertyChanged(() => UserTotalBet_ColorBlackGold);
        }
    }
    public string UserTotalBet_ColorRedSilver
    {
        get => _userTotalBet_ColorRedSilver;
        set
        {
            _userTotalBet_ColorRedSilver = value;
            RaisePropertyChanged(() => UserTotalBet_ColorRedSilver);
        }
    }
    public string UserTotalBet_ColorBlackSilver
    {
        get => _userTotalBet_ColorBlackSilver;
        set
        {
            _userTotalBet_ColorBlackSilver = value;
            RaisePropertyChanged(() => UserTotalBet_ColorBlackSilver);
        }
    }
    public string UserTotalBet_PairGold
    {
        get => _userTotalBet_PairGold;
        set
        {
            _userTotalBet_PairGold = value;
            RaisePropertyChanged(() => UserTotalBet_PairGold);
        }
    }
    public string UserTotalBet_PairSilver
    {
        get => _userTotalBet_PairSilver;
        set
        {
            _userTotalBet_PairSilver = value;
            RaisePropertyChanged(() => UserTotalBet_PairSilver);
        }
    }

    //END New Variant

    #endregion

    #region Game Configuration
    private bool _showTotalBets;
    private bool _isFixedGoldEnabled;
    private bool _isFixedSilverEnabled;
    private bool _isRunndingOddsGoldEnabled;
    private bool _isRunningOddsSilverEnabled;
    private bool _isDrawEnabled;

    private bool _isRunningOddsCancelled;
    private bool _isFixedPriceCancelled;
    public bool ShowTotalBets
    {
        get => _showTotalBets;
        set
        {
            _showTotalBets = value;
            RaisePropertyChanged(() => ShowTotalBets);
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
    public bool IsFixedPrizeCancelled
    {
        get => _isFixedPriceCancelled;
        set
        {
            _isFixedPriceCancelled = value;
            RaisePropertyChanged(() => _isFixedPriceCancelled);
        }
    }

    public bool IsFixedGoldEnabled
    {
        get => _isFixedGoldEnabled;
        set
        {
            _isFixedGoldEnabled = value;
            RaisePropertyChanged(() => IsFixedGoldEnabled);
        }
    }
    public bool IsFixedSilverEnabled
    {
        get => _isFixedSilverEnabled;
        set
        {
            _isFixedSilverEnabled = value;
            RaisePropertyChanged(() => IsFixedSilverEnabled);
        }
    }
    public bool IsRunndingOddsGoldEnabled
    {
        get => _isRunndingOddsGoldEnabled;
        set
        {
            _isRunndingOddsGoldEnabled = value;
            RaisePropertyChanged(() => IsRunndingOddsGoldEnabled);
        }
    }
    public bool IsRunningOddsSilverEnabled
    {
        get => _isRunningOddsSilverEnabled;
        set
        {
            _isRunningOddsSilverEnabled = value;
            RaisePropertyChanged(() => IsRunningOddsSilverEnabled);
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
    public bool isbuttonDisabled { get; set; } = false;
    #endregion


    #region Gold Bets
    private string _fixedPriceGoldTotalBets;
    private string _fixedPriceSilverTotalBets;
    private string _runningOddsGoldTotalBets;
    private string _runningOddSilverTotalBets;
    public string FixedPriceGoldTotalBets
    {
        get => _fixedPriceGoldTotalBets;
        set
        {
            _fixedPriceGoldTotalBets = value;
            RaisePropertyChanged(() => _fixedPriceGoldTotalBets);
        }
    }
    public string FixedPriceSilverTotalBets
    {
        get => _fixedPriceSilverTotalBets;
        set
        {
            _fixedPriceSilverTotalBets = value;
            RaisePropertyChanged(() => _fixedPriceSilverTotalBets);
        }
    }
    public string RunningOddsGoldTotalBets
    {
        get => _runningOddsGoldTotalBets;
        set
        {
            _runningOddsGoldTotalBets = value;
            RaisePropertyChanged(() => _runningOddsGoldTotalBets);
        }
    }
    public string RunningOddSilverTotalBets
    {
        get => _runningOddSilverTotalBets;
        set
        {
            _runningOddSilverTotalBets = value;
            RaisePropertyChanged(() => _runningOddSilverTotalBets);
        }
    }

    //START NEW Variant
    private string _suitsGoldTotalBets;
    private string _suitsSilverTotalBets;
    private string _colorRedGoldTotalBets;
    private string _colorBlackGoldTotalBets;
    private string _colorRedSilverTotalBets;
    private string _colorBlackSilverTotalBets;
    private string _pairGoldTotalBets;
    private string _pairSilverTotalBets;
    private string _trioTotalBets;

    public string TrioTotalBets
    {
        get => _trioTotalBets;
        set
        {
            _trioTotalBets = value;
            RaisePropertyChanged(() => TrioTotalBets);
        }
    }
    public string SuitsGoldTotalBets
    {
        get => _suitsGoldTotalBets;
        set
        {
            _suitsGoldTotalBets = value;
            RaisePropertyChanged(() => _suitsGoldTotalBets);
        }
    }
    public string SuitsSilverTotalBets
    {
        get => _suitsSilverTotalBets;
        set
        {
            _suitsSilverTotalBets = value;
            RaisePropertyChanged(() => _suitsSilverTotalBets);
        }
    }
    public string ColorRedGoldTotalBets
    {
        get => _colorRedGoldTotalBets;
        set
        {
            _colorRedGoldTotalBets = value;
            RaisePropertyChanged(() => _colorRedGoldTotalBets);
        }
    }
    public string ColorBlackGoldTotalBets
    {
        get => _colorBlackGoldTotalBets;
        set
        {
            _colorBlackGoldTotalBets = value;
            RaisePropertyChanged(() => _colorBlackGoldTotalBets);
        }
    }
    public string ColorRedSilverTotalBets
    {
        get => _colorRedSilverTotalBets;
        set
        {
            _colorRedSilverTotalBets = value;
            RaisePropertyChanged(() => _colorRedSilverTotalBets);
        }
    }
    public string ColorBlackSilverTotalBets
    {
        get => _colorBlackSilverTotalBets;
        set
        {
            _colorBlackSilverTotalBets = value;
            RaisePropertyChanged(() => _colorBlackSilverTotalBets);
        }
    }
    public string PairGoldTotalBets
    {
        get => _pairGoldTotalBets;
        set
        {
            _pairGoldTotalBets = value;
            RaisePropertyChanged(() => _pairGoldTotalBets);
        }
    }
    public string PairSilverTotalBets
    {
        get => _pairSilverTotalBets;
        set
        {
            _pairSilverTotalBets = value;
            RaisePropertyChanged(() => _pairSilverTotalBets);
        }
    }
    //END NEW Variant
    #endregion

    #region Multiplier
    private string _fixedPriceGoldMultiplier;
    private string _fixedPriceSilverMultipler;
    private string _runningOddsGoldMultiplier;
    private string _runningOddSilverMultiplier;
    public string FixedPriceGoldMultiplier
    {
        get => _fixedPriceGoldMultiplier;
        set
        {
            _fixedPriceGoldMultiplier = value;
            RaisePropertyChanged(() => FixedPriceGoldMultiplier);
        }
    }
    public string FixedPriceSilverMultipler
    {
        get => _fixedPriceSilverMultipler;
        set
        {
            _fixedPriceSilverMultipler = value;
            RaisePropertyChanged(() => FixedPriceSilverMultipler);
        }
    }
    public string RunningOddsGoldMultiplier
    {
        get => _runningOddsGoldMultiplier;
        set
        {
            _runningOddsGoldMultiplier = value;
            RaisePropertyChanged(() => RunningOddsGoldMultiplier);
        }
    }
    public string RunningOddSilverMultiplier
    {
        get => _runningOddSilverMultiplier;
        set
        {
            _runningOddSilverMultiplier = value;
            RaisePropertyChanged(() => RunningOddSilverMultiplier);
        }
    }
    #endregion

    #region Bet Winning
    private int CardValue1;
    private int CardValue2;
    private int CardValue3;
    private int CardValue4;
    private int CardValue5;
    private int CardValue6;



    private string _sampleBetDisplay;
    private string _sampleWinFixedPrizeGold = "0";
    private string _sampleWinFixedPrizeSilver = "0";
    private string _sampleWinOddsGold = "0";
    private string _sampleWinOddsSilver = "0";
    private string _sampleWinDraw = "0";

    public string GoldCardValue
    {
        get; set;
    }
    public string SilverCardValue
    {
        get; set;
    }

    public string SampleBetDisplay
    {
        get => _sampleBetDisplay;
        set
        {
            _sampleBetDisplay = value;
            RaisePropertyChanged(() => SampleBetDisplay);
        }
    }
    public string SampleWinFixedPrizeGold
    {
        get => _sampleWinFixedPrizeGold;
        set
        {
            _sampleWinFixedPrizeGold = value;
            RaisePropertyChanged(() => SampleWinFixedPrizeGold);
        }
    }
    public string SampleWinFixedPrizeSilver
    {
        get => _sampleWinFixedPrizeSilver;
        set
        {
            _sampleWinFixedPrizeSilver = value;
            RaisePropertyChanged(() => SampleWinFixedPrizeSilver);
        }
    }
    public string SampleWinOddsGold
    {
        get => _sampleWinOddsGold;
        set
        {
            _sampleWinOddsGold = value;
            RaisePropertyChanged(() => SampleWinOddsGold);
        }
    }
    public string SampleWinOddsSilver
    {
        get => _sampleWinOddsSilver;
        set
        {
            _sampleWinOddsSilver = value;
            RaisePropertyChanged(() => SampleWinOddsSilver);
        }
    }
    public string SampleWinDraw
    {
        get => _sampleWinDraw;
        set
        {
            _sampleWinDraw = value;
            RaisePropertyChanged(() => SampleWinDraw);
        }
    }

    //START NEW Variant
    private string _sampleWinTrio = "0";
    private string _sampleWinSuitsGold = "0";
    private string _sampleWinSuitsSilver = "0";
    private string _sampleWinColorRedGold = "0";
    private string _sampleWinColorBlackGold = "0";
    private string _sampleWinColorRedSilver = "0";
    private string _sampleWinColorBlackSilver = "0";
    private string _sampleWinPairGold = "0";
    private string _sampleWinPairSilver = "0";

    //game results cards




    public string SampleWinTrio
    {
        get => _sampleWinTrio;
        set
        {
            _sampleWinTrio = value;
            RaisePropertyChanged(() => SampleWinTrio);
        }
    }

    public string SampleWinSuitsGold
    {
        get => _sampleWinSuitsGold;
        set
        {
            _sampleWinSuitsGold = value;
            RaisePropertyChanged(() => SampleWinSuitsGold);
        }
    }
    public string SampleWinSuitsSilver
    {
        get => _sampleWinSuitsSilver;
        set
        {
            _sampleWinSuitsSilver = value;
            RaisePropertyChanged(() => SampleWinSuitsSilver);
        }
    }
    public string SampleWinColorRedGold
    {
        get => _sampleWinColorRedGold;
        set
        {
            _sampleWinColorRedGold = value;
            RaisePropertyChanged(() => SampleWinColorRedGold);
        }
    }
    public string SampleWinColorBlackGold
    {
        get => _sampleWinColorBlackGold;
        set
        {
            _sampleWinColorBlackGold = value;
            RaisePropertyChanged(() => SampleWinColorBlackGold);
        }
    }
    public string SampleWinColorRedSilver
    {
        get => _sampleWinColorRedSilver;
        set
        {
            _sampleWinColorRedSilver = value;
            RaisePropertyChanged(() => SampleWinColorRedSilver);
        }
    }
    public string SampleWinColorBlackSilver
    {
        get => _sampleWinColorBlackSilver;
        set
        {
            _sampleWinColorBlackSilver = value;
            RaisePropertyChanged(() => SampleWinColorBlackSilver);
        }
    }
    public string SampleWinPairGold
    {
        get => _sampleWinPairGold;
        set
        {
            _sampleWinPairGold = value;
            RaisePropertyChanged(() => SampleWinPairGold);
        }
    }
    public string SampleWinPairSilver
    {
        get => _sampleWinPairSilver;
        set
        {
            _sampleWinPairSilver = value;
            RaisePropertyChanged(() => SampleWinPairSilver);
        }
    }

    //END NEW Variant


    private bool _isPayoutWon_Gold;
    private bool _isPayoutWon_Silver;
    private bool _isOddsWon_Gold;
    private bool _isOddsWon_Silver;
    private bool _isDrawWon;
    public bool IsPayoutWon_Gold
    {
        get => _isPayoutWon_Gold;
        set
        {
            _isPayoutWon_Gold = value;
            RaisePropertyChanged(() => IsPayoutWon_Gold);
        }
    }
    public bool IsPayoutWon_Silver
    {
        get => _isPayoutWon_Silver;
        set
        {
            _isPayoutWon_Silver = value;
            RaisePropertyChanged(() => IsPayoutWon_Silver);
        }
    }
    public bool IsOddsWon_Gold
    {
        get => _isOddsWon_Gold;
        set
        {
            _isOddsWon_Gold = value;
            RaisePropertyChanged(() => IsOddsWon_Gold);
        }
    }
    public bool IsOddsWon_Silver
    {
        get => _isOddsWon_Silver;
        set
        {
            _isOddsWon_Silver = value;
            RaisePropertyChanged(() => IsOddsWon_Silver);
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

    #region Trends
    private ObservableCollection<TrendsDisplayModel> _payoutTrendsDisplay;
    private ObservableCollection<F3GameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _oddsTrendsDisplay;
    public ObservableCollection<F3GameRoundModel> Trends
    {
        get => _trends;
        set
        {
            _trends = value;
            RaisePropertyChanged(() => Trends);
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
        public ObservableCollection<F3GameRoundModel> CurrentList { get; set; }
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
    public static int payoutRowLimit = 10;
    public static int oddsRowLimit = 5;
    #endregion
    #endregion
    #endregion

    #region Life cycle methods
    public First3ViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService,
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

        StreamId = Constants.StreamIDFirs3;
        GametypeId = (int)GameTypes.Gold_And_Silver;
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
        tokenDiv = "block";
        await CallInvoke();
    }
    public async Task HideDivToken()
    {
        tokenDiv = "none";
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
                decimal DrawMultiplierValue = GameSetting.PayoutMultiplier != null ? Convert.ToDecimal(GameSetting.PayoutMultiplier) : 0;
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_Draw) ? decimal.Parse(UserTotalBet_Draw) + Amount : Amount;
                UserTotalBet_Draw = totalBets.ToString("#,##0");
                SampleWinDraw = totalBets != 0 ? (totalBets * DrawMultiplierValue).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateFixed_Gold_BetAmount(int GameType, decimal Amount)
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
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrizeGold) ? decimal.Parse(UserTotalBet_FixedPrizeGold) + Amount : Amount;
                UserTotalBet_FixedPrizeGold = totalBets.ToString("#,##0");
                SampleWinFixedPrizeGold = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateFixed_Silver_BetAmount(int GameType, decimal Amount)
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
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrizeSilver) ? decimal.Parse(UserTotalBet_FixedPrizeSilver) + Amount : Amount;
                UserTotalBet_FixedPrizeSilver = totalBets.ToString("#,##0");
                SampleWinFixedPrizeSilver = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                CurrentGameType = 0;
            }, cancellationToken);
        }
    }
    public async Task UpdateRunningOdds_Gold_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_OddsGold) ? decimal.Parse(UserTotalBet_OddsGold) + Amount : Amount;
            UserTotalBet_OddsGold = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_OddsGold);

            await UpdateOddsValue();
        }
    }
    public async Task UpdateRunningOdds_Silver_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_OddsSilver) ? decimal.Parse(UserTotalBet_OddsSilver) + Amount : Amount;
            UserTotalBet_OddsSilver = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_OddsSilver);

            await UpdateOddsValue();
        }
    }
    public async Task UpdateOddsValue()
    {
        decimal leftPercentage = decimal.Parse(RunningOddsGoldMultiplier.Replace("%", ""));
        decimal rightPercentage = decimal.Parse(RunningOddSilverMultiplier.Replace("%", ""));

        decimal userOddsGoldTotalBets = !string.IsNullOrEmpty(UserTotalBet_OddsGold) ? decimal.Parse(UserTotalBet_OddsGold) : 0;
        SampleWinOddsGold = userOddsGoldTotalBets != 0 ? (userOddsGoldTotalBets * (leftPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOddsGold);

        decimal userOddsSilverTotalBets = !string.IsNullOrEmpty(UserTotalBet_OddsSilver) ? decimal.Parse(UserTotalBet_OddsSilver) : 0;
        SampleWinOddsSilver = userOddsSilverTotalBets != 0 ? (userOddsSilverTotalBets * (rightPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOddsSilver);
        await CallInvoke();
    }
    public async Task GetCurrentRoundBets()
    {
        TotalBets = 0;
        if (F3GameRound.RoundStatus != (int)RoundStatus.Cancelled)
        {
            var tempRoundBets = await _igameRoundService.F3GetBets(_icurrentUser.Id, GametypeId, null);
            if (tempRoundBets != null)
            {
                F3UserBets = new ObservableCollection<F3BetModel>(tempRoundBets);
                foreach (var item in F3UserBets)
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
                    else if (item.GameVariantID == (int)GameVariant.F3Trio)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * F3Trio.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.F3Suits)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * F3Suits.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.F3Color)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * F3Color.FixedPriceMultiplier).ToString("N2");
                    }
                    TotalBets += Convert.ToInt32(item.BetAmount.Value);
                }
                await CallInvoke();
            }
        }
        else
        {
            F3UserBets = new ObservableCollection<F3BetModel>();
        }
    }
    public async Task AssignGameCardResult()
    {
        GameResult.LeftWinningTarget = F3GameRound.LeftWinningTarget;
        GameResult.RightWinningTarget = F3GameRound.RightWinningTarget;
        GameResult.LeftWinningSuits = F3GameRound.LeftWinningSuits;
        GameResult.RightWinningSuits = F3GameRound.RightWinningSuits;
        GameResult.LeftWinningColor = F3GameRound.LeftWinningColor;
        GameResult.RightWinningColor = F3GameRound.RightWinningColor;
        GameResult.LeftWinningPair = F3GameRound.LeftWinningPair;
        GameResult.RightWinningPair = F3GameRound.RightWinningPair;
        GameResult.WinningResult = F3GameRound.WinningResult;
        GameResult.TrioResult = F3GameRound.TrioResult;
        GameResult.ValACard1 = F3GameRound.ValACard1;
        GameResult.ValACard2 = F3GameRound.ValACard2;
        GameResult.ValACard3 = F3GameRound.ValACard3;
        GameResult.ValBCard1 = F3GameRound.ValBCard1;
        GameResult.ValBCard2 = F3GameRound.ValBCard2;
        GameResult.ValBCard3 = F3GameRound.ValBCard3;
        //await ComputeCardValues();

    }

    private async Task ComputeCardValues()
    {
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

        int Goldcard = 0;
        Goldcard = CardValue1 + CardValue2 + CardValue3;
        if (Goldcard > 20)
        {
            Goldcard = Goldcard - 20;
        }
        else if (Goldcard > 10)
        {
            Goldcard = Goldcard - 10;
        }
        GoldCardValue = Goldcard.ToString();

        int Silvercard = 0;
        Silvercard = CardValue4 + CardValue5 + CardValue6;
        if (Silvercard > 20)
        {
            Silvercard = Silvercard - 20;
        }
        else if (Silvercard > 10)
        {
            Silvercard = Silvercard - 10;
        }
        SilverCardValue = Silvercard.ToString();

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

    public async Task F3GetPreviousBets()
    {
        long UserId = _icurrentUser.Id;
        var tempRoundBets = await _igameRoundService.F3GetPrevGameBets(UserId, 5);
        if (tempRoundBets != null)
        {
            F3PreviousBets = new ObservableCollection<F3BetModel>(tempRoundBets);
            CallInvoke();
        }
    }


    public async Task GetGameRound()
    {
        //GameRound = await _igameRoundService.GetRound(GametypeId);
        F3GameRound = await _igameRoundService.F3GetRound(GametypeId);
        if (F3GameRound == null)
        {
            _toastService.ShowError("No round details found.");
            await GetGameSetting();
            return;
        }
        else if (F3GameRound != null)
        {
            if (F3GameRound.RoundNumber > 0)
            {
                RoundNumber = F3GameRound.RoundNumber;
            }

            FixedPriceGoldTotalBets = F3GameRound.FixedLeftBet;
            FixedPriceSilverTotalBets = F3GameRound.FixedRightBet;
            RunningOddsGoldTotalBets = F3GameRound.OddsLeftBet;
            RunningOddSilverTotalBets = F3GameRound.OddsRightBet;
            DrawTotalBets = F3GameRound.DrawBet;

            RunningOddsGoldMultiplier = F3GameRound.OddsLeftPercentage;
            RunningOddSilverMultiplier = F3GameRound.OddsRightPercentage;

            //new
            TrioTotalBets = F3GameRound.TrioBet;
            SuitsGoldTotalBets = F3GameRound.SuitsLeftBet;
            SuitsSilverTotalBets = F3GameRound.SuitsRightBet;
            ColorRedGoldTotalBets = F3GameRound.ColorRedLeftBet;
            ColorBlackGoldTotalBets = F3GameRound.ColorBlackLeftBet;
            ColorRedSilverTotalBets = F3GameRound.ColorRedRightBet;
            ColorBlackSilverTotalBets = F3GameRound.ColorBlackRightBet;
            PairGoldTotalBets = F3GameRound.PairLeftBet;
            PairSilverTotalBets = F3GameRound.PairRightBet;


            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            GameResultString = F3GameRound.WinningResult;
            switch (F3GameRound.RoundStatus)
            {
                case (int)RoundStatus.Open:
                    GameResult = new UpdateGameResultModel();
                    PreGameResult = new UpdateGameResultModel();
                    GameResultString = null;
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;



                    IsFixedGoldEnabled = true;
                    IsFixedSilverEnabled = true;
                    IsRunndingOddsGoldEnabled = true;
                    IsRunningOddsSilverEnabled = true;
                    IsDrawEnabled = true;

                    IsPayoutWon_Gold = false;
                    IsPayoutWon_Silver = false;
                    IsOddsWon_Gold = false;
                    IsOddsWon_Silver = false;
                    IsDrawWon = false;

                    GoldCardValue = "";
                    SilverCardValue = "";
                    break;

                case (int)RoundStatus.Closed:

                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";

                    IsFixedGoldEnabled = false;
                    IsFixedSilverEnabled = false;
                    IsRunndingOddsGoldEnabled = false;
                    IsRunningOddsSilverEnabled = false;
                    IsDrawEnabled = false;
                    IsRunningOddsCancelled = F3GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = F3GameRound.FixedCancelled;

                    if (F3GameRound.WinningResult == Constants.Gold)
                    {
                        if (!F3GameRound.FixedCancelled)
                        {
                            IsPayoutWon_Gold = true;
                        }

                        if (!F3GameRound.RunningOddsCancelled)
                        {
                            IsOddsWon_Gold = true;
                        }
                    }
                    else if (F3GameRound.WinningResult == Constants.Silver)
                    {
                        if (!F3GameRound.FixedCancelled)
                        {
                            IsPayoutWon_Silver = true;
                        }

                        if (!F3GameRound.RunningOddsCancelled)
                        {
                            IsOddsWon_Silver = true;
                        }
                    }
                    else if (F3GameRound.WinningResult == Constants.Draw)
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

                    IsFixedGoldEnabled = false;
                    IsFixedSilverEnabled = false;
                    IsRunndingOddsGoldEnabled = false;
                    IsRunningOddsSilverEnabled = false;
                    IsDrawEnabled = false;
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    IsRunningOddsCancelled = F3GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = F3GameRound.FixedCancelled;

                    break;

                case (int)RoundStatus.PendingResult:
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    IsRunningOddsCancelled = F3GameRound.RunningOddsCancelled;
                    IsFixedPrizeCancelled = F3GameRound.FixedCancelled;
                    IsFixedGoldEnabled = false;
                    IsFixedSilverEnabled = false;
                    IsRunndingOddsGoldEnabled = false;
                    IsRunningOddsSilverEnabled = false;
                    IsDrawEnabled = false;

                    //assign winning results
                    //assign winning results
                    //await AssignGameCardResult();
                    GoldCardValue = "";
                    SilverCardValue = "";
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
                FixedPriceGoldMultiplier = GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * 100).ToString("N0") + "%" : "%";
                FixedPriceSilverMultipler = GameSetting.FixedPriceMultiplier != null ? (Convert.ToDecimal(GameSetting.FixedPriceMultiplier) * 100).ToString("N0") + "%" : "%";
                RunningOddsGoldMultiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
                RunningOddSilverMultiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
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
            GameChips = F3GameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.F3Trio:
                    GameChips = _mapper.Map<GameChipModel>(F3TrioChips);
                    break;
                case (int)GameVariant.F3Color:
                    GameChips = _mapper.Map<GameChipModel>(F3ColorChips);
                    break;
                case (int)GameVariant.F3Suits:
                    GameChips = _mapper.Map<GameChipModel>(F3SuitsChips);
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
            if (item.GameVariantId == (int)GameVariant.F3Trio)
            {
                F3TrioChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.F3Suits)
            {
                F3SuitsChips = item;
            }
            if (item.GameVariantId == (int)GameVariant.F3Color)
            {
                F3ColorChips = item;
            }
        }

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
            if (item.GameVariantId == (int)GameVariant.F3Trio)
            {
                F3TrioChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.F3Suits)
            {
                F3SuitsChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.F3Color)
            {
                F3ColorChips = item;
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
                    if (item.GameVariantID == (int)GameVariant.F3Trio)
                    {
                        F3Trio = item;

                    }
                    if (item.GameVariantID == (int)GameVariant.F3Suits)
                    {
                        F3Suits = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.F3Color)
                    {
                        F3Color = item;
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
            IsFixedGoldEnabled = false;
            IsFixedSilverEnabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyOddsCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsRunningOddsCancelled = true;
            IsRunndingOddsGoldEnabled = false;
            IsRunningOddsSilverEnabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyFixedLeftOptions(int gameTypeId, bool disabled)
    {

        if (GametypeId == gameTypeId)
        {
            if (F3GameRound != null)
            {

                if (F3GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixedGoldEnabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
            //if (IsFixedGoldEnabled)
            //{ 
            //    IsFixedGoldEnabled = !arg.IsDisabled;
            //}
        }

    }
    public async Task NotifyFixedRightOptions(int gameTypeId, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            if (F3GameRound != null)
            {

                if (F3GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixedSilverEnabled = !disabled;
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
            F3BetDTO betParams = new F3BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = F3GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.BetAmount = BetAmount;
            betParams.BetValue = BetCombinationValue;
            betParams.GameVariantID = GameVariantId;
            var popupRes = popupModal.Show<PopupLoading>("");
            var tempBetResult = await _igameRoundService.F3BetOnRound(betParams);
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
                //showDiv = !showDiv;
                tokenDiv = "none";
                isbuttonDisabled = false;
                Token_Animation = "1s";
                BetType = 0;

                if (BetCombinationValue.ToUpper().Contains(Constants.Draw))
                {
                    await UpdateDrawBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedGold))
                {
                    await UpdateFixed_Gold_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedSilver))
                {
                    await UpdateFixed_Silver_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsGold))
                {
                    await UpdateRunningOdds_Gold_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsSilver))
                {
                    await UpdateRunningOdds_Silver_BetAmount(GametypeId, BetAmount);
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
    public async Task SetBetSelectedValue(object value)
    {
        BetAmount = (int)value;

        switch (BetType)
        {
            case (int)F3MainBetTypes.Gold:
            case (int)F3MainBetTypes.Silver:
                Payout = Convert.ToDecimal(BetAmount) * GameSetting.FixedPriceMultiplier.Value;
                break;
            case (int)F3SubBetTypes.Trio:
                Payout = Convert.ToDecimal(BetAmount) * F3Trio.FixedPriceMultiplier;
                break;
            case (int)F3SubBetTypes.TwoRedGold:
            case (int)F3SubBetTypes.TwoBlackGold:
            case (int)F3SubBetTypes.TwoRedSilver:
            case (int)F3SubBetTypes.TwoBlackSilver:
                Payout = Convert.ToDecimal(BetAmount) * F3Color.FixedPriceMultiplier;
                break;
            case (int)F3SubBetTypes.SameSuiteGold:
            case (int)F3SubBetTypes.SameSuiteSilver:
                Payout = Convert.ToDecimal(BetAmount) * F3Suits.FixedPriceMultiplier;
                break;
        }
    }
    public async Task<bool> ValidateBet(string betOptionSelected, string betCombinationValue)
    {
        bool isvalid = true;

        var isCrossBetting = F3IsCrossBetting(betCombinationValue, F3UserBets).Result;
        if (isCrossBetting)
        {
            _toastService.ShowError("Cross betting not allowed!");
            isvalid = false;
        }

        //check if divisible by 10
        var result = BetAmount % 5;
        if (result > 0)
        {
            _toastService.ShowError("Bet amount should be divisible by 5.");
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
                case (int)GameVariant.F3Trio:
                    if (BetAmount < F3Trio.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > F3Trio.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.F3Color:
                    if (BetAmount < F3Color.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > F3Color.MaximumBet)
                    {
                        _toastService.ShowError("Above Maximum Bet");
                        isvalid = false;
                    }
                    break;
                case (int)GameVariant.F3Suits:
                    if (BetAmount < F3Suits.MinimumBet)
                    {
                        _toastService.ShowError("Below Minimum Bet");
                        isvalid = false;
                    }
                    else if (BetAmount > F3Suits.MaximumBet)
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
    public async Task GetRoundBetSummary()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {
            var roundBetDetail = await _igameRoundService.F3GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
            if (roundBetDetail != null)
            {
                int pDelay = 0;
                if (Token_Animation != "")
                {
                    pDelay = 800;
                }

                Task.Delay(pDelay).ContinueWith(async (t) =>
                {
                    UserTotalBet_FixedPrizeGold = !string.IsNullOrEmpty(roundBetDetail.LeftFixedBet) ? roundBetDetail.LeftFixedBet : "0";
                    SampleWinFixedPrizeGold = roundBetDetail.LeftFixedWinnings;

                    UserTotalBet_FixedPrizeSilver = !string.IsNullOrEmpty(roundBetDetail.RightFixedBet) ? roundBetDetail.RightFixedBet : "0";
                    SampleWinFixedPrizeSilver = roundBetDetail.RightFixedWinnings;

                    UserTotalBet_OddsGold = !string.IsNullOrEmpty(roundBetDetail.LeftOddsBet) ? roundBetDetail.LeftOddsBet : "0";
                    SampleWinOddsGold = roundBetDetail.LeftOddsWinnings;

                    UserTotalBet_OddsSilver = !string.IsNullOrEmpty(roundBetDetail.RightOddsBet) ? roundBetDetail.RightOddsBet : "0";
                    SampleWinOddsSilver = roundBetDetail.RightOddsWinnings;

                    UserTotalBet_Draw = !string.IsNullOrEmpty(roundBetDetail.DrawBet) ? roundBetDetail.DrawBet : "0";
                    SampleWinDraw = roundBetDetail.DrawWinnings;

                    #region NEW VARIANT
                    UserTotalBet_Trio = !string.IsNullOrEmpty(roundBetDetail.TrioBet) ? roundBetDetail.TrioBet : "0";
                    SampleWinTrio = roundBetDetail.TrioBetWinnings;

                    UserTotalBet_SuitsGold = !string.IsNullOrEmpty(roundBetDetail.SuitsLeftBet) ? roundBetDetail.SuitsLeftBet : "0";
                    SampleWinSuitsGold = roundBetDetail.SuitsLeftBetWinnings;

                    UserTotalBet_SuitsSilver = !string.IsNullOrEmpty(roundBetDetail.SuitsRightBet) ? roundBetDetail.SuitsRightBet : "0";
                    SampleWinSuitsSilver = roundBetDetail.SuitsRightBetWinnings;

                    UserTotalBet_ColorRedGold = !string.IsNullOrEmpty(roundBetDetail.ColorRedLeftBet) ? roundBetDetail.ColorRedLeftBet : "0";
                    SampleWinColorRedGold = roundBetDetail.ColorRedLeftBetWinnings;

                    UserTotalBet_ColorBlackGold = !string.IsNullOrEmpty(roundBetDetail.ColorBlackLeftBet) ? roundBetDetail.ColorBlackLeftBet : "0";
                    SampleWinColorBlackGold = roundBetDetail.ColorBlackLeftBetWinnings;

                    UserTotalBet_ColorRedSilver = !string.IsNullOrEmpty(roundBetDetail.ColorRedRightBet) ? roundBetDetail.ColorRedRightBet : "0";
                    SampleWinColorRedSilver = roundBetDetail.ColorRedRightBetWinnings;

                    UserTotalBet_ColorBlackSilver = !string.IsNullOrEmpty(roundBetDetail.ColorBlackRightBet) ? roundBetDetail.ColorBlackRightBet : "0";
                    SampleWinColorBlackSilver = roundBetDetail.ColorBlackRightBetWinnings;

                    UserTotalBet_PairGold = !string.IsNullOrEmpty(roundBetDetail.PairLeftBet) ? roundBetDetail.PairLeftBet : "0";
                    SampleWinPairGold = roundBetDetail.PairLeftBetWinnings;

                    UserTotalBet_PairSilver = !string.IsNullOrEmpty(roundBetDetail.PairRightBet) ? roundBetDetail.PairRightBet : "0";
                    SampleWinPairSilver = roundBetDetail.PairRightBetWinnings;
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
        var tempTrends = await _igameRoundService.F3GetTrends();
        if (tempTrends != null)
        {
            await SetPayoutTrendsDisplay(tempTrends);
            await SetOddsTrendsDisplay(tempTrends);
            await CallInvoke();
        }
    }
    public async Task SetOddsTrendsDisplay(List<F3GameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<F3GameRoundModel> CurrentList = new ObservableCollection<F3GameRoundModel>();
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
                        Current.CurrentList = new ObservableCollection<F3GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<F3GameRoundModel>();

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
                    Current.CurrentList = new ObservableCollection<F3GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<F3GameRoundModel>();

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
    public async Task SetPayoutTrendsDisplay(List<F3GameRoundModel> tempTrends)
    {
        Trends = new ObservableCollection<F3GameRoundModel>(tempTrends.OrderBy(x => x.Id));
        var tempTrendList = new List<F3GameRoundModel>();
        var tempTrendListHolder = new TrendsDisplayModel();
        var tempTrendsForDisplay = new List<TrendsDisplayModel>();
        F3GameRoundModel current = new F3GameRoundModel();
        int count = 0;
        int indexCounter = 0;
        F3GameRoundModel lastItem = new F3GameRoundModel();
        F3GameRoundModel lastNonDrawGame = new F3GameRoundModel();
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
                    tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                    tempTrendsForDisplay.Add(tempTrendListHolder);

                    tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(excessList);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        var remaining = tempTrendList.OrderBy(x => x.Id).Skip(payoutRowLimit).ToList();
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(remaining);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }
                    else
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }

                    if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<F3GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<F3GameRoundModel>();
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

            if (GametypeId == gametypeId && F3GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                // Console.WriteLine(int.Parse(RoundTimer.Replace(":", "").TrimStart(new Char[] { '0' })));
                if (F3GameRound.RoundStatus == (int)RoundStatus.Open && timer < 10)
                {
                    ShowFlashing = "timerFlasher";
                }
                else
                {
                    ShowFlashing = "timerNotFlashing";
                }
                if (F3GameRound.RoundStatus == (int)RoundStatus.Closed && AwaitingGameRound == false)
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
                    //HubConnection.On<int, int, decimal>(Constants.NotifyNumberBets, NotifyNumberBets); not F3
                    //HubConnection.On<int, int, bool>(Constants.NotifyNumberOptions, NotifyNumberOptions); not F3
                    //HubConnection.On<int>(Constants.NotifyEnableAllNumbers, NotifyEnableAllNumbers); not F3


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
        decimal DrawMultiplierValue = Convert.ToDecimal(GameSetting.PayoutMultiplier);
        SampleBetDisplay = sampleBet.ToString("0");
        SampleWinFixedPrizeGold = (sampleBet * FixedPriceMultiplier).ToString("0");
        SampleWinFixedPrizeSilver = (sampleBet * FixedPriceMultiplier).ToString("0"); ;
        SampleWinOddsGold = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinOddsSilver = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinDraw = (sampleBet * DrawMultiplierValue).ToString("0");

        //if (GameRound.RoundStatus == (int)RoundStatus.Closed || GameRound.RoundStatus == (int)RoundStatus.Cancelled)
        //{ 
        FixedPriceGoldTotalBets = "0";
        FixedPriceSilverTotalBets = "0";
        RunningOddsGoldTotalBets = "0";
        RunningOddSilverTotalBets = "0";

        UserTotalBet_Draw = "0";
        UserTotalBet_FixedPrizeGold = "0";
        UserTotalBet_FixedPrizeSilver = "0";
        UserTotalBet_OddsGold = "0";
        UserTotalBet_OddsSilver = "0";

        RunningOddsGoldMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
        RunningOddSilverMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";

        //IsFixedGoldEnabled = false;
        //IsFixedSilverEnabled = false;
        //IsFixedPrizeCancelled = false;
        //IsRunningOddsCancelled = false;
        //}


    }
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus, long gameRoundId)
    {
        if (GametypeId == gametypeId)
        {
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
            await GetGameRound();
            if (gameStatus != (int)RoundStatus.Open)
            {
                TickerMessage = "";
                IsFixedGoldEnabled = false;
                IsFixedSilverEnabled = false;
                IsRunndingOddsGoldEnabled = false;
                IsRunningOddsSilverEnabled = false;
                IsDrawEnabled = false;
                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    f3gif = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    await LoadDefaultBetDetails();
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    f3gif = "/img/animation/test-closebet.gif";
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
                tokenDiv = "tokenhide";
                isbuttonDisabled = false;
            }
            else if (gameStatus == (int)RoundStatus.Open)
            {
                TotalBets = 0;

                IsFixedGoldEnabled = true;
                IsFixedSilverEnabled = true;
                IsRunndingOddsGoldEnabled = true;
                IsRunningOddsSilverEnabled = true;
                IsDrawEnabled = true;
                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;
                await LoadDefaultBetDetails();
                F3UserBets = new ObservableCollection<F3BetModel>();

                f3gif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            //switch (gameStatus)
            //{
            //    case (int)RoundStatus.Cancelled:

            //        IsFixedGoldEnabled = false;
            //        IsFixedSilverEnabled = false;
            //        IsRunndingOddsGoldEnabled = false;
            //        IsRunningOddsSilverEnabled = false;
            //        IsDrawEnabled = false;
            //        RoundStatusString = Constants.Cancelled;
            //        break;
            //    case (int)RoundStatus.PendingResult:

            //        IsFixedGoldEnabled = false;
            //        IsFixedSilverEnabled = false;
            //        IsRunndingOddsGoldEnabled = false;
            //        IsRunningOddsSilverEnabled = false;
            //        IsDrawEnabled = false;
            //        RoundStatusString = Constants.Closed;
            //        RoundStatusColor = Constants.GameClosedColor;
            //        break;
            //    case (int)RoundStatus.Open:
            //        RoundStatusString = Constants.Open;
            //        RoundStatusColor = Constants.GameOpenColor;
            //        IsFixedGoldEnabled = true;
            //        IsFixedSilverEnabled = true;
            //        IsRunndingOddsGoldEnabled = true;
            //        IsRunningOddsSilverEnabled = true;
            //        IsDrawEnabled = true;
            //        UserBets = new ObservableCollection<BetModel>();
            //        break;
            //    case (int)RoundStatus.Closed:
            //        RoundStatusString = Constants.Closed;
            //        RoundStatusColor = Constants.GameClosedColor;
            //        break;
            //    default:
            //        break;
            //}
            //await LoadDefaultBetDetails();
            await CallInvoke();

        }

    }
    public async Task UpdateBetValues(BetUpdatesModel paramsModel)
    {
        if (F3GameRound is null) return;
        if (paramsModel.GameTypeId == GametypeId)
        {
            if (F3GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                //if (disableUpdate == true) return;
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {

                    //Console.WriteLine(DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds);
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    DrawTotalBets = paramsModel.DrawBetValue.ToString("#,##0");
                    FixedPriceGoldTotalBets = paramsModel.FixedLeftBetValue.ToString("#,##0");
                    FixedPriceSilverTotalBets = paramsModel.FixedRightBetValue.ToString("#,##0");
                    RunningOddsGoldTotalBets = paramsModel.RunningLeftBetValue.ToString("#,##0");
                    RunningOddSilverTotalBets = paramsModel.RunningRightBetValue.ToString("#,##0");
                    RunningOddsGoldMultiplier = paramsModel.LeftPercentage != 0 ? paramsModel.LeftPercentage.ToString("#.#0") + "%" : "0%";
                    RunningOddSilverMultiplier = paramsModel.RightPercentage != 0 ? paramsModel.RightPercentage.ToString("#.#0") + "%" : "0%";
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
                RunningOddsGoldMultiplier = firstOddvalue != 0 ? firstOddvalue.ToString("#.#0") + "%" : "0%";
                RunningOddSilverMultiplier = secondOddValue != 0 ? secondOddValue.ToString("#.#0") + "%" : "0%";
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

