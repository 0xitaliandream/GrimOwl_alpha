using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlRiptideServer;

public enum MServer : ushort
{
    ServerHello = 1,
    CommandError,
    GameUpdate,
    GameStarted,
}

public enum MClient : ushort
{
    ClientCommand = 200,
}