using Serilog;

namespace MyTrainingSheet.Api
{
    public static class LoggerFactory
    {
        public static Serilog.ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .WriteTo
                .Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }

}
