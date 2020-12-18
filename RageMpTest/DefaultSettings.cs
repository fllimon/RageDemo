using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace RageMpTest
{
    class DefaultSettings : Script
    {
        public const string DEFAULT_POLICE_NAME = "Los Santos Police Department";
        public const uint DEFAULT_POLICE_SPRITE = 60;
        public const float DEFAULT_BLIP_SCALE = 2.0f;

        public static List<InteriorModel> DEFAULT_INTERIOR_LIST = new List<InteriorModel>
        {
            new InteriorModel(DEFAULT_POLICE_NAME, new Vector3(-1111.952f, -824.9194f, 19.31578f), new Vector3(435.4738f, -981.7497f, 30.69148f), string.Empty, 60, DEFAULT_POLICE_NAME)
        };
    }
}
