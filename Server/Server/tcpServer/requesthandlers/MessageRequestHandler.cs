using Server.classes;
using Server.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Request = Server.classes.Request;
using Database = Server.repositories.Database;
using Response = Server.classes.Response;
using Message = Server.classes.Message;
using System.Text.Json;
using System.Net.Sockets;
using Server.classes;
using System.Net;

namespace Server.tcpServer.requesthandlers
{
    public class MessageRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "SEND MESSAGE")
            {
                var request = JsonSerializer.Deserialize<SendMessageRequest>(req.Content);

                database.ChatRepository.AddMessageToChat(request.ChatId, request.Message);

                Response res = new Response { StatusCode = (int)HttpStatusCode.OK };
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }

        public class SendMessageRequest
        {
            public int ChatId {  get; set; }
            public Message Message { get; set; }
        }
    }
}
