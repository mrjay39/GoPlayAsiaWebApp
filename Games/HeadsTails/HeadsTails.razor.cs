using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.HeadsTails
{
    public partial class HeadsTails
    {
        [Inject]
        HeadsAndTailsViewModel iHeadsTailsViewModel { get; set; }
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

                iHeadsTailsViewModel.Notify += OnNotify;
                iHeadsTailsViewModel.popupModal = popupModal;
                iHeadsTailsViewModel.tokenDisabled = true;
                iHeadsTailsViewModel.betDisabled = true;
                iHeadsTailsViewModel.tokenDiv = "tokenhide";
                iHeadsTailsViewModel.Token_Animation = "";

                await iHeadsTailsViewModel.ConnectSignalR();
                await iHeadsTailsViewModel.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");


                await iHeadsTailsViewModel.GetGameSetting();
                await iHeadsTailsViewModel.GetGameRound();

                List<Task> TaskList = new List<Task>();
                TaskList.Add(iHeadsTailsViewModel.GetPlayerCategory()); //new
                TaskList.Add(iHeadsTailsViewModel.GetGameChipsByCategory()); //new
                TaskList.Add(iHeadsTailsViewModel.GetCurrentRoundBets());
                TaskList.Add(iHeadsTailsViewModel.GetPreviousBets((int)GameTypes.Heads_And_Tails));
                if (iHeadsTailsViewModel.streamKey == string.Empty)
                    TaskList.Add(iHeadsTailsViewModel.GenerateStreamKey());
                await Task.WhenAll(TaskList.ToArray());

                popupRef.Close();
                await iHeadsTailsViewModel.GetTrends();

            }

            catch (Exception)
            {

                throw;
            }
        }
        private async Task setGameChips()
        {
            await iHeadsTailsViewModel.GetGameChipsByCategory();
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
            //iDropWinModel.Notify -= OnNotify;
        }
        public async ValueTask DisposeAsync()
        {
            iHeadsTailsViewModel.Notify -= OnNotify;
            // await luckyViewModel.DisconnectSignalR();
        }
    }
}
