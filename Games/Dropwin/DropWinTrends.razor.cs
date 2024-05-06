using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.ViewModels.DropwinViewModel;

namespace GoPlayAsiaWebApp.Games.Dropwin;

public partial class DropWinTrends
{
    [Parameter]
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }
}
