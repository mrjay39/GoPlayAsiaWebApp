using Blazored.Modal.Services;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Reports.BetHistory;

public partial class CompBetHistory
{
    #region Injected Services
    [Inject] CompBetHistoryViewModel _compBetHistoryViewModel { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }


    #endregion

    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        _compBetHistoryViewModel.Category = Settings.Constants.specialty;
        _compBetHistoryViewModel.SelDate = DateTime.Now;
        await _compBetHistoryViewModel.LoadData();

        popupRes.Close();
    }

    public async Task SetCategory(string param)
    {
        await _compBetHistoryViewModel.SetCategory(param);
    }

    private async Task onclick_Go()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        await _compBetHistoryViewModel.LoadData();
        popupRes.Close();
    }

    private void ShowAll()
    {
        if (_compBetHistoryViewModel.ShowAllRows)
        {
            _compBetHistoryViewModel.ShowAllRows = false;
        }
        else
        {
            _compBetHistoryViewModel.ShowAllRows = true;
        }
        _compBetHistoryViewModel.UpdateVisibleGameRounds();
    }

    #endregion
}