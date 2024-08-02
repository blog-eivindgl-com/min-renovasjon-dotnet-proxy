
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Options;
using MinRenovasjonProxy.Core.Configuration;
using MinRenovasjonProxy.Services;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace MinRenovasjonProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Console.WriteLine($"ASPNETCORE_ENVIRONMENT={System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            // Add services to the container.
            var meterProvider = Sdk.CreateMeterProviderBuilder()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddPrometheusExporter()
                .Build();
            builder.Services.AddSingleton(meterProvider);
            builder.Services.AddHttpClient();
            builder.Services.AddMemoryCache();
            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables(prefix: "MinRenovasjonProxy__");
            builder.Logging
                .AddConfiguration(builder.Configuration.GetSection("Logging"))
                .AddConsole();
            builder.Services.Configure<NorkartRenovasjonConfiguration>(builder.Configuration.GetSection(NorkartRenovasjonConfiguration.ConfigSection));
            builder.Services.AddSingleton<INorkartRenovasjonApiService, NorkartRenovasjonApiRestService>();
            builder.Services.AddSingleton<IHentekalenderService, HentekalenderService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                LogConfigValues(app);
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseOpenTelemetryPrometheusScrapingEndpoint();

            app.Run();
        }

        private static void LogConfigValues(WebApplication app)
        {
            var confOptions = app.Services.GetRequiredService<IOptions<NorkartRenovasjonConfiguration>>();
            var confLogger = app.Services.GetRequiredService<ILogger<NorkartRenovasjonConfiguration>>();
            confLogger.LogDebug($"AppKey: {confOptions.Value.AppKey}");
            confLogger.LogDebug($"Kommunenr: {confOptions.Value.Kommunenr}");
            confLogger.LogDebug($"Gatekode: {confOptions.Value.Gatekode}");
            confLogger.LogDebug($"Gatenavn: {confOptions.Value.Gatenavn}");
            confLogger.LogDebug($"Husnr: {confOptions.Value.Husnr}");
        }
    }
}
