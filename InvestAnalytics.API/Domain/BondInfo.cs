using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Domain
{
    public class BondInfo
    {
        public Bond Bond { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }
        public TimeSpan Duration { get; set; }
        public double YearYield { get; set; }
        public double FullYield { get; set; }
        public double SumOfCoupons { get; set; }
        public double LastPrice { get; set; }
        public double Nominal { get; set; }
        public double Period { get; set; }
    }
}
