using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.repositories
{
    public class Database
    {
        public UserRepository UserRepository = new UserRepository();
        public ChatRepository ChatRepository = new ChatRepository();
        public MessageRepository MessageRepository = new MessageRepository();
    }
}
