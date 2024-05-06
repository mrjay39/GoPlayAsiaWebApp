using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.ViewModels.BigWinViewModel;

namespace GoPlayAsiaWebApp.Games.Bigwin;

public partial class BigwinTrends
{
    [Parameter] public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }

    [Parameter] public ObservableCollection<BetModel> UserBets { get; set; }

    [Parameter] public string ticketRows { get; set; }

    [Parameter] public int TotalBets { get; set; }

    [Parameter] public string RoundStatusString { get; set; }
}
