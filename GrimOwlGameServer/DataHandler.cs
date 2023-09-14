using GrimOwlGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameServer;

public class DataHandler
{
    private static DataHandler _instance = null!;
    public static DataHandler Instance => _instance ?? (_instance = new DataHandler());

    public Dictionary<string, int> sessionToToken = new Dictionary<string, int>();
    public Dictionary<int, GrimOwlPlayer> tokenToGrimOwlPlayer = new Dictionary<int, GrimOwlPlayer>();
    public GrimOwlGame Game = TestScenarios.TestScenario1();

    private DataHandler()
    {
    }
}
