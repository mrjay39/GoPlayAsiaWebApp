using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.First3;

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
        await first3ViewModel.SetBetSelectedValue(Amount);
        await OnClickCallback.InvokeAsync();
    }

    public async Task CloseDiv()
    {
        isDisabled = true;
        first3ViewModel.BetAmount = 0;
        await ResetToInitial();
        await first3ViewModel.HideDivToken();
    }

    public async Task SubmitBet()
    {
        bool valid = await first3ViewModel.ValidateBet(first3ViewModel.BetOptionSelected, first3ViewModel.BetCombinationValue);
        if (valid)
        {
            bool doublebet = await first3ViewModel.IsDoubleBet();
            if (doublebet)
            {
                var parametersdb = new ModalParameters();
                parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                ModalResult resultdb = await popupDB.Result;
                if ((bool)resultdb.Data)
                {
                    await first3ViewModel.SubmitBet();
                    await ResetToInitial();
                }
            }
            else
            {
                await first3ViewModel.SubmitBet();
                await ResetToInitial();
            }

        }
    }

    public async Task ResetToInitial()
    {
        isDisabled = true;
        TokenContainer = "DistokenContainer";
        first3ViewModel.Payout = 0;
        first3ViewModel.GameVariantId = 0;
        first3ViewModel.BetOptionSelected = string.Empty;
        first3ViewModel.BetCombinationValue = string.Empty;
        first3ViewModel.isbuttonDisabled = false;
    }

}
