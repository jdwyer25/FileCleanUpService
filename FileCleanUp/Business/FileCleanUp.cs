using System;
using System.IO;

namespace FileCleanUp.Business
{
    public class FileCleanUp
    {
        public void RunCleanUp(Logging logging)
        {
            DirectoryInfo dir = new DirectoryInfo(Config.FolderLocation);

            var count = 0;
            foreach (var folder in dir.GetDirectories())
            {
                folder.Delete(true);
                count++;
                /*if (folder.LastWriteTimeUtc.Date < DateTime.Now.Date.AddDays(-30))
                {
                    folder.Delete(true);
                }*/
            }

            logging.WriteEntry(Enums.Enums.LogLevel.Trace, $"Folders Deleted: {count}");
        }
    }
}
