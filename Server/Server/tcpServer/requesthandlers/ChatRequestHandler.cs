using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Request = Server.classes.Request;
using Database = Server.repositories.Database;
using Response = Server.classes.Response;
using Message = Server.classes.Message;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using Server.classes;

namespace Server.tcpServer.requesthandlers
{
    public class ChatRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "GET CHAT MESSAGES")
            {
                var request = JsonSerializer.Deserialize<GetChatMessagesRequest>(req.Content);

                List<Message> messages = database.MessageRepository.GetMessagesByChatId(request.ChatId);

                if(messages != null)
                {
                    var response = new
                    {
                        Messages = messages
                    };
                    Response res = new Response
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Content = JsonSerializer.Serialize(response)
                    };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
                }
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }
    }

    public class GetChatMessagesRequest
    {
        public int ChatId { get; set; }
    }
}
