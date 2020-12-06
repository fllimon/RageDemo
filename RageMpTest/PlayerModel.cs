using System;
using System.Collections.Generic;
using System.Text;

using GTANetworkAPI;

namespace RageMpTest
{
    class PlayerModel : Script
    {
        public long Id { get; set; }

        public string PlayerLogin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SocialName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public byte PlayerHealth { get; set; }

        public byte PlayerArmor { get; set; }

    }
}
