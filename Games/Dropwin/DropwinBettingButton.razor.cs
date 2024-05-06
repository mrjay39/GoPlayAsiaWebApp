using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Reflection.Metadata;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Dropwin;

public partial class DropwinBettingButton
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
    private string SelGameType = "";
    private string pickguide = "2 Letters (A-J)";
    public int BetAmount = 10;
    public bool luckypickdisabled { get; set; } = false;
    [Parameter]
    public GameChipModel? GameChips { get; set; }
    public IModalReference popupConfirm { get; set; }
    [Parameter]
    public bool IsChipsVisible { get; set; }
    public bool IsBetPopupShown = false;
    public ElementReference txtbetletters;
    public bool showArrow = false;
    [CascadingParameter] public IModalService popupModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        dropwinViewModel.Notify += OnNotify;
        dropwinViewModel.NotifyGameStatus += NotifyGameStatus;

        await ClickBetButton("", true);
        await BetOptionSelected("");

    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            showPointerArrow();
    }
    public async Task BetOptionSelected(string arg)
    {
        await dropwinViewModel.BetOptionSelected(arg);
    }

    public async Task ShowBetPopup(string betOptionSelected, string betCombinationValue)
    {
        //dropwinViewModel.BetOptionSelected = betOptionSelected;
        dropwinViewModel.BetCombinationValue = betCombinationValue;

        bool iscrossbetting = await dropwinViewModel.IsCrossBetting(dropwinViewModel.BetCombinationValue, dropwinViewModel.UserBets);
        if (!iscrossbetting)
        {
            ModalResult result = await dropwinViewModel.popupBet.Result;
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
    public async void NotifyGameStatus()
    {
        F2ActiveHeader = "";
        F3ActiveHeader = "";
        All4ActiveHeader = "";
        await ClearBet();
    }
    public void Dispose()
    {
        dropwinViewModel.Notify -= OnNotify;
        dropwinViewModel.Notify -= NotifyGameStatus;
    }
    private async Task showPointerArrow()
    {
        showArrow = true;
        StateHasChanged();
        await Task.Delay(3000);
        showArrow = false;
        StateHasChanged();
    }
    public async Task ClickBetButton(string BetType, bool onstart)
    {
        if (onstart)
        {
            luckypickdisabled = true;
        }
        else
        {
            luckypickdisabled = false;
        }
        JustifyContents = "justify-content-between";

        if (SelGameType != BetType.ToString() || dropwinViewModel.IsLuckyPick1 || dropwinViewModel.IsLuckyPick5)
        {
            await ClearBet();
            await dropwinViewModel.BetOptionSelected(BetType);
        }


        switch (BetType)
        {
            case "F2":
                F2ActiveHeader = "TabActive";
                F2ActiveBody = "TabActiveHeader";
                F3ActiveHeader = "";
                F3ActiveBody = "";
                All4ActiveHeader = "";
                All4ActiveBody = "";
                pickguide = "2 Letters (A-JX)";
                SelGameType = BetType.ToString();
                break;
            case "F3":
                F2ActiveHeader = "";
                F2ActiveBody = "";
                F3ActiveHeader = "TabActive";
                F3ActiveBody = "TabActiveHeader";
                All4ActiveHeader = "";
                All4ActiveBody = "";
                pickguide = "3 Letters (A-JY)";
                SelGameType = BetType.ToString();
                break;
            case "ALL4":
                F2ActiveHeader = "";
                F2ActiveBody = "";
                F3ActiveHeader = "";
                F3ActiveBody = "";
                All4ActiveHeader = "TabActive";
                All4ActiveBody = "TabActiveHeader";
                JustifyContents = "";
                pickguide = "4 Letters (A-JZ)";
                SelGameType = BetType.ToString();
                break;

            default:
                SelGameType = BetType.ToString();
                break;
        }

        try
        {

            await txtbetletters.FocusAsync();
        }
        catch (Exception)
        {
        }
    }

    private async Task AddCharacter(string param)
    {
        await dropwinViewModel.AddCharacter(param);
    }

    private async Task RemoveLastCharacter()
    {
        await dropwinViewModel.RemoveLastCharacter();
    }
    private async Task SetBetAmount(int? param, string? multipler = "")
    {
        await dropwinViewModel.SetBetAmount(param, multipler);
    }
    private async Task PlaceBet()
    {
        if (IsBetPopupShown) return;
        if (@dropwinViewModel.IsPlaceBetEnabled)
        {
            var valid = await dropwinViewModel.PlaceBet();
            if (valid)
            {
                var parameters = new ModalParameters();
                string msg = "";
                switch (dropwinViewModel.BetTypeId)
                {
                    case 1:
                        msg = "Confirm bet " + dropwinViewModel.BetAmount + " for " + dropwinViewModel.LetterValue;
                        break;
                    case 2:
                        msg = "Confirm bet " + dropwinViewModel.BetAmount + " for Lucky Pick x 1";
                        break;
                    case 3:
                        msg = "Confirm bet " + dropwinViewModel.BetAmount + " for Lucky Pick x 5";
                        break;
                }
                //for (var i = 0; i < 9999999; i++)
                //{
                //dropwinViewModel.BetAmount = 250;
                //await dropwinViewModel.SubmitBet();
                //}
                parameters.Add("Message", msg);

                popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
                IsBetPopupShown = true;
                ModalResult result = await popupConfirm.Result;
                IsBetPopupShown = false;
                if (result.Data != null)
                {
                    if ((bool)result.Data)
                    {
                        bool doublebet = await dropwinViewModel.IsDoubleBet();
                        if (doublebet)
                        {
                            var parametersdb = new ModalParameters();
                            parametersdb.Add("Message", "Already Placed Bet on " + dropwinViewModel.DuplicateBetvalue + ", Do you wish to proceed?");

                            var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                            ModalResult resultdb = await popupDB.Result;
                            if ((bool)resultdb.Data)
                            {
                                await dropwinViewModel.SubmitBet();
                                // F2ActiveHeader = "";
                                // F3ActiveHeader = "";
                                // All4ActiveHeader = "";
                                // SelGameType = "";
                                await ClearBet();
                            }
                        }
                        else
                        {
                            await dropwinViewModel.SubmitBet();
                            // F2ActiveHeader = "";
                            // F3ActiveHeader = "";
                            // All4ActiveHeader = "";
                            // SelGameType = "";
                            await ClearBet();
                        }

                        popupConfirm.Close();
                    }
                }
            }
        }
    }
    private async Task ClearBet()
    {
        dropwinViewModel.BetAmountDisplay = "";
        dropwinViewModel.IsPlaceBetEnabled = false;
        dropwinViewModel.LetterValue = "";
        dropwinViewModel.BetAmount = 10;
        dropwinViewModel.BetMultiplier = "";
        dropwinViewModel.BetAmountDisplay = "10";
        dropwinViewModel.SelectedChipAmount = 10;
        await dropwinViewModel.ResetDefaultAmount();
    }
    private async Task SetLuckyPickBet(string param)
    {
        if (SelGameType != "")
        {
            await dropwinViewModel.SetLuckyPickBet(param);
        }
        else
        {
            showPointerArrow();
        }

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
