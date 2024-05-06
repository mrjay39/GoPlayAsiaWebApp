using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Games.Dropwin
{
    public partial class Dropwin
    {
        [Inject]
        DropwinViewModel iDropWinModel { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        public IModalReference popupwinner { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                iDropWinModel.Notify += OnNotify;

                await iDropWinModel.ConnectSignalR();
                await iDropWinModel.AssignSignalRMethods();
                iDropWinModel.popupModal = popupModal;
                popupRef = popupModal.Show<PopupLoading>("");

                await iDropWinModel.GetGameSetting();
                await iDropWinModel.GetGameRound();

                await iDropWinModel.GetGameChips();
                await iDropWinModel.GetCurrentRoundBets();
                await iDropWinModel.GetPrevCurrentRoundBets();
                if (iDropWinModel.streamKey == string.Empty)
                    await iDropWinModel.GenerateStreamKey();
                popupRef.Close();
                await iDropWinModel.GetTrends();
            }
            catch (Exception)
            {

                //throw;
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

            //iDropWinModel.DisconnectSignalR();
        }
        public async ValueTask DisposeAsync()
        {
            iDropWinModel.Notify -= OnNotify;
            //await iDropWinModel.DisconnectSignalR();
        }
    }
}
