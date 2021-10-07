namespace AppLib.Loggers
{
    using AppLib.Entities;
    using Microsoft.Extensions.Logging;
    using SGKWeb.Lib.EFCore;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class DbLogger : ILogger
    {
        protected readonly DbLoggerProvider _dbLoggerProvider;
        private readonly DbLoggerContext _context;
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider, DbLoggerContext context)
        {
            _dbLoggerProvider = dbLoggerProvider;
            _context = context;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            try
            {
                // return logLevel != LogLevel.None;            
                LogLevel configLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), _dbLoggerProvider.Options.Level);            
                return (int)logLevel >= (int)configLogLevel;
            }
            catch
            {
                return true;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Write log to Database

            InternalLog log = new InternalLog()
            {
                LogLevel = logLevel.ToString(),
                EventId = eventId.ToString(),
                State = state.ToString(),
                ExceptionMessage = exception?.Message,
                InnerExceptionMessage = exception?.InnerException?.Message
            };
            
            _context.InternalLogs.Add(log);
            _context.SaveChanges();
        }
    }
}

/// Log levels
/// ----------
/// Trace/Verbose:  Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment.
/// Debug:          Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value.
/// Information:    Logs that track the general flow of the application. These logs should have long-term value.
/// Warning:        Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.
/// Error:          Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.
/// Critical:       Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.


/*
 Singleton – they’re created first time they’re request, and every time after that the same instance will be reused
Scoped – they’re created once per the request (connection)
Transient are created every time you request them from the DI container

You registered the IEmailRepository as a scoped service, in the Startup class. This means that you can not inject it as a constructor parameter in Middleware because only Singleton services can be resolved by constructor injection in Middleware.
 */

/*
 
 Middleware is always a singleton so you can't have scoped dependencies as constructor dependencies in the constructor of your middleware.

Middleware supports method injection on the Invoke method,so you can just add the IEmailRepository emailRepository as a parameter to that method and it will be injected there and will be fine as scoped.

public async Task Invoke(HttpContext context, IEmailRepository emailRepository)
{

    ....
}

public ExceptionHandlingMiddleware(RequestDelegate next)
{
    _next = next;
}

public async Task Invoke(HttpContext context, IEmailRepository emailRepository)
{
    try
    {
        await _next.Invoke(context);
    }
    catch (Exception ex)
    {
        await HandleExceptionAsync(context, ex, emailRepository);
    }
}
 
 */