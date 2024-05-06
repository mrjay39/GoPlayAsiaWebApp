using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.Bingo;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels
{
    public class BingoViewModel : BaseViewModel
    {
        public IBingoGameRoundService _igameRoundService;
        public new BingoGameRoundDTO GameRound { get; set; }
        public string JackpotPrize { get; set; } = string.Empty;
        public string MiniJackpot { get; set; } = string.Empty;
        public string BaseBetValue { get; set; } = string.Empty;
        public bool IsGameOpen { get; set; } = false;
        public bool IsLuckyPickEnabled { get; set; } = false;
        public new string GameResult { get; set; } = string.Empty;
        public new ObservableCollection<BingoBetDTO> UserBets { get; set; }
        public new BingoGameRoundDTO PrevGameRound { get; set; }
        private new ObservableCollection<BingoBetDTO> UserPrevBets { get; set; }
        public BingoPermutation _bingoPermutation { get; set; } = new BingoPermutation();

        public string BetValue1 { get; set; } = "c-k";
        public string BetValue2 { get; set; } = "d-2";
        public string BetValue3 { get; set; } = "";
        public string BetValue4 { get; set; } = "";
        public string BetValue5 { get; set; } = "";
        public string BetValue6 { get; set; } = "";
        public string BetValue7 { get; set; } = "";
        public string BetValue8 { get; set; } = "";

        public List<BingoPrizePerDrawDTO> _BingoPrizePerDrawDTO { get; set; }

        #region Lifecycle Methods
        public BingoViewModel(ICurrentUser icurrentUser, IConfiguration iconfig,
                              IBingoGameRoundService igameRoundService,
                              IToastService toastService,
                              IAccountService iaccountService,
                              IGameSettingService igameSettingService,
                              NavigationManager navigationManager,
                              AuthenticationStateProvider AuthenticationStateProvider)
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
            GametypeId = (int)GameTypes.Bingo;

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
                // put result update here
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
                    RoundTimer = value != null ? value : "";
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

                BetAmount = int.Parse(BaseBetValue);

                if (gameStatus == (int)RoundStatus.Open)
                {
                    TickerMessage = "";
                    IsGameOpen = true;
                    IsLuckyPickEnabled = true;

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
                await CallInvoke();
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
        public async Task GetTrends()
        {
            return;
            if (GametypeId > 0)
            {
                var tempTrends = await _igameRoundService.GetTrends();
                if (tempTrends != null)
                {
                    PrevGameRound = tempTrends.First();
                    await CallInvoke();
                }
            }
        }
        public async Task GetCurrentRoundBets()
        {
            long UserId = _icurrentUser.Id;
            if (GameRound == null) return;
            var tempRoundBets = await _igameRoundService.BingoGetBet(UserId, GametypeId, GameRound.Id);
            if (tempRoundBets != null)
            {
                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    UserBets = new ObservableCollection<BingoBetDTO>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderByDescending(x => x.BetStatus).ThenBy(x => x.BetValue));
                    var LoseUserPrevBets = new ObservableCollection<BingoBetDTO>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderByDescending(x => x.Id));
                    foreach (var item in LoseUserPrevBets)
                    {
                        UserBets.Add(item);
                    }
                }
                else
                {
                    UserBets = new ObservableCollection<BingoBetDTO>(tempRoundBets.Where(x => x.BetStatus != (int)BetStatus.Lose).OrderByDescending(x => x.BetStatus).ThenBy(x => x.BetValue));
                    var LoseUserPrevBets = new ObservableCollection<BingoBetDTO>(tempRoundBets.Where(x => x.BetStatus == (int)BetStatus.Lose).OrderBy(x => x.BetValue));
                    foreach (var item in LoseUserPrevBets)
                    {
                        UserBets.Add(item);
                    }
                }
                TotalBets = (int)UserBets.Sum(x => Convert.ToDecimal(x.BetAmount));

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
                    _toastService.ShowError("No round details found.");
                }
                else if (tempRoundSettings != null)
                {
                    GameSetting = tempRoundSettings;
                    //BaseBetValue = tempRoundSettings.FixValue != null ? Convert.ToDecimal(tempRoundSettings.FixValue).ToString("0") : "0";
                    //JackpotPrize = tempRoundSettings.Jackpot != null ? Convert.ToDecimal(tempRoundSettings.Jackpot).ToString("#,###.#0") : "0";
                    //MiniJackpot = tempRoundSettings.MiniJackpot != null ? Convert.ToDecimal(tempRoundSettings.MiniJackpot).ToString("#,###.#0") : "0";
                    //ConsolationPrize = tempRoundSettings.Consolation != null ? Convert.ToDecimal(tempRoundSettings.Consolation).ToString("#,##0") : "0";
                    //fixPrize = tempRoundSettings.FixValue != null ? Convert.ToInt32(tempRoundSettings.FixValue) : 0;
                    //BetAmount = int.Parse(BaseBetValue);
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
                RoundTimer = ""; // 00:00
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
                        IsGameOpen = false;
                        IsLuckyPickEnabled = false;
                        GameResult = string.Empty;
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
        public async Task GetPrevRoundBets()
        {
            long UserId = _icurrentUser.Id;
            if (GameRound == null) return;
            var tempRoundBets = await _igameRoundService.GetPrevGameBets(UserId, GametypeId, 100);
            if (tempRoundBets != null)
            {
                UserPrevBets = new ObservableCollection<BingoBetDTO>(tempRoundBets.OrderByDescending(x => x.GameRound.RoundNumber).ThenByDescending(x => x.Winnings).ThenBy(x => x.BetValue));
            }
        }

        public async Task GetGetPrizeByChip(int chip)
        {
            _BingoPrizePerDrawDTO = await _igameRoundService.GetPrizeByChip(chip);

            await CallInvoke();
        }
        #endregion
    }
}
