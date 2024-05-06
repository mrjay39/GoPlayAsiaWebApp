using Microsoft.AspNetCore.Components;
using System.Drawing;
using System;
using Microsoft.AspNetCore.Components.Web;
using static GoplayasiaBlazor.Models.Constants.Settings;
using GoPlayAsiaWebApp.Goplay.ViewModels;

namespace GoPlayAsiaWebApp.Goplay.Games.Bingo.CardTemplate
{

    public partial class Card
    {
        [Parameter]
        public string suite { get; set; }
        [Parameter]
        public string cardValue { get; set; }

        [Parameter]
        public string betValue { get; set; }


        [Inject] BingoViewModel _bingoViewModel { get; set; }

        public string imageStr { get; set; }
        public string cardColor { get; set; }

        [Parameter]
        public EventCallback<string> OnClickCallback { get; set; }

        private void onCardClick()
        {
            if (OnClickCallback.HasDelegate)
            {
                OnClickCallback.InvokeAsync(betValue);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            var SelCardValue = _bingoViewModel._bingoPermutation.BingoCards.Where(x => x.FinalResult == betValue).Take(1).FirstOrDefault();
            if (SelCardValue != null)
            {
                cardValue = SelCardValue.Value;
                switch (SelCardValue.Suit.ToLower())
                {
                    case "h":
                        imageStr = "img/heart.png";
                        cardColor = "red";
                        break;
                    case "d":
                        imageStr = "img/diamond.png";
                        cardColor = "red";
                        break;
                    case "s":
                        imageStr = "img/spade.png";
                        cardColor = "black";
                        break;
                    case "c":
                        imageStr = "img/club.png";
                        cardColor = "black";
                        break;

                }
            }

        }
    }
}
