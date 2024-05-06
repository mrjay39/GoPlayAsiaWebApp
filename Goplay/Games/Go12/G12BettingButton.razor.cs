using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoPlayAsiaWebApp.Goplay.Games.Lucky9;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Go12;

public partial class G12BettingButton
{
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public string Number_01_UserBet { get; set; } = "";
    [Parameter] public string Number_02_UserBet { get; set; } = "";
    [Parameter] public string Number_03_UserBet { get; set; } = "";
    [Parameter] public string Number_04_UserBet { get; set; } = "";
    [Parameter] public string Number_05_UserBet { get; set; } = "";
    [Parameter] public string Number_06_UserBet { get; set; } = "";
    [Parameter] public string Number_07_UserBet { get; set; } = "";
    [Parameter] public string Number_08_UserBet { get; set; } = "";
    [Parameter] public string Number_09_UserBet { get; set; } = "";
    [Parameter] public string Number_10_UserBet { get; set; } = "";
    [Parameter] public string Number_11_UserBet { get; set; } = "";
    [Parameter] public string Number_12_UserBet { get; set; } = "";

    [Parameter] public string Number_Payout_1 { get; set; } = "";
    [Parameter] public string Number_Payout_2 { get; set; } = "";
    [Parameter] public string Number_Payout_3 { get; set; } = "";
    [Parameter] public string Number_Payout_4 { get; set; } = "";
    [Parameter] public string Number_Payout_5 { get; set; } = "";
    [Parameter] public string Number_Payout_6 { get; set; } = "";
    [Parameter] public string Number_Payout_7 { get; set; } = "";
    [Parameter] public string Number_Payout_8 { get; set; } = "";
    [Parameter] public string Number_Payout_9 { get; set; } = "";
    [Parameter] public string Number_Payout_10 { get; set; } = "";
    [Parameter] public string Number_Payout_11 { get; set; } = "";
    [Parameter] public string Number_Payout_12 { get; set; } = "";

    [Parameter] public string RunningOdds_Black_Userbet { get; set; } = "";
    [Parameter] public string RunningOdds_Red_Userbet { get; set; } = "";
    [Parameter] public string RunningOdds_Black_TotalBet { get; set; } = "";
    [Parameter] public string RunningOdds_Red_TotalBet { get; set; } = "";
    [Parameter] public string RunningOdds_Black_Percentage { get; set; } = "";
    [Parameter] public string RunningOdds_Red_Percentage { get; set; } = "";

    [Parameter] public string Black_Odds_WinAmount { get; set; } = "0";
    [Parameter] public string Red_Odds_WinAmount { get; set; } = "0";

    [Parameter]
    public bool IsWon_1 { get; set; } = false;
    [Parameter]
    public bool IsWon_2 { get; set; } = false;
    [Parameter]
    public bool IsWon_3 { get; set; } = false;
    [Parameter]
    public bool IsWon_4 { get; set; } = false;
    [Parameter]
    public bool IsWon_5 { get; set; } = false;
    [Parameter]
    public bool IsWon_6 { get; set; } = false;
    [Parameter]
    public bool IsWon_7 { get; set; } = false;
    [Parameter]
    public bool IsWon_8 { get; set; } = false;
    [Parameter]
    public bool IsWon_9 { get; set; } = false;
    [Parameter]
    public bool IsWon_10 { get; set; } = false;
    [Parameter]
    public bool IsWon_11 { get; set; } = false;
    [Parameter]
    public bool IsWon_12 { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Black { get; set; } = false;
    [Parameter]
    public bool IsOddsWon_Red { get; set; } = false;
    [Parameter]
    public bool IsRunningOddsCancelled { get; set; } = false;
    [Parameter]
    public bool IsFixedPrizeCancelled { get; set; } = false;

    [Parameter]
    public bool IsOptionEnabled_1 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_2 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_3 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_4 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_5 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_6 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_7 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_8 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_9 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_10 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_11 { get; set; } = false;
    [Parameter]
    public bool IsOptionEnabled_12 { get; set; } = false;
    public bool IsBetPopupShown = false;
    [Parameter] public bool IsFixedBlackEnabled { get; set; } = false;
    [Parameter] public bool IsFixedRedEnabled { get; set; } = false;
    [Parameter] public string FixedPriceBlackMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceRedMultiplier { get; set; } = "";
    [Parameter] public bool IsPayoutWon_Black { get; set; } = false;
    [Parameter] public bool IsPayoutWon_Red { get; set; } = false;

    [Parameter] public string G12RegFixedPrice_Red_Multiplier { get; set; } = "";
    [Parameter] public string G12RegFixedPrice_Black_Multiplier { get; set; } = "";
    [Parameter] public string TokenDiv { get; set; } = "block";
    [Parameter] public GameChipModel GameChips { get; set; }

    public async Task SelectBetType(string betValue)
    {
        switch (betValue)
        {
            case Settings.Constants.FixedRed:
            case Settings.Constants.FixedBlack:
                bool iscrossbetting = await go12ViewModel.IsCrossBetting(betValue, go12ViewModel.UserBets);
                if (iscrossbetting)
                {
                    toastService.ShowError("Cross betting not allowed!");
                    return;
                }

                await go12ViewModel.SetGameChips((int)Settings.GameVariant.G12Reg, true);
                go12ViewModel.BetCombinationValue = betValue;
                go12ViewModel.GameVariantId = (int)Settings.GameVariant.G12Reg;
                break;
            default:
                await go12ViewModel.SetGameChips(go12ViewModel.GametypeId, false);
                go12ViewModel.BetCombinationValue = betValue;
                go12ViewModel.GameVariantId = 0;
                break;
        }

        await go12ViewModel.ShowDivToken();
    }
    public static string NumFormatter(string param)
    {
        //parse parameter from string to int
        int amount = int.Parse(param, System.Globalization.NumberStyles.AllowThousands);

        //to decimal
        double value = Math.Truncate(10 * (amount / 1000D)) / 10;

        if (amount == 0 || amount < 10000)
        {
            return amount.ToString();
        }
        else if (amount % 100 == 0)
        {
            return value.ToString() + "k";
        }
        else
        {
            return value.ToString() + "k+";
        }

    }

}
