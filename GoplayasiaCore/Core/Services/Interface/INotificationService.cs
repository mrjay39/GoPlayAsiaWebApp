using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationModel>> GetUserNotifications(long userId);
        Task<NotificationModel> GetNotification(long notificationId);
        Task<bool> SeeNotification(long notificationId);
        Task<bool> SeeAllNotification(long userId);
    }
}
