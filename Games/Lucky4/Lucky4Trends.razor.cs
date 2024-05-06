﻿using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using static GoPlayAsiaWebApp.ViewModels.Lucky4V2ViewModel;

namespace GoPlayAsiaWebApp.Games.Lucky4;

public partial class Lucky4Trends
{
    [Parameter] public string GameResultString { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    [Parameter]
    public ObservableCollection<TrendsDisplayModel> TrendsDisplay { get; set; }
}
