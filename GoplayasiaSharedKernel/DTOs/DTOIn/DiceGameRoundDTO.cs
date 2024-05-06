
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class DiceGameRoundDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int RoundNumber { get; set; }
        public int RoundStatus { get; set; }
        public bool FixedCancelled { get; set; }
        public string OptFirstDice { get; set; }
        public string OptSecondDice { get; set; }
        public string OptThirdDice { get; set; }
        public string ValFirstDice { get; set; }
        public string ValSecondDice { get; set; }
        public string ValThirdDice { get; set; }
        public string OddEvenResult { get; set; }
        public string SmallBigResult { get; set; }
        public string FirstDiceResult { get; set; }
        public string SecondDiceResult { get; set; }
        public string ThirdDiceResult { get; set; }
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

        public string OddBet { get; set; } = "0";
        public string EvenBet { get; set; } = "0";
        public string SmallBet { get; set; } = "0";
        public string BigBet { get; set; } = "0";
        public string FirstDiceBet { get; set; } = "0";
        public string SecondDiceBet { get; set; } = "0";
        public string ThirdDiceBet { get; set; } = "0";
        public string AllDiceBet { get; set; } = "0";
        public string NumberBet { get; set; } = "0";

        public long CreatedById { get; set; }

    }
}
