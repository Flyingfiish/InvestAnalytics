using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Domain;

public class CouponInfo
{
    public Guid Id { get; set; }
    public Guid BondInfoId { get; set; }
    public BondInfo BondInfo { get; set; }
    
    public string Figi { get; set; }
    
    public DateTime CouponDate { get; set; }
    public Int64 CouponNumber { get; set; }
    public DateTime? FixDate { get; set; }
    public double PayOneBond { get; set; }
    public CouponType CouponType { get; set; }
    public DateTime CouponStartDate { get; set; }
    public DateTime CouponEndDate { get; set; }
    public int CouponPeriod { get; set; }
}