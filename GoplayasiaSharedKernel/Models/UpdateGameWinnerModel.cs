
using System;
using System.Collections.Generic;
using System.Text;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoplayasiaBlazor.Models
{
    public class UpdateGameWinnerModel
    {
        public long UserId { get; set; }
        public int GameTypeId { get; set; }
        public string GameName { get; set; }
        public int RoundNo { get; set; }
        public string RoundNumber { get; set; }
        public string WinningCombination { get; set; }
        public bool Won { get; set; }
        public bool Consolled { get; set; }
        public decimal BetAmountFixed { get; set; }
        public decimal BetAmountOdds { get; set; }
        public decimal BetAmountDraw { get; set; }
        public decimal AmountWon { get; set; }
        public decimal FixedWon { get; set; }
        public decimal RunningWon { get; set; }
        public decimal DrawWon { get; set; }
        public int Winners { get; set; }
        public string JackpotPrize { get; set; }
        public string JackpotDividend { get; set; }
        public string Message { get; set; }

        public bool IsFixedWon => SetIsFixedWon();
        public bool IsOddsWon => SetIsOddsWon();
        public bool IsDrawWon => SetIsDrawWon();
        public bool IsJackpotBased => SetIsJackpotBased();
        public bool IsNonFixedOrOddsBased => SetIsNonFixedOrOddsBased();

        public string FixedWonString => SetFixedWonString();
        public string OddsWonString => SetOddsWonString();
        public string DrawWonString => SetDrawWonString();
        public string AmountWonString => SetAmountWonString();

        private bool SetIsFixedWon()
        {
            return FixedWon > 0 ? true : false;
        }
        private bool SetIsOddsWon()
        {
            return RunningWon > 0 ? true : false;
        }
        private bool SetIsDrawWon()
        {
            return DrawWon > 0 ? true : false;
        }
        private string SetAmountWonString()
        {
            return AmountWon > 0 ? AmountWon.ToString("#,###.#0") : "0";
        }

        private string SetFixedWonString()
        {
            return FixedWon > 0 ? FixedWon.ToString("#,###.#0") : "0";
        }

        private string SetOddsWonString()
        {
            return RunningWon > 0 ? RunningWon.ToString("#,###.#0") : "0";
        }
        private string SetDrawWonString()
        {
            return DrawWon > 0 ? DrawWon.ToString("#,###.#0") : "0";
        }

        private bool SetIsJackpotBased()
        {
            bool result = false;
            if (GameTypeId == (int)GameTypes.Go_12)
                result = false;
            else if (GameTypeId == (int)GameTypes.Lucky_9)
                result = false;
            else if (GameTypeId == (int)GameTypes.Gold_And_Silver)
                result = false;
            else if (GameTypeId == (int)GameTypes.Heads_And_Tails)
                result = false;
            else if (GameTypeId == (int)GameTypes.Giga_Draw)
                result = true;
            else if (GameTypeId == (int)GameTypes.Drop_And_Win)
                result = false;
            else if (GameTypeId == (int)GameTypes.BigWin)
                result = true;
            return result;
        }

        public bool SetIsNonFixedOrOddsBased()
        {
            bool result = false;
            if (FixedWon == 0 && RunningWon == 0 && AmountWon > 0)
            {
                result = true;
            }
            return result;
        }

        public List<string> ConsolledCombination { get; set; } = new List<string>();



        #region Lucky 9
        public decimal BetAmountTargetPlayer { get; set; }
        public decimal BetAmountTargetBanker { get; set; }
        public decimal BetAmountSuitsPlayer { get; set; }
        public decimal BetAmountSuitsBanker { get; set; }
        public decimal BetAmountColorRedPlayer { get; set; }
        public decimal BetAmountColorBlackPlayer { get; set; }
        public decimal BetAmountColorRedBanker { get; set; }
        public decimal BetAmountColorBlackBanker { get; set; }
        public decimal BetAmountPairPlayer { get; set; }
        public decimal BetAmountPairBanker { get; set; }

        public decimal TargetPlayerWon { get; set; }
        public decimal TargetBankerWon { get; set; }
        public decimal SuitsPlayerWon { get; set; }
        public decimal SuitsBankerWon { get; set; }
        public decimal ColorRedPlayerWon { get; set; }
        public decimal ColorBlackPlayerWon { get; set; }
        public decimal ColorRedBankerWon { get; set; }
        public decimal ColorBlackBankerWon { get; set; }
        public decimal PairPlayerWon { get; set; }
        public decimal PairBankerWon { get; set; }
        #endregion

        #region First 3
        public decimal BetAmountSuitsGold { get; set; }
        public decimal BetAmountSuitsSilver { get; set; }
        public decimal BetAmountColorRedGold { get; set; }
        public decimal BetAmountColorBlackGold { get; set; }
        public decimal BetAmountColorRedSilver { get; set; }
        public decimal BetAmountColorBlackSilver { get; set; }

        public decimal SuitsGoldWon { get; set; }
        public decimal SuitsSilverWon { get; set; }
        public decimal ColorRedGoldWon { get; set; }
        public decimal ColorBlackGoldWon { get; set; }
        public decimal ColorRedSilverWon { get; set; }
        public decimal ColorBlackSilverWon { get; set; }
        #endregion
    }

    public class ConsolationBetModel
    {
        public string BetValue { get; set; }
        public string Amount { get; set; }
    }
}
