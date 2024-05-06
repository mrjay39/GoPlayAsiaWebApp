using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.Lucky9.Components;

public partial class SubSameSuites
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public string SameSuitePlayer { get; set; } = "";
    [Parameter] public string SameSuiteBanker { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string UserTotalBet_SuitsPlayer { get; set; } = "";
    [Parameter] public string UserTotalBet_SuitsBanker { get; set; } = "";
    [Parameter] public string SampleWinSuitsPlayer { get; set; } = "";
    [Parameter] public string SampleWinSuitsBanker { get; set; } = "";
    [Parameter] public string WinnerResults { get; set; } = "";
}
