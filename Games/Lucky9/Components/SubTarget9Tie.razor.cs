using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.Lucky9.Components;

public partial class SubTarget9Tie
{
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public int BetType { get; set; } = 0;
    [Parameter] public string TargetPlayer { get; set; } = "";
    [Parameter] public string TargetBanker { get; set; } = "";
    [Parameter] public string TargetTie { get; set; } = "";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }

    [Parameter] public string UserTotalBet_TargetPlayer { get; set; } = "";
    [Parameter] public string UserTotalBet_TargetBanker { get; set; } = "";
    [Parameter] public string UserTotalBet_Draw { get; set; } = "";
    [Parameter] public string SampleWinTargetPlayer { get; set; } = "";
    [Parameter] public string SampleWinTargetBanker { get; set; } = "";
    [Parameter] public string SampleWinDraw { get; set; } = "";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }
}
