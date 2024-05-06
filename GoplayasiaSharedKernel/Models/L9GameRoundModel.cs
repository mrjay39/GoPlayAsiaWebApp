
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Models.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoplayasiaBlazor.Models
{
    public class L9GameRoundModel
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
                if (GameTypeId == (int)GameTypes.Lucky_9)
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
                else if (GameTypeId == (int)GameTypes.Gold_And_Silver)
                {
                    if (WinningResult == Settings.Constants.Draw)
                    {
                        resultValue = "draw";
                    }
                    else if (WinningResult == Settings.Constants.Gold)
                    {
                        resultValue = "gold";
                    }
                    else if (WinningResult == Settings.Constants.Silver)
                    {
                        resultValue = "silver";
                    }
                }
                else if (GameTypeId == (int)GameTypes.Heads_And_Tails)
                {
                    if (WinningResult == Settings.Constants.Draw)
                    {
                        resultValue = "draw";
                    }
                    else if (WinningResult == Settings.Constants.Heads)
                    {
                        resultValue = "heads";
                    }
                    else if (WinningResult == Settings.Constants.Tails)
                    {
                        resultValue = "tails";
                    }
                }
                else if (GameTypeId == (int)GameTypes.Giga_Draw)
                {
                    resultValue = "gigadraw";
                }
                else if (GameTypeId == (int)GameTypes.Drop_And_Win)
                {
                    resultValue = "dropwin";
                }
                else if (GameTypeId == (int)GameTypes.Go_12)
                {
                    if (WinningResult.Contains(Settings.Constants.One))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Two))
                    {
                        resultValue = "Black";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Three))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Four))
                    {
                        resultValue = "Black";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Five))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Six))
                    {
                        resultValue = "Black";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Seven))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Eight))
                    {
                        resultValue = "Black";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Nine))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Ten))
                    {
                        resultValue = "Black";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Eleven))
                    {
                        resultValue = "Red";
                    }
                    else if (WinningResult.Contains(Settings.Constants.Twelve))
                    {
                        resultValue = "Black";
                    }
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
        public string OddsTrendsResultColor => SetOddsTrendsResultColor();
        private string SetOddsTrendsResultColor()
        {
            string resultValue = string.Empty;
            if (RoundStatus == (int)Settings.RoundStatus.Cancelled)
            {
                resultValue = "rocancelled";
            }
            else
            {
                if (RunningOddsCancelled)
                {
                    resultValue = "rocancelled";
                }
                else
                {
                    if (GameTypeId == (int)GameTypes.Lucky_9)
                    {
                        if (WinningResult == Settings.Constants.Draw)
                        {
                            resultValue = "rodraw";
                        }
                        else if (WinningResult == Settings.Constants.Player)
                        {
                            resultValue = "roplayer";
                        }
                        else if (WinningResult == Settings.Constants.Banker)
                        {
                            resultValue = "robanker";
                        }
                    }
                    else if (GameTypeId == (int)GameTypes.Gold_And_Silver)
                    {
                        if (WinningResult == Settings.Constants.Draw)
                        {
                            resultValue = "rodraw";
                        }
                        else if (WinningResult == Settings.Constants.Gold)
                        {
                            resultValue = "rogold";
                        }
                        else if (WinningResult == Settings.Constants.Silver)
                        {
                            resultValue = "rosilver";
                        }
                    }
                    else if (GameTypeId == (int)GameTypes.Heads_And_Tails)
                    {
                        if (WinningResult == Settings.Constants.Draw)
                        {
                            resultValue = "rodraw";
                        }
                        else if (WinningResult == Settings.Constants.Heads)
                        {
                            resultValue = "roheads";
                        }
                        else if (WinningResult == Settings.Constants.Tails)
                        {
                            resultValue = "rotails";
                        }
                    }
                    else if (GameTypeId == (int)GameTypes.Go_12)
                    {
                        if (WinningResult.Contains(Settings.Constants.One))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Two))
                        {
                            resultValue = "Black";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Three))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Four))
                        {
                            resultValue = "Black";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Five))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Six))
                        {
                            resultValue = "Black";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Seven))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Eight))
                        {
                            resultValue = "Black";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Nine))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Ten))
                        {
                            resultValue = "Black";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Eleven))
                        {
                            resultValue = "Red";
                        }
                        else if (WinningResult.Contains(Settings.Constants.Twelve))
                        {
                            resultValue = "Black";
                        }
                    }
                }
            }
            return resultValue;
        }
        public DateTime? ConvertedDateCreated { get; set; }
        public DateTime? ConvertedDrawDate { get; set; }
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
        public string LeftWinningPair { get; set; }
        public string RightWinningPair { get; set; }
        public string TrioResult { get; set; }

        public List<Go12Model> Go12Bets { get; set; }

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
                if (GameTypeId == (int)GameTypes.Go_12)
                {
                    if (RoundStatus == (int)Settings.RoundStatus.Closed && FixedCancelled == false)
                    {
                        if (WinningResult.Contains(Settings.Constants.One))
                            resultValue = "1";
                        else if (WinningResult.Contains(Settings.Constants.Two))
                            resultValue = "2";
                        else if (WinningResult.Contains(Settings.Constants.Three))
                            resultValue = "3";
                        else if (WinningResult.Contains(Settings.Constants.Four))
                            resultValue = "4";
                        else if (WinningResult.Contains(Settings.Constants.Five))
                            resultValue = "5";
                        else if (WinningResult.Contains(Settings.Constants.Six))
                            resultValue = "6";
                        else if (WinningResult.Contains(Settings.Constants.Seven))
                            resultValue = "7";
                        else if (WinningResult.Contains(Settings.Constants.Eight))
                            resultValue = "8";
                        else if (WinningResult.Contains(Settings.Constants.Nine))
                            resultValue = "9";
                        else if (WinningResult.Contains(Settings.Constants.Ten))
                            resultValue = "10";
                        else if (WinningResult.Contains(Settings.Constants.Eleven))
                            resultValue = "11";
                        else if (WinningResult.Contains(Settings.Constants.Twelve))
                            resultValue = "12";
                    }
                    else
                    {
                        resultValue = "C";
                    }

                }
                else
                {
                    resultValue = WinningResult;
                }
            }

            return resultValue;
        }
    }
}
