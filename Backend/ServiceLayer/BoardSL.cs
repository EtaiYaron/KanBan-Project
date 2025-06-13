using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardSL
    {
        private readonly int boardId;
        private readonly string name;
        private readonly string owner;


        internal BoardSL(BoardBL bbl)
        {
            this.boardId = bbl.BoardId;
            this.name = bbl.Name;
            this.owner = bbl.Owner;
        }

        [JsonConstructor]
        public BoardSL(int boardId, string name, string owner)
        {
            this.boardId = boardId;
            this.name = name;
            this.owner = owner;
        }

        public BoardSL() { }

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

    }
}
