using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Lucky9;

public partial class Lucky9BettingButton
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public string FixedPriceBankerTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddsPlayerTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddBankerTotalBets { get; set; } = "0";
    [Parameter] public string FixedPricePlayerTotalBets { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizePlayer { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeBanker { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsPlayer { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsBanker { get; set; } = "0";
    [Parameter] public string UserTotalBet_Draw { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizePlayer { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeBanker { get; set; } = "0";
    [Parameter] public string SampleWinOddsPlayer { get; set; } = "0";
    [Parameter] public string SampleWinOddsBanker { get; set; } = "0";
    [Parameter] public string SampleWinDraw { get; set; } = "0";

    [Parameter]
    public bool IsPayoutWon_Player { get; set; } = false;
    [Parameter]
    public bool IsPayoutWon_Banker { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Player { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Banker { get; set; } = false;
    [Parameter]
    public bool IsDrawWon { get; set; } = false;
    [Parameter]
    public bool IsRunningOddsCancelled { get; set; } = false;
    [Parameter]
    public bool IsFixedPrizeCancelled { get; set; } = false;
    [Parameter] public string FixedPricePlayerMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceBankerMultipler { get; set; } = "";
    [Parameter] public string RunningOddsPlayerMultiplier { get; set; } = "";
    [Parameter] public string RunningOddBankerMultiplier { get; set; } = "";
    [Parameter]
    public bool IsFixedBankerEnabled { get; set; } = false;
    [Parameter]
    public bool IsFixedPlayerEnabled { get; set; } = false;
    public bool IsBetPopupShown = false;
    public string activeButtonPlayer { get; set; } = "playerButton"; // (default banker value)
    public string activeButtonBanker { get; set; } = "bankerButton"; // (default banker color value)
    public string TargetPlayer { get; set; } = "pLucky9";
    public string TargetBanker { get; set; } = "bLucky9";
    public string TargetTie { get; set; } = "tieButton";
    public string SubAnyPairPlayer { get; set; } = "anyPairPlayer";
    public string SubAnyPairBanker { get; set; } = "anyPairBanker";
    public string SameSuitePlayer { get; set; } = "sameSuitPlayer";
    public string SameSuiteBanker { get; set; } = "sameSuitBanker";
    public string TwoRedPlayer { get; set; } = "reds2Player";
    public string TwoRedBanker { get; set; } = "reds2Banker";
    public string TwoBlackPlayer { get; set; } = "black2Player"; // (default banker value)
    public string TwoBlackBanker { get; set; } = "black2Banker"; // (default banker color value)
    public string TokenContainer { get; set; } = "DistokenContainer";
    public string resetButtonCss { get; set; } = "disResetButton";
    public string betButtonCss { get; set; } = "disBetButton";

    protected override async Task OnParametersSetAsync()
    {
        if (RoundStatusString != Constants.Open)
        {
            await DisableGameButtonsExceptCurrent();
        }
    }
    protected override async Task OnInitializedAsync()
    {
        luckyViewModel.Notify += OnNotify;

    }
    private async Task ResetGameButtons()
    {
        luckyViewModel.IsbuttonDisabled = !luckyViewModel.IsbuttonDisabled;

        activeButtonPlayer = "playerButton";
        activeButtonBanker = "bankerButton";
        TargetPlayer = "pLucky9";
        TargetBanker = "bLucky9";
        TargetTie = "tieButton";
        TwoRedPlayer = "reds2Player";
        TwoRedBanker = "reds2Banker";
        TwoBlackPlayer = "black2Player";
        TwoBlackBanker = "black2Banker";
        SubAnyPairPlayer = "anyPairPlayer";
        SubAnyPairBanker = "anyPairBanker";
        SameSuitePlayer = "sameSuitPlayer";
        SameSuiteBanker = "sameSuitBanker";

        resetButtonCss = "disResetButton";
        betButtonCss = "disBetButton";
    }
    public async Task DisableGameButtonsExceptCurrent()
    {
        if (luckyViewModel.RoundStatusString != Constants.Open) luckyViewModel.CurrentGameType = 0;
        if (luckyViewModel.CurrentGameType != (int)L9MainBetTypes.Banker) activeButtonBanker = "DisbankerButton";
        if (luckyViewModel.CurrentGameType != (int)L9MainBetTypes.Player) activeButtonPlayer = "DisplayerButton";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.Target9Player) TargetPlayer = "dispLucky9";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.Target9Banker) TargetBanker = "disBLucky9";
        if (luckyViewModel.CurrentGameType != (int)L9MainBetTypes.Draw) TargetTie = "disTieButton";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.AnypairPlayer) SubAnyPairPlayer = "disAnyPairPlayer";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.AnyPairBanker) SubAnyPairBanker = "disAnyPairBanker";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.SameSuitePlayer) SameSuitePlayer = "disSameSuitPlayer";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.SameSuiteBanker) SameSuiteBanker = "disSameSuitBanker";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.TwoRedPlayer) TwoRedPlayer = "disReds2Player";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.TwoRedBanker) TwoRedBanker = "disReds2Banker";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.TwoBlackPlayer) TwoBlackPlayer = "disBlack2Player";
        if (luckyViewModel.CurrentGameType != (int)L9SubBetTypes.TwoBlackBanker) TwoBlackBanker = "disBlack2Banker";

    }

    public async Task CheckIfPlaceBetReady()
    {
        //await DisableGameButtonsExceptCurrent();
        if (luckyViewModel.CurrentGameType != 0 && luckyViewModel.BetAmount > 0)
        {
            resetButtonCss = "resetButton";
            betButtonCss = "betButton";
        }
        else
        {
            resetButtonCss = "DisResetButton";
            betButtonCss = "DisBetButton";
        }
    }

    public async Task ResetToInitial()
    {
        await ResetGameButtons();
        TokenContainer = "DistokenContainer";
        luckyViewModel.CurrentGameType = 0;
        luckyViewModel.BetAmount = 0;
        luckyViewModel.GameVariantId = 0;
        luckyViewModel.BetOptionSelected = string.Empty;
        luckyViewModel.BetCombinationValue = string.Empty;
    }

    public async Task SelectBetType(int BetType)
    {
        //reset all
        await ResetGameButtons();
        TokenContainer = "tokenContainer";
        //set Game Type
        luckyViewModel.CurrentGameType = BetType;
        luckyViewModel.BetType = BetType;

        switch (BetType)
        {
            case (int)L9MainBetTypes.Player:
                var isCrossBetting = luckyViewModel.L9IsCrossBetting(Constants.FixedPlayer, luckyViewModel.L9UserBets).Result;
                if (isCrossBetting)
                {
                    toastService.ShowError("Cross betting not allowed!");
                    await ResetGameButtons();
                    return;
                }
                activeButtonPlayer = "playerButtonActive";
                await luckyViewModel.SetGameChips(luckyViewModel.GametypeId, false);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.FixedPlayer;
                luckyViewModel.GameVariantId = 0;
                break;

            case (int)L9MainBetTypes.Banker:
                var isCrossBettingBanker = luckyViewModel.L9IsCrossBetting(Constants.FixedBanker, luckyViewModel.L9UserBets).Result;
                if (isCrossBettingBanker)
                {
                    toastService.ShowError("Cross betting not allowed!");
                    await ResetGameButtons();
                    return;
                }

                activeButtonBanker = "bankerButtonActive";
                await luckyViewModel.SetGameChips(luckyViewModel.GametypeId, false);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.FixedBanker;
                luckyViewModel.GameVariantId = 0;
                break;

            case (int)L9SubBetTypes.Target9Player:
                await luckyViewModel.SetGameChips((int)GameVariant.L9Target, true);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.TargetPlayer;
                TargetPlayer = "pLucky9Activated";
                luckyViewModel.GameVariantId = (int)GameVariant.L9Target;
                break;
            case (int)L9SubBetTypes.Target9Banker:
                TargetBanker = "bLucky9Activated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Target, true);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.TargetBanker;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Target;
                break;
            case (int)L9MainBetTypes.Draw:
                await luckyViewModel.SetGameChips((int)GameVariant.L9Draw, true);
                TargetTie = "tieButtonActivated";
                luckyViewModel.BetOptionSelected = Constants.Draw;
                luckyViewModel.BetCombinationValue = Constants.Draw;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Draw;
                break;
            case (int)L9SubBetTypes.AnypairPlayer:
                SubAnyPairPlayer = "anyPairPlayerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Pair, true);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.PairPlayer;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Pair;
                break;
            case (int)L9SubBetTypes.AnyPairBanker:
                SubAnyPairBanker = "anyPairBankerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Pair, true);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.PairBanker;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Pair;
                break;
            case (int)L9SubBetTypes.SameSuitePlayer:
                SameSuitePlayer = "sameSuitPlayerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Suits, true);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.SuitsPlayer;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Suits;
                break;
            case (int)L9SubBetTypes.SameSuiteBanker:
                SameSuiteBanker = "sameSuitBankerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Suits, true);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.SuitsBanker;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Suits;
                break;
            case (int)L9SubBetTypes.TwoRedPlayer:
                TwoRedPlayer = "reds2PlayerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Color, true);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.ColorRedPlayer;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Color;
                break;
            case (int)L9SubBetTypes.TwoRedBanker:

                TwoRedBanker = "reds2BankerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Color, true);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.ColorRedBanker;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Color;
                break;
            case (int)L9SubBetTypes.TwoBlackPlayer:
                TwoBlackPlayer = "black2PlayerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Color, true);
                luckyViewModel.BetOptionSelected = Constants.Player;
                luckyViewModel.BetCombinationValue = Constants.ColorBlackPlayer;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Color;
                break;
            case (int)L9SubBetTypes.TwoBlackBanker:
                TwoBlackBanker = "black2BankerActivated";
                await luckyViewModel.SetGameChips((int)GameVariant.L9Color, true);
                luckyViewModel.BetOptionSelected = Constants.Banker;
                luckyViewModel.BetCombinationValue = Constants.ColorBlackBanker;
                luckyViewModel.GameVariantId = (int)GameVariant.L9Color;
                break;
            default:
                break;
        }

        //show chips
        await luckyViewModel.ShowDivToken();
    }
    /*** NOTE OLD METHODS **/
    public async Task ShowBetPopup(string betOptionSelected, string betCombinationValue)
    {
        if (IsBetPopupShown) return;

        luckyViewModel.BetOptionSelected = betOptionSelected;
        luckyViewModel.BetCombinationValue = betCombinationValue;

        bool iscrossbetting = await luckyViewModel.IsCrossBetting(luckyViewModel.BetCombinationValue, luckyViewModel.UserBets);
        if (!iscrossbetting)
        {
            luckyViewModel.popupBet = popupModal.Show<Lucky9BetPopup>("Place Bet", new ModalOptions() { Class = "bet-modal" });
            IsBetPopupShown = true;
            ModalResult result = await luckyViewModel.popupBet.Result;
            IsBetPopupShown = false;
        }
        else
        {
            toastService.ShowError("Cross betting not allowed!");
        }

    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public void Dispose()
    {
        luckyViewModel.Notify -= OnNotify;
    }

    public async Task SubmitBet()
    {
        bool valid = await luckyViewModel.ValidateBet(luckyViewModel.BetOptionSelected, luckyViewModel.BetCombinationValue);
        if (valid)
        {
            //var parameters = new ModalParameters();
            //parameters.Add("Message", "Confirm bet " + luckyViewModel.BetAmount + " for " + luckyViewModel.BetCombinationValue);

            //luckyViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            //IsBetPopupShown = true;
            //ModalResult result = await luckyViewModel.popupConfirm.Result;
            //IsBetPopupShown = false;
            //if (result.Data != null)
            //{
            //    if ((bool)result.Data)
            //    {
            //        if ((bool)result.Data)
            //        {
            bool doublebet = await luckyViewModel.IsDoubleBet();
            if (doublebet)
            {
                var parametersdb = new ModalParameters();
                parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                ModalResult resultdb = await popupDB.Result;
                if ((bool)resultdb.Data)
                {
                    betButtonCss = "disBetButton";
                    resetButtonCss = "disResetButton";
                    await luckyViewModel.SubmitBet();
                    betButtonCss = "BetButton";
                    resetButtonCss = "ResetButton";
                }
            }
            else
            {
                betButtonCss = "disBetButton";
                resetButtonCss = "disResetButton";
                await luckyViewModel.SubmitBet();
                betButtonCss = "BetButton";
                resetButtonCss = "ResetButton";
            }

            await ResetToInitial();
            //luckyViewModel.popupConfirm.Close();

            //        }



            //    }
            //}
        }
    }

}
