using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class L9GameRoundDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int RoundNumber { get; set; }
        public int RoundStatus { get; set; }
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

        public GameTypeDTO GameType { get; set; }
        public UserDTO Operator { get; set; }
        public UserDTO Validator { get; set; }

        public string GameTypeName { get; set; }
        public DateTime? ConvertedDateCreated { get; set; }
        public DateTime? ConvertedDrawDate { get; set; }
        public string Result { get; set; }
        public string DrawDateFullString { get; set; }
        public string DrawDateString { get; set; }
        public string DrawDateTimeString { get; set; }
        public string RoundNumberDisplay { get; set; }
        public string FixedLeftBet { get; set; } 
        public string FixedRightBet { get; set; }
        public string DrawBet { get; set; }
        public string OddsLeftBet { get; set; }
        public string OddsRightBet { get; set; }
        public string OddsLeftPercentage { get; set; }
        public string OddsRightPercentage { get; set; }

        public string TargetLeftBet { get; set; }
        public string TargetRightBet { get; set; }
        public string SuitsLeftBet { get; set; }
        public string SuitsRightBet { get; set; }
        public string ColorRedLeftBet { get; set; }
        public string ColorBlackLeftBet { get; set; }
        public string ColorRedRightBet { get; set; }
        public string ColorBlackRightBet { get; set; }
        public string PairLeftBet { get; set; }
        public string PairRightBet { get; set; }


        public List<Go12DTO> Go12Bets { get; set; }

        public bool BetOnRound { get; set; }
        public List<RoundBetsDetailDTO> BetsOnRoundDetails { get; set; }
        public string RoundBetDetails { get; set; }
    }
}
