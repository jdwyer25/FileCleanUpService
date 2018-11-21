using System;
using System.ServiceProcess;
using FileCleanUp.Business;
using FileCleanUp.Resources;

namespace FileCleanUp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                try
                {
                    var servicesToRun = new ServiceBase[]
                                        {
                                            new FileCleanUpService(),
                                        };
                    ServiceBase.Run(servicesToRun);
                }
                catch (Exception ex)
                {
                    try
                    {
                        var logging = new Logging();
                        logging.WriteEntry(Enums.Enums.LogLevel.Error, $"{System.Reflection.MethodBase.GetCurrentMethod().Name} : {ex.Source} : {ex.Message}");
                    }
                    catch (Exception)
                    {
                        // Nothing else to do
                    }
                }

            }
            else
            {
                var logging = new Logging();
                var fileCleanUp = new Business.FileCleanUp();
                
                try
                {
                    fileCleanUp.RunCleanUp(logging);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Message.ExceptionError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Source, ex.Message);
                }
            }
        }
    }
}
