using Blazored.Modal;
using Blazored.Modal.Services;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.AddCredit
{

    public partial class GoplayGcashInstruction
    {
        #region Injected Services
        [Inject] MainCreditViewModel _mainCreditsViewModel { get; set; }

        [CascadingParameter] public IModalService popupModal { get; set; }
        #endregion


    }
}
