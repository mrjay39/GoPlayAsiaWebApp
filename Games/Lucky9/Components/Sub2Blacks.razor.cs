using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.Lucky9.Components
{
    public partial class Sub2Blacks
    {
        [Parameter] public string BetValueDisplay { get; set; } = "";
        [Parameter] public string TwoBlackPlayer { get; set; } = ""; // (default banker value)
        [Parameter] public string TwoBlackBanker { get; set; } = ""; // (default banker color value)
        [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        [Parameter] public string UserTotalBet_ColorBlackPlayer { get; set; }
        [Parameter] public string UserTotalBet_ColorBlackBanker { get; set; }
        [Parameter] public string SampleWinColorBlackPlayer { get; set; }
        [Parameter] public string SampleWinColorBlackBanker { get; set; }
        [Parameter] public string WinnerResults { get; set; } = "";
        [Parameter] public bool isDisabled { get; set; }
    }
}
