using InvestAnalytics.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInvestApiClient((_, settings) => settings.AccessToken = "t.BlnZPMkPE1UmznuwnAFGS94-7ItMENIcoCp4xt0I97M6MvAbHlqB_SO5VfsFWSLbBIo8x1b-R9tPD7Hxs7XMvg");
builder.Services.AddScoped<TinkoffService>();

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
