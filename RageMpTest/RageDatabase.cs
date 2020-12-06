using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using GTANetworkAPI;

namespace RageMpTest
{
    class RageDatabase : Script
    {
        private readonly string _connectionString = "Data Source=(local);Initial Catalog = RageMpTest; Integrated Security = True";
        private SqlConnection _sqlConnection = null;

        public RageDatabase()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
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
                Console.WriteLine(ex);
                throw;
            }
        }

        public void AddNewAccount(string login, string firstName, string lastName, string socialName, string password, float x, float y, float z)
        {
            try
            {
                string sqlCommand = "INSERT INTO Accounts(PlayerLogin, FirstName, LastName, UserPassword, SocialName, RegistrationDate) " +
                                    "VALUES(@login, @firstName, @lastName, @password, @socialName, CAST(getdate() as date))";

                SqlCommand command = _sqlConnection.CreateCommand();
                command.CommandText = sqlCommand;

                SqlParameter loginParameter = new SqlParameter("@login", login);
                SqlParameter firstNameParameter = new SqlParameter("@firstName", firstName);
                SqlParameter lastNameParameter = new SqlParameter("@lastName", lastName);
                SqlParameter passwordParameter = new SqlParameter("@password", password);
                SqlParameter socialNameParameter = new SqlParameter("@socialName", socialName);

                AddNewParameters(command, loginParameter, firstNameParameter, lastNameParameter, passwordParameter, socialNameParameter);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
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
