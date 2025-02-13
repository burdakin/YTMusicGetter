using YTMusicGetter;

Logger logger = new Logger();

logger.LogInfo(YTMGConstants.STARTUP_MSG);
new YTMGService(logger).Download();
Console.ReadKey();