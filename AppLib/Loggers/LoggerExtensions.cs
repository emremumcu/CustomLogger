namespace AppLib.Loggers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SGKWeb.Lib.EFCore;
    using System;


    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            builder.Services.Configure(configure);
            builder.Services.AddDbContext<DbLoggerContext>();
            return builder;
        }

        [Obsolete(message:"Use the other overload", error: true)]
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, IConfiguration configuration)
        {
            //builder.AddConfiguration();
            //builder.Services.AddDbContext<LoggingContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevelopmentConnection"), x => x.MigrationsHistoryTable("__LoggingMigrationHistory", "dbo")));
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<ILoggerProvider, DatabaseLoggerProvider>());
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IConfigureOptions<LoggerOptions>, LoggerConfigurationOptions>());
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IOptionsChangeTokenSource<LoggerOptions>, LoggerProviderOptionsChangeTokenSource<LoggerOptions, DatabaseLoggerProvider>>());
            return builder;
        }
    }
}
