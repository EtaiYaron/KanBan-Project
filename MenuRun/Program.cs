using System;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using IntroSE.Kanban.Backend.ServiceLayer;

internal class Program
{
    private static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        ILog log = LogManager.GetLogger(typeof(Program));
        log.Info("Starting Kanban system...");

        Menu m = new Menu();
    }
}
