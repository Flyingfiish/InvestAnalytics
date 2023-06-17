using InvestAnalytics.API.Configuration;
using InvestAnalytics.API.Jobs;
using InvestAnalytics.API.Services.TinkoffService;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInvestApiClient((_, settings) =>
    settings.AccessToken = "t.Q6IBV18r_cJY8QPSemrTdJLWQD92fPiSFng7nKWOzS5hpfPwn_5rD09MBjv0ahh3Qy9kWNcCgMNzztVoQbf8qw");
builder.Services.AddScoped<TinkoffService>();
builder.Services.AddQuartz(quartz =>
{
    quartz.UseMicrosoftDependencyInjectionJobFactory();
    quartz.AddQuartzJob<ActualizeBondsJob>(new JobKey(nameof(ActualizeBondsJob)), trigger => trigger
        .StartAt(DateTime.UtcNow.Date.AddDays(1))
        .WithSimpleSchedule(schedule => schedule
            .WithInterval(TimeSpan.FromDays(1))
            .RepeatForever()));
});
builder.Services.AddQuartzHostedService(quartz => quartz.WaitForJobsToComplete = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();