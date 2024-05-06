using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Goplay.Games.First3.Components;

public partial class SubTrio
{
    [Parameter] public string Trio { get; set; } = "";
    [Parameter] public int BetType { get; set; } = 0;
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    [Parameter] public string UserTotalBet_Trio { get; set; } = "";
    [Parameter] public string SampleWinTrio { get; set; } = "";
    [Parameter] public string WinnerResults { get; set; } = "";
    [Parameter] public bool isDisabled { get; set; }
}
