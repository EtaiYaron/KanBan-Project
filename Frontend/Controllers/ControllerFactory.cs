using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Frontend.Controllers
{
    internal class ControllerFactory
    {
        public static ControllerFactory Instance { get; } = new ControllerFactory();
        public readonly UserController UserController;

        private ControllerFactory()
        {
            ServiceFactory serviceFactory = new ServiceFactory();
            UserController = new UserController(serviceFactory.UserService);
        }
    }
}
