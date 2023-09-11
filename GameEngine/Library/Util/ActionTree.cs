using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine;

public class ActionNode
{
    public IAction? Action { get; set; }
    public List<IAction> Children { get; set; }

    public ActionNode()
    {
        Action = null;
        Children = new List<IAction>();
    }
}

