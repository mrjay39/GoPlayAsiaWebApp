
using GoplayasiaBlazor.Models.Base;

namespace GoplayasiaBlazor.Models.Incoming;

public class F3BetResultModel : BaseResultModel
{
    public BetModel Bet { get; set; }
    public string LeftFixedBet { get; set; }
    public string LeftFixedWinnings { get; set; }
    public string LeftOddsBet { get; set; }
    public string LeftOddsWinnings { get; set; }
    public string DrawBet { get; set; }
    public string DrawWinnings { get; set; }
    public string RightFixedBet { get; set; }
    public string RightFixedWinnings { get; set; }
    public string RightOddsBet { get; set; }
    public string RightOddsWinnings { get; set; }


    #region NEW VARIANT
    public string TargetLeftBet { get; set; } = string.Empty;
    public string TargetLeftBetWinnings { get; set; } = "0.00";
    public string TargetRightBet { get; set; } = string.Empty;
    public string TargetRightBetWinnings { get; set; } = "0.00";
    public string SuitsLeftBet { get; set; } = string.Empty;
    public string SuitsLeftBetWinnings { get; set; } = "0.00";
    public string SuitsRightBet { get; set; } = string.Empty;
    public string SuitsRightBetWinnings { get; set; } = "0.00";
    public string ColorRedLeftBet { get; set; } = string.Empty;
    public string ColorRedLeftBetWinnings { get; set; } = "0.00";
    public string ColorBlackLeftBet { get; set; } = string.Empty;
    public string ColorBlackLeftBetWinnings { get; set; } = "0.00";
    public string ColorRedRightBet { get; set; } = string.Empty;
    public string ColorRedRightBetWinnings { get; set; } = "0.00";
    public string ColorBlackRightBet { get; set; } = string.Empty;
    public string ColorBlackRightBetWinnings { get; set; } = "0.00";
    public string PairLeftBet { get; set; } = string.Empty;
    public string PairLeftBetWinnings { get; set; } = "0.00";
    public string PairRightBet { get; set; } = string.Empty;
    public string PairRightBetWinnings { get; set; } = "0.00";
    public string TrioBet { get; set; } = string.Empty;
    public string TrioBetWinnings { get; set; } = "0.00";

    #endregion


    public List<Go12BetsModel> Go12Bets { get; set; }
    public List<BetModel> Bets { get; set; }
    public List<BetModel> F2Bets { get; set; }
    public List<BetModel> F3Bets { get; set; }
    public List<BetModel> A4Bets { get; set; }
    public string F2Sum { get; set; }
    public string F3Sum { get; set; }
    public string A4Sum { get; set; }
}

