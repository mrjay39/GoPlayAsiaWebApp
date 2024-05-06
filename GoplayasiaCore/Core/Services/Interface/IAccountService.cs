using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IAccountService
{
    IModalReference _globalPopup { get; set; }
    IModalService _modal { get; set; }
    Task ClosePopup();
    Task<bool> ForceLogoutUser(long userId, string userName);
    Task<AccountResultDTO> GetUser();
    Task<AccountResultDTO> Login(UserDTO paramsModel);
    Task<bool> Logout();
    Task<bool> UsernameTakenChecker(string username);
    Task<bool> MobileNumberChecker(string mobileNumber);
    Task<bool> EmailTakenChecker(string emailAddress);
    Task<bool> ReferralKeyChecker(string referralKey);
    Task<RegisterResultDTO> Register(SignupDTO paramsModel);
    Task<RegisterResultDTO> RegisterNew(SignupDTO paramsModel);
    Task<RegisterResultDTO> VerifyUser(SignupDTO paramsModel);
    Task<RegisterResultDTO> CreateMasterAgent(SignupDTO paramsModel);
    
    Task<decimal> GetUserCurrency(long userId);
    Task<SMSResultDTO> RequestOTP(string mobileNumber);
    Task<SMSResultDTO> RequestForLoginOTP(string mobileNumber);
    Task<bool> VerifyOTP(string refrenceCode, string otp);
    Task<bool> VerifyMobileNumber();
    Task<string> RequestCode(string emailAddress, long? userId);
    Task<bool> VerifyCode(string emailAddress, string code, long? userId);
    Task<BaseResultDTO> UpdateAccountInformation(UserDTO paramsModel);
    Task<BaseResultDTO> UpdateProfileInformation(UserDTO paramsModel);
    Task<BaseResultModel> UpdatePassword(UserDTO paramsModel);
    Task<UserModel> GetParentUser();
    Task<BaseResultModel> VerifyPassword(long userId, string password);
    Task<bool> ChangePassword(string emailAddress, string password);
    Task<bool> DeactivateUser(long userId, string remarks);
    Task<ForgotPasswordResultDTO> ForgotPasswordInitialization(ForgotPasswordParamsDTO paramsModel);
    Task<ForgotPasswordResultDTO> VerifyForgotPasswordCode(ForgotPasswordParamsDTO paramsModel);
    Task<ForgotPasswordResultDTO> ResetPassword(ForgotPasswordParamsDTO paramsModel);

    Task<bool> PersonTaken(string firstname, string lastname, string birthday, int roleType);
    Task<RefKeyResultDTO> GetReferral( string mobilenumber, string firstname, string lastname);
    Task<AccountResultDTO> LoginWithOTP(string referenceCode, string otp, string mobileNumber, string DeviceToken);
}