using InvestAnalytics.API.Domain;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Dto;

public class GetBondsResponse
{
    public BondInfo? Bond { get; set; }
    public IEnumerable<CouponInfo> Coupons { get; set; }
    public LastPrice? LastPrice { get; set; }
}