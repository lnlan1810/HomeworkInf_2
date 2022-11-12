//using HttpServer.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HttpServer.Controllers
//{
//    [HttpController("users")]
//    internal class Users
//    {
//        [HttpController("users")]
//        public User GetUser(int id)
//        {
//            List<User> users = new List<User>();
//            users.Add(new User() { Id = 1, Name = "Ivan" });

//            return users.FirstOrDefault(t => t.Id == id);
//        }

//        [HttpGET("")]
//        public List<User> GetUsers()
//        {
//            List<User> users = new List<User>();

//            String connectionString = @"Data Soure = (localdb)\MSSQLLocalDB; Initial Catalog = SteamDB; Integrated Security = True";

//            String sqlExpression = "SELECT * FROM [dbo].[Table]";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(sqlExpression, connection);
//                SqlDataReader reader = command.ExecuteReader();

//                if (reader.HasRows)
//                {
//                  //  Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

//                    while (reader.Read())
//                    {
//                        /*
//                        object id = reader.GetValue(0);
//                        object name = reader.GetValue(1);

//                        object age = reader.GetValue(2);

//                        Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
//                        */

//                        users.Add(new User
//                        {
//                            Id = reader.GetInt32(0),
//                            Name = reader.GetString(1)
//                        });
//                    }
//                }
//                reader.Close();

//            }
//            return users;
//        }

//    }



//    internal class User
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//    }
//}
