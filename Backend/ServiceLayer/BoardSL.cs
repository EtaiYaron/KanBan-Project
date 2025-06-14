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
        private readonly List<string> joinedUsers;


        internal BoardSL(BoardBL bbl)
        {
            this.boardId = bbl.BoardId;
            this.name = bbl.Name;
            this.owner = bbl.Owner;
            this.joinedUsers = new List<string>(bbl.JoinedUsers);
        }

        [JsonConstructor]
        public BoardSL(int boardId, string name, string owner, List<string> joinedUsers)
        {
            this.boardId = boardId;
            this.name = name;
            this.owner = owner;
            this.joinedUsers = joinedUsers;
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

        public List<string> JoinedUsers
        {
            get { return joinedUsers; }
        }

    }
}
