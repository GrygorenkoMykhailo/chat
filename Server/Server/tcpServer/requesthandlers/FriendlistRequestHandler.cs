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
    public class FriendlistRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "ADD TO FRIENDLIST")
            {
                var request = JsonSerializer.Deserialize<FriendListRequest>(req.Content);

                database.UserRepository.AddUserToFriendList(request.SenderId, request.TargetId);

                Response response = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize("") };
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));

            }
            else if(req.Type == "REMOVE FROM FRIENDLIST")
            {
                var request = JsonSerializer.Deserialize<FriendListRequest>(req.Content);

                database.UserRepository.RemoveUserFromFriendList(request.SenderId, request.TargetId);
                Response response = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize("") };
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));
            }
            else if(req.Type == "GET FRIND LIST")
            {
                var request = JsonSerializer.Deserialize<GetFriendListRequest>(req.Content);

                List<User>? friends = database.UserRepository.GetUserFriends(request.UserId);

                Response res;
                if(friends != null)
                {
                    res = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize(friends) };
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

        public class FriendListRequest
        {
            public int SenderId { get; set; }
            public int TargetId { get; set; }   
        }

        //GET FRIEND LIST
        public class GetFriendListRequest
        {
            public int UserId { get; set; }
        }
    }
}
