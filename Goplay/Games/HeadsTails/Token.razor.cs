using Blazored.Modal.Services;
using Blazored.Modal;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.HeadsTails;

public partial class Token
{
    [Parameter] public GameChipModel GameChips { get; set; }
    [Parameter] public EventCallback<string> OnClickCallback { get; set; }

    [CascadingParameter] public IModalService popupModalDB { get; set; }

    private bool isDisabled = true;

    private async Task setBetAmount(int? Amount)
    {
        isDisabled = false;
        await headstailsviewmodel.SetBetSelectedValue(Amount);
        await OnClickCallback.InvokeAsync();
    }

    public async Task CloseDiv()
    {
        isDisabled = true;
        headstailsviewmodel.BetAmount = 0;
        await ResetToInitial();
        await headstailsviewmodel.HideDivToken();
    }
    public async Task SubmitBet()
    {
        bool valid = await headstailsviewmodel.ValidateBet(headstailsviewmodel.BetOptionSelected, headstailsviewmodel.BetCombinationValue);
        if (valid)
        {
            bool doublebet = await headstailsviewmodel.IsDoubleBet();
            if (doublebet)
            {
                var parametersdb = new ModalParameters();
                parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                ModalResult resultdb = await popupDB.Result;
                if ((bool)resultdb.Data)
                {
                    await headstailsviewmodel.SubmitBet();
                }
            }
            else
            {
                await headstailsviewmodel.SubmitBet();
            }
            await ResetToInitial();
        }
    }
    public async Task ResetToInitial()
    {
        isDisabled = true;
        headstailsviewmodel.Payout = 0;
        headstailsviewmodel.BetOptionSelected = string.Empty;
        headstailsviewmodel.BetCombinationValue = string.Empty;
    }
}