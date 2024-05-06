namespace GoPlayAsiaWebApp.Games.Bingo.Components
{
    public partial class BetButton
    {
        public partial class Bingo
        {

            private string selectedValue = "5";

            private void SelectOption(string option)
            {
                selectedValue = option;
            }
        }
        public string selectedValue { get; set; } = "";
        public string SelectOption { get; set; } = "";
    }
}
