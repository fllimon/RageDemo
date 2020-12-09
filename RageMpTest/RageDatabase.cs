using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using GTANetworkAPI;

namespace RageMpTest
{
    class RageDatabase : Script, IRageDatabase
    {
        private readonly string _connectionString = "Data Source=(local);Initial Catalog = RageMpTest; Integrated Security = True";
        private SqlConnection _sqlConnection = null;

        public RageDatabase()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        public PlayerModel GetPlayerDataByLogin(string login)
        {
            PlayerModel player = null;

            try
            {
                string sqlCommand = "SELECT PlayerLogin, UserPassword FROM Accounts WHERE PlayerLogin LIKE @login";
                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter loginParameter = new SqlParameter("@login", login);
                AddNewParameters(command, loginParameter);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        player.PlayerLogin = reader[0].ToString();
                        player.Password = reader[1].ToString();
                    }
                }

                return player;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool UpdatePlayerPosition(long id, float x, float y, float z)
        {
            bool isUpdate = false;
            try
            {
                string sqlCommand = "UPDATE PlayersPositions SET PositionX = @x, PositionY = @y, PositionZ = @z WHERE AccountId = @id";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter xParameter = new SqlParameter("@x", x);
                SqlParameter yParameter = new SqlParameter("@y", y);
                SqlParameter zParameter = new SqlParameter("@z", z);
                SqlParameter accountIdParameter = new SqlParameter("@accountId", id);

                AddNewParameters(command, accountIdParameter, xParameter, yParameter, zParameter);
                command.ExecuteNonQuery();

                isUpdate = true;

                return isUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return isUpdate;
            }
        }

        public void AddPlayerPosition(long id, float x, float y, float z)
        {
            try
            {
                string sqlCommand = "INSERT INTO Positions(AccountID, PositionX, PositionY, PositionZ) " +
                                    "VALUES(@accountId, @x, @y, @z)";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter xParameter = new SqlParameter("@x", x);
                SqlParameter yParameter = new SqlParameter("@y", y);
                SqlParameter zParameter = new SqlParameter("@z", z);
                SqlParameter accountIdParameter = new SqlParameter("@accountId", id);

                AddNewParameters(command, accountIdParameter, xParameter, yParameter, zParameter);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public long GetPlayerIdBySocialName(string socialName)
        {
            long playerId = -1;

            try
            {
                string sqlCommand = "SELECT Id FROM Accounts WHERE SocialName = @socialName";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter socialNameParameter = new SqlParameter("@socialName", socialName);

                AddNewParameters(command, socialNameParameter);

                using (SqlDataReader accountReader = command.ExecuteReader())
                {
                    while (accountReader.Read())
                    {
                        playerId = (long)accountReader[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return playerId;
        }

        public bool GetPlayerLogin(string login)
        {
            try
            {
                bool isExist = false;
                string playerLogin = string.Empty;

                string sqlCommand = "SELECT PlayerLogin FROM Accounts WHERE PlayerLogin LIKE @login";
                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter loginParameter = new SqlParameter("@login", login);
                AddNewParameters(command, loginParameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        playerLogin = reader.ToString();
                    }
                }

                if (playerLogin == login)
                {
                    isExist = true;

                    return isExist;
                }

                return isExist;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool IsAccountExistBySocialName(string socialName)
        {
            try
            {
                bool isExist = false;
                string socialClubName = string.Empty;

                string sqlCommand = "SELECT SocialName FROM Accounts WHERE SocialName LIKE @socialName";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter socialNameParameter = new SqlParameter("@socialName", socialName);
                AddNewParameters(command, socialNameParameter);

                using (SqlDataReader accountReader = command.ExecuteReader())
                {
                    while (accountReader.Read())
                    {
                        socialClubName = accountReader.ToString();
                    }
                }

                if (socialClubName == socialName)
                {
                    isExist = true;

                    return isExist;
                }

                return isExist;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void AddNewAccount(PlayerModel player/*string login, string firstName, string lastName, string email, string socialName, string password*/)
        {
            try
            {
                string sqlCommand = "INSERT INTO Accounts(PlayerLogin, FirstName, LastName, Email, SocialName, UserPassword, RegistrationDate) " +
                                    "VALUES(@login, @firstName, @lastName, @email, @socialName, @password, CAST(getdate() as date))";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter loginParameter = new SqlParameter("@login", player.PlayerLogin);
                SqlParameter firstNameParameter = new SqlParameter("@firstName", player.FirstName);
                SqlParameter lastNameParameter = new SqlParameter("@lastName", player.LastName);
                SqlParameter emailParameter = new SqlParameter("@email", player.Email);
                SqlParameter passwordParameter = new SqlParameter("@password", player.Password);
                SqlParameter socialNameParameter = new SqlParameter("@socialName", player.SocialName);

                AddNewParameters(command, loginParameter, firstNameParameter, lastNameParameter, emailParameter, socialNameParameter, passwordParameter);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddNewParameters(SqlCommand newCommand, params SqlParameter[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                newCommand.Parameters.Add(data[i]);
            }
        }
    }
}
