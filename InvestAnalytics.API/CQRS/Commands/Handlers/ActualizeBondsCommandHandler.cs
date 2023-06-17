using InvestAnalytics.API.Services.TinkoffService;
using MediatR;

namespace InvestAnalytics.API.CQRS.Commands.Handlers;

public class ActualizeBondsCommandHandler : IRequestHandler<ActualizeBondsCommand>
{
    private readonly ITinkoffService _tinkoffService;

    public ActualizeBondsCommandHandler(ITinkoffService tinkoffService)
    {
        _tinkoffService = _tinkoffService;
    }

    public async Task Handle(ActualizeBondsCommand request, CancellationToken cancellationToken)
    {
        var bonds = await _tinkoffService.GetBonds();
        throw new NotImplementedException();
    }
}