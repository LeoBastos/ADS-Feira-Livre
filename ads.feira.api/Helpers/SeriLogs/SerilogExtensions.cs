using Serilog;

namespace ads.feira.api.Helpers.SeriLogs
{
    public static class SerilogExtensions
    {
        public static void SerilogConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
