using InvestAnalytics.API.Jobs;
using Quartz;

namespace InvestAnalytics.API.Configuration;

public static class ServiceCollectionExtentions
{
    public static IServiceCollectionQuartzConfigurator AddQuartzJob<T>(this IServiceCollectionQuartzConfigurator quartz,
        JobKey? jobKey = null,
        Action<ITriggerConfigurator>? triggerConfigurator = null) where T : IJob
    {
        jobKey ??= new JobKey(nameof(T));
        quartz.AddJob<T>(opt =>
            opt.WithIdentity(jobKey));
        quartz.AddTrigger(opt =>
        {
            opt.ForJob(jobKey);
            triggerConfigurator?.Invoke(opt);
        });
        return quartz;
    }

    public static IServiceCollectionQuartzConfigurator AddQuartzJob<T>(this IServiceCollectionQuartzConfigurator quartz,
        Action<ITriggerConfigurator>? triggerConfigurator = null) where T : IJob
    {
        quartz.AddQuartzJob<T>(new JobKey(nameof(T)), triggerConfigurator);
        return quartz;
    }
}