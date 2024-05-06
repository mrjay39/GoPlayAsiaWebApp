using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Bingo
{
    public partial class CardSelection
    {
        private string selectedButton = "";

        private void ShowSelectedButton(string buttonName)
        {
            selectedButton = buttonName;
        }
        ///CARD SUITE VALUE
        [Parameter]
        public string suite { get; set; }
        [Parameter]
        public string cardValue { get; set; }
        public string imageStr { get; set; }
        public string cardColor { get; set; }


        protected override async Task OnParametersSetAsync()
        {

            switch (suite)
            {
                case "H":
                    imageStr = "img/heart.png";
                    cardColor = "red";
                    break;
                case "D":
                    imageStr = "img/diamond.png";
                    cardColor = "red";
                    break;
                case "S":
                    imageStr = "img/spade.png";
                    cardColor = "black";
                    break;
                case "C":
                    imageStr = "img/club.png";
                    cardColor = "black";
                    break;

            }
        }
    }
}

