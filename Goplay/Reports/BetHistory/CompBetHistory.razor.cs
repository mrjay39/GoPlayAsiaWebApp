using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

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
        await _compBetHistoryViewModel.LoadData();
        popupRes.Close();
    }
    #endregion
}