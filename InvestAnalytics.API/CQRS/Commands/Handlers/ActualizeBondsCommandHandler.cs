using InvestAnalytics.API.Db;
using InvestAnalytics.API.Services.TinkoffService;
using MediatR;

namespace InvestAnalytics.API.CQRS.Commands.Handlers;

public class ActualizeBondsCommandHandler : IRequestHandler<ActualizeBondsCommand>
{
    private readonly ITinkoffService _tinkoffService;
    private readonly ApplicationContext _context;

    public ActualizeBondsCommandHandler(ITinkoffService tinkoffService, ApplicationContext context)
    {
        _tinkoffService = _tinkoffService;
        _context = context;
    }

    public async Task Handle(ActualizeBondsCommand request, CancellationToken cancellationToken)
    {
        //var bonds = await _tinkoffService.GetBonds();
        throw new NotImplementedException();
    }
}