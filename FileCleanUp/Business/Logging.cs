using System;
using System.Diagnostics;

namespace FileCleanUp.Business
{
    public class Logging : IDisposable
    {
        private readonly EventLog _eventLog;
        private bool _disposed = false;

        public Logging()
        {
            _eventLog = new EventLog
                        {
                            Source = "Application",
                            Log = "Application"
                        };
        }

        public void WriteEntry(Enums.Enums.LogLevel logLevel, string message)
        {
            if (logLevel > Config.LogLevel)
            {
                return;
            }

            _eventLog.WriteEntry(message);

            if (Environment.UserInteractive)
            {
                Console.WriteLine(message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                return;
            }

            if (disposing)
            {
                _eventLog.Dispose();
            }

            _disposed = true;
        }
    }
}
