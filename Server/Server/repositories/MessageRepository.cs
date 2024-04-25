using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppContext = Server.contexts.ApplicationContext;
using Message = Server.classes.Message;
using Server.classes;
using Microsoft.EntityFrameworkCore;

namespace Server.repositories
{
    public class MessageRepository
    {
        public List<Message>? GetMessagesByChatId(int chatId)
        {
            using(AppContext context = new AppContext())
            {
                Chat? chat = context.Chats.Include(c => c.Messages).FirstOrDefault(c => c.Id == chatId);

                return chat?.Messages;
            }
        }
    }
}
