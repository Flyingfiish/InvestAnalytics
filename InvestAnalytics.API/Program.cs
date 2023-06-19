using System.Reflection;
using InvestAnalytics.API.Configuration;
using InvestAnalytics.API.CQRS.Commands;
using InvestAnalytics.API.CQRS.Commands.Handlers;
using InvestAnalytics.API.Db;
using InvestAnalytics.API.Jobs;
using InvestAnalytics.API.Services.TinkoffService;
using MediatR;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json",
    optional: false,
    reloadOnChange: true);

// Add services to the container.

//Db
builder.Services.AddDbContext<ApplicationContext>();

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IRequestHandler<ActualizeBondsCommand>, ActualizeBondsCommandHandler>();

//Services
builder.Services.AddScoped<ITinkoffService, TinkoffService>();

//Tinkoff
builder.Services.AddInvestApiClient((_, settings) => settings.AccessToken = builder.Configuration["tinkoffApiKey"]);

//Quartz
builder.Services.AddQuartz(quartz =>
{
    quartz.UseMicrosoftDependencyInjectionJobFactory();
    quartz.AddQuartzJob<ActualizeBondsJob>(trigger => trigger
        .StartAt(DateTime.UtcNow.Date.AddDays(1))
        .WithSimpleSchedule(schedule => schedule
            .WithInterval(TimeSpan.FromDays(1))
            .RepeatForever())
        .StartNow());
});
builder.Services.AddQuartzHostedService(quartz => quartz.WaitForJobsToComplete = true);

//...

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