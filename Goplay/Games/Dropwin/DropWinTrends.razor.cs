using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.Goplay.ViewModels.DropwinViewModel;

namespace GoPlayAsiaWebApp.Goplay.Games.Dropwin;

public partial class DropWinTrends
{
    [Parameter]
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }
}
