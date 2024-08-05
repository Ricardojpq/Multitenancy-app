using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace WebUtils.Configuration
{
    public class Logger
    {
        public static LoggerConfiguration GetLoggerConfiguration(
            bool useAppSettings = true,
            LogEventLevel aspNetCoreLogLevel = LogEventLevel.Warning)
        {
            var loggerConfiguration = new LoggerConfiguration();

            if (useAppSettings)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                
                loggerConfiguration = loggerConfiguration.ReadFrom.Configuration(configuration);
            }

            loggerConfiguration = loggerConfiguration
                .MinimumLevel.Override("Microsoft.AspNetCore", aspNetCoreLogLevel)
                .Enrich.FromLogContext()
                .WriteTo.Console();

            return loggerConfiguration;
        }
    }
}