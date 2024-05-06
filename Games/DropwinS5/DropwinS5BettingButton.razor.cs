using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.DropwinS5;

public partial class DropwinS5BettingButton
{
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    [Parameter] public string RoundStatusString { get; set; } = "";
    private string F2ActiveHeader = "";
    private string F2ActiveBody = "";
    private string F3ActiveHeader = "";
    private string F3ActiveBody = "";
    private string All4ActiveHeader = "";
    private string All4ActiveBody = "";
    private string JustifyContents = "justify-content-between";
    private int selbetval { get; set; } = 1;
    private ElementReference txtnumref;
    private ElementReference txtcharref;

    [Parameter]
    public GameChipModel? GameChips { get; set; }
    public IModalReference popupConfirm { get; set; }
    [Parameter]
    public bool IsChipsVisible { get; set; }
    public bool IsBetPopupShown = false;
    public string betplaceholder = "";

    [CascadingParameter] public IModalService popupModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        dropwinS5ViewModel.Notify += OnNotify;
        await BetOptionSelected("F2");
    }
    public async Task BetOptionSelected(string arg)
    {
        await dropwinS5ViewModel.BetOptionSelected(arg);
    }

    public async Task ShowBetPopup(string betOptionSelected, string betCombinationValue)
    {
        //dropwinViewModel.BetOptionSelected = betOptionSelected;
        dropwinS5ViewModel.BetCombinationValue = betCombinationValue;

        bool iscrossbetting = await dropwinS5ViewModel.IsCrossBetting(dropwinS5ViewModel.BetCombinationValue, dropwinS5ViewModel.UserBets);
        if (!iscrossbetting)
        {
            ModalResult result = await dropwinS5ViewModel.popupBet.Result;
        }
        else
        {
            toastService.ShowError("Cross betting not allowed!");
        }

    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public void Dispose()
    {
        dropwinS5ViewModel.Notify -= OnNotify;
    }
    public async Task ClickBetButton(string BetType)
    {
        JustifyContents = "justify-content-between";
        switch (BetType)
        {
            case "F2":
                F2ActiveHeader = "TabActive";
                F2ActiveBody = "TabActiveHeader";
                F3ActiveHeader = "";
                F3ActiveBody = "";
                All4ActiveHeader = "";
                All4ActiveBody = "";
                betplaceholder = "2 Numbers (0-9)";
                dropwinS5ViewModel.Dwgametype = 1;
                break;
            case "F3":
                F2ActiveHeader = "";
                F2ActiveBody = "";
                F3ActiveHeader = "TabActive";
                F3ActiveBody = "TabActiveHeader";
                All4ActiveHeader = "";
                All4ActiveBody = "";
                betplaceholder = "3 Numbers (0-9)";
                dropwinS5ViewModel.Dwgametype = 2;
                break;
            case "ALL4":
                F2ActiveHeader = "";
                F2ActiveBody = "";
                F3ActiveHeader = "";
                F3ActiveBody = "";
                All4ActiveHeader = "TabActive";
                All4ActiveBody = "TabActiveHeader";
                JustifyContents = "";
                betplaceholder = "3 Numbers (0-9)";
                dropwinS5ViewModel.Dwgametype = 3;
                break;

            default:
                break;
        }
        await dropwinS5ViewModel.BetOptionSelected(BetType);
    }
    private async Task AddCharacter(string param)
    {
        await dropwinS5ViewModel.AddCharacter(param);
        if (@dropwinS5ViewModel.LetterValue.Length == 3 && @dropwinS5ViewModel.Dwgametype == 3)
        {
            selbetval = 2;
        }

    }
    private async Task AddLastChar(string param)
    {
        await dropwinS5ViewModel.AddLastChar(param);
        //if (@dropwinS5ViewModel.LastValue.Length == 1 && @dropwinS5ViewModel.Dwgametype == 3)
        //{
        //    selbetval = 1;
        //}
    }

    private async Task RemoveLastCharacter(int type)
    {
        if (type == 1)
            await dropwinS5ViewModel.RemoveLastCharacter();
        else
            await dropwinS5ViewModel.RemoveLastLastCharacter();
    }
    private async Task SetBetAmount(int? param)
    {
        await dropwinS5ViewModel.SetBetAmount(param);
    }
    private async Task PlaceBet()
    {
        if (IsBetPopupShown) return;

        if (dropwinS5ViewModel.IsPlaceBetEnabled)
        {
            var valid = await dropwinS5ViewModel.PlaceBet();
            if (valid)
            {
                var parameters = new ModalParameters();
                string msg = "";
                switch (dropwinS5ViewModel.BetTypeId)
                {
                    case 1:
                        bool valbet = await dropwinS5ViewModel.validateBet(@dropwinS5ViewModel.Dwgametype);
                        if (!valbet)
                        {
                            toastService.ShowError("Invalid Betting Parameters!");
                            return;
                        }

                        if (@dropwinS5ViewModel.Dwgametype == 2 || @dropwinS5ViewModel.Dwgametype == 1)
                            msg = "Confirm bet " + dropwinS5ViewModel.BetAmount + " for " + dropwinS5ViewModel.LetterValue;
                        else
                        {
                            msg = "Confirm bet " + dropwinS5ViewModel.BetAmount + " for " + dropwinS5ViewModel.LetterValue + dropwinS5ViewModel.LastValue;
                        }
                        break;
                    case 2:
                        msg = "Confirm bet " + dropwinS5ViewModel.BetAmount + " for Lucky Pick x 1";
                        break;
                    case 3:
                        msg = "Confirm bet " + dropwinS5ViewModel.BetAmount + " for Lucky Pick x 5";
                        break;
                }
                parameters.Add("Message", msg);

                popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
                IsBetPopupShown = true;
                ModalResult result = await popupConfirm.Result;
                IsBetPopupShown = false;
                if (result.Data != null)
                {
                    if ((bool)result.Data)
                    {
                        bool doublebet = await dropwinS5ViewModel.IsDoubleBet();
                        if (doublebet)
                        {
                            var parametersdb = new ModalParameters();
                            parametersdb.Add("Message", "Already Placed Bet on " + dropwinS5ViewModel.DuplicateBetvalue + ", Do you wish to proceed?");

                            var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                            ModalResult resultdb = await popupDB.Result;
                            if ((bool)resultdb.Data)
                            {
                                await dropwinS5ViewModel.SubmitBet();

                            }
                        }
                        else
                        {
                            await dropwinS5ViewModel.SubmitBet();
                        }
                        if (@dropwinS5ViewModel.Dwgametype == 3)
                        {
                            selbetval = 1;
                        }
                        popupConfirm.Close();
                    }
                }
            }
        }
    }
    //private void checkFocus(Microsoft.AspNetCore.Components.ChangeEventArgs args)
    //{
    //    var value = (string)args.Value;
    //    if(value.Length == 3 && @dropwinS5ViewModel.Dwgametype == 3)
    //    {

    //    }
    //}

    private async Task betsel(int betsel)
    {
        selbetval = betsel;
    }
    private async Task SetLuckyPickBet(string param)
    {
        await dropwinS5ViewModel.SetLuckyPickBet(param);

        //var parameters = new ModalParameters();
        //parameters.Add("Message", "Confirm bet " + dropwinViewModel.BetAmount + " for " + param);

        //popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
        //ModalResult result = await popupConfirm.Result;
        //if (result.Data != null)
        //{
        //    if ((bool)result.Data)
        //    {
        //        popupConfirm.Close();
        //        await dropwinViewModel.SetLuckyPickBet(param);
        //    }
        //}


    }
}
