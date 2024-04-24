using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppContext = Server.contexts.ApplicationContext;
using Server.classes;

namespace Server.repositories
{
    public class UserRepository
    {
        public User? GetUserById(int id)
        {
            using(AppContext context = new AppContext())
            {
                return context.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public User? GetUserByUsername(string username)
        {
            using (AppContext context = new AppContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public void DeleteUser(int id) 
        { 
            using(AppContext context = new AppContext())
            {
                User? userToDelete = context.Users.FirstOrDefault();

                if(userToDelete != null) 
                {
                    context.Users.Remove(userToDelete); 
                    context.SaveChanges();
                }
            }
        }
    }
}
