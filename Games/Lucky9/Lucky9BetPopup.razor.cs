using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.Lucky9;

public partial class Lucky9BetPopup
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    public bool IsBetPopupShown = false;
    public async Task Confirmation()
    {
        if (IsBetPopupShown) return;

        luckyViewModel.popupModal = popupModal;
        bool valid = await luckyViewModel.ValidateBet(luckyViewModel.BetOptionSelected, luckyViewModel.BetCombinationValue);
        if (valid)
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Confirm bet " + luckyViewModel.BetAmount + " for " + luckyViewModel.BetCombinationValue);

            luckyViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            IsBetPopupShown = true;
            ModalResult result = await luckyViewModel.popupConfirm.Result;
            IsBetPopupShown = false;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    if ((bool)result.Data)
                    {
                        bool doublebet = await luckyViewModel.IsDoubleBet();
                        if (doublebet)
                        {
                            var parametersdb = new ModalParameters();
                            parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                            var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                            ModalResult resultdb = await popupDB.Result;
                            if ((bool)resultdb.Data)
                            {
                                await luckyViewModel.SubmitBet();
                            }
                        }
                        else
                        {
                            await luckyViewModel.SubmitBet();
                        }


                        luckyViewModel.popupConfirm.Close();
                        luckyViewModel.popupBet.Close();

                    }



                }
            }
        }

    }
    public async void OnNotify()
    {
        await InvokeAsync(() => StateHasChanged());
    }
    public void Dispose()
    {
        luckyViewModel.Notify -= OnNotify;
    }
}
