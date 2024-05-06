using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.ViewModels.GigadrawModel;

namespace GoPlayAsiaWebApp.Games.Gigadraw;

public partial class GigadrawTrends
{
    [Parameter]
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets { get; set; }
    [Parameter]
    public string ticketRows { get; set; }
    [Parameter]
    public int TotalBets { get; set; }
    [Parameter]
    public string RoundStatusString { get; set; }
}
