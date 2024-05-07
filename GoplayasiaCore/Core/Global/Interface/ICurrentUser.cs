using Microsoft.AspNetCore.SignalR.Client;

namespace GoplayasiaBlazor.Core.Global.Interface
{
    public interface ICurrentUser
    {
        long Id { get; set; }
        decimal? Credits { get; set; }
        string? CreditsDisp { get; set; }
        string? EmailAddress { get; set; }
        bool EmailValidated { get; set; }
        string? FullName { get; set; }
        string? Username { get; set; }
        int IdleTimer { get; set; }
        string? MobileNumber { get; set; }
        bool MobileNumberValidated { get; set; }
        int PopupTimer { get; set; }
        string? ProfileImage { get; set; }
        TimeSpan RemainingViewTime { get; set; }
        int RoleType { get; set; }
        string? Token { get; set; }
        string DeviceToken { get; set; }
        string? NotificationCount { get; set; }
        HubConnection? HubConnection { get; set; }
        bool ToppedUp { get; set; }
        bool TourWalletShown { get; set; }
        int? Status { get; set; }
        int? Verified { get; set; } //added by: cjpvaquilar
        decimal? HoldCredits { get; set; }
        bool isApple { get; set; }
        int CategoryId { get; set; } //added by: cjpvaquilar
        DateTime? lastOTP { get; set; }
        Task restoreSessionAsync();
        void Update();
        Task updateSessionAsync();
        Task clearSessionAsync();
        bool isLoggedIn { get; set; }
    }
}