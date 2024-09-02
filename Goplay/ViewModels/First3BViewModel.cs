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

public class First3BViewModel : BaseViewModel
{
    #region LOCAL VARIABLES & PROPERTIES

    #region INJECTED
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region GAME CONFIGURATION
    public int BetType { get; set; } = 0;
    public bool ShowTotalBets { get; set; }
    public int CurrentGameType { get; set; }
    public bool IsFixedGoldEnabled { get; set; }
    public bool IsFixedSilverEnabled { get; set; }
    public bool IsButtonDisabled { get; set; }
    #endregion

    #region CSS / GIF/ ANIMATION
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

    public string GoldCardValue { get; set; }
    public string SilverCardValue { get; set; }
    #endregion

    #region TOTAL BETS
    public string UserTotalBet_FixedPrizeGold { get; set; }
    public string UserTotalBet_FixedPrizeSilver { get; set; }
    public string UserTotalBet_Trio { get; set; }
    public string UserTotalBet_SuitsGold { get; set; }
    public string UserTotalBet_SuitsSilver { get; set; }
    public string UserTotalBet_ColorRedGold { get; set; }
    public string UserTotalBet_ColorRedSilver { get; set; }
    public string UserTotalBet_ColorBlackGold { get; set; }
    public string UserTotalBet_ColorBlackSilver { get; set; }
    #endregion

    #region SAMPLE WINNINGS
    public string SampleWinFixedPrizeGold { get; set; }
    public string SampleWinFixedPrizeSilver { get; set; }
    public string SampleWinTrio { get; set; }
    public string SampleWinSuitsGold { get; set; }
    public string SampleWinSuitsSilver { get; set; }
    public string SampleWinColorRedGold { get; set; }
    public string SampleWinColorRedSilver { get; set; }
    public string SampleWinColorBlackGold { get; set; }
    public string SampleWinColorBlackSilver { get; set; }
    #endregion

    #region MULTIPLIER
    public string FixedPriceGoldMultiplier { get; set; }
    public string FixedPriceSilverMultipler { get; set; }
    #endregion

