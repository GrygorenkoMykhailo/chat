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
using System.Net.Mime;

namespace Server.repositories
{
    public class ChatRepository
    {
        public List<Chat>? GetUserChatsByEmail(string email)
        {
           using(AppContext context = new AppContext())
           {
                User? user = context.Users.Include(u => u.Chats).FirstOrDefault(u => u.Email == email);
                return user?.Chats;
           }    
        }

        public List<Chat>? GetUserChatsById(int id)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Chats).FirstOrDefault(u => u.Id == id);
                return user?.Chats;
            }
        }

        public void AddMessageToChat(int chatId, Message message)
        {
            using (AppContext context = new AppContext())
            {
                Chat? chat = context.Chats.FirstOrDefault(c => c.Id == chatId);

                if (chat != null)
                {
                    chat.Messages.Add(message);
                    context.SaveChanges();
                }
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
