using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.Games.First3
{
    public partial class First3
    {
        [Inject]
        First3ViewModel first3ViewModel { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadPageData();
        }
        private async Task LoadPageData()
        {
            try
            {

                first3ViewModel.Notify += OnNotify;
                first3ViewModel.popupModal = popupModal;
                first3ViewModel.Token_Animation = "";
                first3ViewModel.tokenDiv = "tokenhide";
                first3ViewModel.isbuttonDisabled = false;

                await first3ViewModel.ConnectSignalR();
                await first3ViewModel.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");


                await first3ViewModel.GetGameSetting();
                await first3ViewModel.GetGameSettingVariant();
                await first3ViewModel.GetGameRound();

                List<Task> TaskList = new List<Task>();
                TaskList.Add(first3ViewModel.GetPlayerCategory()); //new
                TaskList.Add(first3ViewModel.GetGameChipsByCategory()); //new
                TaskList.Add(first3ViewModel.GetGameVariantChipsByCategory()); //new
                TaskList.Add(first3ViewModel.GetCurrentRoundBets()); //modified
                if (first3ViewModel.streamKey == string.Empty)
                    TaskList.Add(first3ViewModel.GenerateStreamKey());
                await Task.WhenAll(TaskList.ToArray());

                popupRef.Close();
                await first3ViewModel.GetTrends();
            }

            catch (Exception)
            {
                throw;
            }
        }

        private async Task setGameChips()
        {
            await first3ViewModel.GetGameChipsByCategory();
            await first3ViewModel.GetGameVariantChipsByCategory();

            if (first3ViewModel.CurrentGameType == (int)F3MainBetTypes.Gold || first3ViewModel.CurrentGameType == (int)F3MainBetTypes.Silver)
            {
                await first3ViewModel.SetGameChips(first3ViewModel.GametypeId, false);
            }
            else
            {
                switch (first3ViewModel.CurrentGameType)
                {
                    case (int)F3SubBetTypes.SameSuiteGold:
                    case (int)F3SubBetTypes.SameSuiteSilver:
                        await first3ViewModel.SetGameChips((int)GameVariant.F3Suits, true);
                        break;
                    case (int)F3SubBetTypes.Trio:
                        await first3ViewModel.SetGameChips((int)GameVariant.F3Trio, true);
                        break;
                    case (int)F3SubBetTypes.TwoRedGold:
                    case (int)F3SubBetTypes.TwoRedSilver:
                    case (int)F3SubBetTypes.TwoBlackGold:
                    case (int)F3SubBetTypes.TwoBlackSilver:
                        await first3ViewModel.SetGameChips((int)GameVariant.F3Color, true);
                        break;
                }
            }
        }
        protected override void OnAfterRender(bool firstRender)
        {

        }
        public async void OnNotify()
        {
            await InvokeAsync(() => StateHasChanged());
        }
        public void Dispose()
        {
            //first3ViewModel.DisconnectSignalR(); ;
        }
        public async ValueTask DisposeAsync()
        {
            first3ViewModel.Notify -= OnNotify;
            // await first3ViewModel.DisconnectSignalR();
        }
    }
}

