using Server.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Server.classes;
using Server.tcpServer.requesthandlers;

namespace Server.tcpServer
{
    public class Server
    {
        public delegate void MessageReceivedEventHandler(object sender, string message);
        public event MessageReceivedEventHandler MessageReceived;
        private Database Database = new Database();
        private IHandler _StartHandler;

        public Server()
        {
            _StartHandler = new RegistrationRequestHandler();
        }

        public async Task Start(int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);

            using (TcpListener server = new TcpListener(ep))
            {
                server.Start();
                byte[] buffer = new byte[1024 * 48];
                int bytesRead;

                while (true)
                {
                    using (TcpClient client = server.AcceptTcpClient())
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            try
                            {
                                bytesRead = stream.Read(buffer, 0, buffer.Length);
                                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                MessageReceived.Invoke(this, request);

                                Request req = JsonSerializer.Deserialize<Request>(request);
                                _StartHandler.Handle(req, Database, stream);
                            }
                            catch
                            {
                                Response response = new Response { StatusCode = (int)HttpStatusCode.InternalServerError };
                                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
                            }
                        }
                    }
                }
            }
        }
    }
}
