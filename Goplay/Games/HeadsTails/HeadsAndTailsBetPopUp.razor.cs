using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.HeadsTails;

public partial class HeadsAndTailsBetPopUp
{
    public bool IsBetPopupShown = false;

    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDB { get; set; }

    public async Task Confirmation()
    {
        if (IsBetPopupShown) return;

        IHeadsAndTailsViewModel.popupModal = popupModal;
        bool valid = await IHeadsAndTailsViewModel.ValidateBet(IHeadsAndTailsViewModel.BetOptionSelected, IHeadsAndTailsViewModel.BetCombinationValue);
        if (valid)
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Confirm bet " + IHeadsAndTailsViewModel.BetAmount + " for " + IHeadsAndTailsViewModel.BetCombinationValue);

            IHeadsAndTailsViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            IsBetPopupShown = true;
            ModalResult result = await IHeadsAndTailsViewModel.popupConfirm.Result;
            IsBetPopupShown = false;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    if ((bool)result.Data)
                    {
                        bool doublebet = await IHeadsAndTailsViewModel.IsDoubleBet();
                        if (doublebet)
                        {
                            var parametersdb = new ModalParameters();
                            parametersdb.Add("Message", "Already Placed Bet, Do you wish to proceed?");
                            var popupDB = popupModalDB.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
                            ModalResult resultdb = await popupDB.Result;
                            if ((bool)resultdb.Data)
                            {
                                await IHeadsAndTailsViewModel.SubmitBet();
                            }
                        }
                        else
                        {
                            await IHeadsAndTailsViewModel.SubmitBet();
                        }
                        IHeadsAndTailsViewModel.popupConfirm.Close();
                        IHeadsAndTailsViewModel.popupBet.Close();
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
        IHeadsAndTailsViewModel.Notify -= OnNotify;
    }
}
