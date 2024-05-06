using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IPromotionService
{
    Task<List<voucherActiveModel>> GetActiveVoucherForUser();
    Task<List<voucherCorpModel>> GetMinTopupPromotion();
}