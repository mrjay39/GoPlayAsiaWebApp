using Blazored.Modal;
using Blazored.Modal.Services;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GoPlayAsiaWebApp.Games.HeadsTails;

public partial class GameNumberTokenTotBet
{
    [Parameter] public int MinChipDisplay { get; set; }
    [Parameter] public int MaxChipDisplay { get; set; }
    [Parameter] public int RoundNumber { get; set; }
    [Parameter] public int TotalBets { get; set; }
    [Parameter] public bool BetDisabled { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }

    public static string NumFormatter(int param)
    {
        int amount = param;

        //to decimal
        double value = Math.Truncate(10 * (amount / 1000D)) / 10;

        if (amount == 0 || amount < 10000)
        {
            return amount.ToString();
        }
        else if (amount % 100 == 0)
        {
            return value.ToString() + "K";
        }
        else
        {
            return value.ToString() + "K+";
        }
    }
}
