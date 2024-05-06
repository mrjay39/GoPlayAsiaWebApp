using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class BetUpdatesModel
    {
        /// <summary>
        /// LUCKY 9: Left is Player, Right is Banker
        /// GOLD & SILVER: Left is Gold, Right is Silver
        /// HEADS & TAILS: Left is Heads, Right is Tails
        /// </summary>
        public int GameTypeId { get; set; }
        public decimal FixedLeftBetValue { get; set; }
        public decimal FixedRightBetValue { get; set; }
        public decimal DrawBetValue { get; set; }
        public decimal RunningLeftBetValue { get; set; }
        public decimal RunningRightBetValue { get; set; }
        public decimal LeftPercentage { get; set; }
        public decimal RightPercentage { get; set; }
        public decimal OddsReturnsPercentage { get; set; }
        public decimal PercentageLess { get; set; }
        public decimal LessValue { get; set; }
        public decimal MinimumDifferential { get; set; }
        public decimal MaximumDifferential { get; set; }
        public decimal Differential { get; set; }
        public bool DisableLeft { get; set; }
        public bool DisableRight { get; set; }
        //public decimal LeftQuotient { get; set; }
        //public decimal RightQuotient { get; set; }
        public decimal LeftQuotient => LeftPercentage > 0 ?
     Math.Round(LeftPercentage / 100, 4) :
     1;
        public decimal RightQuotient => RightPercentage > 0 ?
            Math.Round(RightPercentage / 100, 4) :
            1;
    }
}
