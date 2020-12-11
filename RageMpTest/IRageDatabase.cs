using System;
using System.Collections.Generic;
using System.Text;

namespace RageMpTest
{
    interface IRageDatabase
    {
        bool UpdatePlayerPosition(long id, float x, float y, float z);

        bool GetPlayerLogin(string login);

        bool IsExistAccountByFirstLastName(string firstName, string lastName);

        bool IsAccountExistBySocialName(string socialName);

        void AddNewAccount(PlayerModel player);

        long GetPlayerIdBySocialName(string socialName);

        string GetPlayerDataByLogin(string login);

        PlayerModel GetAllPlayerData(string socialName);
    }
}
