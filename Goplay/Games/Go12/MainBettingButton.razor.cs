using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Goplay.Games.Go12;

public partial class MainBettingButton
{
    [Parameter] public string BetValue { get; set; } = "";
    [Parameter] public string BetValueDisplay { get; set; } = "";
    [Parameter] public string UserTotalBet_FixedPrizeRed { get; set; } = "0";
    [Parameter] public string UserTotalBet_FixedPrizeBlack { get; set; } = "0";
    [Parameter] public EventCallback<MouseEventArgs> OnClickCallback { get; set; }

    public static string MultiplierFormatter(decimal param)
    {
        string value = "x" + decimal.ToInt32(param * 100).ToString() + "%";

        return value;
    }
}
