using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Services
{
    public class TinkoffAnalyticsService
    {
        private readonly InvestApiClient _client;
        public TinkoffAnalyticsService(InvestApiClient investApiClient)
        {
            _client = investApiClient;
        }

        public async Task<BondsResponse> GetBonds()
        {
            var bonds = await _client.Instruments.BondsAsync(new InstrumentsRequest() { InstrumentStatus = InstrumentStatus.Base });
            var grouped = bonds.Instruments
                .Where(bond => bond.Currency == "rub"
                && bond.ApiTradeAvailableFlag
                && bond.BuyAvailableFlag);
            var count = grouped.Count();
            var lastPrices = await _client.MarketData.GetLastPricesAsync(new GetLastPricesRequest());

            var bondCoupons = new List<BondCoupons>();
            foreach (var bond in grouped)
            {
                var from = DateTime.UtcNow.ToTimestamp();
                var to = bond.MaturityDate;
                if (from is null || to is null)
                    continue;
                var period = (to - from).ToTimeSpan(); ;
                var coupons = await _client.Instruments.GetBondCouponsAsync(new GetBondCouponsRequest()
                {
                    Figi = bond.Figi,
                    From = DateTime.UtcNow.ToTimestamp(),
                    To = bond.MaturityDate
                });
                //var bondResponse = await _client.Instruments.BondByAsync(new InstrumentRequest() { IdType = InstrumentIdType.Uid, Id = bond.Uid });
                var sumOfCoupons = coupons.Events.Sum(coupon => coupon.PayOneBond.ToDouble());
                var nominal = bond.InitialNominal.ToDouble();
                var nominalPlusAci = nominal + bond.AciValue.ToDouble();
                var lastPrice = lastPrices.LastPrices.FirstOrDefault(price => price.Figi == bond.Figi);


                bondCoupons.Add(new BondCoupons
                {
                    Bond = bond,
                    Coupons = coupons.Events,
                    SumOfCoupons = sumOfCoupons,
                    YearYield = (365 * sumOfCoupons / period.TotalDays) / nominalPlusAci,
                    FullYield = sumOfCoupons / nominalPlusAci,
                    Duration = period,
                    LastPrice = nominal * (lastPrice is null ? 0 : lastPrice.Price.ToDouble()) / 100,
                    Nominal = nominal,
                });

            }
            var withCoupons = bondCoupons.Where(bondCoupon => bondCoupon.Coupons.Count() != 0);
            var ordered = withCoupons.OrderByDescending(bond => bond.YearYield);
            return bonds;
        }

        public class BondCoupons
        {
            public Bond Bond { get; set; }
            public IEnumerable<Coupon> Coupons { get; set; }
            public TimeSpan Duration { get; set; }
            public double YearYield { get; set; }
            public double FullYield { get; set; }
            public double SumOfCoupons { get; set; }
            public double LastPrice { get; set; }
            public double Nominal { get; set; }
        }
    }

    public static class MoneyExtentions
    {
        public static double ToDouble(this MoneyValue moneyValue)
        {
            return Convert.ToDouble($"{moneyValue.Units},{moneyValue.Nano}");
        }

        public static double ToDouble(this Quotation quotation)
        {
            return Convert.ToDouble($"{quotation.Units},{quotation.Nano}");
        }
    }
}
