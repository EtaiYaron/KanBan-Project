using IntroSE.Kanban.Backend.ServiceLayer;
using Tests;

internal class Program
{
    private static void Main(string[] args)
    {
        ServiceFactory sf = new ServiceFactory();

        UserTests userTests = new UserTests(sf.UserService);
        userTests.UserRunTests();

        BoardTests boardTests = new BoardTests(sf.UserService, sf.BoardService, sf.TaskService);
        boardTests.BoardRunTests();

        TaskServiceTests taskTests = new TaskServiceTests(sf.UserService, sf.BoardService, sf.TaskService);
        taskTests.TaskServiceRunTests();
    }
}