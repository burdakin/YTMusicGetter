
namespace YTMusicGetter
{
    using Microsoft.VisualBasic.FileIO;

    public class YTMGService(Logger logger)
    {
        public void Download()
        {
            var helper = new YTMGHelper(logger);

            logger.LogInfo(YTMGConstants.INIT_MSG);
            using TextFieldParser csvResult = helper.ReadFile();

            while (!csvResult.EndOfData)
            {
                if (csvResult.ReadFields().Length == 1) continue;
                string[] fields = csvResult.ReadFields();
                var record = helper.PopulateTrackDTO(fields);
                helper.DownloadUsingProcess(record);
                
                helper.ChangeID3Metadata(record);
            }
        }
    }
}