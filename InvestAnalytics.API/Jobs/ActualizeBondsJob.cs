using InvestAnalytics.API.CQRS.Commands;
using MediatR;
using Quartz;

namespace InvestAnalytics.API.Jobs;

public class ActualizeBondsJob : IJob
{
    private readonly IMediator _mediator;

    public ActualizeBondsJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _mediator.Send(new ActualizeBondsCommand());
    }
}