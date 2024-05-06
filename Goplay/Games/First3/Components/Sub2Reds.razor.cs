using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;

namespace GoPlayAsiaWebApp.Goplay.Games.First3.Components
{
    public partial class Sub2Reds
    {
        [Parameter] public string BetValueDisplay { get; set; } = "";
        [Parameter] public string TwoRedGold { get; set; } = "";
        [Parameter] public string TwoRedSilver { get; set; } = "";
        [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        [Parameter] public string UserTotalBet_ColorRedGold { get; set; }
        [Parameter] public string UserTotalBet_ColorRedSilver { get; set; }
        [Parameter] public string SampleWinColorRedGold { get; set; }
        [Parameter] public string SampleWinColorRedSilver { get; set; }
        [Parameter] public string WinnerResults { get; set; } = "";
        [Parameter] public bool isDisabled { get; set; }
    }
}
