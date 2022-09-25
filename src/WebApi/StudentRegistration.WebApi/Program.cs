using Serilog;
using StudentRegistration.Application;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Infrastructure;
using StudentRegistration.Infrastructure.Repositories;
using StudentRegistration.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
     //.WriteTo.File(new CompactJsonFormatter(), "log.txt", rollingInterval: RollingInterval.Day)
     .WriteTo.File("log.txt", outputTemplate:
        "----------------------------------{NewLine}" +
        "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {NewLine}" +
        "CorrelationId: {CorrelationId} {NewLine} " +
        "{Message:lj}{NewLine}" +
        "{Exception}" +
        "----------------------------------{NewLine}",
        rollingInterval: RollingInterval.Day)
     .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
     .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
     .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
     .Enrich.FromLogContext()
     .CreateLogger();

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddScoped<ITermRepository,TermRepository>();

builder.Services.AddControllers();
builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(builder.Configuration);
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

app.UseMiddleware<RequestContextLogMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
