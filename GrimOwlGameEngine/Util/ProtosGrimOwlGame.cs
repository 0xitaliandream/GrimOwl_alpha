using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;


[ProtoContract]
public class ProtoUpdateGrimOwlGame
{

    public ProtoUpdateGrimOwlGame()
    {

    }

    public ProtoUpdateGrimOwlGame(GrimOwlGame game)
    {
        test = game.test;
    }

    [ProtoMember(1)]
    public int test { get; set; }
}
