using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace RageMpTest
{
    class Registration : Script
    {
        private PlayerModel _playerModel = null;
        private IRageDatabase _db = new RageDatabase();

        [Command("registration", Alias = "reg", SensitiveInfo = true)]
        public void GetPlayerRegistration(Player somePlayer, string login, string firstName, string lastName, string password, string email)
        {
            if (!IsEmptyString(login, firstName, lastName, password, email))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Значения не могут быть пустыми!");

                return;
            }

            if (_db.GetPlayerLogin(login))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~Пользователь с таким логином уже существует!");

                return;
            }

            if (_db.IsAccountExistBySocialName(somePlayer.SocialClubName))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~Пользователь с таким SocialClub уже существует!");

                return;
            }

            _playerModel = new PlayerModel();

            _playerModel.FirstName = firstName;
            _playerModel.LastName = lastName;
            _playerModel.Email = email;
            _playerModel.Password = password;
            _playerModel.SocialName = somePlayer.SocialClubName;
            _playerModel.PlayerHealth = somePlayer.Health;
            _playerModel.PlayerArmor = somePlayer.Armor;

            somePlayer.SetData("PlayerModel", _playerModel);
            _db.AddNewAccount(_playerModel);
           
            NAPI.Notification.SendNotificationToPlayer(somePlayer, "~y~ Вы успешно зарегистрировались! Авторизуйтесь для продолжения игры!");
        }

        [Command("login", Alias ="log", SensitiveInfo = true)]
        public void GetPlayerLogin(Player somePlayer, string login, string password)
        {
            if (IsEmptyString(login, password))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Параметры не могут быть пустыми!");

                return;
            }

            if (somePlayer.HasData("Id"))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Игрок уже авторизован!");

                return;
            }

            PlayerModel model = _db.GetPlayerDataByLogin(login);

            if (model != null)
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Пользователя не существует!");

                return;
            }

            if (model.Password != password)
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Некорректный логин или пароль!");

                return;
            }

            somePlayer.Position = new Vector3(model.OldPosition[0], model.OldPosition[1], model.OldPosition[2]);
            somePlayer.SetData("Id", model.Id);
            somePlayer.SetData("PlayerModel", model);
            NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~y~ Вы успешно авторизовались");
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
