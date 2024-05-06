using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Main.Login; 
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.IO;
using static GoplayasiaBlazor.Models.Constants.Settings;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.ViewModels;

public class HeadsAndTailsViewModel : BaseViewModel
{
    #region INJECTED
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    #endregion

    #region Local Variables & Properties

    #region BETS
    public string UserTotalBet_FixedPrize_Heads
    {
        get; set;
    }

    public string UserTotalBet_FixedPrize_Tails
    {
        get; set;
    }

    public string UserTotalBet_Odds_Heads
    {
        get; set;
    }

    public string UserTotalBet_Odds_Tails
    {
        get; set;
    }

    public string UserTotalBet_Draw
    {
        get; set;
    }
    #endregion

    #region GAME CONFIGURATION
    private bool _showTotalBets;
    public bool ShowTotalBets
    {
        get => _showTotalBets;
        set
        {
            _showTotalBets = value;
            RaisePropertyChanged(() => ShowTotalBets);
        }
    }

    public bool IsFixed_Heads_Enabled
    {
        get; set;
    }

    public bool IsFixed_Tails_Enabled
    {
        get; set;
    }

    public bool IsRunndingOdds_Heads_Enabled
    {
        get; set;
    }

    public bool IsRunningOdds_Tails_Enabled
    {
        get; set;
    }

    public bool IsDrawEnabled
    {
        get; set;
    }

    public bool IsRunningOddsCancelled
    {
        get; set;
    }

    public bool IsFixedPrizeCancelled
    {
        get; set;
    }

    public bool IsBackToGameListingEnabled
    {
        get; set;
    }
    #endregion

    #region PLAYER BETS
    public string DrawTotalBets
    {
        get; set;
    }

    public string FixedPrice_Heads_TotalBets
    {
        get; set;
    }

    public string FixedPrice_Tails_TotalBets
    {
        get; set;
    }

    public string RunningOdds_Heads_TotalBets
    {
        get; set;
    }

    public string RunningOdd_Tails_TotalBets
    {
        get; set;
    }
    #endregion

    #region MULTIPLIER
    public string DrawMultiplier
    {
        get; set;
    }

    public string FixedPrice_Heads_Multiplier
    {
        get; set;
    }

    public string FixedPrice_Tails_Multipler
    {
        get; set;
    }

    public string RunningOdds_Heads_Multiplier
    {
        get; set;
    }

    public string RunningOdd_Tails_Multiplier
    {
        get; set;
    }
    #endregion

    #region BET WINNING
    public string SampleBetDisplay
    {
        get; set;
    }

    public string SampleWinFixedPrize_Heads
    {
        get; set;
    }

    public string SampleWinFixedPrize_Tails
    {
        get; set;
    }

    public string SampleWinOdds_Heads
    {
        get; set;
    }

    public string SampleWinOdds_Tails
    {
        get; set;
    }

    public string SampleWinDraw
    {
        get; set;
    }

    public bool IsPayoutWon_Heads
    {
        get; set;
    }

    public bool IsPayoutWon_Tails
    {
        get; set;
    }

    public bool IsOddsWon_Heads
    {
        get; set;
    }

    public bool IsOddsWon_Tails
    {
        get; set;
    }

    public bool IsDrawWon
    {
        get; set;
    }
    #endregion

    #region TRENDS
    public ObservableCollection<GameRoundModel> Trends
    {
        get; set;
    }

    public ObservableCollection<TrendsDisplayModel> TrendsDisplay
    {
        get; set;
    }

    public ObservableCollection<TrendsDisplayModel> PayoutTrendsDisplay
    {
        get; set;
    }

