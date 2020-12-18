using System;
using GTANetworkAPI;

namespace RageMpTest
{
    class Main : Script
    {
        private IRageDatabase _db = new RageDatabase();

        [ServerEvent(Event.ResourceStart)]
        public void GetResourseStart()
        {
            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetGlobalServerChat(false);

            foreach (InteriorModel interior in DefaultSettings.DEFAULT_INTERIOR_LIST)
            {
                if (interior.BlipId > 0)
                {
                    interior.SomeBlip = NAPI.Blip.CreateBlip(interior.EnterPosition);
                    interior.SomeBlip.Sprite = (uint)interior.BlipId;
                    interior.SomeBlip.Name = interior.BlipName;
                    interior.SomeBlip.ShortRange = true;
                }

                if (interior.CaptionMessage != string.Empty)
                {
                    interior.SomeTextLabel = NAPI.TextLabel.CreateTextLabel(interior.CaptionMessage, interior.EnterPosition,
                                                                            20.0f, 0.75f, 4, new Color(255, 255, 255), false, 0);
                }
            }
        }

        [ServerEvent(Event.PlayerConnected)]
        public void GetPlayerConnected(Player somePlayer)
        {
            Console.WriteLine($"Player {somePlayer.SocialClubName} connected the server!");
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void GetPlayerDisconnect(Player somePlayer, DisconnectionType type, string reason)
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
