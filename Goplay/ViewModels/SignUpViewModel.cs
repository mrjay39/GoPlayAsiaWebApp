using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.ViewModels;

public class SignUpViewModel
{
    private UserModel _userInfo;

    public UserModel UserInfo
    {
        get => _userInfo;
        set
        {
            _userInfo = value;
        }
    }
    private SignupDTO _signupDTO;
    public SignupDTO SignupDTO
    {
        get => _signupDTO;
        set
        {
            _signupDTO = value;
        }
    }
    IMapper _mapper { get; set; }
    IJSRuntime _jsruntime { get; set; }
    //IModalService _popupModal;
    [CascadingParameter] public IModalService _popupModal { get; set; }
    public NavigationManager _navigationManager;

    public bool hasProfimg { get; set; } = false;
    public bool hasGovtimg { get; set; } = false;

    private byte[] _profileImageBytes;
    private byte[] _governmentImageBytes;

    //private string ProfileImageUriB64;
    //private string GovernmentImageB64;
    public byte[] ProfileImageBytes
    {
        get => _profileImageBytes;
        set
        {
            _profileImageBytes = value;
        }
    }
    public byte[] GovernmentImageBytes
    {
        get => _governmentImageBytes;
        set
        {
            _governmentImageBytes = value;
        }
    }
    public bool hasRef { get; set; }
    IToastService _toastService;
    IAccountService _accountService;
    IConstantService _constantService;


    public string errFname { get; set; } = "Ok";
    public string errLname { get; set; } = "Ok";
    public string errBday { get; set; } = "Ok";
    public string errUsername { get; set; } = "Ok";
    public string errPass { get; set; } = "Ok";
    ICurrentUser _iCurrentUser { get; set; }


    #region Survey
    public List<SurveyQuestionModel> SurveyQuestion { get; set; }
    public SurveyQuestionModel? question1 = new SurveyQuestionModel();
    public string strQuestion1 { get; set; }
    public SurveyAnswerModel? selectedAnswer1 { get; set; } = new SurveyAnswerModel();
    public IEnumerable<SurveyAnswerModel> SurveyAnswer1 { get; set; }

    public SurveyQuestionModel? question2;
    public string strQuestion2 { get; set; }
    public SurveyAnswerModel? selectedAnswer2 { get; set; }
    public IEnumerable<SurveyAnswerModel> SurveyAnswer2 { get; set; }
    #endregion



    AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    public SignUpViewModel(IToastService toastService, IAccountService accountService, IMapper mapper, IJSRuntime JSRuntime, NavigationManager navigationManager, ICurrentUser currentUser, AuthenticationStateProvider iauthenticationStateProvider, IConstantService constantService)
    {
        _toastService = toastService;
        _accountService = accountService;
        _mapper = mapper;
        _jsruntime = JSRuntime;
        _navigationManager = navigationManager;
        _iCurrentUser = currentUser;
        AuthenticationStateProvider = iauthenticationStateProvider;
        _constantService = constantService;
    }


    #region PLAYER
    #region STEP 1 VALIDATIONS
    public async Task<string> ValidateMobile(string mobileno)
    {
        var errorMsg = "Ok";

        if (string.IsNullOrEmpty(mobileno) && errorMsg == "Ok")
        {
            errorMsg = "Mobile Number is required";
            return errorMsg;
        }

        var isValid = Regex.IsMatch(mobileno, "^[0-9]+$");
        if (!isValid && errorMsg == "Ok")
        {
            errorMsg = "Mobile number input only allows numeric input";
            return errorMsg;
        }

        if (mobileno.Length != 11 && errorMsg == "Ok")
        {
            errorMsg = "Mobile Number must be in 11 characters";
            return errorMsg;
        }

        try
        {
            if (!await _accountService.MobileNumberChecker(mobileno) && errorMsg == "Ok")
            {
                errorMsg = "Mobile Number already used";
                return errorMsg;
            }
        }
        catch (Exception)
        {
            errorMsg = "Connection to server failed. Make sure you are connected to internet";
            return errorMsg;
        }


        return errorMsg;
    }
    public async Task<RefKeyResultDTO> GetReferralKey(string MobileNumber)
    {
        if (!string.IsNullOrEmpty(MobileNumber))
        {
            var result = await _accountService.GetReferral(MobileNumber, SignupDTO.FirstName, SignupDTO.LastName);
            if (!result.Success)
            {
                return null;
            }

            return result;
        }

        return null;
    }
    public async Task<string> ValidateRefKey()
    {
        var errorMsg = "Ok";

        if (!string.IsNullOrEmpty(SignupDTO.ReferralKey))
        {
            var referralKeyChecker = await _accountService.ReferralKeyChecker(SignupDTO.ReferralKey);
            if (!referralKeyChecker)
            {
                errorMsg = "Referral Key is invalid";
                return errorMsg;
            }
        }

        return errorMsg;
    }
    public async Task<bool> Step1Validation()
    {
        var result = "Ok";

        result = await ValidateMobile(SignupDTO.MobileNumber);
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        result = await ValidateRefKey();
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        return true;
    }
    #endregion

