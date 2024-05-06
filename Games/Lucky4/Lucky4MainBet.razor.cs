using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Lucky4;

public partial class Lucky4MainBet
{
    #region PARAMETERS
    // For Betting
    [Parameter] public int currentChips { get; set; } = 1;
    [Parameter] public GameChipModel? GameChips { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public int RoundNumber { get; set; }



    //For Header
    [Parameter]
    public string LetterValue { get; set; } = "";
    [Parameter]
    public string TotalAccumulatedBetsString { get; set; } = "";
    [Parameter]
    public string LetterValue1 { get; set; } = "";
    [Parameter]
    public string LetterValue2 { get; set; } = "";
    [Parameter]
    public string LetterValue3 { get; set; } = "";
    [Parameter]
    public string LetterValue4 { get; set; } = "";

    //Generic Parameters
    [CascadingParameter] public IModalService popupModal { get; set; }
    #endregion

    #region Trends Parameter for Bets
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F2 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F3 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_All4 { get; set; }
    [Parameter]
    public bool IsPrevious { get; set; } = false;
    [Parameter]
    public GameRoundModel? PrevGameRound { get; set; }
    #endregion

    #region Third Party
    public IModalReference popupConfirm { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    #endregion

    #region UI For Lucky Pick
    public bool luckypickenabled { get; set; } = false;
    public int CurrentLuckyPick { get; set; } = 0;
    public string quickpick1 { get; set; } = "";
    public string quickpick3 { get; set; } = "";
    public string quickpick5 { get; set; } = "";
    private string quickPickDisabledParent = "quickPickDisabledParent";
    #endregion

    #region UI For Token Selection
    public string tokenSelectionCss = "";
    public bool tokenSelectionEnabled = false;
    public string tokenSelectedIcondPath = "tokenSelected";
    public string tokenIconPath = "chipCon";
    #endregion

    #region UI For Bets
    public bool IsBetPopupShown = false;
    public int WinRate { get; set; }
    #endregion

    #region VARIABLES FOR BET BUTTONS

    public string disableCancel { get; set; } = string.Empty;
    public string disableBet { get; set; } = string.Empty;

    #endregion

    #region Variables for Header

    private string GameTypeEz2 = "eazy2";
    private string GameTypeWer3 = "swer3";
    private string GameTypeAll4 = "lucky4";
    private string SelGameType = "";
    private string GameType = "Select Game";
    private string Multiplier = "0.00";

    #endregion

    #region Life Cycle Events

    protected override async Task OnInitializedAsync()
    {
        iLucky4Model.Notify += OnNotify;
        await Reset(true);
    }
    private async Task Reset(bool isStart = false)
    {

        await ClearBet();

    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public async ValueTask DisposeAsync()
    {
        iLucky4Model.Notify -= OnNotify;
    }

    #endregion

    #region Bet Methods

    private async Task RemoveLastCharacter()
    {
        await iLucky4Model.RemoveLastCharacter();
    }
    private async Task ClearBet()
    {
        SelGameType = "";
        iLucky4Model.BetAmountDisplay = "";
        iLucky4Model.IsPlaceBetEnabled = false;
        iLucky4Model.LetterValue = "";
        iLucky4Model.BetAmount = 0;
        iLucky4Model.BetMultiplier = "";
        iLucky4Model.BetAmountDisplay = iLucky4Model.BaseValue.ToString();
        iLucky4Model.SelectedChipAmount = 0;
        iLucky4Model.BetTypeId = (int)NonCardGameBetTypes.Normal;
        await iLucky4Model.ResetDefaultAmount();
        iLucky4Model.IsEnabled_First2 = false;
        iLucky4Model.IsEnabled_First3 = false;
        iLucky4Model.IsEnabled_All4 = false;
    }
    private async Task SetLuckyPick()
    {
        if (!luckypickenabled) return;

        //reset
        quickpick1 = "quickPickDisable";
        quickpick3 = "quickPickDisable";
        quickpick5 = "quickPickDisable";

        if (CurrentLuckyPick == 3)
        {
            CurrentLuckyPick = 1;
        }
        else
        {
            CurrentLuckyPick += 1;
        }
        switch (CurrentLuckyPick)
        {
            case 1:
                quickpick1 = "quickPickActivated-1";
                await iLucky4Model.SetLuckyPickBet("LuckyPick");
                break;
            case 2:
                quickpick3 = "quickPickActivated-3";
                await iLucky4Model.SetLuckyPickBet("LuckyPickx3");
                break;
            case 3:
                quickpick5 = "quickPickActivated-5";
                await iLucky4Model.SetLuckyPickBet("LuckyPickx5");
                break;

        }
    }
    private async Task EnableDisableLuckyPick(bool enable)
    {
        if (enable)
        {
            quickPickDisabledParent = "QuickPick";
            luckypickenabled = enable;
            quickpick1 = "quickPickActivated-1";
            quickpick3 = "quickPickActivated-3";
            quickpick5 = "quickPickActivated-5";
        }
        else
        {
            quickPickDisabledParent = "quickPickDisabledParent";
            quickpick1 = "quickPickDisable";
            quickpick3 = "quickPickDisable";
            quickpick5 = "quickPickDisable";
            luckypickenabled = enable;
            CurrentLuckyPick = 0;
        }
    }
    private async Task AddCharacter(string param)
    {

        await iLucky4Model.AddCharacter(param);


    }
    public async Task ClickBetButton(string BetType)
    {
        #region if Luckypick is active set gam,e type only
        if (iLucky4Model.BetTypeId != (int)NonCardGameBetTypes.Normal)             //disable if lucky pick due to chips
        {

            //GameTypeEz2 = "TabDisable TabDisableBg";
            //GameTypeWer3 = "TabDisable TabDisableBg";
            //GameTypeAll4 = "TabDisable TabDisableBg";
            //SelGameType = BetType.ToString();
            //switch (BetType)
            //{
            //    case "F2":
            //        GameType = "EAZY 2";           
            //        GameTypeEz2 = "eazy2";                  
            //        Multiplier = iLucky4Model.RatioDisplay_F2;
            //        await iLucky4Model.SetGameTypeForLuckyPick(BetType.ToString());
            //        break;
            //    case "F3":
            //        GameType = "SWER 3";   
            //        GameTypeWer3 = "swer3";
            //        Multiplier = iLucky4Model.RatioDisplay_F3;
            //        await iLucky4Model.SetGameTypeForLuckyPick(BetType.ToString());
            //        break;
            //    case "ALL4":
            //        GameType = "LUCKY 4";
            //        GameTypeAll4 = "lucky4";
            //        Multiplier = iLucky4Model.RatioDisplay_All4;
            //        await iLucky4Model.SetGameTypeForLuckyPick(BetType.ToString());
            //        break;              
            //}

            return;
        }
        #endregion

        if (SelGameType != string.Empty && iLucky4Model.LetterValue.Length > 0)
        {
            return;
        }
        #region if Lucky pick is not enabled
        // set lucky pic as disabled on start

        await EnableDisableLuckyPick(true);

        if (SelGameType != BetType.ToString() || iLucky4Model.IsLuckyPick1 || iLucky4Model.IsLuckyPick5)
        {
            await iLucky4Model.BetOptionSelected(BetType);
        }

        SelGameType = BetType.ToString();
        await EnableDisableLuckyPick(true);
        switch (BetType)
        {
            case "F2":
                GameType = "EAZY 2";
                Multiplier = iLucky4Model.RatioDisplay_F2;
                await iLucky4Model.BetOptionSelected(BetType.ToString());
                iLucky4Model.IsBettingEnabled = false;
                WinRate = iLucky4Model.SampleWin_F2;
                break;
            case "F3":
                GameType = "SWER 3";
                Multiplier = iLucky4Model.RatioDisplay_F3;
                await iLucky4Model.BetOptionSelected(BetType.ToString());
                iLucky4Model.IsBettingEnabled = false;
                WinRate = iLucky4Model.SampleWin_F3;
                break;
            case "ALL4":
                GameType = "LUCKY 4";
                Multiplier = iLucky4Model.RatioDisplay_All4;
                await iLucky4Model.BetOptionSelected(BetType.ToString());
                iLucky4Model.IsBettingEnabled = false;
                WinRate = iLucky4Model.SampleWin_All4;
                break;
            default:
                GameType = "Select Game";
                await ClearBet();
                await EnableDisableLuckyPick(false);
                Multiplier = "0.00";
                iLucky4Model.IsBettingEnabled = false;
                break;

        }
        await SetScreenState(2);
        #endregion
    }


    private async Task LetterClicked()
    {
        StateHasChanged();

    }

    private async Task PlaceBet()
    {
        //if (IsBetPopupShown) return;
        if (iLucky4Model.IsPlaceBetEnabled)
        {
            iLucky4Model.IsPlaceBetEnabled = false;
            var valid = await iLucky4Model.PlaceBet();
            if (valid)
            {
                //skip confirmation
                await iLucky4Model.SubmitBet();
                iLucky4Model.CurrView = "Current";
                await Reset();
                return;

                var parameters = new ModalParameters();
                string msg = "";
                switch (iLucky4Model.BetTypeId)
                {
                    case (int)NonCardGameBetTypes.Normal:
                        msg = "Confirm bet " + iLucky4Model.BetAmount + " for " + iLucky4Model.LetterValue;
                        break;
                    case (int)NonCardGameBetTypes.LuckyPick:
                        msg = "Confirm bet " + iLucky4Model.BetAmount + " for Lucky Pick x 1";
                        break;
                    case (int)NonCardGameBetTypes.LuckyPickx5:
                        msg = "Confirm bet " + iLucky4Model.BetAmount + " for Lucky Pick x 5";
                        break;
                    case (int)NonCardGameBetTypes.LuckyPickx3:
                        msg = "Confirm bet " + iLucky4Model.BetAmount + " for Lucky Pick x 3";
                        break;
                }

                parameters.Add("Message", msg);

                popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
                IsBetPopupShown = true;
                ModalResult result = await popupConfirm.Result;
                IsBetPopupShown = false;
                if (result.Data != null)
                {
                    if ((bool)result.Data)
                    {
                        bool doublebet = await iLucky4Model.IsDoubleBet();
                        if (doublebet)
                        {
                            var parametersdb = new ModalParameters();
                            parametersdb.Add("Message", "Already Placed Bet on " + iLucky4Model.DuplicateBetvalue + ", Do you wish to proceed?");

                            var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                            ModalResult resultdb = await popupDB.Result;
                            if ((bool)resultdb.Data)
                            {
                                await iLucky4Model.SubmitBet();
                                iLucky4Model.CurrView = "Current";
                                await Reset();
                            }
                        }
                        else
                        {
                            await iLucky4Model.SubmitBet();
                            iLucky4Model.CurrView = "Current";
                            await Reset();
                        }

                        popupConfirm.Close();
                    }
                }
            }
        }
    }
    #endregion

    #region Screen Functions

    private async Task SetScreenState(int state)
    {
        iLucky4Model.Screenstate = state;
        switch (state)
        {
            case 1: // initial state
                disableBet = " betNumberDisable betNumberDisableBg ";
                disableCancel = " betNumberDisable betNumberDisableBg ";

                SelGameType = "";
                GameTypeEz2 = "eazy2";
                GameTypeWer3 = "swer3";
                GameTypeAll4 = "lucky4";
                tokenSelectionCss = "tokenContainerDisabled";
                tokenSelectionEnabled = false;
                iLucky4Model.SelectedChipAmount = 0;
                await EnableDisableLuckyPick(false);
                iLucky4Model.ShowLetterBetOptions = false;
                GameType = "";
                WinRate = 0;
                break;
            case 2: //select lucky pick or bets
                iLucky4Model.IsBettingEnabled = false;
                disableBet = "";
                disableCancel = "  ";
                GameTypeEz2 = "TabDisable TabDisableBg";
                GameTypeWer3 = "TabDisable TabDisableBg";
                GameTypeAll4 = "TabDisable TabDisableBg";
                iLucky4Model.ShowLetterBetOptions = false;
                switch (SelGameType) // all for css screen
                {
                    case "F2":
                        GameTypeEz2 = "eazy2";
                        break;
                    case "F3":
                        GameTypeWer3 = "swer3";
                        break;
                    case "ALL4":
                        GameTypeAll4 = "lucky4";
                        break;
                    default:
                        break;
                }
                quickPickDisabledParent = "QuickPick";
                break;
            case 3: //enable lucky pick
                if (!luckypickenabled) return;
                await EnableDisableLuckyPick(true);
                await SetLuckyPick();
                disableBet = " betNumberDisable betNumberDisableBg ";
                tokenSelectionCss = "";
                tokenSelectionEnabled = true;
                disableCancel = "  ";
                break;

            case 4:
                await EnableDisableLuckyPick(false);

                break;
        }

        StateHasChanged();
    }

    private async Task SetCurrChips(int chip)
    {
        currentChips = chip;
    }

    private async Task SetBetAmount(int? param, string? multipler = "")
    {
        await iLucky4Model.SetBetAmount(param, multipler);

    }
    #endregion


}
