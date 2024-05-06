using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Reports.Statistics;

public partial class Statistics
{
    #region Injected Services
    [Inject] StatisticsViewModel _statisticsViewModel { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    #endregion

    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        _statisticsViewModel.popupModal = popupModal;
        var popupRes = popupModal.Show<PopupLoading>("");
        await _statisticsViewModel.GetStatistics();
        popupRes.Close();
    }
    public async ValueTask DisposeAsync()
    {
        //await _statisticsViewModel.DisconnectSignalR();
    }
    #endregion
}