    #region STEP 2 VALIDATIONS
    public async Task<string> ValidateUsername(string username)
    {
        var errorMsg = "Ok";
        if (string.IsNullOrEmpty(username) || username.Length < 8 && errorMsg == "Ok")
        {
            errorMsg = "Username must be minimum of 8 characters";
            return errorMsg;
        }

        try
        {
            if (!await _accountService.UsernameTakenChecker(username) && errorMsg == "Ok")
            {
                errorMsg = "Username already taken";
                return errorMsg;
            }
        }
        catch (Exception)
        {
            errorMsg = "Connection to server failed. Make sure you are connected to internet";
            return errorMsg;
        }



        if (username.Contains(" ") && errorMsg == "Ok")
        {
            errorMsg = "Username cannot contain spaces";
            return errorMsg;
        }
        return errorMsg;
    }
    public async Task<string> ValidatePassword(string password, string confirmpass)
    {
        var errorMsg = "Ok";

        if (string.IsNullOrEmpty(password) && errorMsg == "Ok")
        {
            errorMsg = "Password cannot be empty";
        }

        int valpass = await ValidatorHelper.ValidatePassword(password, confirmpass);
        if (valpass > 0 && errorMsg == "Ok")
        {
            switch (valpass)
            {
                case 1:
                    errorMsg = "Password and confirm password did not match";
                    break;
                case 2:
                    errorMsg = "Password cannot be empty and must be at least 8 characters";
                    break;
                case 3:
                    errorMsg = "Password cannot contain spaces";
                    break;
                default:
                    break;
            }
        }
        return errorMsg;
    }
    #endregion

