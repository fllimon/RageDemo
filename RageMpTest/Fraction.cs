using System;
using System.Collections.Generic;
using System.Text;

using GTANetworkAPI;

namespace RageMpTest
{
    abstract class Fraction : Script
    {
        [Command("r")]
        protected abstract void GetFravtionChat(Player somePlayer, string message);

        [Command("rank")]
        protected abstract void GiveRankPlayer(Player somePlayer, string firstName, string lastName);

        [Command("uninvite")]
        protected abstract void UninvitePlayer(Player somePlayer, string firstName, string lastName);

        [Command("invite")]
        protected abstract void InvitePlayer(Player somePlayer, string firstName, string lastName);
    }
}
