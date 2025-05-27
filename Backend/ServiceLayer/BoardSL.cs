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
        private readonly int boardId;
        private readonly string name;
        private readonly string owner;
        private readonly int maxTasks0;
        private readonly int maxTasks1;
        private readonly int maxTasks2;


        internal BoardSL(BoardBL bbl)
        {
            this.boardId = bbl.BoardId;
            this.name = bbl.Name;
            this.owner = bbl.Owner;
            this.maxTasks0 = bbl.MaxTasks0;
            this.maxTasks1 = bbl.MaxTasks1;
            this.maxTasks2 = bbl.MaxTasks2;
        }

        public int BoardId
        {
            get { return boardId; }
        }

        public string Owner
        {
            get { return owner; }
        }

        public string Name
        {
            get { return name; }
        }

        public int MaxTasks0
        {
            get { return maxTasks0; }
        }

        public int MaxTasks1
        {
            get { return maxTasks1; }
        }

        public int MaxTasks2
        {
            get { return maxTasks2; }
        }
    }
}
