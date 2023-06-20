using AutoMapper;
using InvestAnalytics.API.Domain;
using InvestAnalytics.API.Utils;
using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponInfo>()
            .ForMember(c => c.Figi, opt => opt.MapFrom(c => c.Figi))
            .ForMember(c => c.CouponPeriod, opt => opt.MapFrom(c => c.CouponPeriod))
            .ForMember(c => c.CouponDate, opt => opt.MapFrom(c => c.CouponDate.ToDateTime()))
            .ForMember(c => c.CouponNumber, opt => opt.MapFrom(c => c.CouponNumber))
            .ForMember(c => c.CouponType, opt => opt.MapFrom(c => c.CouponType))
            .ForMember(c => c.FixDate, opt => opt.MapFrom(c => c.FixDate.ToDateTime()))
            .ForMember(c => c.CouponEndDate, opt => opt.MapFrom(c => c.CouponEndDate.ToDateTime()))
            .ForMember(c => c.CouponStartDate, opt => opt.MapFrom(c => c.CouponStartDate.ToDateTime()))
            .ForMember(c => c.PayOneBond, opt => opt.MapFrom(c => c.PayOneBond.ToDouble()));
        
        CreateMap<Bond, BondInfo>()
            .ForMember(b => b.Amortization, opt => opt.MapFrom(b => b.AmortizationFlag))
            .ForMember(b => b.Figi, opt => opt.MapFrom(b => b.Figi))
            .ForMember(b => b.MaturityDate, opt => opt.MapFrom(b => b.MaturityDate.ToDateTime()))
            .ForMember(b => b.Currency, opt => opt.MapFrom(b => b.Currency))
            .ForMember(b => b.Exchange, opt => opt.MapFrom(b => b.Exchange))
            .ForMember(b => b.Isin, opt => opt.MapFrom(b => b.Isin))
            .ForMember(b => b.Lot, opt => opt.MapFrom(b => b.Lot))
            .ForMember(b => b.Name, opt => opt.MapFrom(b => b.Name))
            .ForMember(b => b.Nominal, opt => opt.MapFrom(b => b.Nominal.ToDouble()))
            .ForMember(b => b.Otc, opt => opt.MapFrom(b => b.OtcFlag))
            .ForMember(b => b.Perpetual, opt => opt.MapFrom(b => b.PerpetualFlag))
            .ForMember(b => b.Sector, opt => opt.MapFrom(b => b.Sector))
            .ForMember(b => b.Ticker, opt => opt.MapFrom(b => b.Ticker))
            .ForMember(b => b.BuyAvailable, opt => opt.MapFrom(b => b.BuyAvailableFlag))
            .ForMember(b => b.ClassCode, opt => opt.MapFrom(b => b.ClassCode))
            .ForMember(b => b.FloatingCoupon, opt => opt.MapFrom(b => b.FloatingCouponFlag))
            .ForMember(b => b.InitialNominal, opt => opt.MapFrom(b => b.InitialNominal.ToDouble()))
            //.ForMember(b => b.IssueDate, opt => opt.MapFrom(b => b.iss))
            .ForMember(b => b.PlacementDate, opt => opt.MapFrom(b => b.PlacementDate.ToDateTime()))
            .ForMember(b => b.PlacementPrice, opt => opt.MapFrom(b => b.PlacementPrice.ToDouble()))
            .ForMember(b => b.RiskLevel, opt => opt.MapFrom(b => b.RiskLevel))
            .ForMember(b => b.SellAvailable, opt => opt.MapFrom(b => b.SellAvailableFlag))
            .ForMember(b => b.UId, opt => opt.MapFrom(b => b.Uid))
            .ForMember(b => b.ForQualInvestor, opt => opt.MapFrom(b => b.ForQualInvestorFlag))
            .ForMember(b => b.MinPriceIncrement, opt => opt.MapFrom(b => b.MinPriceIncrement.ToDouble()))
            .ForMember(b => b.StateRegDate, opt => opt.MapFrom(b => b.StateRegDate.ToDateTime()))
            .ForMember(b => b.CountryOfRiskCode, opt => opt.MapFrom(b => b.CountryOfRisk))
            .ForMember(b => b.CountryOfRiskName, opt => opt.MapFrom(b => b.CountryOfRiskName))
            .ForMember(b => b.CouponQuantityPerYear, opt => opt.MapFrom(b => b.CouponQuantityPerYear));
    }
}