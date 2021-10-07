#if PROD
#warning "Compiling for PROD environment"
#endif

namespace CustomLogger
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using AppLib.Loggers;

    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            /// IServiceScope scope = host.Services.CreateScope();
            /// IServiceProvider services = scope.ServiceProvider;
            /// IWebHostEnvironment environment = services.GetRequiredService<IWebHostEnvironment>();

            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false)
                .AddCommandLine(args)
                .Build();

            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"{System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace} has been started");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostBuilderContext, logging) =>
                {
#if PROD
                    // Clear the following providers registered by Host.CreateDefaultBuilder method:
                    // logging.AddConsole(), logging.AddDebug(), logging.AddEventSourceLogger()                     
                    logging.ClearProviders();
#endif                    
                    logging.AddConsole();
                    // logging.AddDbLogger(hostBuilderContext.Configuration);
                    logging.AddDbLogger((options =>
                    {
                        hostBuilderContext.Configuration.GetSection("Logging").GetSection("DbLogging").GetSection("Options").Bind(options);
                    }));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
