using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoPlayAsiaWebApp.Games.Lucky9;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Main.Settings;

public partial class SettingsView
{
    #region Injected Services
    [Inject] SettingsViewModel _settingsViewModel { get; set; }
    [Inject] ICurrentUser _currentUser { get; set; }
    [Inject] IConstantService iConstantService { get; set; }
    [Inject] IMapper _mapper { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }


    [Parameter]
    public IEnumerable<NatureOfWorkModel> NatureOfWorkList { get; set; }
    private NatureOfWorkModel? selectedNatureofwork;
    private bool disNatureofWork = true;

    private bool disBtnUpdateAcc = true;
    private string strDisBtnUpdateAcc = "disInput";

    private bool disBtnUpdateProf = true;
    private string strDisBtnUpdateProf = "disInput";

    public IEnumerable<SourceOfIncomeModel> SourceOfIncomeList { get; set; }
    private SourceOfIncomeModel? selectedSourceofincome;
    private bool disSourceofIncome = true;

    private bool hideConfirmPass = true;
    private string msgConfirmPass = "";

    private bool hideSettings = true;
    private bool hideVerifiedStatus = true;
    #endregion

    #region Local Methods
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var currentRoute = MyNavigationManager.Uri.ToLower().ToString().Replace(MyNavigationManager.BaseUri, "");
            if (currentRoute.ToLower().Contains("setting"))
            {
                hideSettings = false;
                hideVerifiedStatus = true;
            }
            else if (currentRoute.ToLower().Contains("verifystatus"))
            {
                hideSettings = true;
                hideVerifiedStatus = false;
            }
        }

    }
    private async Task onchange_email(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        if (value != _currentUser.EmailAddress)
        {
            disBtnUpdateAcc = false;
            strDisBtnUpdateAcc = "btn btn-primary";
        }
        else
        {
            disBtnUpdateAcc = true;
            strDisBtnUpdateAcc = "disInput";
        }
    }
    private async Task UpdateAccount()
    {
        await _settingsViewModel.UpdateAccountInformation();
        disBtnUpdateAcc = true;
        strDisBtnUpdateAcc = "disInput";
        await _settingsViewModel.GetUserInfo();
    }
    private async Task DeactivateAccount()
    {
        await _settingsViewModel.DeactivateAccount();
    }


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
            _settingsViewModel.UserInfo.NatureOfWorkId = selectedNatureofwork.Id;
            if (selectedNatureofwork.Id == 6)  // 6 - Others
            {
                disNatureofWork = false;
            }
            else
            {
                disNatureofWork = true;
                _settingsViewModel.UserInfo.OtherNatureOfWork = null;
            }

            if (selectedNatureofwork.Id != _settingsViewModel.currNatureOfWorkID ||
                _settingsViewModel.UserInfo.OtherNatureOfWork != _settingsViewModel.currOtherNatureOfWork ||
                _settingsViewModel.UserInfo.SourceOfIncomeId != _settingsViewModel.currSourceofIncome ||
                _settingsViewModel.UserInfo.OtherSourceOfIncome != _settingsViewModel.currOtherSourceOfIncome ||
                _settingsViewModel.UserInfo.PlaceOfBirth != _settingsViewModel.currPlaceOfBirth)
            {
                disBtnUpdateProf = false;
                strDisBtnUpdateProf = "btn btn-primary";
            }
            else
            {
                disBtnUpdateProf = true;
                strDisBtnUpdateProf = "disInput";
            }
        }
        else
        {
            disNatureofWork = true;
            _settingsViewModel.UserInfo.NatureOfWorkId = null;
            _settingsViewModel.UserInfo.OtherNatureOfWork = null;
            disBtnUpdateProf = true;
            strDisBtnUpdateProf = "disInput";
        }
    }
    private async Task onchange_natureofwork(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        if (_settingsViewModel.UserInfo.NatureOfWorkId != _settingsViewModel.currNatureOfWorkID ||
            value != _settingsViewModel.currOtherNatureOfWork ||
            _settingsViewModel.UserInfo.SourceOfIncomeId != _settingsViewModel.currSourceofIncome ||
            _settingsViewModel.UserInfo.OtherSourceOfIncome != _settingsViewModel.currOtherSourceOfIncome ||
            _settingsViewModel.UserInfo.PlaceOfBirth != _settingsViewModel.currPlaceOfBirth)
        {
            disBtnUpdateProf = false;
            strDisBtnUpdateProf = "btn btn-primary";
        }
        else
        {
            disBtnUpdateProf = true;
            strDisBtnUpdateProf = "disInput";
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
            _settingsViewModel.UserInfo.SourceOfIncomeId = selectedSourceofincome.Id;
            if (selectedSourceofincome.Id == 4)  // 4 - Others
            {
                disSourceofIncome = false;
            }
            else
            {
                disSourceofIncome = true;
                _settingsViewModel.UserInfo.OtherSourceOfIncome = null;
            }

            if (_settingsViewModel.UserInfo.NatureOfWorkId != _settingsViewModel.currNatureOfWorkID ||
                _settingsViewModel.UserInfo.OtherNatureOfWork != _settingsViewModel.currOtherNatureOfWork ||
                selectedSourceofincome.Id != _settingsViewModel.currSourceofIncome ||
                _settingsViewModel.UserInfo.OtherSourceOfIncome != _settingsViewModel.currOtherSourceOfIncome ||
                _settingsViewModel.UserInfo.PlaceOfBirth != _settingsViewModel.currPlaceOfBirth)
            {
                disBtnUpdateProf = false;
                strDisBtnUpdateProf = "btn btn-primary";
            }
            else
            {
                disBtnUpdateProf = true;
                strDisBtnUpdateProf = "disInput";
            }
        }
        else
        {
            disSourceofIncome = true;
            _settingsViewModel.UserInfo.SourceOfIncomeId = null;
            _settingsViewModel.UserInfo.OtherSourceOfIncome = null;
            disBtnUpdateProf = true;
            strDisBtnUpdateProf = "disInput";
        }

    }
    private async Task onchange_sourceofincome(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        if (_settingsViewModel.UserInfo.NatureOfWorkId != _settingsViewModel.currNatureOfWorkID ||
            _settingsViewModel.UserInfo.OtherNatureOfWork != _settingsViewModel.currOtherNatureOfWork ||
            _settingsViewModel.UserInfo.SourceOfIncomeId != _settingsViewModel.currSourceofIncome ||
            value != _settingsViewModel.currOtherSourceOfIncome ||
            _settingsViewModel.UserInfo.PlaceOfBirth != _settingsViewModel.currPlaceOfBirth)
        {
            disBtnUpdateProf = false;
            strDisBtnUpdateProf = "btn btn-primary";
        }
        else
        {
            disBtnUpdateProf = true;
            strDisBtnUpdateProf = "disInput";
        }
    }
    private async Task onchange_birthpalce(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        if (_settingsViewModel.UserInfo.NatureOfWorkId != _settingsViewModel.currNatureOfWorkID ||
            _settingsViewModel.UserInfo.OtherNatureOfWork != _settingsViewModel.currOtherNatureOfWork ||
            _settingsViewModel.UserInfo.SourceOfIncomeId != _settingsViewModel.currSourceofIncome ||
            _settingsViewModel.UserInfo.OtherSourceOfIncome != _settingsViewModel.currOtherSourceOfIncome ||
            value != _settingsViewModel.currPlaceOfBirth)
        {
            disBtnUpdateProf = false;
            strDisBtnUpdateProf = "btn btn-primary";
        }
        else
        {
            disBtnUpdateProf = true;
            strDisBtnUpdateProf = "disInput";
        }
    }
    private async Task UpdateProfile()
    {
        await _settingsViewModel.UpdateProfileInformation();
        disBtnUpdateProf = true;
        strDisBtnUpdateProf = "disInput";
        await _settingsViewModel.GetUserInfo();
    }


    private async Task onchange_confirmpass(ChangeEventArgs e)
    {
        var value = (string)e.Value;
        string valPass = await _settingsViewModel.PasswordMatchChecker(value);
        msgConfirmPass = valPass;
        if (valPass != "Ok")
        {
            hideConfirmPass = false;
        }
        else
        {
            hideConfirmPass = true;
        }
    }
    private async Task UpdatePassword()
    {
        await _settingsViewModel.UpdatePassword();
        //await _settingsViewModel.GetUserInfo();
    }
    #endregion

    #region Lifecycle Method
    protected override async Task OnInitializedAsync()
    {
        try
        {


            var popupRes = popupModal.Show<PopupLoading>("");
            await _settingsViewModel.GetUserInfo();
            await _settingsViewModel.ConnectSignalR();
            _settingsViewModel.popupModal = popupModal;
            popupRes.Close();

            var constants = await iConstantService.GetConstants();
            if (constants != null)
            {
                NatureOfWorkList = _mapper.Map<IEnumerable<NatureOfWorkModel>>(constants.NaturesOfWork);
                selectedNatureofwork = NatureOfWorkList.FirstOrDefault(p => p.Id == _settingsViewModel.UserInfo.NatureOfWorkId);
                _settingsViewModel.UserInfo.NatureOfWorkId = selectedNatureofwork.Id;
                if (selectedNatureofwork.Id == 6) // 6 - Others
                {
                    disNatureofWork = false;
                }
                else
                {
                    disNatureofWork = true;
                }

                SourceOfIncomeList = _mapper.Map<IEnumerable<SourceOfIncomeModel>>(constants.SourcesOfIncome);
                selectedSourceofincome = SourceOfIncomeList.FirstOrDefault(p => p.Id == _settingsViewModel.UserInfo.SourceOfIncomeId);
                if (selectedSourceofincome.Id == 4) // 4 - Others
                {
                    disSourceofIncome = false;
                }
                else
                {
                    disSourceofIncome = true;
                }
            }


        }
        catch (Exception)
        {

        }
    }
    #endregion
    public async ValueTask DisposeAsync()
    {
        //await _settingsViewModel.DisconnectSignalR();
    }
}