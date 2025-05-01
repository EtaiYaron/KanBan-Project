using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardSL
    {
        private readonly string name;
        private readonly int maxTasks0;
        private readonly int maxTasks1;
        private readonly int maxTasks2;
        public BoardSL(BoardBL bbl)
        {
            this.name = bbl.Name;
            this.maxTasks0 = bbl.MaxTasks0;
            this.maxTasks1 = bbl.MaxTasks1;
            this.maxTasks2 = bbl.MaxTasks2;
        }
    }
}
