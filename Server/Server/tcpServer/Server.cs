using Server.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.tcpServer
{
    public class Server
    {
        private UserRepository _UserRepository = new UserRepository();
        private ChatRepository _ChatRepository = new ChatRepository();
        private MessageRepository _MessageRepository = new MessageRepository();

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
                            bytesRead = stream.Read(buffer, 0, buffer.Length);
                            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            string response = HandleRequest(request);

                            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                            stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        }
                    }
                }
            }
        }

        private string HandleRequest(string request)
        {
            return "OK";
        }
    }
}
