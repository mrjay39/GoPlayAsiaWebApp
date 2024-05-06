
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models.Base;

namespace GoplayasiaBlazor.Models.Incoming;

public class DiceBetResultModel : BaseResultModel
{
    public BetModel Bet { get; set; }

    #region NEW VARIANT
    public string AllDiceBet { get; set; } = string.Empty;
    public string EvenBet { get; set; } = string.Empty;
    public string OddBet { get; set; } = string.Empty;
    public string SmallBet { get; set; } = string.Empty;
    public string BigBet { get; set; } = string.Empty;
    public string FirstDiceBet { get; set; } = string.Empty;
    public string SecondDiceBet { get; set; } = string.Empty;
    public string ThirdDiceBet { get; set; } = string.Empty;


    public string AllDiceWinnings { get; set; } = "0.00";
    public string EvenWinnings { get; set; } = "0.00";
    public string OddWinnings { get; set; } = "0.00";
    public string SmallWinnings { get; set; } = "0.00";
    public string BigWinnings { get; set; } = "0.00";
    public string FirstDiceWinnings { get; set; } = "0.00";
    public string SecondDiceWinnings { get; set; } = "0.00";
    public string ThirdDiceWinnings { get; set; } = "0.00";
    #endregion

    public List<DiceBetModel> FirstDiceBets { get; set; }
    public string FirstDiceSum
    {
        get
        {
            if (FirstDiceBets != null && FirstDiceBets.Any())
            {
                var firstDiceSum = FirstDiceBets
                    .Sum(x => x.BetAmount);
                if (firstDiceSum.HasValue && firstDiceSum > 0)
                    return firstDiceSum.Value.ToString("N0");
            }
            return "0";
        }
    }
    public List<DiceBetModel> SecondDiceBets { get; set; }
    public string SecondDiceSum
    {
        get
        {
            if (SecondDiceBets != null && SecondDiceBets.Any())
            {
                var secondDiceSum = SecondDiceBets
                    .Sum(x => x.BetAmount);
                if (secondDiceSum.HasValue && secondDiceSum > 0)
                    return secondDiceSum.Value.ToString("N0");
            }
            return "0";
        }
    }
    public List<DiceBetModel> ThirdDiceBets { get; set; }
    public string ThirdDiceSum
    {
        get
        {
            if (ThirdDiceBets != null && ThirdDiceBets.Any())
            {
                var thirdDiceSum = ThirdDiceBets
                    .Sum(x => x.BetAmount);
                if (thirdDiceSum.HasValue && thirdDiceSum > 0)
                    return thirdDiceSum.Value.ToString("N0");
            }
            return "0";
        }
    }
    public List<DiceBetModel> AllDiceBets { get; set; }
    public string AllDiceSum
    {
        get
        {
            if (AllDiceBets != null && AllDiceBets.Any())
            {
                var allDiceSum = AllDiceBets
                    .Sum(x => x.BetAmount);
                if (allDiceSum.HasValue && allDiceSum > 0)
                    return allDiceSum.Value.ToString("N0");
            }
            return "0";
        }
    }

    public string BetNumber1 { get; set; } = string.Empty;
    public string BetNumber2 { get; set; } = string.Empty;
    public string BetNumber3 { get; set; } = string.Empty;
    public string BetNumber4 { get; set; } = string.Empty;
    public string BetNumber5 { get; set; } = string.Empty;
    public string BetNumber6 { get; set; } = string.Empty;
}

