using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.DTOs;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.AddCredit

{
    public partial class Voucher
    {
        [Parameter]
        public voucherActiveModel activeVoucher { get; set; }
        [CascadingParameter] public IModalService popselVoucher { get; set; }

        [Parameter]
        public decimal? Amount { get; set; }

        public async Task showApplicableVoucher()
        {
            var selVoucher = popselVoucher.Show<PopVoucherSelect>("Select Voucher", new ModalOptions() { Class = "op-modal" });
        }
    }
}
