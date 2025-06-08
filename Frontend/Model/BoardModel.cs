using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class BoardModel
    {
        public string Name { get; }
        public string Owner { get; }

        internal BoardModel(string boardName, string boardOwner)
        {
            Name = boardName;
            Owner = boardOwner;
        }
    }
}
