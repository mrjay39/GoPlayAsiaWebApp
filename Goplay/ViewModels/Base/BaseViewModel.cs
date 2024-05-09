using Blazored.Modal.Services;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using static GoplayasiaBlazor.Models.Constants.Settings;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using AutoMapper;
using Microsoft.AspNetCore.Components.Authorization;


namespace GoPlayAsiaWebApp.Goplay.ViewModels.Base;

public abstract class BaseViewModel
{
    #region Local variables

    #region SignalR Variables

    #region TimerUpdate
    public TimeSpan lastBetUpdateReg { get; set; }
    public TimeSpan lastBetUpdateRo { get; set; }
    public int BetsDisplayDelay { get; set; }
    //public bool disableUpdate { get; set; } = false;
    #endregion

    // main hub connections
    //public  HubConnection? HubConnection;
    public static HubConnection? HubConnection { get; set; }
    #endregion

    #region Injected Services
    public ICurrentUser _icurrentUser;
    public IAccountService _iaccountService;
    public IConfiguration _config;
    public IGameRoundService _igameRoundService;
    public IDiceGameRoundService _iDiceGameRoundService;
    public IGameSettingService _igameSettingService;
    public IToastService _toastService;
    public IPromotionService _promotionService;
    public NavigationManager _navigationManager;
    public AuthenticationStateProvider _AuthenticationStateProvider;
    public IConstantService _constantService;
    #endregion

    #region Player Bets

    // Bet Values
    private int _betAmount;
    private int _minChip;
    private int _maxChip;
    private int _totalBets;
    private decimal _payout;
    private string _betOptionSelected;
    private string _betCombinationValue;
    public CorporationSettingModel corporationSettings;

