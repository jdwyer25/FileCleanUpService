using System;
using System.Configuration;

namespace FileCleanUp.Business
{
    internal class Config
    {
        public static Enums.Enums.LogLevel LogLevel
        {
            get
            {
                var result = Enum.TryParse(ConfigurationManager.AppSettings["LogLevel"], out Enums.Enums.LogLevel logLevel);

                if (result == false)
                {
                    throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : App config 'LogLevel' invalid.  Must be string value Off, Error, or Trace");
                }
                return logLevel;
            }
        }

        public static Enums.Enums.Mode Mode
        {
            get
            {
                var result = Enum.TryParse(ConfigurationManager.AppSettings["Mode"], out Enums.Enums.Mode mode);

                if (result == false)
                {
                    throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : App.configu 'Mode' invalid.  Must be string value of Dev or Prod.");
                }
                return mode;
            }
        }

        public static int RunInterval
        {
            get
            {
                var result = int.TryParse(ConfigurationManager.AppSettings["RunInterval"], out int runInterval);
                if (!result || runInterval < 0)
                {
                    throw new ApplicationException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} : App.config 'RunInterval' invalid.  Must be an integer greater than 0.");
                }
                return runInterval;
            }
        }

        public static string FolderLocation => ConfigurationManager.AppSettings["FolderLocation"];

        public static string NumberOfDaysOld => ConfigurationManager.AppSettings["NumberOfDaysOld"];
    }
}
