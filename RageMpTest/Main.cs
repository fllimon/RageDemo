using System;
using GTANetworkAPI;

namespace RageMpTest
{
    class Main : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public void GetPlayerConnected(Player somePlayer)
        {
            Console.WriteLine($"{somePlayer.SocialClubName} connected the server!");
        }
    }
}
