using System;
using System.IO;

namespace FileCleanUp.Business
{
    public class FileCleanUp
    {
        public void RunCleanUp(Logging logging)
        {
            DirectoryInfo dir = new DirectoryInfo(Config.FolderLocation);
            Int32.TryParse(Config.NumberOfDaysOld, out int numberOfDaysOld);

            var count = 0;
            foreach (var folder in dir.GetDirectories())
            {
                count++;
                if (folder.LastWriteTimeUtc.Date < DateTime.Now.Date.AddDays(-1 * numberOfDaysOld))
                {
                    folder.Delete(true);
                }
            }

            logging.WriteEntry(Enums.Enums.LogLevel.Trace, $"Folders Deleted: {count}");
        }
    }
}
