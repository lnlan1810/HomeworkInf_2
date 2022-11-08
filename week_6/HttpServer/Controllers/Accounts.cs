using HttpServer.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Controllers
{
    [HttpController("accounts")]
    public class Accounts
    {
        [HttpController("accounts")]
        public Account GetAccount(int id)
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(new Account() { Id = 1, Login = "Ivan", Password = "123" });

            return accounts.FirstOrDefault(t => t.Id == id);
        }

        [HttpGET]
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";

            string sqlExpression = "SELECT * FROM  [dbo].[Table]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {

                            Id = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            Password = reader.GetString(2)
                        });
                    }
                }

                reader.Close();
            }

            return accounts;
        }

        [HttpPOST]
        public void SaveAccount(string login, string password)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";
            string sqlExpression = $"INSERT INTO Accounts (Login, Password) VALUES ('{login}', '{password}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }

    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Account()
        {

        }

        public Account(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }



}
