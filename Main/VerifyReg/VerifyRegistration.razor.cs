using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Services;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup; 
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static GoplayasiaBlazor.Models.Constants.Settings;
using static System.Net.Mime.MediaTypeNames;

namespace GoPlayAsiaWebApp.Main.VerifyReg;

public partial class VerifyRegistration
{
    public string showpass = "password";
    public string showpassicon = "bi bi-eye-fill";
    public string showpassConfirm = "password";
    public string showpassiconConfirm = "bi bi-eye-fill";

    #region Injected Services 
    [Inject] IConstantService iConstantService { get; set; }
    [Inject] IMapper _mapper { get; set; }
    [Inject] SignUpViewModel _signupViewModel { get; set; }
    [Inject] VerifyRegistrationViewModel _verifyRegistrationViewModel { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] IToastService toastService { get; set; }
    #endregion


    #region Local Variables

    [CascadingParameter] public IModalService popupModal { get; set; }
    public IModalReference refpopuLoadingpModal { get; set; }
    private EditContext? editContext;

    private bool step1 { get; set; } = false;
    private bool step2 { get; set; } = true;
    private bool step3 { get; set; } = true;

    private string? ProfileImageUri = "/img/verify-profile.jpg";
    private string? IDImageUri = "/img/verify-government.jpg";


    [Parameter]
    public IEnumerable<CityModel> CityList { get; set; }
    public IEnumerable<NationalityModel> NationalityList { get; set; }
    public IEnumerable<NatureOfWorkModel> NatureOfWorkList { get; set; }
    public IEnumerable<SourceOfIncomeModel> SourceOfIncomeList { get; set; }
    [CascadingParameter] public IModalService popuLoadingpModal { get; set; }


    private NationalityModel? selectedNationality { get; set; }
    private CityModel? selectedCurrCity;
    private CityModel? selectedCurrProv;
    private CityModel? selectedPermCity;
    private CityModel? selectedPermProv;
    private CityModel? selectedPermProvOld;
    private string PermanentStreetOldVal = "";
    private bool isChecked { get; set; } = true;
    private bool hidePermanentAdd = true;

    private NatureOfWorkModel? selectedNatureofwork;
    private bool hideOtherNatureOfWOrk = true;
    private SourceOfIncomeModel? selectedSourceofincome;
    private bool hideOtherSourceOfIncome = true;

    private string refClass = "referralKey";
    private bool refDisable = true;
    private bool refIsChecked = false;
    private bool refWarning = true;
    private string refValue { get; set; }
    private string refOldvalue = "";

    private bool agreePolicy = false;
    private bool disableSubmit = true;
    private string submitClass = "referralKey";

    private SignupDTO _signupDTO;
    public SignupDTO SignupDTO;

    private string _termsAndConditionsUrl;
    FileUploadProgress? progress { get; set; }
    FileUploadProgress? progressid { get; set; }
    string CurrUrl;
    private CancellationTokenSource cancelation;

    protected override void OnParametersSet()
    {

        //CurrUrl = _navigationManager.Uri.ToString();
    }

    public string TermsAndConditionsUrl
    {
        get => _termsAndConditionsUrl;
        set
        {
            _termsAndConditionsUrl = value;
        }
    }

    private string _privacyPolicyUrl;
    public string PrivacyPolicyUrl
    {
        get => _privacyPolicyUrl;
        set
        {
            _privacyPolicyUrl = value;
        }
    }

    private ElementReference txtUsername;
    #endregion


    #region Local Methods

    record FileUploadProgress(string FileName, long Size)
    {
        public long UploadedBytes { get; set; }
        public double UploadedPercentage => UploadedBytes / (double)Size * 100d;
    }
    string FormatBytes(long value)
    {
        return (value / 1024f / 1024f).ToString();
    }