    #region TRENDS
    public static int payoutRowLimit = 10;
    public static int oddsRowLimit = 5;
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<F3GameRoundModel> CurrentList { get; set; }
    }
    public ObservableCollection<TrendsDisplayModel> PayoutTrendsDisplay { get; set; }
    public ObservableCollection<F3GameRoundModel> Trends { get; set; }
    #endregion

    #endregion

    #region LIFE CYCLE METHODS
    public First3BViewModel(IConfiguration iconfig,
                            ICurrentUser icurrentUser,
                            IF3GameRoundService if3gameRoundService,
                            IGameSettingService igameSettingsService,
                            IToastService toastService,
                            IAccountService iaccountService,
                            NavigationManager navigationManager,
                            AuthenticationStateProvider authenticationStateProvider,
                            IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _if3gameRoundService = if3gameRoundService;
        _igameSettingService = igameSettingsService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = authenticationStateProvider;
        _mapper = mapper;

        StreamId = Constants.StreamIDFirst3B;
        GametypeId = (int)GameTypes.First3B;
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
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                    HubConnection.On<int>(Constants.UpdateEnableOpenButton, UpdateEnableOpenButton);
                    HubConnection.On<int, string, string>(Constants.UpdateCardResults, UpdateCardResults);

                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                    HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);
                    HubConnection.On<int, bool>(Constants.NotifyFixedLeftOptions, NotifyFixedLeftOptions);
                    HubConnection.On<int, bool>(Constants.NotifyFixedRightOptions, NotifyFixedRightOptions);
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
            if (GametypeId == gametypeId && F3GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);

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
    protected async Task UpdateGameStatus(int gametypeId, int gameStatus, long gameRoundId)
    {
        if (GametypeId == gametypeId)
        {
            await GetGameRound();
            if (gameStatus != (int)RoundStatus.Open)
            {
                TickerMessage = "";
                IsFixedGoldEnabled = false;
                IsFixedSilverEnabled = false;

                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    GIF = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    await LoadDefaultBetDetails();
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    GIF = "/img/animation/test-closebet.gif";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
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

                IsFixedGoldEnabled = true;
                IsFixedSilverEnabled = true;

                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;
                await LoadDefaultBetDetails();
                F3UserBets = new ObservableCollection<F3BetModel>();

                GIF = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            await CallInvoke();
        }
    }
    private async Task LoadDefaultBetDetails()
    {
        var sampleBet = 0;
        decimal FixedPriceMultiplier = Convert.ToDecimal(GameSetting.FixedPriceMultiplier);
        decimal RunningOddsPercentage = Convert.ToDecimal(GameSetting.RunningOddsPercentage) / 100;
        decimal DrawMultiplierValue = Convert.ToDecimal(GameSetting.PayoutMultiplier);

        SampleWinFixedPrizeGold = (sampleBet * FixedPriceMultiplier).ToString("0");
        SampleWinFixedPrizeSilver = (sampleBet * FixedPriceMultiplier).ToString("0"); ;

        UserTotalBet_FixedPrizeGold = "0";
        UserTotalBet_FixedPrizeSilver = "0";
    }
    public async Task UpdateBetValues(BetUpdatesModel paramsModel)
    {
        if (F3GameRound is null) return;
        if (paramsModel.GameTypeId == GametypeId)
        {
            if (F3GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    DrawTotalBets = paramsModel.DrawBetValue.ToString("#,##0");
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
    public async Task UpdateEnableOpenButton(int GameTypeId)
    {
        if (GameTypeId == GametypeId)
        {
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
    public async void NotifyGameRoundResult(UpdateGameResultModel result)
    {
        if (result != null && result.GameTypeId == GametypeId)
        {
            GameResultString = result.WinningCombination;
            GameResult = result;
            await GetCurrentRoundBets();
        }
    }
    public async Task NotifyFixedCancelled(long userId, int gameTypeId)
    {
        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsFixedGoldEnabled = false;
            IsFixedSilverEnabled = false;
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
                    await CallInvoke();
                }
            }
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
                    await CallInvoke();
                }
            }
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
    public async Task GetGameRound()
    {
        F3GameRound = await _if3gameRoundService.GetRound(GametypeId);
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

            DrawTotalBets = F3GameRound.DrawBet;

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

                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    break;

                case (int)RoundStatus.PendingResult:
                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;

                    IsFixedGoldEnabled = false;
                    IsFixedSilverEnabled = false;

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
                    if (item.GameVariantID == (int)GameVariant.F3BTrio)
                    {
                        F3Trio = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.F3BSuits)
                    {
                        F3Suits = item;
                    }
                    if (item.GameVariantID == (int)GameVariant.F3BColor)
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
            if (item.GameVariantId == (int)GameVariant.F3BTrio)
            {
                F3TrioChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.F3BSuits)
            {
                F3SuitsChips = item;
                chipsList.Add(item.Chip1);
            }
            if (item.GameVariantId == (int)GameVariant.F3BColor)
            {
                F3ColorChips = item;
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
            GameChips = F3BGameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.F3BTrio:
                    GameChips = _mapper.Map<GameChipModel>(F3TrioChips);
                    break;
                case (int)GameVariant.F3BColor:
                    GameChips = _mapper.Map<GameChipModel>(F3ColorChips);
                    break;
                case (int)GameVariant.F3BSuits:
                    GameChips = _mapper.Map<GameChipModel>(F3SuitsChips);
                    break;
            }
        }
        await CallInvoke();
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
    public async Task GetCurrentRoundBets()
    {
        TotalBets = 0;
        if (F3GameRound.RoundStatus != (int)RoundStatus.Cancelled)
        {
            var tempRoundBets = await _if3gameRoundService.GetBets(_icurrentUser.Id, GametypeId, null);
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
                    else if (item.GameVariantID == (int)GameVariant.F3BTrio)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * F3Trio.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.F3BSuits)
                    {
                        item.WinableAmount = Convert.ToDecimal(item.BetAmount * F3Suits.FixedPriceMultiplier).ToString("N2");
                    }
                    else if (item.GameVariantID == (int)GameVariant.F3BColor)
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
    public async Task GetRoundBetSummary()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {
            var roundBetDetail = await _if3gameRoundService.GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
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
    public async Task GetTrends()
    {
        var tempTrends = await _if3gameRoundService.GetTrends();
        if (tempTrends != null)
        {
            await SetPayoutTrendsDisplay(tempTrends);
            await CallInvoke();
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
    #endregion

    #region BET METHODS
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
            var tempBetResult = await _if3gameRoundService.BetOnRound(betParams);
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

                if (BetCombinationValue.ToUpper().Contains(Constants.FixedGold))
                {
                    await UpdateFixed_Gold_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedSilver))
                {
                    await UpdateFixed_Silver_BetAmount(GametypeId, BetAmount);
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
                case (int)GameVariant.F3BTrio:
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
                case (int)GameVariant.F3BColor:
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
                case (int)GameVariant.F3BSuits:
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
    #endregion

}
