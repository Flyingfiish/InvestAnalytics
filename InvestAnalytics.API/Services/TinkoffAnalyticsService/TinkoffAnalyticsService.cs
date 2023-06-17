using InvestAnalytics.API.Services.TinkoffService;

namespace InvestAnalytics.API.Services.TinkoffAnalyticsService
{
    public class TinkoffAnalyticsService : ITinkoffAnalyticsService
    {
        private readonly ITinkoffService _tinkoffService;
        public TinkoffAnalyticsService(ITinkoffService tinkoffService)
        {
            _tinkoffService = tinkoffService;
        }
    }
}
