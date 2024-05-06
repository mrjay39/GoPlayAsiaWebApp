using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Shared
{
    public partial class TokenDisplay
    {
        [Parameter] public string Amount { get; set; } = "";

        public static string NumFormatter(string param)
        {
            //parse parameter from string to int
            int amount = int.Parse(param, System.Globalization.NumberStyles.AllowThousands);

            //to decimal
            double value = Math.Truncate(10 * (amount / 1000D)) / 10;

            if (amount == 0 || amount < 10000)
            {
                return amount.ToString();
            }
            else if (amount % 100 == 0)
            {
                return value.ToString() + "k";
            }
            else
            {
                return value.ToString() + "k+";
            }

        }
    }
}
