using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.Goplay.ViewModels.DropwinS5ViewModel;

namespace GoPlayAsiaWebApp.Goplay.Games.DropwinS5;

public partial class DropWinS5Trends
{
    [Parameter]
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F2 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F3 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_All4 { get; set; }
}
