using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.ObjectModel;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class NotificationViewModel : BaseViewModel
{
    #region Local Variable & Properties
    INotificationService _notificationService;
    IHTTPClientHelper _httpClientHelper;

    private string _notificationCount;
    public string NotificationCount
    {
        get => _notificationCount;
        set
        {
            _notificationCount = value;
        }
    }

    private ObservableCollection<NotificationDisplayModel> _notifications;
    public ObservableCollection<NotificationDisplayModel> Notifications
    {
        get => _notifications;
        set
        {
            _notifications = value;
        }
    }
    #endregion

    #region Life cycle methods
    public NotificationViewModel(INotificationService notificationService, ICurrentUser currentUser, IHTTPClientHelper httpClientHelper, NavigationManager navigationManager, IAccountService iaccountService, IToastService toastService,
        AuthenticationStateProvider AuthenticationStateProvider)
    {
        _notificationService = notificationService;
        _icurrentUser = currentUser;
        _httpClientHelper = httpClientHelper;
        _navigationManager = navigationManager;
        _iaccountService = iaccountService;
        _toastService = toastService;
        _AuthenticationStateProvider = AuthenticationStateProvider;
    }
    #endregion

    #region Local Methods
    public async Task GetUserNotifications()
    {
        try
        {

            var tempNotifications = await _notificationService.GetUserNotifications(_icurrentUser.Id);
            if (tempNotifications != null && tempNotifications.Count > 0)
            {
                var unseenCount = tempNotifications.Where(x => x.Seen == false).ToList().Count;
                NotificationCount = unseenCount > 0 ? unseenCount.ToString() : string.Empty;

                var notificationGroups = tempNotifications.OrderByDescending(x => x.DateCreated).GroupBy(x => x.DateCreated.Value.Date).ToList();
                if (notificationGroups != null && notificationGroups.Count > 0)
                {
                    NotificationDisplayModel model = new NotificationDisplayModel();
                    ObservableCollection<NotificationDisplayModel> tempList = new ObservableCollection<NotificationDisplayModel>();
                    DateTime displayDate = new DateTime();
                    foreach (var n in notificationGroups)
                    {
                        model = new NotificationDisplayModel();
                        displayDate = n.FirstOrDefault() != null ? n.FirstOrDefault().DateCreated.Value : new DateTime();
                        if (displayDate != new DateTime())
                        {
                            if (displayDate.Date == DateTime.Now.Date)
                            {
                                model.Date = "Today";
                            }
                            else
                            {
                                model.Date = displayDate.ToString("MMMM dd, yyyy");
                            }
                        }
                        model.NotificationsList = new ObservableCollection<NotificationModel>(n);
                        tempList.Add(model);
                    }

                    if (tempList != null)
                    {
                        Notifications = tempList;
                    }
                }
            }
            else
            {
                NotificationCount = string.Empty;
            }

        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }

    public async Task MarkAllAsRead()
    {
        try
        {
            var res = await _notificationService.SeeAllNotification(_icurrentUser.Id);
            await GetUserNotifications();

        }
        catch (Exception ex)
        {
            // Console.WriteLine(ex);
        }
    }

    public async Task SeeNotification(long notificationId)
    {
        try
        {
            await _httpClientHelper.PutAsync<bool>($"Notification/SeeNotification/{notificationId}", _icurrentUser.Token, notificationId);
            await GetUserNotifications();
        }
        catch (Exception ex)
        {
            // Console.WriteLine(ex);
        }
    }
    #endregion
}
