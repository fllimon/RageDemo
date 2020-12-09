using System;
using System.Collections.Generic;
using System.Text;

namespace RageMpTest
{
    interface IRageDatabase
    {
        bool GetPlayerLogin(string login);

        bool IsAccountExistBySocialName(string socialName);
        void AddNewAccount(PlayerModel player /*string login, string firstName, string lastName, string email, string socialName, string password*/);

        long GetPlayerIdBySocialName(string socialName);

        void AddPlayerPosition(long id, float x, float y, float z);

        PlayerModel GetPlayerDataByLogin(string login);
    }
}
