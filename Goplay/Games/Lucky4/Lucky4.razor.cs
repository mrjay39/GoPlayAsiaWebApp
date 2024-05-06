using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace GoPlayAsiaWebApp.Goplay.Games.Lucky4
{
    public partial class Lucky4
    {
        [Inject]
        Lucky4V2ViewModel iLucky4Model { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        public IModalReference popupwinner { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                iLucky4Model.Notify += OnNotify;

                iLucky4Model.popupModal = popupModal;
                iLucky4Model.IsChipsDisabled = true;
                iLucky4Model.BetAmount = 0;
                iLucky4Model.SelectedChipAmount = 0;

                popupRef = popupModal.Show<PopupLoading>("");

                await iLucky4Model.ConnectSignalR();
                await iLucky4Model.AssignSignalRMethods();

                await iLucky4Model.GetGameSetting();
                await iLucky4Model.GetPlayerCategory(); //new
                await iLucky4Model.GetGameVariantChipsByCategory(); //new
                await iLucky4Model.GetGameRound();
                await iLucky4Model.GetCurrentRoundBets();

                if (iLucky4Model.streamKey == string.Empty)
                    await iLucky4Model.GenerateStreamKey();
                popupRef.Close();
                await iLucky4Model.GetTrends();
            }
            catch (Exception)
            {

            }
        }

        private async Task setGameChips()
        {
            await iLucky4Model.GetGameVariantChipsByCategory();


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

            //iLucky4Model.DisconnectSignalR();
        }
        public async ValueTask DisposeAsync()
        {
            iLucky4Model.Notify -= OnNotify;
            //await iLucky4Model.DisconnectSignalR();
        }
    }
}
