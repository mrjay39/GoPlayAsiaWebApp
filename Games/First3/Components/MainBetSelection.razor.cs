using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.First3.Components;

public partial class MainBetSelection
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public bool ShowTotalBets { get; set; }
    [Parameter] public string FixedPriceGoldTotalBets { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeGold { get; set; } = "0";
    [Parameter] public string FixedPriceGoldMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceSilverTotalBets { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeSilver { get; set; } = "0";
    [Parameter] public string FixedPriceSilverMultipler { get; set; } = "";
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string activeButtonGold { get; set; } = "GoldButton"; // (default Silver value)
    [Parameter] public string activeButtonSilver { get; set; } = "SilverButton"; // (default Silver color value)
    [Parameter] public string UserTotalBet_FixedPrizeGold { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeSilver { get; set; } = "0";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }

}

