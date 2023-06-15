using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Services;

public class TinkoffService
{
    private readonly InvestApiClient _client;

    public TinkoffService(InvestApiClient investApiClient)
    {
        _client = investApiClient;
    }

    public async Task<IEnumerable<KeyValuePair<Bond, IEnumerable<Coupon>>>> GetBonds()
    {
        Console.WriteLine("start");
        var bonds = await _client.Instruments.BondsAsync(new InstrumentsRequest()
            { InstrumentStatus = InstrumentStatus.Base });
        var grouped = bonds.Instruments
            .Where(bond => bond is
            {
                ApiTradeAvailableFlag: true,
                BuyAvailableFlag: true,
                Currency: "rub"
            });
        var count = grouped.Count();
        var lastPrices = await _client.MarketData.GetLastPricesAsync(new GetLastPricesRequest());

        var bondCoupons = new List<KeyValuePair<Bond, IEnumerable<Coupon>>>();
        foreach (var bond in grouped)
        {
            var from = DateTime.UtcNow.ToTimestamp();
            var to = bond.MaturityDate;
            if (from is null || to is null)
                continue;
            //var period = (to - from).ToTimeSpan();
            var coupons = await _client.Instruments.GetBondCouponsAsync(new GetBondCouponsRequest()
            {
                Figi = bond.Figi,
                From = DateTime.UtcNow.ToTimestamp(),
                To = bond.MaturityDate
            });
            //var bondResponse = await _client.Instruments.BondByAsync(new InstrumentRequest() { IdType = InstrumentIdType.Uid, Id = bond.Uid });
            //var sumOfCoupons = coupons.Events.Sum(coupon => coupon.PayOneBond.ToDouble());
            //var nominal = bond.InitialNominal.ToDouble();
            //var nominalPlusAci = nominal + bond.AciValue.ToDouble();
            //var lastPrice = lastPrices.LastPrices.FirstOrDefault(price => price.Figi == bond.Figi);


            //bondCoupons.Add(new BondInfo
            //{
            //    Bond = bond,
            //    Coupons = coupons.Events,
            //    SumOfCoupons = sumOfCoupons,
            //    YearYield = 365 * (sumOfCoupons / nominalPlusAci) / period.TotalDays,
            //    FullYield = sumOfCoupons / nominalPlusAci,
            //    Duration = period,
            //    LastPrice = nominal * (lastPrice is null ? 0 : lastPrice.Price.ToDouble()) / 100,
            //    Nominal = nominal,
            //    Period = period.TotalDays
            //});
            bondCoupons.Add(new KeyValuePair<Bond, IEnumerable<Coupon>>(bond, coupons.Events));
        }

        var withCoupons = bondCoupons.Where(bondCoupon => bondCoupon.Value.Count() != 0);
        //var ordered = withCoupons.OrderByDescending(bond => bond.YearYield);
        return withCoupons;
    }
}