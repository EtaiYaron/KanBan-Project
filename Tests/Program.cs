using System.Reflection;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net.Config;
using log4net;
using Tests;


internal class Program
{
    private static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        ServiceFactory sf = new ServiceFactory();

        UserTests userTests = new UserTests(sf.UserService);
        userTests.UserRunTests();

        
        BoardTests boardTests = new BoardTests(sf.UserService, sf.BoardService, sf.TaskService);  
        boardTests.BoardRunTests();  

        TaskServiceTests taskTests = new TaskServiceTests(sf.UserService, sf.BoardService, sf.TaskService);  
        taskTests.TaskServiceRunTests();  
    }
}
