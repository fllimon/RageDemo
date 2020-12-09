using System;
using System.Collections.Generic;
using System.Text;

namespace RageMpTest
{
    interface IRageDatabase
    {
        bool GetPlayerLogin(string login);

        bool IsAccountExistBySocialName(string socialName);
        void AddNewAccount(PlayerModel player);

        long GetPlayerIdBySocialName(string socialName);

        void AddPlayerPosition(long id, float x, float y, float z);

        string GetPlayerDataByLogin(string login);

        PlayerModel GetAllPlayerData(string socialName);
    }
}
