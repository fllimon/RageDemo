using System;
using System.Collections.Generic;
using System.Text;

using GTANetworkAPI;

namespace RageMpTest
{
    class Command : Script
    {
        [Command("position", Alias = "pos")]
        public void GetPlayerPosition(Player somePlayer)
        {
            NAPI.Chat.SendChatMessageToPlayer(somePlayer, $"X:{somePlayer.Position.X} Y:{somePlayer.Position.Y} Z:{somePlayer.Position.Z}");
        }

        [Command("kick")]
        public void KickPlayer(Player somePlayer)
        {
            somePlayer.Kick();
            Console.WriteLine($"Player {somePlayer.Name} has kicked");
        }
    }
}
