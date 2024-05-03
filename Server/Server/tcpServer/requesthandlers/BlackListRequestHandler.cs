using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Request = Server.classes.Request;
using Database = Server.repositories.Database;
using Response = Server.classes.Response;
using Message = Server.classes.Message;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using Server.classes;
using static Server.tcpServer.requesthandlers.FriendlistRequestHandler;

namespace Server.tcpServer.requesthandlers
{
    public class BlackListRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if (req.Type == "ADD TO BLACKLIST")
            {
                var request = JsonSerializer.Deserialize<BlackListRequest>(req.Content);

                database.UserRepository.AddUserToBlackList(request.SenderId, request.TargetId);
                Response response = new Response { StatusCode = (int)HttpStatusCode.OK };
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));

            }
            else if (req.Type == "REMOVE FROM BLACKLIST")
            {
                var request = JsonSerializer.Deserialize<BlackListRequest>(req.Content);

                database.UserRepository.RemoveUserFromBlackList(request.SenderId, request.TargetId);
                Response response = new Response { StatusCode = (int)HttpStatusCode.OK };
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));
            }
            else if (req.Type == "GET BLACKLIST")
            {
                var request = JsonSerializer.Deserialize<GetFriendListRequest>(req.Content);

                List<User>? blocked = database.UserRepository.GetUserBlockedUsers(request.UserId);

                Response res;
                if (blocked != null)
                {
                    res = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize(blocked) };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
                }
                else
                {
                    res = new Response { StatusCode = (int)HttpStatusCode.NotFound };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
                }
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }

        public class BlackListRequest
        {
            public int SenderId { get; set; }
            public int TargetId { get; set; }
        }

        //GET BLACKLIST
        public class GetBlackListRequest
        {
            public int UserId { get; set; }
        }
    }
}
