using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.Games.First3;

public partial class First3BettingButton
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public string FixedPriceSilverTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddsGoldTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddSilverTotalBets { get; set; } = "0";
    [Parameter] public string FixedPriceGoldTotalBets { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeGold { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeSilver { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsGold { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsSilver { get; set; } = "0";
    [Parameter] public string UserTotalBet_Draw { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeGold { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeSilver { get; set; } = "0";
    [Parameter] public string SampleWinOddsGold { get; set; } = "0";
    [Parameter] public string SampleWinOddsSilver { get; set; } = "0";
    [Parameter] public string SampleWinDraw { get; set; } = "0";

    [Parameter]
    public bool IsPayoutWon_Gold { get; set; } = false;
    [Parameter]
    public bool IsPayoutWon_Silver { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Gold { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Silver { get; set; } = false;
    [Parameter]
    public bool IsDrawWon { get; set; } = false;
    [Parameter]
    public bool IsRunningOddsCancelled { get; set; } = false;
    [Parameter]
    public bool IsFixedPrizeCancelled { get; set; } = false;
    [Parameter] public string FixedPriceGoldMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceSilverMultipler { get; set; } = "";
    [Parameter] public string RunningOddsGoldMultiplier { get; set; } = "";
    [Parameter] public string RunningOddSilverMultiplier { get; set; } = "";
    [Parameter]
    public bool IsFixedSilverEnabled { get; set; } = false;
    [Parameter]
    public bool IsFixedGoldEnabled { get; set; } = false;
    public bool IsBetPopupShown = false;
    public string activeButtonGold { get; set; } = "goldButton"; // (default Silver value)
    public string activeButtonSilver { get; set; } = "silverButton"; // (default Silver color value)
    public string Trio { get; set; } = "trioButton";
    public string SubAnyPairGold { get; set; } = "anyPairGold";
    public string SubAnyPairSilver { get; set; } = "anyPairSilver";
    public string SameSuiteGold { get; set; } = "sameSuitGold";
    public string SameSuiteSilver { get; set; } = "sameSuitSilver";
    public string TwoRedGold { get; set; } = "reds2Gold";
    public string TwoRedSilver { get; set; } = "reds2Silver";
    public string TwoBlackGold { get; set; } = "black2Gold"; // (default Silver value)
    public string TwoBlackSilver { get; set; } = "black2Silver"; // (default Silver color value)
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
        first3ViewModel.Notify += OnNotify;

    }
    private async Task ResetGameButtons()
    {
        first3ViewModel.isbuttonDisabled = !first3ViewModel.isbuttonDisabled;

        activeButtonSilver = "silverButton";
        activeButtonGold = "goldButton";
        Trio = "trioButton";
        SubAnyPairGold = "anyPairGold";
        SubAnyPairSilver = "anyPairSilver";
        SameSuiteGold = "sameSuitGold";
        SameSuiteSilver = "sameSuitSilver";
        TwoRedGold = "reds2Gold";
        TwoRedSilver = "reds2Silver";
        TwoBlackGold = "black2Gold";
        TwoBlackSilver = "black2Silver";
        resetButtonCss = "disResetButton";
        betButtonCss = "disBetButton";
    }
    public async Task DisableGameButtonsExceptCurrent()
    {
        if (first3ViewModel.RoundStatusString != Constants.Open) first3ViewModel.CurrentGameType = 0;
        if (first3ViewModel.CurrentGameType != (int)F3MainBetTypes.Silver) activeButtonSilver = "DisSilverButton";
        if (first3ViewModel.CurrentGameType != (int)F3MainBetTypes.Gold) activeButtonGold = "DisGoldButton";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.Trio) Trio = "disTrioButton";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.SameSuiteGold) SameSuiteGold = "disSameSuitGold";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.SameSuiteSilver) SameSuiteSilver = "disSameSuitSilver";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.TwoRedGold) TwoRedGold = "disReds2Gold";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.TwoRedSilver) TwoRedSilver = "disReds2Silver";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.TwoBlackGold) TwoBlackGold = "disBlack2Gold";
        if (first3ViewModel.CurrentGameType != (int)F3SubBetTypes.TwoBlackSilver) TwoBlackSilver = "disBlack2Silver";

    }

    public async Task CheckIfPlaceBetReady()
    {
        //await DisableGameButtonsExceptCurrent();
        if (first3ViewModel.CurrentGameType != 0 && first3ViewModel.BetAmount > 0)
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
        first3ViewModel.CurrentGameType = 0;
        first3ViewModel.BetAmount = 0;
        first3ViewModel.GameVariantId = 0;
        first3ViewModel.BetOptionSelected = string.Empty;
        first3ViewModel.BetCombinationValue = string.Empty;
    }

    public async Task SelectBetType(int BetType)
    {
        //reset all
        await ResetGameButtons();
        TokenContainer = "tokenContainer";
        //set Game Type
        first3ViewModel.CurrentGameType = BetType;
        first3ViewModel.BetType = BetType;

        switch (BetType)
        {
            case (int)F3MainBetTypes.Gold:
                var isCrossBetting = first3ViewModel.F3IsCrossBetting(Constants.FixedGold, first3ViewModel.F3UserBets).Result;
                if (isCrossBetting)
                {
                    toastService.ShowError("Cross betting not allowed!");
                    await ResetGameButtons();
                    return;
                }
                activeButtonGold = "goldButtonActive";
                await first3ViewModel.SetGameChips(first3ViewModel.GametypeId, false);
                first3ViewModel.BetOptionSelected = Constants.Gold;
                first3ViewModel.BetCombinationValue = Constants.FixedGold;
                first3ViewModel.GameVariantId = 0;

                break;
            case (int)F3MainBetTypes.Silver:
                var isCrossBettingSilver = first3ViewModel.F3IsCrossBetting(Constants.FixedSilver, first3ViewModel.F3UserBets).Result;
                if (isCrossBettingSilver)
                {
                    toastService.ShowError("Cross betting not allowed!");
                    await ResetGameButtons();
                    return;
                }

                activeButtonSilver = "silverButtonActive";
                await first3ViewModel.SetGameChips(first3ViewModel.GametypeId, false);
                first3ViewModel.BetOptionSelected = Constants.Silver;
                first3ViewModel.BetCombinationValue = Constants.FixedSilver;
                first3ViewModel.GameVariantId = 0;
                break;

            case (int)F3SubBetTypes.Trio:
                await first3ViewModel.SetGameChips((int)GameVariant.F3Trio, true);
                first3ViewModel.BetOptionSelected = Constants.Trio;
                first3ViewModel.BetCombinationValue = Constants.Trio;
                Trio = "trioButtonActivated";
                first3ViewModel.GameVariantId = (int)GameVariant.F3Trio;
                break;
            case (int)F3SubBetTypes.SameSuiteGold:
                SameSuiteGold = "sameSuitGoldActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Suits, true);
                first3ViewModel.BetOptionSelected = Constants.Gold;
                first3ViewModel.BetCombinationValue = Constants.SuitsGold;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Suits;
                break;
            case (int)F3SubBetTypes.SameSuiteSilver:
                SameSuiteSilver = "sameSuitSilverActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Suits, true);
                first3ViewModel.BetOptionSelected = Constants.Silver;
                first3ViewModel.BetCombinationValue = Constants.SuitsSilver;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Suits;
                break;
            case (int)F3SubBetTypes.TwoRedGold:
                TwoRedGold = "reds2GoldActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Color, true);
                first3ViewModel.BetOptionSelected = Constants.Gold;
                first3ViewModel.BetCombinationValue = Constants.ColorRedGold;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Color;
                break;
            case (int)F3SubBetTypes.TwoRedSilver:

                TwoRedSilver = "reds2SilverActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Color, true);
                first3ViewModel.BetOptionSelected = Constants.Silver;
                first3ViewModel.BetCombinationValue = Constants.ColorRedSilver;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Color;
                break;
            case (int)F3SubBetTypes.TwoBlackGold:
                TwoBlackGold = "black2GoldActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Color, true);
                first3ViewModel.BetOptionSelected = Constants.Gold;
                first3ViewModel.BetCombinationValue = Constants.ColorBlackGold;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Color;
                break;
            case (int)F3SubBetTypes.TwoBlackSilver:
                TwoBlackSilver = "black2SilverActivated";
                await first3ViewModel.SetGameChips((int)GameVariant.F3Color, true);
                first3ViewModel.BetOptionSelected = Constants.Silver;
                first3ViewModel.BetCombinationValue = Constants.ColorBlackSilver;
                first3ViewModel.GameVariantId = (int)GameVariant.F3Color;
                break;
            default:
                break;
        }

        //show chips
        await first3ViewModel.ShowDivToken();
    }
    /*** NOTE OLD METHODS **/
    public async Task ShowBetPopup(string betOptionSelected, string betCombinationValue)
    {
        if (IsBetPopupShown) return;

        first3ViewModel.BetOptionSelected = betOptionSelected;
        first3ViewModel.BetCombinationValue = betCombinationValue;

        bool iscrossbetting = await first3ViewModel.IsCrossBetting(first3ViewModel.BetCombinationValue, first3ViewModel.UserBets);
        if (!iscrossbetting)
        {
            first3ViewModel.popupBet = popupModal.Show<PopupConfirm>("Place Bet", new ModalOptions() { Class = "bet-modal" });
            IsBetPopupShown = true;
            ModalResult result = await first3ViewModel.popupBet.Result;
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
        first3ViewModel.Notify -= OnNotify;
    }

    public async Task SubmitBet()
    {
        bool valid = await first3ViewModel.ValidateBet(first3ViewModel.BetOptionSelected, first3ViewModel.BetCombinationValue);
        if (valid)
        {
            //var parameters = new ModalParameters();
            //parameters.Add("Message", "Confirm bet " + first3ViewModel.BetAmount + " for " + first3ViewModel.BetCombinationValue);

            //first3ViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            //IsBetPopupShown = true;
            //ModalResult result = await first3ViewModel.popupConfirm.Result;
            //IsBetPopupShown = false;
            //if (result.Data != null)
            //{
            //    if ((bool)result.Data)
            //    {
            //        if ((bool)result.Data)
            //        {
            bool doublebet = await first3ViewModel.IsDoubleBet();
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
                    await first3ViewModel.SubmitBet();
                    betButtonCss = "BetButton";
                    resetButtonCss = "ResetButton";
                }
            }
            else
            {
                betButtonCss = "disBetButton";
                resetButtonCss = "disResetButton";
                await first3ViewModel.SubmitBet();
                betButtonCss = "BetButton";
                resetButtonCss = "ResetButton";
            }

            await ResetToInitial();
            //first3ViewModel.popupConfirm.Close();


            //}



            //    }
            //}
        }
    }

}
