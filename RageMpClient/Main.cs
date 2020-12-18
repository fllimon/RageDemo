using System;
using System.Drawing;
using System.Collections.Generic;

using RAGE;
using RAGE.Elements;

namespace RageMpClient
{
    class Main : Events.Script
    {
        private readonly string _playerMoney;
        private Blip _fractionBlip = null;

        public Main()
        {
            Events.Tick += GetTickEvent;
        }

        private void GetTickEvent(List<Events.TickNametagData> nametags)
        {
            RAGE.NUI.UIResText.Draw(_playerMoney, 1900, 60, RAGE.Game.Font.Pricedown,
                                    0.5f, Color.DarkOliveGreen, RAGE.NUI.UIResText.Alignment.Right,
                                    true, true, 0);
        }

        //private void DrawPoliceBlipOnMap(object[] args)
        //{
        //    _fractionBlip = new Blip((uint)args[0], (Vector3)args[1], (string)args[2], (float)args[3], 
        //                             (int)args[4], (int)args[5], (float)args[6], (bool)args[7],
        //                             (int)args[8], (float)args[9], (uint)args[10]);
        //}
    }
}