    public async Task PlayerRegistration()
    {
        if (hasRef)
        {
            if (string.IsNullOrEmpty(SignupDTO.ReferralKey))
            {
                _toastService.ShowError("Referral cannot be empty if option is checked");
                return;
            }
        }
        var DeviceToken = await _jsruntime.InvokeAsync<string>(identifier: "identifyBrowser");
        var popupRes = _popupModal.Show<PopupLoading>("");

        UserInfo = new UserModel()
        {
            RoleType = SignupDTO.RoleType,
            ReferralKey = SignupDTO.ReferralKey,
            DeviceToken = DeviceToken,
            Username = SignupDTO.Username,
            Password = SignupDTO.Password,
            FirstName = SignupDTO.FirstName,
            MiddleName = SignupDTO.MiddleName,
            LastName = SignupDTO.LastName,
            //DateOfBirth = SignupDTO.DateOfBirth,
            MobileNumber = SignupDTO.MobileNumber,
            MobileNumberValidated = true
        };
        RegisterResultDTO registerUser;
        registerUser = await _accountService.RegisterNew(_mapper.Map<SignupDTO>(UserInfo));

        popupRes.Close();
        if (registerUser == null)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }
        if (!registerUser.Success)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }

        _toastService.ShowSuccess("Registration Successful");
        await Login(SignupDTO.Username, SignupDTO.Password, DeviceToken);
    }
    private async Task Login(string username, string password, string deviceToken)
    {
        var popupRes = _popupModal.Show<PopupLoading>("");
        UserDTO userParams = new UserDTO() { Username = username, Password = password, DeviceToken = deviceToken };
        var response = await _accountService.Login(userParams);

        _iCurrentUser.Id = response.User.Id;
        _iCurrentUser.Username = response.User.Username;
        _iCurrentUser.FullName = response.User.FullName;
        _iCurrentUser.Token = response.User.JWTToken;
        _iCurrentUser.EmailAddress = response.User.EmailAddress;
        _iCurrentUser.EmailValidated = response.User.EmailValidated;
        _iCurrentUser.MobileNumber = response.User.MobileNumber;
        _iCurrentUser.MobileNumberValidated = response.User.MobileNumberValidated;
        _iCurrentUser.Credits = response.User.Credits;
        _iCurrentUser.CreditsDisp = string.Format("{0:0,0.00}", response.User.Credits);
        _iCurrentUser.RoleType = response.User.RoleType;
        _iCurrentUser.RemainingViewTime = response.User.RemainingViewTime;
        _iCurrentUser.ProfileImage = response.User.ProfileImageFullPath;
        _iCurrentUser.PopupTimer = response.User.PopupTimer;
        _iCurrentUser.IdleTimer = response.User.IdleTimer;
        _iCurrentUser.DeviceToken = response.User.DeviceToken;
        _iCurrentUser.Status = response.User.Status;
        _iCurrentUser.ToppedUp = response.User.ToppedUp;
        _iCurrentUser.Verified = response.User.Verified;
        await _iCurrentUser.updateSessionAsync();

        popupRes.Close();

        await ((CustomAuthStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated();
        _navigationManager.NavigateTo("/landing", true, true);

    }
    #endregion

    #region AGENT/ MASTER AGENT

    #region STEP 1 VALIDATIONS
    //1 VALIDATE USERNAME
    //2 VALIDATE PASSWORD
    public async Task<string> ValidateEmail(string email)
    {
        var errorMsg = "Ok";

        if (string.IsNullOrEmpty(email) || email.Contains(" ") && errorMsg == "Ok")
        {
            errorMsg = "Email Address cannot be empty, cannot contain spaces and must be valid";
        }

        if (string.IsNullOrEmpty(email) && errorMsg == "Ok")
        {
            errorMsg = "Email Address is required";
        }

        if (!await ValidatorHelper.ValidateEmail(email) && errorMsg == "Ok")
        {
            errorMsg = "Email Address is not valid";
        }

        try
        {
            if (!await _accountService.EmailTakenChecker(email) && errorMsg == "Ok")
            {
                errorMsg = "Email Address is already taken";
            }
        }
        catch (Exception)
        {
            errorMsg = "Connection to server failed. Make sure you are connected to internet";
            return errorMsg;
        }

        return errorMsg;
    }
    //4 VALIDATE MOBILE NUMBER
    public async Task<bool> ValidateStep1()
    {
        var result = "Ok";

        result = await ValidateUsername(SignupDTO.Username);
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        result = await ValidatePassword(SignupDTO.Password, SignupDTO.ConfirmPassword);
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        result = await ValidateEmail(SignupDTO.EmailAddress);
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        result = await ValidateMobile(SignupDTO.MobileNumber);
        if (result != "Ok")
        {
            _toastService.ShowError(result);
            return false;
        }

        return true;
    }
    #endregion

    #region STEP 2 VALIDATIONS
    public async Task<bool> ValidateStep2()
    {
        if (string.IsNullOrEmpty(SignupDTO.FirstName) || string.IsNullOrEmpty(SignupDTO.LastName))
        {
            _toastService.ShowError("First Name and Last Name cannot be empty");
            return false;
        }

        if (string.IsNullOrEmpty(SignupDTO.Month) || SignupDTO.Month.Length != 2 || !int.TryParse(SignupDTO.Month, out int monthResult))
        {
            _toastService.ShowError("Birthdate month cannot be empty and must be valid (e.g. 07)");
            return false;
        }
        if (string.IsNullOrEmpty(SignupDTO.Day) || SignupDTO.Day.Length != 2 || !int.TryParse(SignupDTO.Day, out int dayResult))
        {
            _toastService.ShowError("Birthdate day cannot be empty and must be valid (e.g. 02)");
            return false;
        }
        if (string.IsNullOrEmpty(SignupDTO.Year) || SignupDTO.Year.Length != 4 || !int.TryParse(SignupDTO.Year, out int yearResult))
        {
            _toastService.ShowError("Birthdate year cannot be empty and must be valid (e.g. 2000)");
            return false;
        }
        if (!await ValidatorHelper.ValidateDate(monthResult, dayResult, yearResult))
        {
            _toastService.ShowError("Birthdate is not a valid date");
            return false;
        }
        SignupDTO.DateOfBirth = new DateTime(yearResult, monthResult, dayResult);
        if (!await ValidatorHelper.ValidateAge(21, SignupDTO.DateOfBirth.Value))
        {
            _toastService.ShowError("Age is not acceptable");
            return false;
        }
        try
        {
            bool res = await _accountService.PersonTaken(SignupDTO.FirstName, SignupDTO.LastName, SignupDTO.DateOfBirth.Value.ToString("yyy-MM-dd"), SignupDTO.RoleType);
            if (res == false)
            {
                _toastService.ShowError("Name already registered...");
                return false;
            }
        }
        catch (Exception)
        {
            _toastService.ShowError("Connection to server failed. Make sure you are connected to internet");
            return false;
        }

        return true;
    }
    #endregion

    #region STEP 3 VALIDATIONS
    public async Task<bool> ValidateStep3()
    {
        try
        {
            var result = "Ok";
            result = await ValidateEmail(SignupDTO.EmailAddress);
            if (result != "Ok")
            {
                _toastService.ShowError(result);
                return false;
            }
            if (string.IsNullOrEmpty(SignupDTO.PlaceOfBirth))
            {
                _toastService.ShowError("Place of Birth cannot be empty");
                return false;
            }
            if (SignupDTO.Gender == null)
            {
                _toastService.ShowError("Gender selection is needed");
                return false;
            }
            if (SignupDTO.NationalityId == null)
            {
                _toastService.ShowError("Nationality selection is needed");
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region STEP 4 VALIDATIONS
    public async Task<bool> ValidateStep4()
    {
        if (SignupDTO.CurrentCityId < 1 || SignupDTO.CurrentCityId is null || string.IsNullOrEmpty(SignupDTO.CurrentStreet))
        {
            _toastService.ShowError("Current Address fields needs to be complete");
            return false;
        }
        if (SignupDTO.AddressAreSame == false && (SignupDTO.PermanentCityId == null || SignupDTO.PermanentCityId < 1 || string.IsNullOrEmpty(SignupDTO.PermanentStreet)))
        {
            _toastService.ShowError("Permanent Address fields needs to be complete if addresses are not the same");
            return false;
        }
        return true;
    }
    #endregion

    #region STEP 5 VALIDATIONS 
    public async Task<bool> ValidateStep5()
    {
        if (!hasProfimg || !hasGovtimg)
        {
            _toastService.ShowError("Profile Image and a valid Government Issued ID Image is needed");
            return false;
        }
        if (string.IsNullOrEmpty(SignupDTO.IdentificationNumber))
        {
            _toastService.ShowError("Government ID Idenitification Number is needed");
            return false;
        }
        if (!await ValidatorHelper.ValidateIssueId(SignupDTO.IdentificationNumber))
        {
            _toastService.ShowError("Identification Number only allows letters, dashes and numeric inputs");
            return false;
        }
        if (SignupDTO.NatureOfWorkId is null)
        {
            _toastService.ShowError("Nature of work is needed");
            return false;
        }
        if (SignupDTO.SourceOfIncomeId is null)
        {
            _toastService.ShowError("Source of income is needed");
            return false;
        }

        return true;
    }
    #endregion

    #region STEP 6 VALIDATIONS
    public async Task<bool> ValidateReferralKey()
    {
        if (!string.IsNullOrEmpty(SignupDTO.ReferralKey))
        {
            var referralKeyChecker = await _accountService.ReferralKeyChecker(SignupDTO.ReferralKey);
            if (!referralKeyChecker)
            {
                _toastService.ShowError("Referral Key is invalid");
                return false;
            }
        }

        return true;
    }
    #endregion

    public async Task SubmitDetails()
    {
        if (hasRef)
        {
            if (string.IsNullOrEmpty(SignupDTO.ReferralKey))
            {
                _toastService.ShowError("Referral cannot be empty if option is checked");
                return;
            }
        }
        if (ProfileImageBytes == null || GovernmentImageBytes == null)
        {
            _toastService.ShowError("Profile Image and a valid Government Issued ID Image is needed");
            return;
        }
        //var roleType = (int)RoleTypes.Player;
        var DeviceToken = await _jsruntime.InvokeAsync<string>(identifier: "identifyBrowser");
        var popupRes = _popupModal.Show<PopupLoading>("");
        UserInfo = new UserModel()
        {
            RoleType = SignupDTO.RoleType,
            ReferralKey = SignupDTO.ReferralKey,
            DeviceToken = DeviceToken,
            IdentificationNumber = SignupDTO.IdentificationNumber,
            Username = SignupDTO.Username,
            Password = SignupDTO.Password,
            FirstName = SignupDTO.FirstName,
            MiddleName = SignupDTO.MiddleName,
            LastName = SignupDTO.LastName,
            DateOfBirth = SignupDTO.DateOfBirth.Value,
            PlaceOfBirth = SignupDTO.PlaceOfBirth,
            Gender = SignupDTO.Gender,
            SourceOfIncomeId = SignupDTO.SourceOfIncomeId,
            OtherSourceOfIncome = SignupDTO.OtherSourceOfIncome,
            NatureOfWorkId = SignupDTO.NatureOfWorkId,
            OtherNatureOfWork = SignupDTO.OtherNatureOfWork,
            NationalityId = SignupDTO.NationalityId,
            EmailAddress = SignupDTO.EmailAddress,
            MobileNumber = SignupDTO.MobileNumber,
            MobileNumberValidated = true,
            CurrentCityId = SignupDTO.CurrentCityId,
            CurrentStreet = SignupDTO.CurrentStreet,
            AddressAreSame = SignupDTO.AddressAreSame,
            PermanentCityId = SignupDTO.PermanentCityId,
            PermanentStreet = SignupDTO.PermanentStreet,
            ProfileImage = new UploadModel
            {
                FileBytes = ProfileImageBytes,
                OriginalFileName = "Profile" + SignupDTO.FirstName.Replace(" ", string.Empty) + DateTime.UtcNow.ToString("MMddyyyy") + ".jpeg"
            },
            GovernmentImage = new UploadModel
            {
                FileBytes = GovernmentImageBytes,
                OriginalFileName = "Government" + SignupDTO.FirstName.Replace(" ", string.Empty) + DateTime.UtcNow.ToString("MMddyyyy") + ".jpeg"
            },
        };
        RegisterResultDTO registerUser;
        if (SignupDTO.RoleType == (int)RoleTypes.MasterAgent)
        {
            registerUser = await _accountService.CreateMasterAgent(_mapper.Map<SignupDTO>(UserInfo));
        }
        else
        {
            registerUser = await _accountService.Register(_mapper.Map<SignupDTO>(UserInfo));
        }

        popupRes.Close();
        if (registerUser == null)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }
        if (!registerUser.Success)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }

        _toastService.ShowSuccess("Registration Successful");
        //_navigationManager.NavigateTo("/");
        if (SignupDTO.RoleType == (int)RoleTypes.Player)
        {
            await Login(SignupDTO.Username, SignupDTO.Password, DeviceToken);
        }
    }

    #endregion

    public async Task RegisterNow()
    {
        if (hasRef)
        {
            if (string.IsNullOrEmpty(SignupDTO.ReferralKey))
            {
                _toastService.ShowError("Referral cannot be empty if option is checked");
                return;
            }
        }
        //var roleType = (int)RoleTypes.Player;
        var DeviceToken = await _jsruntime.InvokeAsync<string>(identifier: "identifyBrowser");
        var popupRes = _popupModal.Show<PopupLoading>("");

        UserInfo = new UserModel()
        {
            RoleType = SignupDTO.RoleType,
            ReferralKey = SignupDTO.ReferralKey,
            DeviceToken = DeviceToken,
            Username = SignupDTO.Username,
            Password = SignupDTO.Password,
            FirstName = SignupDTO.FirstName,
            MiddleName = SignupDTO.MiddleName,
            LastName = SignupDTO.LastName,
            DateOfBirth = SignupDTO.DateOfBirth.Value,
            MobileNumber = SignupDTO.MobileNumber,
            MobileNumberValidated = true
        };
        RegisterResultDTO registerUser;
        if (SignupDTO.RoleType == (int)RoleTypes.MasterAgent)
        {
            registerUser = await _accountService.CreateMasterAgent(_mapper.Map<SignupDTO>(UserInfo));
        }
        else
        {
            //registerUser = await _accountService.RegisterNew(_mapper.Map<SignupDTO>(UserInfo));
            registerUser = null;
        }

        popupRes.Close();
        if (registerUser == null)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }
        if (!registerUser.Success)
        {
            _toastService.ShowError("An error occured while attempting to register. Please try again later");
            return;
        }

        _toastService.ShowSuccess("Registration Successful");
        // _navigationManager.NavigateTo("/");
        await Login(SignupDTO.Username, SignupDTO.Password, DeviceToken);
    }

    #region New Events
    public async Task<bool> ValidateRegistration()
    {
        errFname = "Ok";
        errLname = "Ok";
        errBday = "Ok";
        if (string.IsNullOrEmpty(SignupDTO.FirstName))
        {
            _toastService.ShowError("First Name cannot be empty");
            errFname = "First Name cannot be empty";
            return false;
        }

        if (string.IsNullOrEmpty(SignupDTO.LastName))
        {
            _toastService.ShowError("Last Name cannot be empty");
            errLname = "Last Name cannot be empty";
            return false;
        }

        if (string.IsNullOrEmpty(SignupDTO.Month) || SignupDTO.Month.Length != 2 || !int.TryParse(SignupDTO.Month, out int monthResult))
        {
            _toastService.ShowError("Birthdate month cannot be empty and must be valid (e.g. 07)");
            errBday = "Birthdate month cannot be empty and must be valid (e.g. 07)";
            return false;
        }
        if (string.IsNullOrEmpty(SignupDTO.Day) || SignupDTO.Day.Length != 2 || !int.TryParse(SignupDTO.Day, out int dayResult))
        {
            _toastService.ShowError("Birthdate day cannot be empty and must be valid (e.g. 02)");
            errBday = "Birthdate day cannot be empty and must be valid (e.g. 02)";
            return false;
        }
        if (string.IsNullOrEmpty(SignupDTO.Year) || SignupDTO.Year.Length != 4 || !int.TryParse(SignupDTO.Year, out int yearResult))
        {
            _toastService.ShowError("Birthdate year cannot be empty and must be valid (e.g. 2000)");
            errBday = "Birthdate year cannot be empty and must be valid (e.g. 2000)";
            return false;
        }
        if (!await ValidatorHelper.ValidateDate(monthResult, dayResult, yearResult))
        {
            _toastService.ShowError("Birthdate is not a valid date");
            errBday = "Birthdate is not a valid date";
            return false;
        }
        SignupDTO.DateOfBirth = new DateTime(yearResult, monthResult, dayResult);
        if (!await ValidatorHelper.ValidateAge(21, SignupDTO.DateOfBirth.Value))
        {
            _toastService.ShowError("Age is not acceptable");
            errBday = "Age is not acceptable";
            return false;
        }

        var ret = await ValidateUsername(SignupDTO.Username);
        if (ret != "Ok")
        {
            _toastService.ShowError(ret);
            return false;
        }

        ret = await ValidatePassword(SignupDTO.Password, SignupDTO.ConfirmPassword);
        if (ret != "Ok")
        {
            _toastService.ShowError(ret);
            return false;
        }

        ret = await ValidateMobile(SignupDTO.MobileNumber);
        if (ret != "Ok")
        {
            _toastService.ShowError(ret);
            return false;
        }

        try
        {
            bool res = await _accountService.PersonTaken(SignupDTO.FirstName, SignupDTO.LastName, SignupDTO.DateOfBirth.Value.ToString("yyy-MM-dd"), SignupDTO.RoleType);
            if (res == false)
            {
                _toastService.ShowError("Name already registered...");
                return false;
            }
        }
        catch (Exception)
        {
            _toastService.ShowError("Connection to server failed. Make sure you are connected to internet");
            return false;
        }
        return true;
    }
    public async Task<bool> VerifyMobile()
    {


        if (!_iCurrentUser.MobileNumberValidated)
        {
            var parameters = new ModalParameters();
            parameters.Add("MobileNumber", _iCurrentUser.MobileNumber);
            var popupOtp = _popupModal.Show<PopupSMS>("", parameters);
            var result = await popupOtp.Result;
            if (!(bool)result.Data)
            {
                return false;
            }
            else
            {
                var verifyMobile = await _accountService.VerifyMobileNumber();
                if (!verifyMobile)
                {
                    _toastService.ShowError("An error occured while attempting to verify mobile number. Please try again later");
                    return false;
                }
                else
                {
                    _iCurrentUser.MobileNumberValidated = true;
                }
            }

        }

        return true;
    }
    public async Task<bool> VerifyMobileReg(string MobileNumber)
    {
        var parameters = new ModalParameters();
        parameters.Add("MobileNumber", MobileNumber);


        var popupOtp = _popupModal.Show<PopupSMS>("", parameters);
        var result = await popupOtp.Result;
        if (!(bool)result.Data)
        {
            return false;
        }
        else
        {
            return true;

        }
    }
    public async Task initializeSurvey()
    {
        //SurveyQuestion = await _constantService.GetSurveyQuestions();
        //question1 = SurveyQuestion.FirstOrDefault(p => p.Id == 1);
        //strQuestion1 = question1.Question;

        //question2 = SurveyQuestion.FirstOrDefault(p => p.Id == 2);
        //strQuestion2 = question2.Question;

        //SurveyAnswer1 = await _constantService.GetSurveyAnswers(question1.Id);
        //SurveyAnswer2 = await _constantService.GetSurveyAnswers(question2.Id);
    }
    #endregion
}
