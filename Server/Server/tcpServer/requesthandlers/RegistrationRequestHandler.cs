using Server.repositories;
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
using System.Net;
using Server.classes;

namespace Server.tcpServer.requesthandlers
{
    public class RegistrationRequestHandler : IHandler
    {
        public IHandler Next { get; set; }

        public void Handle(Request req, Database database, NetworkStream stream)
        {
            if(req.Type == "REGISTRATION")
            {
                var request = JsonSerializer.Deserialize<RegistrationRequest>(req.Content);

                string username = request.Username;
                string email = request.Email;
                string password = request.Password;

                Response? response = null;

                if (!database.UserRepository.IsEmailExist(email))
                {
                    string salt = PasswordEncryptor.GenerateSalt(70);
                    string hash = PasswordEncryptor.HashPassword
                        (
                            password, 
                            salt, 
                            PasswordEncryptor.nIterations, 
                            PasswordEncryptor.nHash
                        );

                    database.UserRepository.AddUser(new User { Username = username, Email = email, Hash = hash, Salt = salt });
                    User u = database.UserRepository.GetUserByEmail(email);
                    response = new Response { StatusCode = (int)HttpStatusCode.Created, Content = JsonSerializer.Serialize(u) };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response)));
                }
                else
                {
                    response = new Response { StatusCode = (int)HttpStatusCode.Conflict };
                    stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response)));
                }
            }
            else
            {
                Next.Handle(req, database, stream);
            }
        }

        public class RegistrationRequest
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
