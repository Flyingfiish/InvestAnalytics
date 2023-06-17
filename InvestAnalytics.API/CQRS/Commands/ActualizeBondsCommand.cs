using InvestAnalytics.API.Dto;
using MediatR;

namespace InvestAnalytics.API.CQRS.Commands;

public record ActualizeBondsCommand() : IRequest;