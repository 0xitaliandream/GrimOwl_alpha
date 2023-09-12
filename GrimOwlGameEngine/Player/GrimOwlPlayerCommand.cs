using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;

public class GrimOwlPlayerCommand
{
    public GrimOwlPlayerCommand()
    {
    }

    public GrimOwlPlayerCommand(GrimOwlPlayer player, string command)
    {
        this.Player = player;
        this.Command = command;
    }

    public string Command { get; set; } = null!;
    public GrimOwlPlayer Player { get; set; } = null!;
}
