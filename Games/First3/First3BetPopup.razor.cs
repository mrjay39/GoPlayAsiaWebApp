using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.First3;

public partial class First3BetPopup
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }
    public bool IsBetPopupShown = false;
    public async Task Confirmation()
    {
        if (IsBetPopupShown) return;

        first3ViewModel.popupModal = popupModal;
        bool valid = await first3ViewModel.ValidateBet(first3ViewModel.BetOptionSelected, first3ViewModel.BetCombinationValue);
        if (valid)
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Confirm bet " + first3ViewModel.BetAmount + " for " + first3ViewModel.BetCombinationValue);

            first3ViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            IsBetPopupShown = true;
            ModalResult result = await first3ViewModel.popupConfirm.Result;
            IsBetPopupShown = false;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    if ((bool)result.Data)
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
                            }
                        }
                        else
                        {
                            await first3ViewModel.SubmitBet();
                        }


                        first3ViewModel.popupConfirm.Close();
                        first3ViewModel.popupBet.Close();

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
        first3ViewModel.Notify -= OnNotify;
    }
}
