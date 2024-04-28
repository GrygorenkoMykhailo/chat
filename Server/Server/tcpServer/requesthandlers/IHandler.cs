using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Request = Server.classes.Request;
using Database = Server.repositories.Database;
using System.Net.Sockets;

namespace Server.tcpServer.requesthandlers
{
    public interface IHandler
    {
        public IHandler Next { get; set; }
        public void Handle(Request req, Database database, NetworkStream stream);   
    }
}
