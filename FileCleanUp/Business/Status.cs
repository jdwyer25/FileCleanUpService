using System.Threading;
using FileCleanUp.Resources;

namespace FileCleanUp.Business
{
    public class Status
    {
        private readonly object _lockObject = new object();
        private readonly Logging _logging;

        public Status(Logging logging)
        {
            _logging = logging;
        }

        public void CheckStatus(object stateInfo)
        {
            if (!Monitor.TryEnter(_lockObject))
            {
                return;
            }

            try
            {
                var autoResetEvent = (AutoResetEvent)stateInfo;
                autoResetEvent.Set();
            }
            finally
            {
                _logging.WriteEntry(Enums.Enums.LogLevel.Error, Error.Error_StatusClass);
                Monitor.Exit(_lockObject);
            }
        }
    }
}
