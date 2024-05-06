using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Goplay.Games.First3.Components;

public partial class SubSameSuites
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public string SameSuiteGold { get; set; } = "";
    [Parameter] public string SameSuiteSilver { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string UserTotalBet_SuitsGold { get; set; } = "";
    [Parameter] public string UserTotalBet_SuitsSilver { get; set; } = "";
    [Parameter] public string SampleWinSuitsGold { get; set; } = "";
    [Parameter] public string SampleWinSuitsSilver { get; set; } = "";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }
}
