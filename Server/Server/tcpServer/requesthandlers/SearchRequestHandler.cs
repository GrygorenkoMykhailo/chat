using Server.classes;
using Server.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.tcpServer.requesthandlers
{
    public class SearchRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "SEARCH USER BY TAG")
            {
                var request = JsonSerializer.Deserialize<SearchUserByTagRequest>(req.Content);

                User? u = database.UserRepository.GetUserByTag(request.Tag);

                if(u != null)
                {
                    Response res = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize(u) };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
                }
                else
                {
                    Response res = new Response { StatusCode = (int)HttpStatusCode.NotFound };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res)));
                }
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }
    }

    public class SearchUserByTagRequest
    {
        public string Tag { get; set; }
    }
}
