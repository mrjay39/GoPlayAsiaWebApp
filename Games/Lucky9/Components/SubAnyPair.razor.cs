using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.Lucky9.Components;

public partial class SubAnyPair
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public string SubAnyPairPlayer { get; set; } = "";
    [Parameter] public string SubAnyPairBanker { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string UserTotalBet_PairPlayer { get; set; } = "";
    [Parameter] public string UserTotalBet_PairBanker { get; set; } = "";
    [Parameter] public string SampleWinSuitsPlayer { get; set; } = "";
    [Parameter] public string SampleWinSuitsBanker { get; set; } = "";

    [Parameter] public string SampleWinPairPlayer { get; set; } = "";
    [Parameter] public string SampleWinPairBanker { get; set; } = "";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }
}
