using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Goplay.Games.First3.Components
{
    public partial class Sub2Blacks
    {
        [Parameter] public string BetValueDisplay { get; set; } = "";
        [Parameter] public string TwoBlackGold { get; set; } = ""; // (default Silver value)
        [Parameter] public string TwoBlackSilver { get; set; } = ""; // (default Silver color value)
        [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        [Parameter] public string UserTotalBet_ColorBlackGold { get; set; }
        [Parameter] public string UserTotalBet_ColorBlackSilver { get; set; }
        [Parameter] public string SampleWinColorBlackGold { get; set; }
        [Parameter] public string SampleWinColorBlackSilver { get; set; }
        [Parameter] public string WinnerResults { get; set; } = "";
        [Parameter] public bool isDisabled { get; set; }
    }
}
