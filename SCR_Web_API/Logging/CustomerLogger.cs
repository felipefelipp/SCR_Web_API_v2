namespace SCR_Web_API.Logging;

public class CustomerLogger : ILogger
{
    readonly string _loggerName;
    readonly CustomLoggerProviderConfiguration _loggerConfig;

    public CustomerLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        _loggerName = loggerName;
        _loggerConfig = loggerConfig;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _loggerConfig.LogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoDoArquivoLog = @"C:\Users\felip\source\repos\SCR_Web_API\SCR_Web_API\LogHistory\LogHistory.txt";
        using (StreamWriter streamWriter = new StreamWriter(caminhoDoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
