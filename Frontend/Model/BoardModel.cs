using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class BoardModel
    {
        public string Name { get; }
        public string Owner { get; }

        internal BoardModel(BoardSL board)
        {
            Name = board.Name;
            Owner = board.Owner;
        }

        internal BoardModel(string boardName, string owner)
        {
            Name = boardName;
            Owner = owner;
        }
    }
}
