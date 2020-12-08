using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace RageMpTest
{
    class Registration : Script
    {
        [Command("registration", Alias = "reg", SensitiveInfo = true)]
        public void GetPlayerRegistration(Player somePlayer, string login, string name, string lastName, string password, string email)
        {
            if (!IsEmptyString(login, name, lastName, password, email))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Значение ня могут быть пустыми!");
            }
        }

        private bool IsEmptyString(params string[] data)
        {
            bool isNotEmpty = false;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null)
                {
                    isNotEmpty = true;

                    break;
                }
            }

            return isNotEmpty;
        } 
    }
}
