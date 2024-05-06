using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.HeadsTails;

public partial class HeadsTailsBettingButton
{
    #region PARAMETERS
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    public bool IsBetPopupShown = false;

    #region User Total Bets
    [Parameter] public string UserTotalBet_FixedPrizeHeads { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeTails { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsHeads { get; set; } = "0";
    [Parameter] public string UserTotalBet_OddsTails { get; set; } = "0";
    #endregion

    #region Fixed and Running Total Bets
    [Parameter] public string FixedPriceHeadsTotalBets { get; set; } = "0";
    [Parameter] public string FixedPriceTailsTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddsHeadsTotalBets { get; set; } = "0";
    [Parameter] public string RunningOddTailsTotalBets { get; set; } = "0";
    #endregion

    #region Sample Param
    [Parameter] public string SampleWinDraw { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeHeads { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeTails { get; set; } = "0";
    [Parameter] public string SampleWinOddsHeads { get; set; } = "0";
    [Parameter] public string SampleWinOddsTails { get; set; } = "0";
    #endregion

    #region Multiplier
    [Parameter] public string FixedPriceHeadsMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceTailsMultipler { get; set; } = "";
    [Parameter] public string RunningOddsHeadsMultiplier { get; set; } = "";
    [Parameter] public string RunningOddTailsMultiplier { get; set; } = "";
    #endregion

    #region Win
    [Parameter] public bool IsPayoutWon_Heads { get; set; } = false;
    [Parameter] public bool IsPayoutWon_Tails { get; set; } = false;
    [Parameter] public bool IsOddsWon_Heads { get; set; } = false;
    [Parameter] public bool IsOddsWon_Tails { get; set; } = false;
    #endregion

    [Parameter] public bool IsFixedHeadsEnabled { get; set; } = false;
    [Parameter] public bool IsFixedTailsEnabled { get; set; } = false;
    [Parameter] public bool IsFixedPrizeCancelled { get; set; } = false;
    [Parameter] public bool IsRunningOddsCancelled { get; set; } = false;
    #endregion

    [Parameter] public EventCallback<string> OnClickCallback { get; set; }

    public async Task enableToken(string betOptionSelected, string betCombinationValue)
    {
        iHeadsTailsViewModel.BetAmount = 0;

        bool iscrossbetting = await iHeadsTailsViewModel.IsCrossBetting(betCombinationValue, iHeadsTailsViewModel.UserBets);
        if (!iscrossbetting)
        {
            iHeadsTailsViewModel.BetOptionSelected = betOptionSelected;
            iHeadsTailsViewModel.BetCombinationValue = betCombinationValue;
            await iHeadsTailsViewModel.ShowDivToken();

            await OnClickCallback.InvokeAsync();
        }
        else
        {
            toastService.ShowError("Cross betting not allowed!");
        }
    }

    public async Task CheckIfPlaceBetReady()
    {

    }

    protected override async Task OnInitializedAsync()
    {
        iHeadsTailsViewModel.Notify += OnNotify;
    }

    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        iHeadsTailsViewModel.Notify -= OnNotify;
    }
}

