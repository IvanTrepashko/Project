using System.IO;
using log4net;
using log4net.Config;


namespace FinancialAssistant
{
    class Logger
    {
        public static ILog Log { get; set; }
        public static void CreateLog()
        {
            var logRepos = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepos, new FileInfo("log4net.config"));
            var logger = LogManager.GetLogger(typeof(Logger));
            Directory.CreateDirectory(@"..\..\..\logs");
            Log = logger;
        }
    }
}