    #region STEP 1
    private async Task onchange_bdmonth(ChangeEventArgs e)
    {
        var valInt = 0;
        var value = (string)e.Value;

        if (value != "")
        {
            valInt = int.Parse(value);
        }

        if (value.Length == 1)
        {
            SignupDTO.Month = "0" + value;
        }
        else if (valInt > 12)
        {
            SignupDTO.Month = "00";
        }
        else
        {
            SignupDTO.Month = value;
        }
    }
    private async Task onchange_bdday(ChangeEventArgs e)
    {
        var valInt = 0;
        var value = (string)e.Value;

        if (value != "")
        {
            valInt = int.Parse(value);
        }

        if (value.Length == 1)
        {
            SignupDTO.Day = "0" + value;
        }
        else if (valInt > 31)
        {
            SignupDTO.Day = "00";
        }
        else
        {
            SignupDTO.Day = value;
        }
    }
    public async Task LoadProfileImage(InputFileChangeEventArgs inputFileChangeEventArgs)
    {
        try
        {
            #region Main Photo
            long maxFileSize = 1024 * 1024 * 50;

            var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 1280, 720);
            //var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 5000, 5000);
            progress = new FileUploadProgress(image.Name, image.Size);

            using var stream = image.OpenReadStream(maxFileSize);

            var buffer = new byte[4 * 1096];
            int bytesRead;
            double totalRead = 0;
            await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
            timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

            using (MemoryStream ms = new MemoryStream())
            {
                while ((bytesRead = await stream.ReadAsync(buffer, cancelation.Token)) != 0)
                {
                    totalRead += bytesRead;
                    ms.Write(buffer, 0, bytesRead);

                    progress.UploadedBytes = (int)totalRead;
                    StateHasChanged();
                }
                _verifyRegistrationViewModel.ProfileImageBytes = ms.ToArray();
                ////convert stream to base64
                //ProfileImageUrib64 = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
                StateHasChanged();
            }

            #endregion
            #region Preview Photo

            var pimage = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 150, 150);
            using var pstream = pimage.OpenReadStream(maxFileSize);

            var pbuffer = new byte[4 * 1096];
            int pbytesRead;

            using (MemoryStream pms = new MemoryStream())
            {
                while ((pbytesRead = await pstream.ReadAsync(pbuffer)) != 0)
                {
                    pms.Write(pbuffer, 0, pbytesRead);
                }
                ////convert stream to base64
                ProfileImageUri = $"data:image/png;base64,{Convert.ToBase64String(pms.ToArray())}";
                StateHasChanged();
            }
            _verifyRegistrationViewModel.hasProfimg = true;

