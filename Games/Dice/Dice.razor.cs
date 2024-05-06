using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Dice
{
    public partial class Dice
    {
        #region INJECTED & PARAMTERS
        [Inject] DiceViewModel _diceViewModel { get; set; }
        [CascadingParameter] public IModalService _popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        #endregion


        protected override async Task OnInitializedAsync()
        {
            await LoadPageData();
        }

        private async Task LoadPageData()
        {
            try
            {
                _diceViewModel.Notify += OnNotify;
                _diceViewModel.popupModal = _popupModal;
                _diceViewModel.Token_Animation = "";
                _diceViewModel.tokenDiv = "tokenhide";
                _diceViewModel.CSS_SingleColor_NumberBtn_Div = "hide";
                _diceViewModel.IsBetting_Button_Disabled = false;
                _diceViewModel.BetCombinationValue = string.Empty;
                _diceViewModel.CurrentGameType = 0;

                await _diceViewModel.ConnectSignalR();
                await _diceViewModel.AssignSignalRMethods();
                popupRef = _popupModal.Show<PopupLoading>("");


                await _diceViewModel.GetGameSetting();
                await _diceViewModel.GetGameSettingVariant();
                await _diceViewModel.GetGameRound();

                List<Task> TaskList = new List<Task>();
                TaskList.Add(_diceViewModel.GetPlayerCategory());
                TaskList.Add(_diceViewModel.GetGameChipsByCategory());
                TaskList.Add(_diceViewModel.GetGameVariantChipsByCategory());
                TaskList.Add(_diceViewModel.GetCurrentRoundBets());
                if (_diceViewModel.streamKey == string.Empty)
                    TaskList.Add(_diceViewModel.GenerateStreamKey());
                await Task.WhenAll(TaskList.ToArray());

                popupRef.Close();
                await _diceViewModel.GetTrends();

            }

            catch (Exception)
            {
                throw;
            }
        }
        public async void OnNotify()
        {
            await InvokeAsync(() => StateHasChanged());
        }
        private async Task SetGameChips()
        {
            await _diceViewModel.GetGameChipsByCategory();
            await _diceViewModel.GetGameVariantChipsByCategory();

            if (_diceViewModel.CurrentGameType == (int)DiceBetTypes.AllDice)
            {
                await _diceViewModel.SetGameChips(_diceViewModel.GametypeId, false);
            }
            else
            {
                switch (_diceViewModel.CurrentGameType)
                {
                    case (int)DiceBetTypes.Odd:
                    case (int)DiceBetTypes.Even:
                        await _diceViewModel.SetGameChips((int)GameVariant.DiceOddEven, true);
                        break;
                    case (int)DiceBetTypes.Small:
                    case (int)DiceBetTypes.Big:
                        await _diceViewModel.SetGameChips((int)GameVariant.DiceSmallBig, true);
                        break;
                    case (int)DiceBetTypes.Single:
                        await _diceViewModel.SetGameChips((int)GameVariant.DiceSingle, true);
                        break;
                }
            }
        }

        private async Task NumberClicked()
        {
            StateHasChanged();
        }
    }
}
