using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using InvestAnalytics.API.Dto;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Services.TinkoffService;

public class TinkoffService : ITinkoffService
{
    private readonly InvestApiClient _client;

    public TinkoffService(InvestApiClient investApiClient)
    {
        _client = investApiClient;
    }

    public async Task<IEnumerable<GetBondsResponse>> GetBonds()
    {
        Console.WriteLine("start");
        var bonds = (await _client.Instruments.BondsAsync(new InstrumentsRequest()
                { InstrumentStatus = InstrumentStatus.Base })).Instruments
            .Where(bond => bond is
            {
                ApiTradeAvailableFlag: true,
                BuyAvailableFlag: true,
                Currency: "rub"
            });
        var lastPrices = await GetBondsLastPrice(bonds);
        var getBondsResponses = new List<GetBondsResponse>();
        foreach (var bond in bonds)
        {
            getBondsResponses.Add(new GetBondsResponse()
            {
                Bond = bond,
                Coupons = await GetCoupons(bond),
                LastPrice = lastPrices.FirstOrDefault(lastPrice => lastPrice.InstrumentUid == bond.Uid)
            });
        }

        return getBondsResponses.Where(getBondsResponse => getBondsResponse.Coupons.Count() != 0);
    }

    public async Task<IEnumerable<Coupon>> GetCoupons(Bond bond)
    {
        var from = DateTime.UtcNow.ToTimestamp();
        var to = bond.MaturityDate;
        if (from is null || to is null)
            return new List<Coupon>();
        var coupons = await _client.Instruments.GetBondCouponsAsync(new GetBondCouponsRequest()
        {
            Figi = bond.Figi,
            From = DateTime.UtcNow.ToTimestamp(),
            To = bond.MaturityDate
        });
        return coupons.Events;
    }

    public async Task<IEnumerable<LastPrice>> GetBondsLastPrice(IEnumerable<Bond> bonds)
    {
        return (await _client.MarketData.GetLastPricesAsync(new GetLastPricesRequest
        {
            InstrumentId = { bonds.Select(bond => bond.Uid) }
        })).LastPrices;
    }
}