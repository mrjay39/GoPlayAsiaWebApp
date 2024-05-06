using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class GameRoundDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int RoundNumber { get; set; }
        public int RoundStatus { get; set; }
        public bool FixedCancelled { get; set; }
        public bool RunningOddsCancelled { get; set; }
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
        public List<Go12DTO> Go12Bets { get; set; }

        public bool BetOnRound { get; set; }
        public List<RoundBetsDetailDTO> BetsOnRoundDetails { get; set; }
        public string RoundBetDetails { get; set; }
    }

    public class Go12DTO
    {
        public int Number { get; set; }
        public decimal NumberBetValue { get; set; }
    }

    public class RoundBetsDetailDTO
    {
        public string BetValue { get; set; }
        public string BetAmount { get; set; }
        public bool Won { get; set; }
        public bool Lost { get; set; }
        public string WonAmount { get; set; }
        public string LostAmount { get; set; }
    }
}
