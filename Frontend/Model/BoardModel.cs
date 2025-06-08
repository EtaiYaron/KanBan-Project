using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class BoardModel
    {
        public String name { get; }
        public String owner { get; }

        internal BoardModel(string boardName, string boardOwner)
        {
            name = boardName;
            owner = boardOwner;
        }
    }
}
