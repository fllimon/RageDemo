using System;
using GTANetworkAPI;

namespace RageMpTest
{
    class Main : Script
    {
        private IRageDatabase _db = new RageDatabase();

        [ServerEvent(Event.PlayerConnected)]
        public void GetPlayerConnected(Player somePlayer)
        {
            Console.WriteLine($"Player {somePlayer.SocialClubName} connected the server!");
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void GetPlayerDisconnect(Player somePlayer, DisconnectionType type)
        {
            if (somePlayer.HasData("Id") == false)
            {
                return;
            }

            PlayerModel model = somePlayer.GetData<PlayerModel>("PlayerModel");
            model.OldPosition = new float[] { somePlayer.Position.X, somePlayer.Position.Y, somePlayer.Position.Z };

            long playerId = _db.GetPlayerIdBySocialName(somePlayer.SocialClubName);
            _db.UpdatePlayerPosition(playerId, somePlayer.Position.X, somePlayer.Position.Y, somePlayer.Position.Z);
        }
    }
}
