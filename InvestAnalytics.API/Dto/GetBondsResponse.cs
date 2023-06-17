using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Dto;

public class GetBondsResponse
{
    public Bond Bond { get; set; }
    public IEnumerable<Coupon> Coupons { get; set; }
    public LastPrice LastPrice { get; set; }
}