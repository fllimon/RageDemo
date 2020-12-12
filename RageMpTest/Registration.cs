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
            if (IsEmptyString(login, firstName, lastName, password, email))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Значения не могут быть пустыми!");

                return;
            }

            if (_db.IsExistAccountByFirstLastName(firstName, lastName))
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Аккаунт с таким Именем и Фамилией уже существует!");

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

            _playerModel.PlayerLogin = login;
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

            string pass = _db.GetPlayerDataByLogin(login);

            if (pass != password)
            {
                NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~r~ Некорректный логин или пароль!");

                return;
            }

            long playerId = _db.GetPlayerIdBySocialName(somePlayer.SocialClubName);

            PlayerModel model  = _db.GetAllPlayerData(somePlayer.SocialClubName);
           
            somePlayer.Position = new Vector3(model.OldPosition[0], model.OldPosition[1], model.OldPosition[2]);
            somePlayer.SetData("Id", model.Id);
            somePlayer.SetData("PlayerModel", model);
            somePlayer.Name = model.FirstName + " " + model.LastName;

            NAPI.Chat.SendChatMessageToPlayer(somePlayer, "~y~ Вы успешно авторизовались");
        }

        [ServerEvent(Event.PlayerSpawn)]
        private void RenderDebugText(Player somePlayer)
        {
            NAPI.ClientEvent.TriggerClientEvent(somePlayer, "DrawPlayerPosition",
                                                $"{somePlayer.Position.X} - {somePlayer.Position.Y} - {somePlayer.Position.Z}",
                                                0.24, 0.97, 255, 255, 255, 1);
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
