using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Request = Server.classes.Request;
using Database = Server.repositories.Database;
using Response = Server.classes.Response;
using System.Text.Json;
using System.Net.Sockets;
using Server.classes;
using System.Net;

namespace Server.tcpServer.requesthandlers
{
    public class AuthorizationRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "AUTHORIZATION")
            {
                var request = JsonSerializer.Deserialize<AuthorizationRequest>(req.Content);

                Response? response = null;
                if (database.UserRepository.IsEmailExist(request.Email))
                {
                    User u = database.UserRepository.GetUserByEmail(request.Email);

                    string userHash = PasswordEncryptor.HashPassword
                        (
                            request.Password, 
                            u.Salt, 
                            PasswordEncryptor.nIterations, 
                            PasswordEncryptor.nHash
                        );

                    if(userHash != u.Hash) 
                    {
                        response = new Response { StatusCode = (int)HttpStatusCode.Unauthorized };
                        stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response)));
                    }
                    else
                    {
                        response = new Response { StatusCode = (int)HttpStatusCode.OK, Content = JsonSerializer.Serialize(u) };
                        stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response)));
                    }  
                }
                else
                {
                    response = new Response { StatusCode = (int)HttpStatusCode.Unauthorized };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response)));
                }
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }

        class AuthorizationRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
