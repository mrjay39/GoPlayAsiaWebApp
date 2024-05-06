namespace GoplayasiaBlazor.Models;

public class CurrenUserModel
{
    public long Id
    {
        get; set;
    }
    public decimal? Credits
    {
        get; set;
    }
    public decimal? HoldCredits
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
    public bool? toppedUp
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
    public bool isApple
    {
        get; set;
    }
    public int CategoryId
    {
        get; set;
    }
    public DateTime? lastOTP { get; set; }
}
