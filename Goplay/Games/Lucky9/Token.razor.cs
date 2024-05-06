using Blazored.Modal.Services;
using Blazored.Modal;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Lucky9;

public partial class Token
{
    [CascadingParameter] public IModalService popupModalDB { get; set; }

    [Parameter] public GameChipModel GameChips { get; set; }
    [Parameter] public string TokenContainer { get; set; }
    [Parameter] public EventCallback<string> OnClickCallback { get; set; }

    private bool isDisabled = true;



    private async Task setBetAmount(int? Amount)
    {
        isDisabled = false;
        await lucky9ViewModel.SetBetSelectedValue(Amount);
        await OnClickCallback.InvokeAsync();
    }

    public async Task CloseDiv()
    {
        isDisabled = true;
        lucky9ViewModel.BetAmount = 0;
        await ResetToInitial();
        await lucky9ViewModel.HideDivToken();
    }

    public async Task SubmitBet()
    {
        bool valid = await lucky9ViewModel.ValidateBet(lucky9ViewModel.BetOptionSelected, lucky9ViewModel.BetCombinationValue);
        if (valid)
        {
            bool doublebet = await lucky9ViewModel.IsDoubleBet();
            if (doublebet)
            {
                var parametersdb = new ModalParameters();
                parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                ModalResult resultdb = await popupDB.Result;
                if ((bool)resultdb.Data)
                {
                    await lucky9ViewModel.SubmitBet();
                    await ResetToInitial();
                }
            }
            else
            {
                await lucky9ViewModel.SubmitBet();
                await ResetToInitial();
            }

        }
    }
    public async Task ResetToInitial()
    {
        isDisabled = true;
        TokenContainer = "DistokenContainer";
        lucky9ViewModel.Payout = 0;
        lucky9ViewModel.GameVariantId = 0;
        lucky9ViewModel.BetOptionSelected = string.Empty;
        lucky9ViewModel.BetCombinationValue = string.Empty;
        lucky9ViewModel.IsbuttonDisabled = false;
    }
}