            #endregion
        }
        catch (Exception ex)
        {
            toastService.ShowError(ex.Message);
            _verifyRegistrationViewModel.hasProfimg = false;
        }
    }
    private void ShowStep1()
    {
        step1 = false;
        step2 = true;
        step3 = true;
    }
    private async Task ValdiateStep1()
    {
        refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
        bool valStep1 = await _verifyRegistrationViewModel.Step1Validation(SignupDTO);
        if (valStep1)
        {
            ShowStep2();
        }
        refpopuLoadingpModal.Close();
    }
    #endregion

    #region STEP 2
    private async Task onchange_txtEmail(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        if (!string.IsNullOrEmpty(value))
        {
            string valEmail = await _verifyRegistrationViewModel.ValidateEmail(value);
            _verifyRegistrationViewModel.errEmail = valEmail;
        }
        else
        {
            _verifyRegistrationViewModel.errEmail = "Ok";
        }

    }
    private async Task<IEnumerable<NationalityModel>> SearchNationality(string searchText)
    {
        return await Task.FromResult(NationalityList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
    }
    private NationalityModel LoadNationality(int? id) => NationalityList.FirstOrDefault(p => p.Id == id);
    private async Task<IEnumerable<CityModel>> SearchCity(string searchText)
    {
        return await Task.FromResult(CityList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
    }
    private CityModel LoadSelCurrCity(int? id) => CityList.FirstOrDefault(p => p.Id == id);
    private async Task selectedCurrCityChanged(CityModel result)
    {
        selectedCurrCity = result;
        if (selectedCurrCity is not null)
        {
            refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
            selectedCurrProv = await iConstantService.GetParentsOfCity(result.Id);
            refpopuLoadingpModal.Close();

            SignupDTO.CurrentCityId = selectedCurrCity.Id;
            SignupDTO.CurrentProvince = selectedCurrProv.Province.Name;
            SignupDTO.CurrentRegion = selectedCurrProv.Region.Name;
        }
        else
        {
            SignupDTO.CurrentCityId = null;
            SignupDTO.CurrentProvince = null;
            SignupDTO.CurrentRegion = null;
        }
    }
    private async Task ShowStep2()
    {
        step1 = true;
        step2 = false;
        step3 = true;
    }
    private async Task ValidateStep2()
    {
        refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
        if (selectedNationality is not null)
        {
            SignupDTO.NationalityId = selectedNationality.Id;
        }
        else
        {
            SignupDTO.NationalityId = null;
        }
        SignupDTO.PlaceOfBirth = "";
        SignupDTO.EmailAddress = "";
        bool valStep2 = await _verifyRegistrationViewModel.Step2Validation(SignupDTO);
        if (valStep2)
        {
            ShowStep3();
        }
        refpopuLoadingpModal.Close();
    }
    #endregion

    #region STEP 3
    private async Task<IEnumerable<NatureOfWorkModel>> SearchNatureofwork(string searchText)
    {
        return await Task.FromResult(NatureOfWorkList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
    }
    private NatureOfWorkModel LoadSelNatureOfWork(int? id) => NatureOfWorkList.FirstOrDefault(p => p.Id == id);
    private async Task selectedNatureOfWorChanged(NatureOfWorkModel result)
    {
        selectedNatureofwork = result;
        if (selectedNatureofwork is not null)
        {
            SignupDTO.NatureOfWorkId = selectedNatureofwork.Id;
            if (selectedNatureofwork.Id == 6)  // 6 - Others
            {
                hideOtherNatureOfWOrk = false;
            }
            else
            {
                hideOtherNatureOfWOrk = true;
                SignupDTO.OtherNatureOfWork = "";
            }
        }
        else
        {
            SignupDTO.NatureOfWorkId = null;
            hideOtherNatureOfWOrk = true;
            SignupDTO.OtherNatureOfWork = "";
        }

    }
    private async Task<IEnumerable<SourceOfIncomeModel>> SearchSourcofIncome(string searchText)
    {
        return await Task.FromResult(SourceOfIncomeList.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
    }
    private SourceOfIncomeModel LoadSelSourceOfIncome(int? id) => SourceOfIncomeList.FirstOrDefault(p => p.Id == id);
    private async Task selectedSourceOfIncomeChanged(SourceOfIncomeModel result)
    {
        selectedSourceofincome = result;
        if (selectedSourceofincome is not null)
        {
            SignupDTO.SourceOfIncomeId = selectedSourceofincome.Id;
            if (selectedSourceofincome.Id == 4)  // 4 - Others
            {
                hideOtherSourceOfIncome = false;
            }
            else
            {
                hideOtherSourceOfIncome = true;
                SignupDTO.OtherSourceOfIncome = "";
            }
        }
        else
        {
            SignupDTO.SourceOfIncomeId = null;
            hideOtherSourceOfIncome = true;
            SignupDTO.OtherSourceOfIncome = "";
        }

    }
    public async Task LoadIDImage(InputFileChangeEventArgs inputFileChangeEventArgs)
    {

        try
        {
            #region Main Photo
            long maxFileSize = 1024 * 1024 * 50;

            var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 1280, 720);

            //var image = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 5000, 5000);
            progressid = new FileUploadProgress(image.Name, image.Size);

            using var stream = image.OpenReadStream(maxFileSize);

            var buffer = new byte[4 * 1096];
            int bytesRead;
            double totalRead = 0;
            await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
            timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

            using (MemoryStream ms = new MemoryStream())
            {
                while ((bytesRead = await stream.ReadAsync(buffer, cancelation.Token)) != 0)
                {
                    totalRead += bytesRead;
                    ms.Write(buffer, 0, bytesRead);

                    progressid.UploadedBytes = (int)totalRead;
                    StateHasChanged();
                }
                _verifyRegistrationViewModel.GovernmentImageBytes = ms.ToArray();
                ////convert stream to base64
                //IDImageUrib64 = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
                StateHasChanged();
            }
            #endregion
            #region Preview Photo

            var pimage = await inputFileChangeEventArgs.File.RequestImageFileAsync("image/png", 150, 150);
            using var pstream = pimage.OpenReadStream(maxFileSize);

            var pbuffer = new byte[4 * 1096];
            int pbytesRead;

            using (MemoryStream pms = new MemoryStream())
            {
                while ((pbytesRead = await pstream.ReadAsync(pbuffer)) != 0)
                {
                    pms.Write(pbuffer, 0, pbytesRead);
                }
                ////convert stream to base64
                IDImageUri = $"data:image/png;base64,{Convert.ToBase64String(pms.ToArray())}";
                StateHasChanged();
            }
            _verifyRegistrationViewModel.hasGovtimg = true;
            #endregion
        }
        catch (Exception ex)
        {
            toastService.ShowError(ex.Message);
            _verifyRegistrationViewModel.hasGovtimg = false;
        }
    }
    private async Task ShowStep3()
    {
        step1 = true;
        step2 = true;
        step3 = false;
    }
    private async Task ValidateStep3()
    {
        refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
        bool valStep3 = await _verifyRegistrationViewModel.Step3Validation(SignupDTO);
        if (valStep3)
        {
            var parameters = new ModalParameters();
            parameters.Add("Message", "Submit verification data?");
            var popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });

            ModalResult result = await popupConfirm.Result;

            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    refpopuLoadingpModal.Close();
                    await _verifyRegistrationViewModel.SubmitDetails(SignupDTO);
                }
            }
        }
        refpopuLoadingpModal.Close();
    }
    #endregion

    #endregion


    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        refpopuLoadingpModal = popupModal.Show<PopupLoading>("");
        SignupDTO = new SignupDTO();
        _signupViewModel._popupModal = popupModal;
        var res = await _verifyRegistrationViewModel.GetUserInfo();
        if (res.Verified != null)
        {
            if (res.Verified == 1)
            {
                _navigationManager.NavigateTo("/lobby");
                return;
            }
        }
        SignupDTO.FirstName = res.FirstName;
        SignupDTO.MiddleName = res.MiddleName;
        SignupDTO.LastName = res.LastName;
        SignupDTO.DateOfBirth = res.DateOfBirth;
        SignupDTO.Username = res.Username;
        SignupDTO.MobileNumber = res.MobileNumber;
        SignupDTO.RoleType = res.RoleType;
        SignupDTO.AddressAreSame = isChecked;
        SignupDTO.Month = res.DateOfBirth.HasValue ? res.DateOfBirth.Value.ToString("MM") : "";
        SignupDTO.Day = res.DateOfBirth.HasValue ? res.DateOfBirth.Value.ToString("dd") : "";
        SignupDTO.Year = res.DateOfBirth.HasValue ? res.DateOfBirth.Value.ToString("yyyy") : "";

        SignupDTO.Gender = "MALE";

        var constants = await iConstantService.GetConstants();
        CityList = _mapper.Map<IEnumerable<CityModel>>(constants.Cities);

        NationalityList = _mapper.Map<IEnumerable<NationalityModel>>(constants.Nationalities);
        selectedNationality = NationalityList.FirstOrDefault(p => p.Id == 62);
        SignupDTO.NationalityId = selectedNationality.Id;

        NatureOfWorkList = _mapper.Map<IEnumerable<NatureOfWorkModel>>(constants.NaturesOfWork);
        selectedNatureofwork = NatureOfWorkList.FirstOrDefault(p => p.Id == 1);
        SignupDTO.NatureOfWorkId = selectedNatureofwork.Id;

        SourceOfIncomeList = _mapper.Map<IEnumerable<SourceOfIncomeModel>>(constants.SourcesOfIncome);
        selectedSourceofincome = SourceOfIncomeList.FirstOrDefault(p => p.Id == 1);
        SignupDTO.SourceOfIncomeId = selectedSourceofincome.Id;

        cancelation = new CancellationTokenSource();
        refpopuLoadingpModal.Close();
    }
    #endregion

}
