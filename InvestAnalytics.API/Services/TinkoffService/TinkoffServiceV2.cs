using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using InvestAnalytics.API.Domain;
using InvestAnalytics.API.Dto;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Services.TinkoffService;

public class TinkoffServiceV2 : ITinkoffService
{
    private readonly InvestApiClient _client;
    private readonly IMapper _mapper;

    public TinkoffServiceV2(InvestApiClient investApiClient, IMapper mapper)
    {
        _client = investApiClient;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<GetBondsResponse>> GetBonds()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CouponInfo>> GetCoupons(Bond bond)
    {
        var from = DateTime.UtcNow.ToTimestamp();
        var to = bond.MaturityDate;
        if (from is null || to is null)
            return new List<CouponInfo>();
        var coupons = await _client.Instruments.GetBondCouponsAsync(new GetBondCouponsRequest()
        {
            Figi = bond.Figi,
            From = DateTime.UtcNow.ToTimestamp(),
            To = bond.MaturityDate
        });
        return coupons.Events.Select(coupon => _mapper.Map<CouponInfo>(coupon));
    }

    public Task<IEnumerable<LastPrice>> GetBondsLastPrice(IEnumerable<Bond> bonds)
    {
        throw new NotImplementedException();
    }
}