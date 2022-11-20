using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace contro1
{
    public class HttpServer
    {
        private readonly string _url;
        private readonly HttpListener _listener;
        private HttpListenerContext _httpContext;

        public HttpServer(string url)
        {
            _listener = new HttpListener();
            _url = url;

            _listener.Prefixes.Add(_url);
        }

        public object Status { get; internal set; }

        public void Start()
        {
            Console.WriteLine("Server start ....");
            _listener.Start();

            // Сервер запущен
            Console.WriteLine("Server started");

            Listener();

        }

        public void Stop()
        {
            //  Остановка сервера...
            Console.WriteLine("Stopping the server...");

            _listener.Stop();

            // сервер остановлен
            Console.WriteLine("server stopped");
        }

        private void Listener()
        {
            _httpContext = _listener.GetContext();

            //Сервер ожидает запроса
            Console.WriteLine("Server waiting for request");

            HttpListenerRequest request = _httpContext.Request;

            // получаем обьеккт ответа 
            HttpListenerResponse response = _httpContext.Response;

            string text = "Привет мир!";
            string text1 = Decipher(text, 3);

            // создаем ответ в виде кода html
            string responseStr = $"<html><head><meta charset='utf8'></head><body>{text} <br> {text1}</body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);

            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // закрываем поток
            output.Close();
        }

        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }

        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cipher(ch, key);

            return output;
        }

        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }
    }
}
