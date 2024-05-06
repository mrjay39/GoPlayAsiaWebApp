
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Models.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoplayasiaBlazor.Models
{
    public class BetModel
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public long GameRoundId { get; set; }
        public long UserId { get; set; }
        public int? BetStatus { get; set; }
        public decimal? BetAmount { get; set; }
        public string BetValue { get; set; }
        public DateTime? DateCreated { get; set; }

        public GameRoundModel GameRound { get; set; }
        public GameTypeModel GameType { get; set; }
        public UserDTO User { get; set; }

        public DateTime? ConvertedDateCreated { get; set; }
        public string GameTypeName { get; set; }
        public string DrawDateString { get; set; }
        public string DrawDateTimeString { get; set; }
        public string RoundNumberDisplay { get; set; }
        public string Result { get; set; }
        public string GameResult { get; set; }        
        public string GigaCombination { get; set; }
        public decimal? Winnings { get; set; }

        public int DropAndWinBetType => SetDropAndWinBetType();
        private int SetDropAndWinBetType()
        {
            int result = 0;
            if (!String.IsNullOrEmpty(BetValue))
            {
                if (BetValue.Length == 2)
                {
                    result = (int)DropAndWinMainBetOption.F2;
                }
                else if (BetValue.Length == 3)
                {
                    result = (int)DropAndWinMainBetOption.F3;
                }
                else if (BetValue.Length == 4)
                {
                    result = (int)DropAndWinMainBetOption.All4;
                }
            }
            return result;
        }

        public string DropAndWinWinnings { get; set; }

        /// <summary>
        /// for display
        /// </summary>
        public string BetAmountDisplay => SetBetAmountDisplay();
        public string WinableAmount { get; set;  }
        public string SetBetAmountDisplay()
        {
            return BetAmount != null ? Convert.ToDecimal(BetAmount).ToString("#,##0") : "0";
        }

        //for Giga draw and Drop and win
        public string BetFrameBorderColor => SetBetFrameBorderColor();
        private string SetBetFrameBorderColor()
        {
            string result = "#ffffff";
            if (GameTypeId == (int)GameTypes.Giga_Draw || GameTypeId == (int)GameTypes.Drop_And_Win)
            {
                if (BetStatus == (int)Settings.BetStatus.Win) 
                {
                    result = "#66ff33";
                }
            }
            return result;
        }
    }
}
