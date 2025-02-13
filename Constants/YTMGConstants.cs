namespace YTMusicGetter
{
    public static class YTMGConstants
    {
        public static readonly string FFMPEG_ARG = "--ffmpeg-location ../../../Apps/ffmpeg-master-latest-win64-gpl-shared/bin/ffmpeg.exe";
        public static readonly string YTDL_PATH = "../../../Apps/yt-dlp.exe";
        public static readonly string INITIAL_LIST = "../../../Files/InitialList.csv";
        public static readonly string OUTPUT_ARG = "../../../Output\\{0}.mp3";
        public static readonly string FORMAT_ARG = "-f mp4";
        public static readonly string EXPORT_ONLY_MUSIC_ARG = "-x";
        public static readonly string QUALITY_ARG = "--audio-quality 0";
        public static readonly string MUS_FORMAT_ARG = "--audio-format mp3";
        public static readonly string START_DWNLD__MSG = "Starting download for: ";
        public static readonly string STARTUP_MSG = "Starting download for: ";
        public static readonly string INIT_MSG = "Getting Tracks from initial list";
    }
}