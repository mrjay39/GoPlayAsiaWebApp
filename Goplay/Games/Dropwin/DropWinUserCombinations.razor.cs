using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace GoPlayAsiaWebApp.Goplay.Games.Dropwin;

public partial class DropWinUserCombinations
{
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F2 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_F3 { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets_All4 { get; set; }
    [Parameter]
    public bool IsPrevious { get; set; } = false;
    [Parameter]
    public string RoundStatusString { get; set; } = "";
    [Parameter]
    public GameRoundModel? PrevGameRound { get; set; }
}