    public ObservableCollection<TrendsDisplayModel> OddsTrendsDisplay
    {
        get; set;
    }

    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<GameRoundModel> CurrentList { get; set; }
    }
    #endregion

    public bool tokenDisabled = true;

    public bool betDisabled = true;

    public static int payoutRowLimit = 10;

    public static int oddsRowLimit = 5;

    public string _tokenAnimation = "";
    public string Token_Animation
    {
        get => _tokenAnimation;
        set
        {
            _tokenAnimation = value;
            RaisePropertyChanged(() => Token_Animation);
        }
    }

    public string htgif = "";

    public string tokenDiv = "tokenhide";

    #endregion

    #region Life cycle methods
    public HeadsAndTailsViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IGameSettingService igameSettingsService, IToastService toastService, IAccountService iaccountService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _igameSettingService = igameSettingsService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;

        StreamId = Constants.StreamIDHeadTails;
        GametypeId = (int)GameTypes.Heads_And_Tails;
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

    #region Signal R Methods
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
                    HubConnection.Remove(Constants.NotifyFixedCancelled);
                    HubConnection.Remove(Constants.NotifyOddsCancelled);
                    HubConnection.Remove(Constants.NotifyFixedLeftOptions);
                    HubConnection.Remove(Constants.NotifyFixedRightOptions);
                    HubConnection.Remove(Constants.NotifyNumberBets);
                    HubConnection.Remove(Constants.NotifyNumberOptions);
                    HubConnection.Remove(Constants.NotifyEnableAllNumbers);

                    //HubConnection.On<long, string>(Constants.NotifyForceLogout, NotifyForceLogout);
                    //HubConnection.On<long, decimal>(Constants.UpdateGameTokens, UpdateTokenValue);
                    HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                    HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                    HubConnection.On<string, string>(Constants.UpdateNotificationBadge, UpdateNotificationBadge);
                    HubConnection.On<string, int>(Constants.UpdateNotificationBadgeCount, UpdateNotificationBadgeCount);

                    HubConnection.On<int, decimal, decimal>(Constants.UpdateGameOdds, UpdateGameOdds);

                    HubConnection.On<BetUpdatesModel>(Constants.UpdateBetValues, UpdateBetValues);
                    HubConnection.On<UpdateGameWinnerModel>(Constants.UpdateGameWinners, UpdateGameWinners);
                    HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                    HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);

                    HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);
                    HubConnection.On<long, int>(Constants.NotifyOddsCancelled, NotifyOddsCancelled);

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
                IsFixed_Heads_Enabled = false;
                IsFixed_Tails_Enabled = false;
                IsRunndingOdds_Heads_Enabled = false;
                IsRunningOdds_Tails_Enabled = false;
                IsDrawEnabled = false;
                if (gameStatus == (int)RoundStatus.Cancelled)
                {
                    htgif = "";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";

                    await LoadDefaultBetDetails();
                }
                else if (gameStatus == (int)RoundStatus.PendingResult)
                {
                    htgif = "/img/animation/test-closebet.gif";
                    JsRuntime.InvokeVoidAsync("funcAnimation");

                    RoundTimer = ""; // 00:00
                    ShowFlashing = "timerNotFlashing";

                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                }

                BetAmount = 0;
                Payout = 0;
                tokenDiv = "tokenhide";
                //MessagingCenter.Send(this, Constants.CloseBettingPopups, string.Empty);
            }
            else if (gameStatus == (int)RoundStatus.Open)
            {
                IsFixed_Heads_Enabled = true;
                IsFixed_Tails_Enabled = true;
                IsRunndingOdds_Heads_Enabled = true;
                IsRunningOdds_Tails_Enabled = true;
                IsDrawEnabled = true;
                TotalBets = 0;
                await GetPreviousBets((int)GameTypes.Heads_And_Tails);
                await LoadDefaultBetDetails();
                UserBets = new ObservableCollection<BetModel>();

                htgif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            await CallInvoke();

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
                RunningOdds_Heads_Multiplier = firstOddvalue != 0 ? firstOddvalue.ToString("#.#0") + "%" : "0%";
                RunningOdd_Tails_Multiplier = secondOddValue != 0 ? secondOddValue.ToString("#.#0") + "%" : "0%";
            }
            await UpdateOddsValue();
            await CallInvoke();
        }
    }
    public async Task UpdateOddsValue()
    {
        decimal leftPercentage = decimal.Parse(RunningOdds_Heads_Multiplier.Replace("%", ""));
        decimal rightPercentage = decimal.Parse(RunningOdd_Tails_Multiplier.Replace("%", ""));

        decimal userOddsPlayerTotalBets = !string.IsNullOrEmpty(UserTotalBet_Odds_Heads) ? decimal.Parse(UserTotalBet_Odds_Heads) : 0;
        SampleWinOdds_Heads = userOddsPlayerTotalBets != 0 ? (userOddsPlayerTotalBets * (leftPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOdds_Heads);

        decimal userOddsBankerTotalBets = !string.IsNullOrEmpty(UserTotalBet_Odds_Tails) ? decimal.Parse(UserTotalBet_Odds_Tails) : 0;
        SampleWinOdds_Tails = userOddsBankerTotalBets != 0 ? (userOddsBankerTotalBets * (rightPercentage / 100)).ToString("#,###.#0") : "0";
        RaisePropertyChanged(() => SampleWinOdds_Tails);
        await CallInvoke();
    }
    public async Task UpdateBetValues(BetUpdatesModel paramsModel)
    {
        if (GameRound is null) return;
        if (paramsModel.GameTypeId == GametypeId)
        {
            if (GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                //if (disableUpdate == true) return;
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {

                    //Console.WriteLine(DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds);
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    DrawTotalBets = paramsModel.DrawBetValue.ToString("#,##0");
                    FixedPrice_Heads_TotalBets = paramsModel.FixedLeftBetValue.ToString("#,##0");
                    FixedPrice_Tails_TotalBets = paramsModel.FixedRightBetValue.ToString("#,##0");
                    RunningOdds_Heads_TotalBets = paramsModel.RunningLeftBetValue.ToString("#,##0");
                    RunningOdd_Tails_TotalBets = paramsModel.RunningRightBetValue.ToString("#,##0");
                    RunningOdds_Heads_Multiplier = paramsModel.LeftPercentage != 0 ? paramsModel.LeftPercentage.ToString("#.#0") + "%" : "0%";
                    RunningOdd_Tails_Multiplier = paramsModel.RightPercentage != 0 ? paramsModel.RightPercentage.ToString("#.#0") + "%" : "0%";
                    await UpdateOddsValue();
                    await CallInvoke();
                }
            }
        }
    }
    public void NotifyGameRoundResult(UpdateGameResultModel result)
    {
        if (result != null && result.GameTypeId == GametypeId)
        {
            //LoadDefaultBetDetails();
            GameResultString = result.WinningCombination;
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
    public async Task NotifyFixedCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsFixedPrizeCancelled = true;
            IsFixed_Heads_Enabled = false;
            IsFixed_Tails_Enabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyOddsCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsRunningOddsCancelled = true;
            IsRunndingOdds_Heads_Enabled = false;
            IsRunningOdds_Tails_Enabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyFixedLeftOptions(int gameTypeId, bool disabled)
    {

        if (GametypeId == gameTypeId)
        {
            if (GameRound != null)
            {

                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixed_Heads_Enabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
        }

    }
    public async Task NotifyFixedRightOptions(int gameTypeId, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            if (GameRound != null)
            {

                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixed_Tails_Enabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
        }
    }
    private async Task LoadDefaultBetDetails()
    {
        var sampleBet = 0;
        decimal FixedPriceMultiplier = Convert.ToDecimal(GameSetting.FixedPriceMultiplier);
        decimal RunningOddsPercentage = Convert.ToDecimal(GameSetting.RunningOddsPercentage) / 100;
        decimal DrawMultiplierValue = Convert.ToDecimal(GameSetting.PayoutMultiplier);
        SampleBetDisplay = sampleBet.ToString("0");
        SampleWinFixedPrize_Heads = (sampleBet * FixedPriceMultiplier).ToString("0");
        SampleWinFixedPrize_Tails = (sampleBet * FixedPriceMultiplier).ToString("0"); ;
        SampleWinOdds_Heads = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinOdds_Tails = (sampleBet * RunningOddsPercentage).ToString("0");
        SampleWinDraw = (sampleBet * DrawMultiplierValue).ToString("0");


        FixedPrice_Heads_TotalBets = "0";
        FixedPrice_Tails_TotalBets = "0";
        RunningOdds_Heads_TotalBets = "0";
        RunningOdd_Tails_TotalBets = "0";

        UserTotalBet_Draw = "0";
        UserTotalBet_FixedPrize_Heads = "0";
        UserTotalBet_FixedPrize_Tails = "0";
        UserTotalBet_Odds_Heads = "0";
        UserTotalBet_Odds_Tails = "0";

        RunningOdds_Heads_Multiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
        RunningOdd_Tails_Multiplier = GameSetting.RunningOddsPercentage != null && Convert.ToDecimal(GameSetting.RunningOddsPercentage) != 0 ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
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
                FixedPrice_Heads_Multiplier = GameSetting.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(GameSetting.FixedPriceMultiplier).ToString() : "x";
                FixedPrice_Tails_Multipler = GameSetting.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(GameSetting.FixedPriceMultiplier).ToString() : "x";
                RunningOdds_Heads_Multiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
                RunningOdd_Tails_Multiplier = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
                MinBet = GameSetting.MinimumBet != null ? GameSetting.MinimumBet.Value.ToString("#,##0") : "0";
                MaxBet = GameSetting.MaximumBet != null ? GameSetting.MaximumBet.Value.ToString("#,##0") : "0";
                ////await LoadDefaultBetDetails();
                //StreamUrl = GameSetting.GameStreamUrl;
                //// added by bong
                //if (StreamUrl != null)
                //{
                //    if (StreamUrl.Contains("ws://stream.goplayasia.com") || Constants.ForceAntMedia == true)
                //    {

                //        //isAntMedia = true;
                //        //AntManager.Current.Init(StreamID, AntWebRTCMode.Play, false);
                //        //AMInitMode = AntViewInitMode.InitOnViewRender;
                //        //AntManager.Current.DefaultServer = InitialData.SERVER_URL;
                //        //AntManager.Current.DefaultToken = InitialData.Token;
                //        //IsVideoPlaying = false;
                //        //IsVideoNotPlaying = true;
                //        //StreamUrl = InitialData.SERVER_URL;
                //        //MessagingCenter.Send(new GameHeaderView(), Constants.PlayAntMedia, StreamID);

                //        // Get from game setting
                //        isAntMedia = true;
                //        StreamID = StreamUrl.Split('/').Last(); //get last char as streamid
                //        StreamUrl = StreamUrl.TrimEnd('/').Remove(StreamUrl.LastIndexOf('/') + 1); //remove streamid
                //        StreamUrl = StreamUrl.Remove(StreamUrl.Length - 1, 1); // remove slash

                //        // Init antmedia
                //        AntManager.Current.Init(StreamID, AntWebRTCMode.Play, false);
                //        AMInitMode = AntViewInitMode.InitOnViewRender;
                //        AntManager.Current.DefaultServer = StreamUrl;
                //        AntManager.Current.DefaultToken = InitialData.Token;
                //        IsVideoPlaying = false;
                //        IsVideoNotPlaying = true;
                //        return;

                //    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public async Task GetGameRound()
    {
        int roundId = 0;
        IsBackToGameListingEnabled = true;

        var tempRound = await _igameRoundService.GetRound(GametypeId);
        if (tempRound == null)
        {

            await GetGameSetting();
            return;
        }
        else if (tempRound != null)
        {
            GameRound = tempRound;
            FixedPrice_Heads_TotalBets = GameRound.FixedLeftBet;
            FixedPrice_Tails_TotalBets = GameRound.FixedRightBet;
            RunningOdds_Heads_TotalBets = GameRound.OddsLeftBet;
            RunningOdd_Tails_TotalBets = GameRound.OddsRightBet;
            DrawTotalBets = GameRound.DrawBet;

            RunningOdds_Heads_Multiplier = GameRound.OddsLeftPercentage;
            RunningOdd_Tails_Multiplier = GameRound.OddsRightPercentage;

            GameResultString = !string.IsNullOrEmpty(GameRound.WinningResult) ? GameRound.WinningResult : null;

            if (GameSetting == null)
            {
                await GetGameSetting();
            }

            if (GameRound.RoundNumber > 0)
            {
                RoundNumber = GameRound.RoundNumber;
            }

            if (GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                GameResultString = null;

                RoundStatusString = Constants.Open;
                RoundStatusColor = Constants.GameOpenColor;
                IsFixed_Heads_Enabled = true;
                IsFixed_Tails_Enabled = true;
                IsRunndingOdds_Heads_Enabled = true;
                IsRunningOdds_Tails_Enabled = true;
                IsDrawEnabled = true;

                IsPayoutWon_Heads = false;
                IsPayoutWon_Tails = false;
                IsOddsWon_Heads = false;
                IsOddsWon_Tails = false;
                IsDrawWon = false;
            }
            else if (GameRound.RoundStatus == (int)RoundStatus.Closed)
            {
                RoundTimer = ""; // 00:00
                ShowFlashing = "timerNotFlashing";

                RoundStatusString = Constants.Closed;
                RoundStatusColor = Constants.GameClosedColor;
                IsFixed_Heads_Enabled = false;
                IsFixed_Tails_Enabled = false;
                IsRunndingOdds_Heads_Enabled = false;
                IsRunningOdds_Tails_Enabled = false;
                IsDrawEnabled = false;
                IsRunningOddsCancelled = GameRound.RunningOddsCancelled;
                IsFixedPrizeCancelled = GameRound.FixedCancelled;

                if (GameRound.WinningResult == Constants.Heads)
                {
                    if (!GameRound.FixedCancelled)
                    {
                        IsPayoutWon_Heads = true;
                    }

                    if (!GameRound.RunningOddsCancelled)
                    {
                        IsOddsWon_Heads = true;
                    }
                }
                else if (GameRound.WinningResult == Constants.Tails)
                {
                    if (!GameRound.FixedCancelled)
                    {
                        IsPayoutWon_Tails = true;
                    }

                    if (!GameRound.RunningOddsCancelled)
                    {
                        IsOddsWon_Tails = true;
                    }
                }
                else if (GameRound.WinningResult == Constants.Draw)
                {
                    IsDrawWon = true;
                }
            }
            else if (GameRound.RoundStatus == (int)RoundStatus.Paused)
            {
                RoundStatusString = Constants.Paused;
                RoundStatusColor = Constants.GamePausedColor;
                IsBackToGameListingEnabled = false;
            }
            else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
            {
                RoundTimer = ""; // 00:00
                ShowFlashing = "timerNotFlashing";

                IsFixed_Heads_Enabled = false;
                IsFixed_Tails_Enabled = false;
                IsRunndingOdds_Heads_Enabled = false;
                IsRunningOdds_Tails_Enabled = false;
                IsDrawEnabled = false;
                RoundStatusString = Constants.Cancelled;
                RoundStatusColor = Constants.GameCancelledColor;
                IsRunningOddsCancelled = GameRound.RunningOddsCancelled;
                IsFixedPrizeCancelled = GameRound.FixedCancelled;
            }
            else if (GameRound.RoundStatus == (int)RoundStatus.PendingResult)
            {
                RoundTimer = ""; // 00:00
                ShowFlashing = "timerNotFlashing";

                RoundStatusString = Constants.Closed;
                RoundStatusColor = Constants.GameClosedColor;
                IsRunningOddsCancelled = GameRound.RunningOddsCancelled;
                IsFixedPrizeCancelled = GameRound.FixedCancelled;
                IsFixed_Heads_Enabled = false;
                IsFixed_Tails_Enabled = false;
                IsRunndingOdds_Heads_Enabled = false;
                IsRunningOdds_Tails_Enabled = false;
                IsDrawEnabled = false;
            }

            await GetRoundBetSummary();
        }
    }
    public async Task GetCurrentRoundBets()
    {
        TotalBets = 0;
        var tempRoundBets = await _igameRoundService.GetBets(_icurrentUser.Id, GametypeId, null);
        if (tempRoundBets != null)
        {
            UserBets = new ObservableCollection<BetModel>(tempRoundBets);
            foreach (var item in UserBets)
            {
                TotalBets += Convert.ToInt32(item.BetAmount.Value);
            }
        }
    }
    public async Task NotifySignalRReconnection()
    {
        await GetRoundBetSummary();
        await GetCurrentRoundBets();
    }
    public async Task UpdateDrawBetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal DrawMultiplierValue = GameSetting.PayoutMultiplier != null ? Convert.ToDecimal(GameSetting.PayoutMultiplier) : 0;
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_Draw) ? decimal.Parse(UserTotalBet_Draw) + Amount : Amount;
            UserTotalBet_Draw = totalBets.ToString("#,##0");
            SampleWinDraw = totalBets != 0 ? (totalBets * DrawMultiplierValue).ToString("#,###.#0") : "0";
        }
    }
    public async Task UpdateFixed_Heads_BetAmount(int GameType, decimal Amount)
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
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrize_Heads) ? decimal.Parse(UserTotalBet_FixedPrize_Heads) + Amount : Amount;
                UserTotalBet_FixedPrize_Heads = totalBets.ToString("#,##0");
                SampleWinFixedPrize_Heads = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                BetCombinationValue = "";
            }, cancellationToken);
        }
    }
    public async Task UpdateFixed_Tails_BetAmount(int GameType, decimal Amount)
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
                decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrize_Tails) ? decimal.Parse(UserTotalBet_FixedPrize_Tails) + Amount : Amount;
                UserTotalBet_FixedPrize_Tails = totalBets.ToString("#,##0");
                SampleWinFixedPrize_Tails = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";

                Token_Animation = "";
                BetAmount = 0;
                BetCombinationValue = "";
            }, cancellationToken);
        }
    }
    public async Task UpdateRunningOdds_Heads_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_Odds_Heads) ? decimal.Parse(UserTotalBet_Odds_Heads) + Amount : Amount;
            UserTotalBet_Odds_Heads = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_Odds_Heads);

            await UpdateOddsValue();
        }
    }
    public async Task UpdateRunningOdds_Tails_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_Odds_Tails) ? decimal.Parse(UserTotalBet_Odds_Tails) + Amount : Amount;
            UserTotalBet_Odds_Tails = totalBets.ToString("#,##0");
            RaisePropertyChanged(() => UserTotalBet_Odds_Tails);

            await UpdateOddsValue();
        }
    }
    #endregion

    #region Trends
    public async Task GetTrends()
    {

        var tempTrends = await _igameRoundService.GetTrends(GametypeId);
        if (tempTrends != null)
        {
            //await SetResultBasedTrends(tempTrends);
            await SetPayoutTrendsDisplay(tempTrends);
            //await SetOddsTrendsDisplay(tempTrends);
        }
    }
    public async Task SetPayoutTrendsDisplay(List<GameRoundModel> tempTrends)
    {
        Trends = new ObservableCollection<GameRoundModel>(tempTrends.OrderBy(x => x.Id));
        var tempTrendList = new List<GameRoundModel>();
        var tempTrendListHolder = new TrendsDisplayModel();
        var tempTrendsForDisplay = new List<TrendsDisplayModel>();
        GameRoundModel current = new GameRoundModel();
        int count = 0;
        int indexCounter = 0;
        GameRoundModel lastNonDrawGame = new GameRoundModel();
        GameRoundModel lastItem = new GameRoundModel();

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
                    tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                    tempTrendsForDisplay.Add(tempTrendListHolder);

                    tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        count = 0;
                    }
                    else if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(excessList);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        var remaining = tempTrendList.OrderBy(x => x.Id).Skip(payoutRowLimit).ToList();
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(remaining);
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }
                    else
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
                        tempTrendListHolder = new TrendsDisplayModel();

                        current = t;
                        tempTrendList.Add(current);
                    }

                    if (lastItem.Id == t.Id)
                    {
                        indexCounter++;
                        tempTrendListHolder.ColumnIndex = indexCounter;
                        tempTrendListHolder.CurrentList = new ObservableCollection<GameRoundModel>(tempTrendList.OrderBy(x => x.Id));
                        tempTrendsForDisplay.Add(tempTrendListHolder);

                        tempTrendList = new List<GameRoundModel>();
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
    public async Task SetOddsTrendsDisplay(List<GameRoundModel> tempTrends)
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
                        Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<GameRoundModel>();

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
                    Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderBy(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<GameRoundModel>();

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
    #endregion

    #region Bet Methods
    public async Task OnCallBack()
    {
        await CallInvoke();
    }
    public async Task SetBetSelectedValue(object value)
    {
        BetAmount = (int)value;

        Payout = Convert.ToDecimal(BetAmount) * GameSetting.FixedPriceMultiplier.Value;
    }
    public async Task<bool> ValidateBet(string betOptionSelected, string betCombinationValue)
    {
        bool isvalid = true;

        var isCrossBetting = IsCrossBetting(betCombinationValue, UserBets).Result;
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
        else if (BetAmount > _icurrentUser.Credits)
        {
            popupModal.Show<PopupAddCredit>("Insufficient Credits", new ModalOptions() { Class = "op-modal", HideHeader = false });
            isvalid = false;
        }
        return isvalid;
    }
    public async Task<bool> IsDoubleBet()
    {
        bool result = false;
        if (UserBets != null && UserBets.Count > 0)
        {
            var duplicateBets = UserBets.Where(x => x.BetValue.ToLower().Contains(BetCombinationValue.ToLower())).ToList();
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

            BetDTO betParams = new BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.BetAmount = BetAmount;
            betParams.BetValue = BetCombinationValue;

            var popupRes = popupModal.Show<PopupLoading>("");
            var tempBetResult = await _igameRoundService.BetOnRound(betParams);
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

                tokenDiv = "none";
                tokenDisabled = true;
                betDisabled = true;
                Token_Animation = "1s";

                await GetCurrentRoundBets();
                if (BetCombinationValue.ToUpper().Contains(Constants.Draw))
                {
                    await UpdateDrawBetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedHeads))
                {
                    await UpdateFixed_Heads_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedTails))
                {
                    await UpdateFixed_Tails_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsHeads))
                {
                    await UpdateRunningOdds_Heads_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.RunningOddsTails))
                {
                    await UpdateRunningOdds_Tails_BetAmount(GametypeId, BetAmount);
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
    public async Task GetRoundBetSummary()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {

            var roundBetDetail = await _igameRoundService.GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
            if (roundBetDetail != null)
            {
                int pDelay = 0;
                if (Token_Animation != "")
                {
                    pDelay = 800;
                }

                Task.Delay(pDelay).ContinueWith(async (t) =>
                {
                    UserTotalBet_FixedPrize_Heads = !string.IsNullOrEmpty(roundBetDetail.LeftFixedBet) ? roundBetDetail.LeftFixedBet : "0";
                    SampleWinFixedPrize_Heads = roundBetDetail.LeftFixedWinnings;

                    UserTotalBet_FixedPrize_Tails = !string.IsNullOrEmpty(roundBetDetail.RightFixedBet) ? roundBetDetail.RightFixedBet : "0";
                    SampleWinFixedPrize_Tails = roundBetDetail.RightFixedWinnings;

                    UserTotalBet_Odds_Heads = !string.IsNullOrEmpty(roundBetDetail.LeftOddsBet) ? roundBetDetail.LeftOddsBet : "0";
                    SampleWinOdds_Heads = roundBetDetail.LeftOddsWinnings;

                    UserTotalBet_Odds_Tails = !string.IsNullOrEmpty(roundBetDetail.RightOddsBet) ? roundBetDetail.RightOddsBet : "0";
                    SampleWinOdds_Tails = roundBetDetail.RightOddsWinnings;

                    UserTotalBet_Draw = !string.IsNullOrEmpty(roundBetDetail.DrawBet) ? roundBetDetail.DrawBet : "0";
                    SampleWinDraw = roundBetDetail.DrawWinnings;

                    Token_Animation = "";
                    BetAmount = 0;
                    BetCombinationValue = "";
                }, cancellationToken);
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}

