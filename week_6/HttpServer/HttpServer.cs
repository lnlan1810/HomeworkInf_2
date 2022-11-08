using System;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using HttpServer.Attributes;

namespace HttpServer
{
    class HttpServer
    {
        private readonly HttpListener _httpListener;
        public string SettingsPath;

        private ServerSettings _serverSettings;

        public ServerStatus ServerStatus { get; private set; } = ServerStatus.Stopped;

        public HttpServer(string settingsPath)
        {
            SettingsPath = settingsPath;
            _httpListener = new HttpListener();
        }

        public async void Start()
        {
            if (ServerStatus == ServerStatus.Started)
            {
                Console.WriteLine("Server is already running!");
                return;
            }

            if (!File.Exists(SettingsPath))
            {
                Console.WriteLine("Settings file not found!");
                return;
            }
            _serverSettings = JsonSerializer.Deserialize<ServerSettings>(File.ReadAllBytes(SettingsPath));

            //var a = "http://localhost:" + serverSettings.Port + "/";
            _httpListener.Prefixes.Clear();
            _httpListener.Prefixes.Add("http://localhost:" + _serverSettings.Port + "/");

            Console.WriteLine("Server start...");
            _httpListener.Start();

            Console.WriteLine("Server started");
            ServerStatus = ServerStatus.Started;

            Listening();
        }

        public void Stop()
        {
            if (ServerStatus == ServerStatus.Stopped)
                return;

            Console.WriteLine("Server stop...");
            _httpListener.Stop();
            ServerStatus = ServerStatus.Stopped;
            Console.WriteLine("Server stopped.");
        }

        private async void Listening()
        {
            while (_httpListener.IsListening)
            {
                var _httpContext = await _httpListener.GetContextAsync();
                if (MethodHandler(_httpContext)) return;

                StaticFiles(_httpContext.Request, _httpContext.Response);
            }
        }

        private void StaticFiles(HttpListenerRequest request, HttpListenerResponse response)
        {
            byte[] buffer;

            if (Directory.Exists(_serverSettings.Path))
            {
                buffer = getFile(request.RawUrl.Replace("%20", " "));

                if (buffer == null)
                {
                    response.Headers.Set("Content-Type", "text/plain");

                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    string err = "404- not found";
                    buffer = Encoding.UTF8.GetBytes(err);
                }
            }

            else
            {
                var err = $"Directory '{_serverSettings.Path}'  not found";
                buffer = Encoding.UTF8.GetBytes(err);
            }

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();

        }

        private byte[] getFile(string rawUrl)
        {
            byte[] buffer = null;
            var filePath = _serverSettings.Path + rawUrl;

            if (Directory.Exists(filePath))
            {
                //каталог
                filePath = filePath + "/index.html";
                if (File.Exists(filePath))
                {
                    buffer = File.ReadAllBytes(filePath);
                }

            }
            else if (File.Exists(filePath))
            {
                //файл
                buffer = File.ReadAllBytes(filePath);
            }

            return buffer;

        }

        private bool MethodHandler(HttpListenerContext _httpContext)
        {
            HttpListenerRequest request = _httpContext.Request;

            HttpListenerResponse response = _httpContext.Response;

            if (_httpContext.Request.Url.Segments.Length > 1) return false;

            string controllerName = _httpContext.Request.Url.Segments[1].Replace("/", "");

            string[] strParams = _httpContext.Request.Url
                .Segments
                .Skip(2)
                .Select(s => s.Replace("/", ""))
                .ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController)))
                .FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());

            if (controller == null) return false;

            var test = typeof(HttpController).Name;
            var method = controller.GetMethods().Where(t => t.GetCustomAttributes(true).Any(attr => attr.GetType().Name == $"http{_httpContext.Request.HttpMethod}"))
                .FirstOrDefault();

            if (method == null) return false;


            Object[] queryParms = method.GetParameters()
                .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
                .ToArray();

            var ret = method.Invoke(Activator.CreateInstance(controller), queryParms);

            response.ContentType = "Application/json";

            byte[] buffer = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(ret));
            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();

            return true;
        }

    }
}
