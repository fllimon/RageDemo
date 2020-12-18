using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace RageMpTest
{
    class InteriorModel
    {
        public string CaptionMessage { get; set; }
        public Vector3 EnterPosition { get; set; }
        public Vector3 ExitPosition { get; set; }
        public string IplName { get; set; }
        public int BlipId { get; set; }
        public Blip SomeBlip { get; set; }
        public TextLabel SomeTextLabel { get; set; }
        public string BlipName { get; set; }

        public InteriorModel(string captionMessage, Vector3 enterPosition, Vector3 exitPosition,
                            string iplName, int blipId, string blipName)
        {
            CaptionMessage = captionMessage;
            EnterPosition = enterPosition;
            ExitPosition = exitPosition;
            IplName = iplName;
            BlipId = blipId;
            BlipName = blipName;
        }
    }
}
