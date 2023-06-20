using InvestAnalytics.API.Domain;
using InvestAnalytics.API.Dto;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Services.TinkoffService;

public interface ITinkoffService
{
    Task<IEnumerable<GetBondsResponse>> GetBonds();
    Task<IEnumerable<CouponInfo>> GetCoupons(Bond bond);
    Task<IEnumerable<LastPrice>> GetBondsLastPrice(IEnumerable<Bond> bonds);
}