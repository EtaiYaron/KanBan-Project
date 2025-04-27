using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade boardFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(BoardService));
        /// <summary>
        /// Empty Constructor for the BoardService class just for now.
        /// </summary>
        public BoardService()
        {
            this.boardFacade = new BoardFacade(new AuthenticationFacade());
        }
        internal BoardService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }


        public string CreateBoard(string name)
        {
            try
            {
                log.Info($"Attempting to create board {name}.");
                BoardBL bbl = boardFacade.CreateBoard(name);
                log.Info($"Board {name} was created successfully.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to create board {name}: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string DeleteBoard(string name)
        {
            try
            {
                log.Info($"Attempting to delete board {name}.");
                BoardBL bbl = boardFacade.DeleteBoard(name);
                log.Info($"Board {name} was deleted successfully.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to delete board {name}: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string GetBoard(string name)
        {
            try
            {
                log.Info($"Attempting to get board {name}.");
                BoardBL bbl = boardFacade.GetBoard(name);
                log.Info($"successfully got board {name}.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to get board {name}: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string LimitTasks(string name, int column, int newLimit)
        {
            try
            {
                log.Info($"Attempting to limit tasks in column {column} on board {name}.");
                BoardBL bbl = boardFacade.LimitTasks(name, column, newLimit);
                log.Info($"Successfully limited tasks in column {column} on board {name}.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to limit tasks in column {column} on board {name}: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public Response GetId(string name)
        {
            throw new NotImplementedException();
        }
    }
}
