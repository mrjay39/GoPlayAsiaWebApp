using GoplayasiaBlazor.Models;
using System.Linq.Expressions;

namespace GoplayasiaBlazor.Core.Global.Interface;
public interface IGameValues
{
    string FixedPriceBankerTotalBets { get; set; }
    string FixedPricePlayerTotalBets { get; set; }
    GameRoundModel GameRound { get; set; }
    int GametypeId { get; set; }
    int RoundNumber { get; set; }
    string RoundStatusColor { get; set; }
    string? RoundStatusString { get; set; }
    string? RoundTimer { get; set; }
    string RunningOddBankerTotalBets { get; set; }
    string RunningOddsPlayerTotalBets { get; set; }
    string? StreamId { get; set; }

    event Action Notify;

    Task ConnectSignalR();
    Task GetGameRound();
    void RaisePropertyChanged<T>(Expression<Func<T>> property);
}