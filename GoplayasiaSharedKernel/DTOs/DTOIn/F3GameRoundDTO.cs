
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class F3GameRoundDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int RoundNumber { get; set; }
        public int RoundStatus { get; set; }
        public decimal? F2ratioAsOf { get; set; }
        public decimal? F3ratioAsOf { get; set; }
        public decimal? A4ratioAsOf { get; set; }
        public bool FixedCancelled { get; set; }
        public bool RunningOddsCancelled { get; set; }
        public string OptACard1 { get; set; }
        public string OptACard2 { get; set; }
        public string OptACard3 { get; set; }
        public string OptBCard1 { get; set; }
        public string OptBCard2 { get; set; }
        public string OptBCard3 { get; set; }
        public string ValACard1 { get; set; }
        public string ValACard2 { get; set; }
        public string ValACard3 { get; set; }
        public string ValBCard1 { get; set; }
        public string ValBCard2 { get; set; }
        public string ValBCard3 { get; set; }
        public string LeftWinningTarget { get; set; }
        public string RightWinningTarget { get; set; }
        public string LeftWinningSuits { get; set; }
        public string RightWinningSuits { get; set; }
        public string LeftWinningColor { get; set; }
        public string RightWinningColor { get; set; }
        public string LeftWinningPair { get; set; }
        public string RightWinningPair { get; set; }
        public string TrioResult { get; set; }
        public string WinningResult { get; set; }
        public long? OperatorId { get; set; }
        public string WinningResultOperator { get; set; }
        public DateTime? OperatorInputDate { get; set; }
        public long? ValidatorId { get; set; }
        public string WinningResultValidator { get; set; }
        public DateTime? ValidatorInputDate { get; set; }
        public bool JackpotWinner { get; set; }
        public decimal? JackpotPrize { get; set; }
        public bool MiniJackpotWinner { get; set; }
        public decimal? MiniJackpotPrize { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DrawDate { get; set; }

        //public GameTypeDTO GameType { get; set; }
        //public UserDTO Operator { get; set; }
        //public UserDTO Validator { get; set; }

        public string FixedLeftBet { get; set; } = "0";
        public string FixedRightBet { get; set; } = "0";
        public string DrawBet { get; set; } = "0";
        public string TrioBet { get; set; } = "0";
        public string OddsLeftBet { get; set; } = "0";
        public string OddsRightBet { get; set; } = "0";
        public string OddsLeftPercentage { get; set; } = "100.00%";
        public string OddsRightPercentage { get; set; } = "100.00%";


        public string TargetLeftBet { get; set; } = "0";
        public string TargetRightBet { get; set; } = "0";
        public string SuitsLeftBet { get; set; } = "0";
        public string SuitsRightBet { get; set; } = "0";
        public string ColorRedLeftBet { get; set; } = "0";
        public string ColorBlackLeftBet { get; set; } = "0";
        public string ColorRedRightBet { get; set; } = "0";
        public string ColorBlackRightBet { get; set; } = "0";
        public string PairLeftBet { get; set; } = "0";
        public string PairRightBet { get; set; } = "0";


        //public List<Go12Model> Go12Bets { get; set; }

        public long CreatedById { get; set; }

    }
}
