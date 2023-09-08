using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlRiptideClient;



public enum MConnection : ushort
{
    ServerHello = 1,
}

public enum MNotificator : ushort
{
    GameState = 100,
    GameStarted,
}

public enum MClient : ushort
{
    ClientCommand = 200,
}

public enum MCommand : ushort
{
    Summon = 1000,
    EndsTurn,
    Move,
    Attack,
}