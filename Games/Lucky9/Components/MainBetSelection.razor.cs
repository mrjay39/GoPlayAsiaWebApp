using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.Lucky9.Components;

public partial class MainBetSelection
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public bool ShowTotalBets { get; set; }
    [Parameter] public string FixedPricePlayerTotalBets { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizePlayer { get; set; } = "0";
    [Parameter] public string FixedPricePlayerMultiplier { get; set; } = "";
    [Parameter] public string FixedPriceBankerTotalBets { get; set; } = "0";
    [Parameter] public string SampleWinFixedPrizeBanker { get; set; } = "0";
    [Parameter] public string FixedPriceBankerMultipler { get; set; } = "";
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string activeButtonPlayer { get; set; } = "playerButton"; // (default banker value)
    [Parameter] public string activeButtonBanker { get; set; } = "bankerButton"; // (default banker color value)
    [Parameter] public string UserTotalBet_FixedPrizePlayer { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeBanker { get; set; } = "0";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }


}

