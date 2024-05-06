
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Models.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoplayasiaBlazor.Models
{
    public class DiceGameRoundModel
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

        public GameTypeModel GameType { get; set; }
        public UserDTO Operator { get; set; }
        public UserDTO Validator { get; set; }

        public string Result { get; set; }
        public string DrawDateFullString { get; set; }
        public string DrawDateString { get; set; }
        public string DrawDateTimeString { get; set; }
        public string GameTypeName { get; set; }
        public string RoundNumberDisplay { get; set; }
        public string TrendsNumberDisplay => SetTrendsNumberDisplay();
        public string Trends_PayoutDisplay => SetTrends_PayoutDisplay();
        public string SetTrendsFontColor()
        {
            string resultColor = "#000000";
            switch (GameTypeId)
            {
                case (int)GameTypes.Go_12:
                    resultColor = "#FFFFFF";
                    break;
                case (int)GameTypes.Lucky_9:
                    resultColor = "#000000";
                    break;
                case (int)GameTypes.Heads_And_Tails:
                    resultColor = "#000000";
                    break;
                case (int)GameTypes.Gold_And_Silver:
                    resultColor = "#FFFFFF";
                    break;
            }
            return resultColor;
            //return GameTypeId != (int)GameTypes.Gold_And_Silver ? "#FFFFFF" : "#000000";
        }
        public string TrendsResultColor => SetTrendsResultColor();
        private string SetTrendsResultColor()
        {
            string resultValue = string.Empty;
            if (RoundStatus == (int)Settings.RoundStatus.Cancelled)
            {
                resultValue = "cancelled";
            }
            else
            {
                if (WinningResult == GoplayasiaBlazor.Models.Constants.Settings.Constants.Draw)
                {
                    resultValue = "draw";
                }
                else if (WinningResult == GoplayasiaBlazor.Models.Constants.Settings.Constants.Player)
                {
                    resultValue = "player";
                }
                else if (WinningResult == GoplayasiaBlazor.Models.Constants.Settings.Constants.Banker)
                {
                    resultValue = "banker";
                }
            }
            return resultValue;
        }
        public string SetTrendsNumberDisplay()
        {
            return RoundNumber.ToString();
        }
        private string SetTrends_PayoutDisplay()
        {
            string result = string.Empty;
            if (RoundStatus == (int)GoplayasiaBlazor.Models.Constants.Settings.RoundStatus.Cancelled)
            {
                result = GoplayasiaBlazor.Models.Constants.Settings.Constants.Cancelled;
            }
            else if (RoundStatus == (int)GoplayasiaBlazor.Models.Constants.Settings.RoundStatus.Closed)
            {
                result = FixedCancelled ? GoplayasiaBlazor.Models.Constants.Settings.Constants.Cancelled : WinningResult;
            }

            return result;
        }

        public DateTime? ConvertedDateCreated { get; set; }
        public DateTime? ConvertedDrawDate { get; set; }
        public string OddBet { get; set; } = "0";
        public string EvenBet { get; set; } = "0";
        public string SmallBet { get; set; } = "0";
        public string BigBet { get; set; } = "0";
        public string FirstDiceBet { get; set; } = "0";
        public string SecondDiceBet { get; set; } = "0";
        public string ThirdDiceBet { get; set; } = "0";
        public string AllDiceBet { get; set; } = "0";
        public string NumberBet { get; set; } = "0";

        public bool BetOnRound { get; set; }
        public List<RoundBetsDetailModel> BetsOnRoundDetails { get; set; }
        public string RoundBetDetails { get; set; }
        public string NonCardsResultDisplay => SetNonCardsResultDisplay();
        public string SetNonCardsResultDisplay()
        {
            string resultValue = string.Empty;
            if (RoundStatus == (int)Settings.RoundStatus.Cancelled)
            {
                resultValue = "C";
            }
            else
            {
                resultValue = WinningResult;
            }

            return resultValue;
        }
    }
}
