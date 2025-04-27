using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ServiceFactory
    {
        private UserService userService;
        private BoardService boardService;
        private TaskService taskService;

        public ServiceFactory()
        {
            AuthenticationFacade authenticationFacade = new AuthenticationFacade();
            UserFacade userFacade = new UserFacade(authenticationFacade);
            BoardFacade boardFacade = new BoardFacade(authenticationFacade);

            this.userService = new UserService(userFacade);
            this.boardService = new BoardService(boardFacade);
            this.taskService = new TaskService(boardFacade);
        }

        public UserService UserService {
            get { return this.userService; }}

        public BoardService BoardService { 
            get { return this.boardService; }}

        public TaskService TaskService { 
            get { return this.taskService; }}
    }
}
