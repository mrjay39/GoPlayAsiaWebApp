using Blazored.Modal.Services;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Reports.Notification;

public partial class Notification
{
    #region Injected Services
    [Inject] NotificationViewModel _notificationViewModel { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    #endregion

    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        _notificationViewModel.popupModal = popupModal;
        var popupRes = popupModal.Show<PopupLoading>("");
        await _notificationViewModel.GetUserNotifications();
        popupRes.Close();
    }
    public async ValueTask DisposeAsync()
    {
        //await _notificationViewModel.DisconnectSignalR();
    }
    #endregion
}
