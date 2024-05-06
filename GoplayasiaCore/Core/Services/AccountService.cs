using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;
using System;

namespace GoplayasiaBlazor.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        public IModalService _modal { get; set; } = default!;

        private readonly IToastService _toastService;

        public IModalReference _globalPopup { get; set; }
        public AccountService(IHTTPClientHelper httpClientHelper, ICurrentUser currentUser, IModalService Modal, IToastService toastService)
        {
            _httpClientHelper = httpClientHelper;
            _currentUser = currentUser;
            _modal = Modal;
            _toastService = toastService;
        }
        public async Task<AccountResultDTO> Login(UserDTO paramsModel)
        {
            try
            {
                //    _globalPopup = _modal.Show<PopupLoading>("");
                //_globalPopup = _modal.Show<PopupLoading>("");
                var result = await _httpClientHelper.PostAsync<AccountResultDTO>("Account/Authenticate", string.Empty, paramsModel);
                //_globalPopup.Close();
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UsernameTakenChecker(string username)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/UsernameTakenChecker/{username}", string.Empty);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<bool> Logout()
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/Logout/{_currentUser.Id}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ForceLogoutUser(long userId, string userName)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>("Account/ForceLogout?userId=" + userId + "&userName=" + userName + "", string.Empty);
            }
            catch
            {
                return false;
            }
        }
        public async Task<AccountResultDTO> GetUser()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<AccountResultDTO>($"Account/User/{_currentUser.Id}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> MobileNumberChecker(string mobileNumber)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/MobileNumberChecker/{mobileNumber}", string.Empty);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<bool> ReferralKeyChecker(string referralKey)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/ReferralKeyChecker/{referralKey}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EmailTakenChecker(string emailAddress)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/EmailTakenChecker/{emailAddress}", string.Empty);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<RegisterResultDTO> Register(SignupDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<RegisterResultDTO>("Account/Register", string.Empty, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterResultDTO> RegisterNew(SignupDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<RegisterResultDTO>("Account/RegisterNew", string.Empty, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<RegisterResultDTO> VerifyUser(SignupDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<RegisterResultDTO>("Account/VerifyUser", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterResultDTO> CreateMasterAgent(SignupDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<RegisterResultDTO>("Account/CreateMasterAgent", string.Empty, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task ClosePopup()
        {
            if (_globalPopup != null)
            {
                _globalPopup.Close();
            }

        }
        public async Task<decimal> GetUserCurrency(long userId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<CurrencyResultDTO>($"Account/UserCurrency/{userId}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return result.Credits;
            }
            catch
            {
                return 0;
            }

        }
        public async Task<SMSResultDTO> RequestOTP(string mobileNumber)
        {
            try
            {
                if (_currentUser.lastOTP != null)
                {
                    if ((DateTime.Now - _currentUser.lastOTP).Value.TotalSeconds < 60)
                    {
                        _toastService.ShowError($"Please wait {(int)Math.Round(60 - (DateTime.Now - _currentUser.lastOTP).Value.TotalSeconds)} seconds before requesting a new OTP");
                        return null;
                    }
                }

                var res = await _httpClientHelper.GetAsync<SMSResultDTO>($"Account/SendOTP/{mobileNumber}", string.Empty);
                _currentUser.lastOTP = DateTime.Now;
                await _currentUser.updateSessionAsync();

                return res;
            }
            catch
            {
                return null;
            }
        }
        public async Task<SMSResultDTO> RequestForLoginOTP(string mobileNumber)
        {
            try
            {
                if (_currentUser.lastOTP != null)
                {
                    if ((DateTime.Now - _currentUser.lastOTP).Value.TotalSeconds < 60)
                    {
                        _toastService.ShowError($"Please wait {(int)Math.Round(60 - (DateTime.Now - _currentUser.lastOTP).Value.TotalSeconds)} seconds before requesting a new OTP");
                        return null;
                    }
                }

                var res = await _httpClientHelper.GetAsync<SMSResultDTO>($"Account/SendOTPForLogin/{mobileNumber}", string.Empty);

                _currentUser.lastOTP = DateTime.Now;
                await _currentUser.updateSessionAsync();

                return res;

            }
            catch
            {
                return null;
            }
        }
        
        public async Task<bool> VerifyOTP(string refrenceCode, string otp)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/VerifyOTP/{refrenceCode}/{otp}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerifyMobileNumber()
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/VerifyMobileNumber/{_currentUser.Id}", _currentUser.Token);
            }
            catch
            {
                return false;
            }
        }
        public async Task<string> RequestCode(string emailAddress, long? userId)
        {
            try
            {
                if (userId.HasValue && userId > 0)
                    return await _httpClientHelper.GetAsync<string>($"Account/SendCode/{emailAddress}?userId={userId.Value}", string.Empty);
                else
                    return await _httpClientHelper.GetAsync<string>($"Account/SendCode/{emailAddress}", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<bool> VerifyCode(string emailAddress, string code, long? userId)
        {
            try
            {
                if (userId.HasValue && userId > 0)
                    return await _httpClientHelper.GetAsync<bool>($"Account/VerifyCode/{emailAddress}/{code}?userId={userId.Value}", string.Empty);
                else
                    return await _httpClientHelper.GetAsync<bool>($"Account/VerifyCode/{emailAddress}/{code}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<BaseResultDTO> UpdateAccountInformation(UserDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<BaseResultDTO>("Account/UpdateAccountInformation", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BaseResultDTO> UpdateProfileInformation(UserDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<BaseResultDTO>("Account/UpdateProfileInformation", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BaseResultModel> UpdatePassword(UserDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<BaseResultModel>("Account/UpdatePassword", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserModel> GetParentUser()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<UserModel>($"Account/ParentUser/{_currentUser.Id}", _currentUser.Token);
                if (result == null || result.Id < 1)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BaseResultModel> VerifyPassword(long userId, string password)
        {
            try
            {
                VerifyPasswordDTO passitem = new VerifyPasswordDTO();
                passitem.UserId = userId;
                passitem.Password = password;

                var result = await _httpClientHelper.PostAsync<BaseResultModel>($"Account/VerifyPass", _currentUser.Token, passitem);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangePassword(string emailAddress, string password)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"Account/ChangePassword/{emailAddress}/{password}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeactivateUser(long userId, string remarks)
        {
            try
            {
                return await _httpClientHelper.DeleteAsync<bool>($"Account/Deactivate/{userId}/{remarks}", string.Empty);
            }
            catch
            {
                return false;
            }
        }

        public async Task<ForgotPasswordResultDTO> ForgotPasswordInitialization(ForgotPasswordParamsDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<ForgotPasswordResultDTO>("Account/ForgotPassword", string.Empty, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ForgotPasswordResultDTO> VerifyForgotPasswordCode(ForgotPasswordParamsDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<ForgotPasswordResultDTO>("Account/VerifyForgotPassword", string.Empty, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ForgotPasswordResultDTO> ResetPassword(ForgotPasswordParamsDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<ForgotPasswordResultDTO>("Account/ResetPassword", paramsModel.JWTToken, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> PersonTaken(string firstname, string lastname, string birthday, int roletype)
        {
            try
            {
                return await _httpClientHelper.GetAsync<bool>($"/Account/PersonTaken/{firstname}/{lastname}/{birthday}/{roletype}", string.Empty);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<RefKeyResultDTO> GetReferral( string mobilenumber, string firstname = "e", string lastname = "e")
        {
            try
            {
                //var result = await _httpClientHelper.GetAsync<RefKeyResultDTO>($"Account/GetReferralKey/{firstname}/{lastname}/{mobilenumber}", string.Empty);
                var result = await _httpClientHelper.GetAsync<RefKeyResultDTO>($"Account/GetReferralKey/{mobilenumber}?Firstname={firstname}&Lastname={lastname}", string.Empty);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch(Exception ex) { }
            {
                return null;
            }
        }
        public async Task<AccountResultDTO> LoginWithOTP(string referenceCode, string otp, string mobileNumber, string DeviceToken)
        {
            try
            {

                var result = await _httpClientHelper.GetAsync<AccountResultDTO>($"Account/LoginWithOTP/{referenceCode}/{otp}/{mobileNumber}/{DeviceToken}", string.Empty);
                if (result == null)
                    throw new Exception();
                
        
                return result;
            }
            catch (Exception ex) { }
            {
                return null;
            }
        }
    }
}
