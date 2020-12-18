using System;
using System.Collections.Generic;
using System.Text;

namespace RageMpTest
{
    class PlayerModel
    {
        public long Id { get; set; }

        public string PlayerLogin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SocialName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int PlayerHealth { get; set; }

        public int PlayerArmor { get; set; }

        public float[] OldPosition { get; set; }
    }
}
