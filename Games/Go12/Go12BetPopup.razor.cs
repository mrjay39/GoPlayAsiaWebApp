using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Games.Go12;

public partial class Go12BetPopup
{
    [CascadingParameter] public IModalService popupModal { get; set; }
    [CascadingParameter] public IModalService popupModalDb { get; set; }
    public bool IsBetPopupShown = false;
    public async Task Confirmation()
    {
        if (IsBetPopupShown) return;

        go12ViewModel.popupModal = popupModal;
        //var valid = true;
        bool valid = await go12ViewModel.ValidateBet();
        if (valid)
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Confirm bet " + go12ViewModel.BetAmount + " for " + go12ViewModel.BetCombinationValue);
            go12ViewModel.popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            IsBetPopupShown = true;
            ModalResult result = await go12ViewModel.popupConfirm.Result;
            IsBetPopupShown = false;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {

                    bool doublebet = await go12ViewModel.IsDoubleBet();
                    if (doublebet)
                    {
                        var parametersdb = new ModalParameters();
                        parametersdb.Add("Message", "You already placed a bet for " + go12ViewModel.DuplicateBetvalue + ", Do you wish to proceed?");

                        var popupDB = popupModalDb.Show<PopupConfirm>("", parametersdb, new ModalOptions() { Class = "op-modal" });
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


                    go12ViewModel.popupConfirm.Close();
                    go12ViewModel.popupBet.Close();

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
        go12ViewModel.Notify -= OnNotify;
    }
}
