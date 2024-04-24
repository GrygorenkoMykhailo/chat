using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppContext = Server.contexts.ApplicationContext;
using Message = Server.classes.Message;
using Server.classes;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Server.repositories
{
    public class ChatRepository
    {
        public List<Chat>? GetChatsByUsername(string username)
        {
           using(AppContext context = new AppContext())
           {
                User? user = context.Users.Include(u => u.Chats).FirstOrDefault(u => u.Username == username);
                return user?.Chats;
           }    
        }

        public void DeleteChat(int id)
        {
            using (AppContext context = new AppContext())
            {
                Chat? chat = context.Chats.FirstOrDefault(c => c.Id == id);
                if(chat != null)
                {
                    context.Chats.Remove(chat);
                }
            }
        }
    }
}
