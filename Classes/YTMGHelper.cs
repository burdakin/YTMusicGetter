using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace YTMusicGetter
{
    public class YTMGHelper(Logger logger)
    {
        public TextFieldParser ReadFile()
        {
            TextFieldParser parser = new TextFieldParser(YTMGConstants.INITIAL_LIST);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            return parser;
        }

        public void DownloadUsingProcess(YouTubeTrackRecordDTO record)
        {
            var processInfo = new ProcessStartInfo(YTMGConstants.YTDL_PATH)
            {
                Arguments = GetArguments(record),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var process = Process.Start(processInfo);
            Console.WriteLine(YTMGConstants.START_DWNLD__MSG + record.Artist + " " + record.Title + " " + record.URL);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (e.Data == null) return;
                logger.LogError(e.Data + " " + record.Artist + " " + record.Title);
                Console.WriteLine("error>>" + e.Data);
            };
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            if (process.ExitCode == 0) Console.WriteLine(record.Artist + " " + record.Title + " downloaded" + 
            "-------------------------");
            process.Close();
        }

        public YouTubeTrackRecordDTO PopulateTrackDTO(string[] fields)
        {
            return new YouTubeTrackRecordDTO
            {
                URL = fields[0],
                Title = fields[1],
                Artist = fields[3],
                Album = fields[2]
            };
        }

        public void ChangeID3Metadata(YouTubeTrackRecordDTO track)
        {
            TagLib.File tfile;
            try
            {
                tfile = TagLib.File.Create(track.TrackPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            tfile.Tag.Title = track.Title;
            tfile.Tag.Album = track.Album;
            tfile.Tag.Performers = new[] { track.Artist };
            tfile.Save();
        }

        private string GetOutputPath(YouTubeTrackRecordDTO record)
        {
            var path = string.Format(YTMGConstants.OUTPUT_ARG,
                Regex.Replace(record.Title, "[^0-9A-Za-zА-Яа-яЁё ,]", "").Replace(" ", "_") + "-" +
                Regex.Replace(record.Artist, "[^0-9A-Za-zА-Яа-яЁё ,]", "").Replace(" ", "_"));

            record.TrackPath = path;

            return path;
        }

        private string GetArguments(YouTubeTrackRecordDTO record)
        {
            return " " + record.URL + " " +
                   YTMGConstants.FORMAT_ARG + " " +
                   "-o " + GetOutputPath(record) + " " +
                   YTMGConstants.FFMPEG_ARG + " " +
                   YTMGConstants.EXPORT_ONLY_MUSIC_ARG + " " +
                   YTMGConstants.QUALITY_ARG + " " +
                   YTMGConstants.MUS_FORMAT_ARG;
        }
    }
}