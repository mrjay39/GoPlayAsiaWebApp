using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        public NotificationService(IHTTPClientHelper httpClientHelper, IMapper mapper, ICurrentUser currentUser)
        {
            _httpClientHelper = httpClientHelper;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<NotificationModel> GetNotification(long notificationId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<NotificationDTO>($"Notification/Notification/{notificationId}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<NotificationModel>(result);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<List<NotificationModel>> GetUserNotifications(long userId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<NotificationDTO>>($"Notification/UserNotifications/{userId}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<NotificationModel>>(result);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<bool> SeeNotification(long notificationId)
        {
            try
            {
                return await _httpClientHelper.PutAsync<bool>($"Notification/SeeNotification/{notificationId}", _currentUser.Token, notificationId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<bool> SeeAllNotification(long userId)
        {
            try
            {
                return await _httpClientHelper.PutAsync<bool>($"Notification/SeeAll/{userId}", _currentUser.Token, userId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return false;
            }
        }

    }
}
