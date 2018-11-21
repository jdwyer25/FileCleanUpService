using System;
using System.ServiceProcess;
using System.Threading;
using FileCleanUp.Business;
using FileCleanUp.Resources;

namespace FileCleanUp
{
    public partial class FileCleanUpService : ServiceBase
    {
        private Logging _logging;
        private Business.FileCleanUp _fileCleanUp;
        private Timer _timer;
        public AutoResetEvent AutoResetEvent;
        public Status Status;

        public FileCleanUpService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var mainProcess = new Thread(RunService);
            mainProcess.Start();
        }

        protected override void OnStop()
        {
            try
            {
                _logging.WriteEntry(Enums.Enums.LogLevel.Trace, Message.Stop);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : {ex.Source} : {ex.Message}");
            }
            finally
            {
                _timer?.Dispose();
            }
        }

        protected override void OnContinue()
        {
            try
            {
                base.OnContinue();
                _logging.WriteEntry(Enums.Enums.LogLevel.Trace, Message.Continue);
                _timer.Change(0, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : {ex.Source} : {ex.Message}");
            }
        }

        protected override void OnPause()
        {
            try
            {
                base.OnPause();
                _logging.WriteEntry(Enums.Enums.LogLevel.Trace, Message.Pause);
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : {ex.Source} : {ex.Message}");
            }
        }

        private void RunService()
        {
            try
            {
                _logging = new Logging();
                _fileCleanUp = new Business.FileCleanUp();

                _logging.WriteEntry(Enums.Enums.LogLevel.Trace, Message.Start);

                AutoResetEvent = new AutoResetEvent(false);
                Status = new Status(_logging);

                TimerCallback timerCallback = Status.CheckStatus;
                _timer = new Timer(timerCallback, AutoResetEvent, 0, Timeout.Infinite);

                while (_timer != null)
                {
                    AutoResetEvent.WaitOne();
                    _timer.Change(Config.RunInterval * 60000, Timeout.Infinite);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : {ex.Source} : {ex.Message}");
            }
        }
    }
}
