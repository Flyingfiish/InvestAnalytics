using AutoMapper;
using InvestAnalytics.API.Db;
using InvestAnalytics.API.Domain;
using InvestAnalytics.API.Services.TinkoffService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvestAnalytics.API.CQRS.Commands.Handlers;

public class ActualizeBondsCommandHandler : IRequestHandler<ActualizeBondsCommand>
{
    private readonly ITinkoffService _tinkoffService;
    private readonly ApplicationContext _context;

    public ActualizeBondsCommandHandler(ITinkoffService tinkoffService, ApplicationContext context)
    {
        _tinkoffService = tinkoffService;
        _context = context;
    }

    public async Task Handle(ActualizeBondsCommand request, CancellationToken cancellationToken)
    {
        var getBondsResponses = await _tinkoffService.GetBonds();
        var bonds = getBondsResponses.Select(gbr => gbr.Bond);
        var coupons = new List<CouponInfo>();

        bonds = await CreateOrUpdateBonds(bonds);
        foreach (var getBondsResponse in getBondsResponses)
        {
            coupons.AddRange(getBondsResponse.Coupons.Select(c =>
            {
                var bond = bonds.FirstOrDefault(b => b.Figi == c.Figi);
                if (bond is not null)
                    c.BondInfoId = bond.Id;
                return c;
            }));
        }

        await CreateOrUpdateCoupons(coupons);

    }

    private async Task<IEnumerable<BondInfo>> CreateOrUpdateBonds(IEnumerable<BondInfo> bonds)
    {
        foreach (var bond in bonds)
        {
            await _context.Bonds.Update(bond, b => b.UId == bond.UId);
        }

        await _context.SaveChangesAsync();
        return bonds;
    }

    private async Task CreateOrUpdateCoupons(IEnumerable<CouponInfo> coupons)
    {
        foreach (var coupon in coupons)
        {
            await _context.Coupons.Update(coupon, c => c.Figi == coupon.Figi && c.CouponNumber == coupon.CouponNumber);
        }

        await _context.SaveChangesAsync();
    }
}