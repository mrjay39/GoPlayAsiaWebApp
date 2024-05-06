using Blazored.Modal.Services;
using Blazored.Modal;
using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.ViewModels;

namespace GoPlayAsiaWebApp.Goplay.Games.Go12;

public partial class Token
{
    [CascadingParameter] public IModalService popupModalDB { get; set; }

    [Parameter] public GameChipModel GameChips { get; set; }
    [Parameter] public EventCallback<string> OnClickCallback { get; set; }

    private bool isDisabled = true;

    public static string BetValueDisplay(string param)
    {
        if (param == Settings.Constants.FixedBlack || param == Settings.Constants.FixedRed)
        {
            return "";
        }
        else
        {
            return param;
        }
    }
    private async Task setBetAmount(int? Amount)
    {
        isDisabled = false;
        await go12ViewModel.SetBetSelectedValue(Amount);
        await OnClickCallback.InvokeAsync();
    }
    public async Task CloseDiv()
    {
        isDisabled = true;
        await ResetToInitial();
        await go12ViewModel.HideDivToken();
    }
    public async Task SubmitBet()
    {
        bool valid = await go12ViewModel.ValidateBet();
        if (valid)
        {
            bool doublebet = await go12ViewModel.IsDoubleBet();
            if (doublebet)
            {
                var parametersdb = new ModalParameters();
                parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                ModalResult resultdb = await popupDB.Result;
                if ((bool)resultdb.Data)
                {
                    await go12ViewModel.SubmitBet();
                }
            }
            else
            {
                await go12ViewModel.SubmitBet();
            }
            await ResetToInitial();
        }
    }
    public async Task ResetToInitial()
    {
        isDisabled = true;
        go12ViewModel.BetAmount = 0;
        go12ViewModel.Payout = 0;
        go12ViewModel.GameVariantId = 0;
        go12ViewModel.BetCombinationValue = string.Empty;
    }
}
