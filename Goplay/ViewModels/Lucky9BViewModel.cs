using AutoMapper;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaCore.Core.Services.Interface;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class Lucky9BViewModel : BaseViewModel
{
    #region LOCAL VARIABLES & PROPERTIES

    #region INJECTED
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region GAME CONFIGURATION
    public int BetType { get; set; } = 0;
    public bool ShowTotalBets { get; set; }
    public int CurrentGameType { get; set; }
    public bool IsFixedPlayerEnabled { get; set; }
    public bool IsFixedBankerEnabled { get; set; }
    public bool IsButtonDisabled { get; set; }
    #endregion

    #region CSS / GIF / ANIMATION
    public string TokenDiv { get; set; } = "tokenhide";
    public string Token_Animation { get; set; }
    public string GIF { get; set; }
    #endregion

    #region CARDS 
    private int CardValue1;
    private int CardValue2;
    private int CardValue3;
    private int CardValue4;
    private int CardValue5;
    private int CardValue6;
    public string PlayerCardValue { get; set; }
    public string BankerCardValue { get; set; }
    #endregion

    #region TOTAL BETS
    public string UserTotalBet_FixedPrizePlayer { get; set; }
    public string UserTotalBet_FixedPrizeBanker { get; set; }
    public string UserTotalBet_OddsPlayer { get; set; }
    public string UserTotalBet_OddsBanker { get; set; }
    public string UserTotalBet_Draw { get; set; }
    public string UserTotalBet_TargetPlayer { get; set; }
    public string UserTotalBet_TargetBanker { get; set; }
    public string UserTotalBet_ColorRedPlayer { get; set; }
    public string UserTotalBet_ColorRedBanker { get; set; }
    public string UserTotalBet_ColorBlackPlayer { get; set; }
    public string UserTotalBet_ColorBlackBanker { get; set; }
    public string UserTotalBet_PairPlayer { get; set; }
    public string UserTotalBet_PairBanker { get; set; }
    public string UserTotalBet_SuitsPlayer { get; set; }
    public string UserTotalBet_SuitsBanker { get; set; }
    #endregion

    #region SAMPLE WINNINGS
    public string SampleWinDraw { get; set; }
    public string SampleWinFixedPrizePlayer { get; set; }
    public string SampleWinFixedPrizeBanker { get; set; }
    public string SampleWinOddsPlayer { get; set; }
    public string SampleWinOddsBanker { get; set; }
    public string SampleWinTargetPlayer { get; set; }
    public string SampleWinTargetBanker { get; set; }
    public string SampleWinColorRedPlayer { get; set; }
    public string SampleWinColorRedBanker { get; set; }
    public string SampleWinColorBlackPlayer { get; set; }
    public string SampleWinColorBlackBanker { get; set; }
    public string SampleWinPairPlayer { get; set; }
    public string SampleWinPairBanker { get; set; }
    public string SampleWinSuitsPlayer { get; set; }
    public string SampleWinSuitsBanker { get; set; }
    #endregion

    #region SAMPLE BET
    public string SampleBetDisplay { get; set; }
    #endregion

    #region MULTIPLIER
    public string FixedPricePlayerMultiplier { get; set; }
    public string FixedPriceBankerMultipler { get; set; }
    public string RunningOddsPlayerMultiplier { get; set; }
    public string RunningOddBankerMultiplier { get; set; }
    #endregion

    #region TRENDS
    public static int payoutRowLimit = 10;
    public static int oddsRowLimit = 5;
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<L9GameRoundModel> CurrentList { get; set; }
    }
    public ObservableCollection<L9GameRoundModel> Trends { get; set; }
    public ObservableCollection<TrendsDisplayModel> PayoutTrendsDisplay { get; set; }
    #endregion

    #endregion

    #region LIFE CYCLE METHOD
    public Lucky9BViewModel(IConfiguration iconfig,
                             ICurrentUser icurrentUser,
                             IL9GameRoundService il9gameRoundService,
                             IGameSettingService igameSettingsService,
                             IToastService toastService,
                             IAccountService iaccountService,
                             NavigationManager navigationManager,
                             AuthenticationStateProvider AuthenticationStateProvider,
                             IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _il9gameRoundService = il9gameRoundService;
        _igameSettingService = igameSettingsService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        _mapper = mapper;

        StreamId = Constants.StreamIDLucky9B;
        GametypeId = (int)GameTypes.Lucky9B;
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


    #region SIGNALR METHODS
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
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);

                    HubConnection.On<int>(Constants.UpdateEnableOpenButton, UpdateEnableOpenButton);
                    HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);

                    HubConnection.On<int, bool>(Constants.NotifyFixedLeftOptions, NotifyFixedLeftOptions);
                    HubConnection.On<int, bool>(Constants.NotifyFixedRightOptions, NotifyFixedRightOptions);
                    HubConnection.On<int, string, string>(Constants.UpdateCardResults, UpdateCardResults);
                }
            }
        }
        catch (Exception)
        {

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

        UserTotalBet_Draw = "0";
        UserTotalBet_FixedPrizePlayer = "0";
        UserTotalBet_FixedPrizeBanker = "0";
        UserTotalBet_OddsPlayer = "0";
        UserTotalBet_OddsBanker = "0";

        RunningOddsPlayerMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
        RunningOddBankerMultiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
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
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus, long gameRoundId)
    {
        if (GametypeId == gametypeId)
        {
            await GetGameRound();

            if (gameStatus != (int)RoundStatus.Open)
            {
                TickerMessage = "";
                IsFixedPlayerEnabled = false;
                IsFixedBankerEnabled = false;
                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    GIF = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = "";
                    ShowFlashing = "timerNotFlashing";
                    await LoadDefaultBetDetails();
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    GIF = "/img/animation/test-closebet.gif";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = "";
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                }
                ShowFlashing = "timerNotFlashing";

                BetAmount = 0;
                Payout = 0;
                TokenDiv = "tokenhide";
                IsButtonDisabled = false;
            }
            else if (gameStatus == (int)RoundStatus.Open)
            {
                TotalBets = 0;
                IsFixedPlayerEnabled = true;
                IsFixedBankerEnabled = true;
                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;
                await LoadDefaultBetDetails();
                L9UserBets = new ObservableCollection<L9BetModel>();

                GIF = "/img/animation/test-openbet.gif";
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
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    DrawTotalBets = paramsModel.DrawBetValue.ToString("#,##0");
                    RunningOddsPlayerMultiplier = paramsModel.LeftPercentage != 0 ? paramsModel.LeftPercentage.ToString("#.#0") + "%" : "0%";
                    RunningOddBankerMultiplier = paramsModel.RightPercentage != 0 ? paramsModel.RightPercentage.ToString("#.#0") + "%" : "0%";
                    await UpdateOddsValue();
                    await CallInvoke();
                }
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
    public async void NotifyGameRoundResult(UpdateGameResultModel result)
    {
        if (result != null && result.GameTypeId == GametypeId)
        {
            GameResultString = result.WinningCombination;
            GameResult = result;
            await GetCurrentRoundBets();
        }
    }
    public async Task UpdateEnableOpenButton(int GameTypeId)
    {
        if (GameTypeId == GametypeId)
        {
        }
    }
    public async Task NotifyFixedCancelled(long userId, int gameTypeId)
    {
        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsFixedPlayerEnabled = false;
            IsFixedBankerEnabled = false;
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
                    await CallInvoke();
                }
            }
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

    #region GAME METHODS
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
        L9GameRound = await _il9gameRoundService.GetRound(GametypeId);
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

            DrawTotalBets = L9GameRound.DrawBet;

            RunningOddsPlayerMultiplier = L9GameRound.OddsLeftPercentage;
            RunningOddBankerMultiplier = L9GameRound.OddsRightPercentage;

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
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;

                    break;

                case (int)RoundStatus.PendingResult:
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    IsFixedPlayerEnabled = false;
                    IsFixedBankerEnabled = false;

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
                    if (item.GameVariantID == (int)GameVariant.L9BTarget)
                    {
                        L9Target = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9BSuits)
                    {
                        L9Suits = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9BColor)
                    {
                        L9Color = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9BPair)
                    {
                        L9Pair = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.L9BDraw)
                    {
                        L9Draw = item;
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
            if (item.GameVariantId == (int)GameVariant.L9BTarget)
            {
                L9TargetChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9BSuits)
            {
                L9SuitsChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9BColor)
            {
                L9ColorChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9BPair)
            {
                L9PairChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.L9BDraw)
            {
                L9DrawChips = item;
                chipsList.Add(item.Chip1);
            }
        }
        MinChip = chipsList.Min();

        await CallInvoke();
    }
    public async Task SetGameChips(int GameVariantId, bool isSubGames)
    {
        if (!isSubGames)
        {
            GameChips = L9BGameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.L9BTarget:
                    GameChips = _mapper.Map<GameChipModel>(L9TargetChips);
                    break;
                case (int)GameVariant.L9BPair:
                    GameChips = _mapper.Map<GameChipModel>(L9PairChips);
                    break;
                case (int)GameVariant.L9BColor:
                    GameChips = _mapper.Map<GameChipModel>(L9ColorChips);
                    break;
                case (int)GameVariant.L9BSuits:
                    GameChips = _mapper.Map<GameChipModel>(L9SuitsChips);
                    break;
                case (int)GameVariant.L9BDraw:
                    GameChips = _mapper.Map<GameChipModel>(L9DrawChips);
                    break;
            }
        }
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
            var tempRoundBets = await _il9gameRoundService.GetBets(_icurrentUser.Id, GametypeId, null);
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
                    else if (item.GameVariantID == (int)GameVariant.L9BTarget)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Target.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9BSuits)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Suits.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9BColor)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * L9Color.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.L9BPair)
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
    public async Task GetRoundBetSummary()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {
            var roundBetDetail = await _il9gameRoundService.GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
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
        }
    }
    public async Task GetTrends()
    {
        var tempTrends = await _il9gameRoundService.GetTrends();
        if (tempTrends != null)
        {
            await SetPayoutTrendsDisplay(tempTrends);
            await CallInvoke();
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
    #endregion

    #region BET METHODS
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
            var tempBetResult = await _il9gameRoundService.BetOnRound(betParams);
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
                IsButtonDisabled = false;
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

                await GetRoundBetSummary();
                await ValidateUser();
                await CallInvoke();
            }
        }
        catch (Exception ex)
        {
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

        var result = BetAmount % 10;
        if (result > 0)
        {
            _toastService.ShowError("Bet amount should be divisible by 10.");
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
    #endregion

}
