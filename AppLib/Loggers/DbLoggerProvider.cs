namespace AppLib.Loggers
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using SGKWeb.Lib.EFCore;
    using System;

    [ProviderAlias("DbLogger")]
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly DbLoggerOptions Options;
        private readonly IServiceProvider Provider;

        public DbLoggerProvider(IOptions<DbLoggerOptions> _options, IServiceProvider _provider)
        {
            Options = _options.Value;
            Provider = _provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            DbLoggerContext dbLoggerContext;

            // Ex: Cannot resolve scoped service from root provider
            // var service = Provider.GetService(typeof(CMSDbContext));
            // https://medium.com/volosoft/asp-net-core-dependency-injection-best-practices-tips-tricks-c6e9c67f9d96
            // https://stackoverflow.com/questions/48590579/cannot-resolve-scoped-service-from-root-provider-net-core-2
            // For anybody trying to access EF Contexts in middleware this is the way to go as they are scoped by default
            // may be NOT the best solution consider improving the code
            var scope = Provider.CreateScope();            
            dbLoggerContext = scope.ServiceProvider.GetRequiredService<DbLoggerContext>();                            

            return new DbLogger(this, dbLoggerContext);
        }

        public void Dispose()
        {
            // ...
        }
    }
}
