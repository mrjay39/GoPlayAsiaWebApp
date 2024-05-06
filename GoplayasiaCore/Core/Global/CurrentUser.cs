using Blazored.SessionStorage;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoplayasiaBlazor.Core.Global
{

    public class CurrentUser : ICurrentUser
    {

        #region Injected Services
        Blazored.SessionStorage.ISessionStorageService _protectedSessionStore;
        #endregion
        public CurrentUser(Blazored.SessionStorage.ISessionStorageService ProtectedSessionStore)
        {
            _protectedSessionStore = ProtectedSessionStore;

        }
        public long Id
        {
            get; set;
        }
        public decimal? Credits
        {
            get; set;
        }
        public string? CreditsDisp
        {
            get; set;
        }
        public string? EmailAddress
        {
            get; set;
        }
        public bool EmailValidated
        {
            get; set;
        }
        public string? FullName
        {
            get; set;
        }
        public string? Username
        {
            get; set;
        }

        public int IdleTimer
        {
            get; set;
        }
        public string? MobileNumber
        {
            get; set;
        }
        public bool MobileNumberValidated
        {
            get; set;
        }
        public int PopupTimer
        {
            get; set;
        }
        public string? ProfileImage
        {
            get; set;
        }
        public TimeSpan RemainingViewTime
        {
            get; set;
        }
        public int RoleType
        {
            get; set;
        }
        public string? Token
        {
            get; set;
        }
        public string NotificationCount
        {
            get; set;
        }
        public string? DeviceToken
        {
            get; set;
        }
        public async Task SetSession(string key, object value)
        {
            await _protectedSessionStore.SetItemAsync(key, value);
        }
        public bool ToppedUp
        {
            get; set;
        }
        public int? Status
        {
            get; set;
        }
        public int? Verified
        {
            get; set;
        }
        public bool TourWalletShown
        {
            get; set;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public decimal? HoldCredits
        {
            get; set;
        }
        public bool isApple
        {
            get; set;
        }
        public int CategoryId
        {
            get; set;
        } = (int)PlayerCategory.Starter;

        public DateTime? lastOTP { get; set; }
        public async Task updateSessionAsync()
        {
            CurrenUserModel user = new CurrenUserModel();
            user.Id = Id;
            user.Username = Username;
            user.FullName = FullName;
            user.Token = Token;
            user.EmailAddress = EmailAddress;
            user.EmailValidated = EmailValidated;
            user.MobileNumber = MobileNumber;
            user.MobileNumberValidated = MobileNumberValidated;
            user.Credits = Credits;
            user.CreditsDisp = String.Format("{0:0,0.00}", Credits);
            user.RoleType = RoleType;
            user.RemainingViewTime = RemainingViewTime;
            user.ProfileImage = ProfileImage;
            user.PopupTimer = PopupTimer;
            user.IdleTimer = IdleTimer;
            user.DeviceToken = DeviceToken;
            user.toppedUp = ToppedUp;
            user.Status = Status;
            user.Verified = Verified;
            user.TourWalletShown = TourWalletShown;
            user.HoldCredits = HoldCredits;
            user.isApple = isApple;
            user.CategoryId = CategoryId;
            user.lastOTP = lastOTP;

            string json = Base64Encode(JsonConvert.SerializeObject(user));
            await _protectedSessionStore.SetItemAsync("gpa", json);

        }
        public async Task clearSessionAsync()
        {
            await _protectedSessionStore.RemoveItemAsync("gpa");
        }

        public async Task restoreSessionAsync()
        {
            var user = await getSessionStringValueAsync("gpa");

            if (user == null) return;

            Id = user.Id;

            Token = user.Token;

            RoleType = user.RoleType;

            MobileNumber = user.MobileNumber;

            EmailAddress = user.EmailAddress;

            FullName = user.FullName;

            Credits = user.Credits;
            ProfileImage = "/img/unverified-icon.png";
            CreditsDisp = user.CreditsDisp;

            Username = user.Username;

            DeviceToken = user.DeviceToken;

            Status = user.Status;

            Verified = user.Verified;

            ToppedUp = user.ToppedUp;

            TourWalletShown = user.TourWalletShown;

            MobileNumberValidated = user.MobileNumberValidated;

            HoldCredits = user.HoldCredits;

            isApple = user.isApple;

            CategoryId = user.CategoryId;

            lastOTP = user.lastOTP;
            Update();
        }

        private async Task<CurrentUser> getSessionStringValueAsync(string key)
        {
            try
            {

                var result = await _protectedSessionStore.GetItemAsync<String>(key);
                if (result != null)
                {
                    result = Base64Decode(result);
                    return JsonConvert.DeserializeObject<CurrentUser>(result);
                }
                   
                else return null;
            }
            catch (Exception)
            {

                return null;
            }

        }

        private async Task<int> getSessionIntValueAsync(string key)
        {
            try
            {
                var result = await _protectedSessionStore.GetItemAsync<int>(key);
                return result;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        private async Task<decimal> getSessionDecimalValueAsync(string key)
        {
            try
            {
                var result = await _protectedSessionStore.GetItemAsync<decimal>(key);
                return result;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        event Action OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void Update()
        {
            NotifyStateChanged();
        }

        public HubConnection? HubConnection { get; set; }
    }

}
