namespace YTMusicGetter;

using Microsoft.Extensions.Logging;
using NReco.Logging.File;

public class Logger
{
    private readonly ILogger<Program> logger;
    private readonly ILoggerFactory loggerFactory;

    public Logger()
    {
        loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFile(GetLogsPath());
        });

        logger = loggerFactory.CreateLogger<Program>();
    }
    
    public void LogError(string msg)
    {
        Console.WriteLine(msg);
        logger.LogError(msg);
    }
    
    public void LogInfo(string msg)
    {
        Console.WriteLine(msg);
        logger.LogInformation(msg);
        logger.Log(LogLevel.Trace, msg);
    }

    private string GetLogsPath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        for (int i = 0; i < 4; i++)
        {
            baseDirectory = Directory.GetParent(baseDirectory)?.FullName ?? baseDirectory;
        }

        string logDirectory = Path.Combine(baseDirectory, "Logs");
        Directory.CreateDirectory(logDirectory);

        string logFileName = $"Failed-downloads-log{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        return Path.Combine(logDirectory, logFileName);
    }
    
    public void Dispose()
    {
        loggerFactory.Dispose();
    }
}