    public int BetAmount
    {
        get => _betAmount;
        set
        {
            _betAmount = value;
            RaisePropertyChanged(() => BetAmount);
        }
    }
    public int MinChip
    {
        get => _minChip;
        set
        {
            _minChip = value;
            RaisePropertyChanged(() => MinChip);
        }
    }
    public int MaxChip
    {
        get => _maxChip;
        set
        {
            _maxChip = value;
            RaisePropertyChanged(() => MaxChip);
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
    public decimal Payout
    {
        get => _payout;
        set
        {
            _payout = value;
            RaisePropertyChanged(() => Payout);
        }
    }
    public string BetOptionSelected
    {
        get => _betOptionSelected;
        set
        {
            _betOptionSelected = value;
            RaisePropertyChanged(() => BetOptionSelected);
        }
    }
    public string BetCombinationValue
    {
        get => _betCombinationValue;
        set
        {
            _betCombinationValue = value;
            RaisePropertyChanged(() => BetAmount);
        }
    }


    private ObservableCollection<BetModel> _userBets;
    public ObservableCollection<BetModel> UserBets
    {
        get => _userBets;
        set
        {
            _userBets = value;
            RaisePropertyChanged(() => UserBets);
        }
    }

    private ObservableCollection<L9BetModel> _l9userBets;
    public ObservableCollection<L9BetModel> L9UserBets
    {
        get => _l9userBets;
        set
        {
            _l9userBets = value;
            RaisePropertyChanged(() => L9UserBets);
        }
    }
    private ObservableCollection<F3BetModel> _f3userBets;
    public ObservableCollection<F3BetModel> F3UserBets
    {
        get => _f3userBets;
        set
        {
            _f3userBets = value;
            RaisePropertyChanged(() => F3UserBets);
        }
    }

    private ObservableCollection<DiceBetModel> _diceuserBets;
    public ObservableCollection<DiceBetModel> DiceUserBets
    {
        get => _diceuserBets;
        set
        {
            _diceuserBets = value;
            RaisePropertyChanged(() => DiceUserBets);
        }
    }

    private ObservableCollection<BetModel> _previousBets = new ObservableCollection<BetModel>();
    public ObservableCollection<BetModel> PreviousBets
    {
        get => _previousBets;
        set
        {
            _previousBets = value;
            RaisePropertyChanged(() => PreviousBets);
        }
    }
    private ObservableCollection<L9BetModel> _l9previousBets = new ObservableCollection<L9BetModel>();
    public ObservableCollection<L9BetModel> L9PreviousBets
    {
        get => _l9previousBets;
        set
        {
            _l9previousBets = value;
            RaisePropertyChanged(() => L9PreviousBets);
        }
    }
    private ObservableCollection<F3BetModel> _f3previousBets = new ObservableCollection<F3BetModel>();
    public ObservableCollection<F3BetModel> F3PreviousBets
    {
        get => _f3previousBets;
        set
        {
            _f3previousBets = value;
            RaisePropertyChanged(() => F3PreviousBets);
        }
    }
    #endregion

    #region Player Bets
    public int prevBetsCount { get; set; } = 5;
    private string _drawTotalBets = "0";
    public string DrawTotalBets
    {
        get => _drawTotalBets;
        set
        {
            _drawTotalBets = value;
            RaisePropertyChanged(() => DrawTotalBets);
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

    #endregion

    #region Global Popups
    public IModalService popupModal { get; set; }
    public IModalReference popupBet { get; set; }
    public IModalReference popupLoading { get; set; }
    public IModalReference popupConfirm { get; set; }
    #endregion

    #region Notify Blazor Properties updated
    public event Action Notify;


    #endregion

    #region UI Variables for Display
    public DateTime lastExecution = DateTime.Now;

    private string? _roundTimer = "...";
    private string _showFlashing = "timerNotFlashing";
    private string? _roundStatusString;
    private string _gameResultString;
    private UpdateGameResultModel _gameResult = new UpdateGameResultModel();
    private UpdateGameResultModel _pregameResult = new UpdateGameResultModel();
    private string _roundStatusColor;
    private string _drawMultiplier;
    private GameRoundModel _gameRound;
    public bool AwaitingGameRound { get; set; } = false;

    public string TickerMessage { get; set; } = "";

    public string? RoundStatusString
    {
        get => _roundStatusString;
        set
        {
            _roundStatusString = value;

            RaisePropertyChanged(() => _roundStatusString);
        }
    }
    public string RoundStatusColor
    {
        get => _roundStatusColor;
        set
        {
            _roundStatusColor = value;
            RaisePropertyChanged(() => _roundStatusColor);
        }
    }
    public string? ShowFlashing
    {
        get => _showFlashing;
        set
        {
            _showFlashing = value;
            RaisePropertyChanged(() => _showFlashing);
        }
    }
    public string? GameResultString
    {
        get => _gameResultString;
        set
        {
            _gameResultString = value;
            RaisePropertyChanged(() => _gameResultString);
        }
    }

    public UpdateGameResultModel GameResult
    {
        get => _gameResult;
        set
        {
            _gameResult = value;
            RaisePropertyChanged(() => _gameResult);
        }
    }

    public string? RoundTimer
    {
        get => _roundTimer;
        set
        {
            _roundTimer = value;
            RaisePropertyChanged(() => _roundTimer);
        }
    }
    public GameRoundModel GameRound
    {
        get => _gameRound;
        set
        {
            _gameRound = value;
            RaisePropertyChanged(() => _gameRound);
        }
    }
    public string DrawMultiplier
    {
        get => _drawMultiplier;
        set
        {
            _drawMultiplier = value;
            RaisePropertyChanged(() => DrawMultiplier);
        }
    }

    #region GAME ROUND MODEL
    private L9GameRoundModel _l9gameRound;
    private F3GameRoundModel _f3gameRound;
    private DiceGameRoundModel _diceGameRound;

    public L9GameRoundModel L9GameRound
    {
        get => _l9gameRound;
        set
        {
            _l9gameRound = value;
            RaisePropertyChanged(() => _l9gameRound);
        }
    }
    public F3GameRoundModel F3GameRound
    {
        get => _f3gameRound;
        set
        {
            _f3gameRound = value;
            RaisePropertyChanged(() => _f3gameRound);
        }
    }
    public DiceGameRoundModel DiceGameRound
    {
        get => _diceGameRound;
        set
        {
            _diceGameRound = value;
            RaisePropertyChanged(() => DiceGameRound);
        }
    }
    #endregion

    public UpdateGameResultModel PreGameResult
    {
        get => _pregameResult;
        set
        {
            _pregameResult = value;
            RaisePropertyChanged(() => _pregameResult);
        }
    }


    #endregion

    public string streamKey
    {
        get; set;
    } =
    string.Empty;
    #region Current Game Variables
    public IMapper _mapper;
    private int _gametypeId;
    private int _gamevariantId;
    private int _roundNumber;
    private string _minBet;
    private string _maxBet;

    private string? _streamId = "";
    private string? _streamURL = "";
    public string? StreamURL
    {
        get => _streamURL;
        set
        {
            _streamURL = value;
            RaisePropertyChanged(() => _streamURL);
        }
    }
    public string? StreamId
    {
        get => _streamId;
        set
        {
            _streamId = value;
            RaisePropertyChanged(() => _streamId);
        }
    }
    public int GametypeId
    {
        get => _gametypeId;
        set
        {
            _gametypeId = value;
            RaisePropertyChanged(() => _gametypeId);
        }
    }
    public int GameVariantId
    {
        get => _gamevariantId;
        set
        {
            _gamevariantId = value;
            RaisePropertyChanged(() => GameVariantId);
        }
    }
    public int RoundNumber
    {
        get => _roundNumber;
        set
        {
            _roundNumber = value;
            RaisePropertyChanged(() => _roundNumber);
        }
    }
    public string MinBet
    {
        get => _minBet;
        set
        {
            _minBet = value;
            RaisePropertyChanged(() => MinBet);
        }
    }
    public string MaxBet
    {
        get => _maxBet;
        set
        {
            _maxBet = value;
            RaisePropertyChanged(() => MaxBet);
        }
    }


    #endregion

    #endregion

    #region Object Properties
    private GameChipModel _gameChips;
    private GameChipModel _L9gameChips;
    private GameChipModel _F3gameChips;
    private GameChipModel _Go12gameChips;
    private GameChipModel _dicegameChips;
    private GameSettingModel _gameSetting;
    private GameVariantModel _gameSettingVariant;
    private GameRoundModel _prevGameRound;
    private ObservableCollection<PlayerCategoryModel> _playercategory;

    public GameSettingModel GameSetting
    {
        get => _gameSetting;
        set
        {
            _gameSetting = value;
            RaisePropertyChanged(() => GameSetting);
        }
    }
    public GameChipModel GameChips
    {
        get => _gameChips;
        set
        {
            _gameChips = value;
            RaisePropertyChanged(() => GameChips);
        }
    }
    public GameChipModel L9GameChips
    {
        get => _L9gameChips;
        set
        {
            _L9gameChips = value;
            RaisePropertyChanged(() => L9GameChips);
        }
    }
    public GameChipModel F3GameChips
    {
        get => _F3gameChips;
        set
        {
            _F3gameChips = value;
            RaisePropertyChanged(() => F3GameChips);
        }
    }
    public GameChipModel Go12GameChips
    {
        get => _Go12gameChips;
        set
        {
            _Go12gameChips = value;
            RaisePropertyChanged(() => Go12GameChips);
        }
    }
    public GameChipModel DiceGameChips
    {
        get => _dicegameChips;
        set
        {
            _dicegameChips = value;
            RaisePropertyChanged(() => DiceGameChips);
        }
    }
    public ObservableCollection<PlayerCategoryModel> PlayerCategory
    {
        get => _playercategory;
        set
        {
            _playercategory = value;
            RaisePropertyChanged(() => PlayerCategory);
        }
    }

    #region L9 Game Variant Settings
    // L9 Game Variant Settings
    public GameVariantModel GameSettingVariant
    {
        get => _gameSettingVariant;
        set
        {
            _gameSettingVariant = value;
            RaisePropertyChanged(() => GameSettingVariant);
        }
    }

    private GameVariantModel _l9Target;
    public GameVariantModel L9Target
    {
        get => _l9Target;
        set
        {
            _l9Target = value;
            RaisePropertyChanged(() => L9Target);
        }
    }

    private GameVariantModel _l9Suits;
    public GameVariantModel L9Suits
    {
        get => _l9Suits;
        set
        {
            _l9Suits = value;
            RaisePropertyChanged(() => L9Suits);
        }
    }

    private GameVariantModel _l9Color;
    public GameVariantModel L9Color
    {
        get => _l9Color;
        set
        {
            _l9Color = value;
            RaisePropertyChanged(() => L9Color);
        }
    }

    private GameVariantModel _l9Pair;
    public GameVariantModel L9Pair
    {
        get => _l9Pair;
        set
        {
            _l9Pair = value;
            RaisePropertyChanged(() => L9Pair);
        }
    }

    private GameVariantModel _l9Draw;
    public GameVariantModel L9Draw
    {
        get => _l9Draw;
        set
        {
            _l9Draw = value;
            RaisePropertyChanged(() => L9Draw);
        }
    }
    #endregion

    #region F3 Game Variant Settings
    // F3 Game Variant Settings
    private GameVariantModel _f3Trio;
    public GameVariantModel F3Trio
    {
        get => _f3Trio;
        set
        {
            _f3Trio = value;
            RaisePropertyChanged(() => F3Trio);
        }
    }

    private GameVariantModel _f3Suits;
    public GameVariantModel F3Suits
    {
        get => _f3Suits;
        set
        {
            _f3Suits = value;
            RaisePropertyChanged(() => F3Suits);
        }
    }

    private GameVariantModel _f3Color;
    public GameVariantModel F3Color
    {
        get => _f3Color;
        set
        {
            _f3Color = value;
            RaisePropertyChanged(() => F3Color);
        }
    }

    private GameVariantModel _f3Pair;
    public GameVariantModel F3Pair
    {
        get => _f3Pair;
        set
        {
            _f3Pair = value;
            RaisePropertyChanged(() => F3Pair);
        }
    }
    #endregion

    #region Dice Game Variant Settings
    private GameVariantModel _diceOddEven;
    private GameVariantModel _diceSmallBig;
    private GameVariantModel _diceSingle;
    private GameVariantModel _diceNumber;


    public GameVariantModel DiceOddEven
    {
        get => _diceOddEven;
        set
        {
            _diceOddEven = value;
            RaisePropertyChanged(() => DiceOddEven);
        }
    }
    public GameVariantModel DiceSmallBig
    {
        get => _diceSmallBig;
        set
        {
            _diceSmallBig = value;
            RaisePropertyChanged(() => DiceSmallBig);
        }
    }
    public GameVariantModel DiceSingle
    {
        get => _diceSingle;
        set
        {
            _diceSingle = value;
            RaisePropertyChanged(() => DiceSingle);
        }
    }
    public GameVariantModel DiceNumber
    {
        get => _diceNumber;
        set
        {
            _diceNumber = value;
            RaisePropertyChanged(() => DiceNumber);
        }
    }
    #endregion

    #region L9 Game Variant Chips
    // L9 Game Variant Chips
    private GameVariantChipsModel _l9TargetChips;
    public GameVariantChipsModel L9TargetChips
    {
        get => _l9TargetChips;
        set
        {
            _l9TargetChips = value;
            RaisePropertyChanged(() => L9TargetChips);
        }
    }

    private GameVariantChipsModel _l9SuitsChips;
    public GameVariantChipsModel L9SuitsChips
    {
        get => _l9SuitsChips;
        set
        {
            _l9SuitsChips = value;
            RaisePropertyChanged(() => L9SuitsChips);
        }
    }

    private GameVariantChipsModel _l9ColorChips;
    public GameVariantChipsModel L9ColorChips
    {
        get => _l9ColorChips;
        set
        {
            _l9ColorChips = value;
            RaisePropertyChanged(() => L9ColorChips);
        }
    }

    private GameVariantChipsModel _l9PairChips;
    public GameVariantChipsModel L9PairChips
    {
        get => _l9PairChips;
        set
        {
            _l9PairChips = value;
            RaisePropertyChanged(() => L9PairChips);
        }
    }

    private GameVariantChipsModel _l9DrawChips;
    public GameVariantChipsModel L9DrawChips
    {
        get => _l9DrawChips;
        set
        {
            _l9DrawChips = value;
            RaisePropertyChanged(() => L9DrawChips);
        }
    }
    #endregion

    #region F3 Game Variant Chips
    // F3 Game Variant Chips
    private GameVariantChipsModel _f3TrioChips;
    public GameVariantChipsModel F3TrioChips
    {
        get => _f3TrioChips;
        set
        {
            _f3TrioChips = value;
            RaisePropertyChanged(() => F3TrioChips);
        }
    }

    private GameVariantChipsModel _f3SuitsChips;
    public GameVariantChipsModel F3SuitsChips
    {
        get => _f3SuitsChips;
        set
        {
            _f3SuitsChips = value;
            RaisePropertyChanged(() => F3SuitsChips);
        }
    }

    private GameVariantChipsModel _f3ColorChips;
    public GameVariantChipsModel F3ColorChips
    {
        get => _f3ColorChips;
        set
        {
            _f3ColorChips = value;
            RaisePropertyChanged(() => F3ColorChips);
        }
    }

    private GameVariantChipsModel _f3PairChips;
    public GameVariantChipsModel F3PairChips
    {
        get => _f3PairChips;
        set
        {
            _f3PairChips = value;
            RaisePropertyChanged(() => F3PairChips);
        }
    }
    #endregion

    #region L4 Game Variant Chips
    private GameVariantChipsModel _l4Pick2Chips;
    public GameVariantChipsModel L4Pick2Chips
    {
        get => _l4Pick2Chips;
        set
        {
            _l4Pick2Chips = value;
            RaisePropertyChanged(() => L4Pick2Chips);
        }
    }

    private GameVariantChipsModel _l4Pick3Chips;
    public GameVariantChipsModel L4Pick3Chips
    {
        get => _l4Pick3Chips;
        set
        {
            _l4Pick3Chips = value;
            RaisePropertyChanged(() => L4Pick3Chips);
        }
    }

    private GameVariantChipsModel _l4All4Chips;
    public GameVariantChipsModel L4All4Chips
    {
        get => _l4All4Chips;
        set
        {
            _l4All4Chips = value;
            RaisePropertyChanged(() => L4All4Chips);
        }
    }
    #endregion

    #region Dice Game Variant Chips
    private GameVariantChipsModel _diceOddEvenChips;
    private GameVariantChipsModel _diceSmallBigChips;
    private GameVariantChipsModel _diceSingleChips;
    private GameVariantChipsModel _diceNumberChips;

    public GameVariantChipsModel DiceOddEvenChips
    {
        get => _diceOddEvenChips;
        set
        {
            _diceOddEvenChips = value;
            RaisePropertyChanged(() => DiceOddEvenChips);
        }
    }
    public GameVariantChipsModel DiceSmallBigChips
    {
        get => _diceSmallBigChips;
        set
        {
            _diceSmallBigChips = value;
            RaisePropertyChanged(() => DiceSmallBigChips);
        }
    }
    public GameVariantChipsModel DiceSingleChips
    {
        get => _diceSingleChips;
        set
        {
            _diceSingleChips = value;
            RaisePropertyChanged(() => DiceSingleChips);
        }
    }
    public GameVariantChipsModel DiceNumberChips
    {
        get => _diceNumberChips;
        set
        {
            _diceNumberChips = value;
            RaisePropertyChanged(() => DiceNumberChips);
        }
    }
    #endregion

    #endregion

    #region Life cycle methods
    public BaseViewModel()
    {
        lastBetUpdateReg = DateTime.Now.TimeOfDay;
        lastBetUpdateRo = DateTime.Now.TimeOfDay;
        //disableUpdate = false;
        lastExecution = DateTime.Now;
    }
    public void RaisePropertyChanged<T>(Expression<Func<T>> property)
    {
        //if (Notify != null)
        //{
        //    if(GameRound != null)
        //    {
        //        if (GameRound.RoundStatus != (int)RoundStatus.Open)
        //        {
        //            Notify?.Invoke();
        //            lastExecution = DateTime.Now;
        //            return;
        //        }
        //    }          
        //    if ((DateTime.Now - lastExecution).TotalSeconds > 1 )
        //    {
        //        Notify?.Invoke();
        //        lastExecution = DateTime.Now;
        //    }
        //}

    }
    public async Task CallInvoke()
    {
        Notify?.Invoke();
    }
    #endregion

    #region Shared Methods
    public async Task<bool> IsCrossBetting(string betCombinationValue, ObservableCollection<BetModel> UserBets)
    {
        bool result = false;
        try
        {
            if (GametypeId == (int)GameTypes.Lucky_9)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.FixedPlayer)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedBanker).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsBanker).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.FixedBanker)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedPlayer).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsPlayer).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsPlayer)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsBanker).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsfixed = UserBets.Where(x => x.BetValue == Constants.FixedBanker).FirstOrDefault();
                        if (betsfixed != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.RunningOddsBanker)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsPlayer).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.FixedPlayer).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                }
            }
            else if (GametypeId == (int)GameTypes.Gold_And_Silver)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.FixedGold)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedSilver).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsSilver).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.FixedSilver)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedGold).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsGold).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsGold)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsSilver).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.FixedSilver).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsSilver)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsGold).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.FixedGold).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                }
            }
            else if (GametypeId == (int)GameTypes.Heads_And_Tails)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.FixedHeads)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedTails).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsTails).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.FixedTails)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedHeads).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsHeads).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsHeads)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsTails).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsfixed = UserBets.Where(x => x.BetValue == Constants.FixedTails).FirstOrDefault();
                        if (betsfixed != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsTails)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsHeads).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsfixed = UserBets.Where(x => x.BetValue == Constants.FixedHeads).FirstOrDefault();
                        if (betsfixed != null)
                        {
                            result = true;
                        }
                    }
                }
            }
            else if (GametypeId == (int)GameTypes.Go_12)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.Black)
                    {
                        var bets = UserBets.Where(x => x.BetValue.ToLower().Contains(Constants.Red.ToLower())).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.Red)
                    {
                        var bets = UserBets.Where(x => x.BetValue.ToLower().Contains(Constants.Black.ToLower())).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                    }

                    if (betCombinationValue == Constants.FixedBlack)
                    {
                        var bets = UserBets.Where(x => x.BetValue.ToLower().Contains(Constants.FixedRed.ToLower())).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.FixedRed)
                    {
                        var bets = UserBets.Where(x => x.BetValue.ToLower().Contains(Constants.FixedBlack.ToLower())).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
        return result;
    }
    public async Task<bool> L9IsCrossBetting(string betCombinationValue, ObservableCollection<L9BetModel> UserBets)
    {
        bool result = false;
        try
        {
            if (GametypeId == (int)GameTypes.Lucky_9)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.FixedPlayer)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedBanker).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsBanker).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.FixedBanker)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedPlayer).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsPlayer).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsPlayer)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsBanker).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsfixed = UserBets.Where(x => x.BetValue == Constants.FixedBanker).FirstOrDefault();
                        if (betsfixed != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.RunningOddsBanker)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsPlayer).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.FixedPlayer).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
        return result;
    }
    public async Task<bool> F3IsCrossBetting(string betCombinationValue, ObservableCollection<F3BetModel> UserBets)
    {
        bool result = false;
        try
        {
            if (GametypeId == (int)GameTypes.Gold_And_Silver)
            {
                if (UserBets != null && UserBets.Count > 0)
                {
                    if (betCombinationValue == Constants.FixedGold)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedSilver).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsSilver).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.FixedSilver)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.FixedGold).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.RunningOddsGold).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                    else if (betCombinationValue == Constants.RunningOddsGold)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsSilver).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsfixed = UserBets.Where(x => x.BetValue == Constants.FixedSilver).FirstOrDefault();
                        if (betsfixed != null)
                        {
                            result = true;
                        }

                    }
                    else if (betCombinationValue == Constants.RunningOddsSilver)
                    {
                        var bets = UserBets.Where(x => x.BetValue == Constants.RunningOddsGold).FirstOrDefault();
                        if (bets != null)
                        {
                            result = true;
                        }
                        var betsro = UserBets.Where(x => x.BetValue == Constants.FixedGold).FirstOrDefault();
                        if (betsro != null)
                        {
                            result = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
        return result;
    }
    public async Task GetCurrentTokenValue()
    {
        try
        {
            if (_icurrentUser != null)
            {
                var currentToken = await _iaccountService.GetUserCurrency(_icurrentUser.Id);
                if (currentToken > 0)
                {
                    if (_icurrentUser != null)
                    {
                        _icurrentUser.Credits = currentToken;
                        _icurrentUser.CreditsDisp = currentToken.ToString("N2");
                        _icurrentUser.updateSessionAsync();
                    }

                }

            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task Logout()
    {
        try
        {
            await ((CustomAuthStateProvider)_AuthenticationStateProvider).MarkUserAsLoggedOut();
            if (HubConnection is not null)
            {
                await DisconnectSignalR();

            }
            await _icurrentUser.clearSessionAsync();
            await _iaccountService.Logout();
            _navigationManager.NavigateTo("/");
            _toastService.ShowInfo("You have been logged out...");

            //it reload the page to reflect updated authentication state
            _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);

        }
        catch (Exception)
        {

        }

        //_navigationManager.NavigateTo("/");
    }
    #endregion

    #region SignalR

    public async Task NotifyForceLogout(long userId, string username)
    {
        //&& _icurrentUser.Id != 10153
        if (_icurrentUser.Id == userId)
        {
            await Logout();
            Notify?.Invoke();
            Notify?.Invoke();
        }
    }
    public async Task UpdateTokenValue(long userId, decimal token)
    {
        if (_icurrentUser.Id == userId)
        {
            _icurrentUser.CreditsDisp = token > 0 ? token.ToString("#,###.#0") : "0";
            _icurrentUser.Credits = token;
            _icurrentUser.updateSessionAsync();
            Notify?.Invoke();
        }
    }
    public void UpdateNotificationBadgeCount(string userId, int count)
    {
        if (_icurrentUser.Id == long.Parse(userId))
        {
            _icurrentUser.NotificationCount = count.ToString();
        }
        Notify?.Invoke();
    }

    public void UpdateNotificationBadge(string userId, string message)
    {
        if (_icurrentUser.Id == long.Parse(userId))
        {

        }

    }
    public async Task GameWinner(UpdateGameWinnerModel gameWinner)
    {
        try
        {

            var options = new ModalOptions()
            {
                Position = ModalPosition.TopCenter
            };

            var parameters = new ModalParameters();
            parameters.Add("GameWinner", gameWinner);
            //disableUpdate = true;
            var popupwin = popupModal.Show<PopupGameWinner>("", parameters, options);
            Notify?.Invoke();
            await Task.Delay(3000);
            Notify?.Invoke();
            popupwin.Close();
            //disableUpdate = false;

        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        //show winner
    }
    public async Task UpdateGameWinners(UpdateGameWinnerModel paramsModel)
    {
        TickerMessage = "G# " + paramsModel.RoundNumber + " Pay - " + paramsModel.AmountWonString;
        if (_icurrentUser.Id == paramsModel.UserId && GametypeId != paramsModel.GameTypeId)
        {

            await GameWinner(paramsModel);
            //if (paramsModel.GameTypeId != (int)GameTypes.Giga_Draw)
            //{
            //    winnerCounter++;
            //    if (roundNumber != paramsModel.RoundNo)
            //    {
            //        GametypeId = paramsModel.GameTypeId;
            //        RoundNumber = paramsModel.RoundNo;
            //        winnerCounter = 0;
            //    }
            //    else if (RoundNumber == paramsModel.RoundNo && GametypeId != paramsModel.GameTypeId)
            //    {
            //        gameType = paramsModel.GameTypeId;
            //        roundNumber = paramsModel.RoundNo;
            //        winnerCounter = 0;
            //    }

            //    if (winnerCounter == 0)
            //{
            //        Task.Run(async () => await GameWinner(paramsModel));
            //    }
            //}
            //else
            //{
            //    Task.Run(async () => await GameWinner(paramsModel));
            //}

        }
    }
    public virtual void UpdateGameTimer(int gametypeId, string value)
    {
        try
        {

            if (GametypeId == gametypeId && GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                // Console.WriteLine(int.Parse(RoundTimer.Replace(":", "").TrimStart(new Char[] { '0' })));
                if (GameRound.RoundStatus == (int)RoundStatus.Open && timer < 10)
                {
                    ShowFlashing = "timerFlasher";
                }
                else
                {
                    ShowFlashing = "timerNotFlashing";
                }
                Notify?.Invoke();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }

    }
    public async Task DisconnectSignalR()
    {
        try
        {
            if (HubConnection != null)
            {
                if (HubConnection.State == HubConnectionState.Connected)
                {
                    await HubConnection.InvokeAsync("RemoveIdToConnection", HubConnection.ConnectionId, _icurrentUser.Id);
                }
                try
                {
                    await HubConnection.StopAsync();
                }
                finally
                {
                    if (HubConnection != null)
                        await HubConnection.DisposeAsync();
                    HubConnection = null;
                }

                //await HubConnection.DisposeAsync();

            }

        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task ConnectSignalR()
    {
        bool isLogin = _icurrentUser.Token != null ? true : false;
        //connect signal R
        try
        {
            if (!string.IsNullOrEmpty(_icurrentUser.Token))
            {
                //if (HubConnection != null)
                //{
                //    await HubConnection.DisposeAsync();
                //}
                if (HubConnection == null)
                {
                    //|| HubConnection .State == HubConnectionState.Disconnected
                    HubConnection = null;

                    string baseURL = _config.GetValue<string>("API") + _config.GetValue<string>("SignalRHub");

                    HubConnection = new HubConnectionBuilder().WithUrl(baseURL, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_icurrentUser.Token);
                        //**PRD * * //
                        //options.SkipNegotiation = true;
                        //options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                        //**PRD * * //

                        ////** UAT ** //
                        options.SkipNegotiation = false;
                        ////** UAT ** //

                    }).WithAutomaticReconnect().AddMessagePackProtocol().Build();
                    await HubConnection.StartAsync();
                    await HubConnection.InvokeAsync("AddIdToConnection", HubConnection.ConnectionId, _icurrentUser.Id);
                    //Console.WriteLine(HubConnection.ConnectionId + "userid"+ _icurrentUser.Id);
                    await HubConnection.InvokeAsync("AddConnectionToGame", HubConnection.ConnectionId, GametypeId);
                    //if (GametypeId != (int)GameTypes.Lucky_9)
                    //{
                    //    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    //}
                    HubConnection.On<long, string>(Constants.NotifyForceLogout, NotifyForceLogout);
                    HubConnection.On<long, decimal>(Constants.UpdateGameTokens, UpdateTokenValue);
                    HubConnection.On<string, string>(Constants.UpdateNotificationBadge, UpdateNotificationBadge);
                    HubConnection.On<string, int>(Constants.UpdateNotificationBadgeCount, UpdateNotificationBadgeCount);

                    HubConnection.On<UpdateGameWinnerModel>(Constants.UpdateGameWinners, UpdateGameWinners);

                    //HubConnection.KeepAliveInterval = new TimeSpan(60);
                    //HubConnection.Closed += async (exception) =>
                    //{
                    //    popupLoading = popupModal.Show<PopupLoading>("", new ModalOptions() { Class = "blazored-login" });
                    //};
                    HubConnection.Reconnected += async (exception) =>
                    {
                        await HubConnection.InvokeAsync("AddConnectionToGame", HubConnection.ConnectionId, GametypeId);
                        popupLoading.Close();
                    };
                    HubConnection.Reconnecting += async (exception) =>
                    {
                        var parameters = new ModalParameters();
                        parameters.Add("Message", "Attempting to reconnect...");
                        popupLoading = popupModal.Show<PopupLoading>("", parameters);
                    };


                    //HubConnection.On<int, int>(Constants.UpdateGameStatus, UpdateGameStatus); in gameviewmodel
                    //HubConnection.On<int, decimal, decimal>(Constants.UpdateGameOdds, UpdateGameOdds); in game view model
                    //HubConnection.On<BetUpdatesModel>(Constants.UpdateBetValues, UpdateBetValues); in game view model
                    //HubConnection.On<int>(Constants.UpdateEnableOperatorInput, UpdateEnableOperatorInput); not player
                    //HubConnection.On<int>(Constants.UpdateEnableValidatorInput, UpdateEnableValidatorInput); not player


                    //HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);

                    //HubConnection.On<int>(Constants.UpdateEnableOpenButton, UpdateEnableOpenButton);
                    //HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);
                    //HubConnection.On<long, int>(Constants.NotifyOddsCancelled, NotifyOddsCancelled);

                    //HubConnection.On<int, bool>(Constants.NotifyFixedLeftOptions, NotifyFixedLeftOptions);
                    //HubConnection.On<int, bool>(Constants.NotifyFixedRightOptions, NotifyFixedRightOptions);

                    //HubConnection.On<int, int, decimal>(Constants.NotifyNumberBets, NotifyNumberBets);
                    //HubConnection.On<int, int, bool>(Constants.NotifyNumberOptions, NotifyNumberOptions);
                    //HubConnection.On<int>(Constants.NotifyEnableAllNumbers, NotifyEnableAllNumbers);

                    // await PopAllNoInternetPopup();
                    await GetCurrentTokenValue();

                    //MessagingCenter.Send(this, Constants.NotifySignalRReconnection, string.Empty);
                }
                else
                {

                    //if (GametypeId != (int)GameTypes.Lucky_9)
                    //{
                    //    HubConnection.Remove(Constants.UpdateGameTimer);
                    //    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    //}
                    HubConnection.Remove(Constants.NotifyForceLogout);
                    HubConnection.Remove(Constants.UpdateGameTokens);
                    HubConnection.Remove(Constants.UpdateNotificationBadge);
                    HubConnection.Remove(Constants.UpdateNotificationBadgeCount);
                    HubConnection.Remove(Constants.UpdateGameWinners);

                    HubConnection.On<long, string>(Constants.NotifyForceLogout, NotifyForceLogout);
                    HubConnection.On<long, decimal>(Constants.UpdateGameTokens, UpdateTokenValue);
                    HubConnection.On<string, string>(Constants.UpdateNotificationBadge, UpdateNotificationBadge);
                    HubConnection.On<string, int>(Constants.UpdateNotificationBadgeCount, UpdateNotificationBadgeCount);
                    HubConnection.On<UpdateGameWinnerModel>(Constants.UpdateGameWinners, UpdateGameWinners);

                    await HubConnection.InvokeAsync("AddConnectionToGame", HubConnection.ConnectionId, GametypeId);
                    //HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    //HubConnection.On<int, int>(Constants.UpdateGameStatus, UpdateGameStatus);
                }

            }
        }
        catch (Exception ex)
        {
            HubConnection = null;
            _toastService.ShowError("Cannot connect to Goplayasia, please try to login again..");
            Logout();
        }
    }
    #endregion

    #region Game Methods
    public async Task GetGameChips()
    {

        var tempGameChips = await _igameSettingService.GetGameChips(GametypeId);
        if (tempGameChips == null)
        {
            _toastService.ShowInfo("No game settings found.");
            return;
        }
        switch (GametypeId)
        {
            case (int)GameTypes.Lucky_9:
                L9GameChips = tempGameChips;
                break;
            case (int)GameTypes.Gold_And_Silver:
                F3GameChips = tempGameChips;
                break;

        }
        GameChips = tempGameChips;
        await CallInvoke();

    }
    public async Task GetPlayerCategory()
    {

        var tempCategory = await _igameSettingService.GetPlayerCategory();
        if (tempCategory == null)
        {
            _toastService.ShowInfo("No player categroy found.");
            return;
        }

        PlayerCategory = new ObservableCollection<PlayerCategoryModel>(tempCategory);
        await CallInvoke();

    }
    public async Task GetGameChipsByCategory()
    {
        var tempChips = await _igameSettingService.GetGameChipsByCategory(GametypeId, _icurrentUser.CategoryId);
        if (tempChips == null)
        {
            _toastService.ShowInfo("No game chips by category found.");
            return;
        }

        int[] chips = { tempChips.Chip1 != null ? tempChips.Chip1.Value : 0,
                        tempChips.Chip2 != null ? tempChips.Chip2.Value : 0,
                        tempChips.Chip3 != null ? tempChips.Chip3.Value : 0,
                        tempChips.Chip4 != null ? tempChips.Chip4.Value : 0,
                        tempChips.Chip5 != null ? tempChips.Chip5.Value : 0,
                        tempChips.Chip6 != null ? tempChips.Chip6.Value : 0,
                        tempChips.Chip7 != null ? tempChips.Chip7.Value : 0,
                        tempChips.Chip8 != null ? tempChips.Chip8.Value : 0,
                        tempChips.Chip9 != null ? tempChips.Chip9.Value : 0,
                        tempChips.Chip10 != null ? tempChips.Chip10.Value : 0
                      };

        switch (GametypeId)
        {
            case (int)GameTypes.Go_12:
                Go12GameChips = tempChips;
                MinChip = chips.Min();
                break;
            case (int)GameTypes.Lucky_9:
                L9GameChips = tempChips;
                MaxChip = chips.Max();
                break;
            case (int)GameTypes.Gold_And_Silver:
                F3GameChips = tempChips;
                MaxChip = chips.Max();
                break;
            case (int)GameTypes.Heads_And_Tails:
                MinChip = chips.Min();
                MaxChip = chips.Max();
                break;
            case (int)GameTypes.Dice:
                DiceGameChips = tempChips;
                MaxChip = chips.Max();
                break;
        }
        GameChips = tempChips;
        await CallInvoke();
    }
    #endregion

    public async Task GenerateStreamKey()
    {
        //var streamres = await _igameSettingService.GenerateStreamKey(StreamId, "play");
        //if (streamres == null)
        //{
        //    streamKey = "123";
        //    _toastService.ShowInfo("Clould not generate stream key");
        //    return;
        //}
        //else
        //{
        //    streamKey = streamres.tokenId;
        //}

        streamKey = "disabled";

    }

    #region CorpSEttings
    public async Task GetCorpSettings()
    {
        corporationSettings = await _constantService.GetCorporationSettings();

    }
    public async Task GetPreviousBets(int GametypeId)
    {
        long UserId = _icurrentUser.Id;
        var tempRoundBets = await _igameRoundService.GetPrevGameBets(UserId, GametypeId, prevBetsCount);
        if (tempRoundBets != null)
        {
            PreviousBets = new ObservableCollection<BetModel>(tempRoundBets);
            CallInvoke();
        }
    }

    #endregion


}
