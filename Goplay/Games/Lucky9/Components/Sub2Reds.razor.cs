using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;

namespace GoPlayAsiaWebApp.Goplay.Games.Lucky9.Components
{
    public partial class Sub2Reds
    {
        [Parameter] public string BetValueDisplay { get; set; } = "";
        [Parameter] public string TwoRedPlayer { get; set; } = "";
        [Parameter] public string TwoRedBanker { get; set; } = "";
        [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        [Parameter] public string UserTotalBet_ColorRedPlayer { get; set; }
        [Parameter] public string UserTotalBet_ColorRedBanker { get; set; }
        [Parameter] public string SampleWinColorRedPlayer { get; set; }
        [Parameter] public string SampleWinColorRedBanker { get; set; }
        [Parameter] public string WinnerResults { get; set; } = "";
        [Parameter] public bool isDisabled { get; set; }

    }
}
