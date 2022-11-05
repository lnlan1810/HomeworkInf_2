using HttpServer.Attributes;
using HttpServer.ORM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Controllers
{
    [HttpController("accounts")]

    public class AccountController
    {
        private const string DbName = "SteamDB";
        private const string TableName = "[dbo].[Accounts]";

        [HttpPOST]
        public static void SaveAccount(string login, string password)
        {
            var dao = new AccountDao();
            dao.Insert(login, password);
        }

        [HttpGET(@"\d")]
        public static Account? GetAccountById(int id)
        {
            var dao = new AccountDao();
            return dao.GetById(id);
        }

        [HttpGET]
        public static List<Account> GetAccounts()
        {
            var dao = new AccountDao();
            return dao.GetAll().ToList();
        }
    }

    //public class Accounts
    //{
    //    [HttpGET]
    //    public List<Account> GetAccounts()
    //    {
    //        var accounts = new List<Account>();

    //        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";

    //        string sqlExpression = "SELECT * FROM Accounts";
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            SqlDataReader reader = command.ExecuteReader();

    //            if (reader.HasRows) // если есть данные
    //            {
    //                while (reader.Read()) // построчно считываем данные
    //                {
    //                    accounts.Add(new Account
    //                        (
    //                        reader.GetInt32(0),
    //                        reader.GetString(1),
    //                        reader.GetString(2)
    //                        ));
    //                }
    //            }

    //            reader.Close();
    //        }

    //        return accounts;
    //    }

    //    [HttpGET]
    //    public Account GetAccountById(int id)
    //    {
    //        Account account = null;

    //        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";
    //        string sqlExpression = $"SELECT * FROM Accounts WHERE Id = {id}";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            SqlDataReader reader = command.ExecuteReader();
    //            if (reader.HasRows)
    //            {
    //                reader.Read();

    //                account = new Account
    //                    (
    //                    reader.GetInt32(0),
    //                    reader.GetString(1),
    //                    reader.GetString(2)
    //                    );
    //            }

    //            reader.Close();
    //        }

    //        return account;
    //    }
    //    [HttpPOST]
    //    public void SaveAccount(string login, string password)
    //    {
    //        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";
    //        string sqlExpression = $"INSERT INTO Accounts (Login, Password) VALUES ('{login}', '{password}')";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}
}
