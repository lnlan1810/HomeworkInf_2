using contro1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contro1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string url = "http://localhost:9000/";
            HttpServer server = new HttpServer(url);

            server.Start();
            server.Stop();
            Console.Read();
            
        }
     
    }
}
