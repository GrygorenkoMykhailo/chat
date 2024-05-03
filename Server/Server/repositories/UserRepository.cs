using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppContext = Server.contexts.ApplicationContext;
using Server.classes;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Server.repositories
{
    public class UserRepository
    {
        public User? GetUserById(int id)
        {
            using(AppContext context = new AppContext())
            {
                return context.Users.
                    Include(u => u.Chats).
                    Include(u => u.Friends).
                    Include(u => u.Blocked).
                    FirstOrDefault(u => u.Id == id);
            }
        }

        public User? GetUserByEmail(string email)
        {
            using (AppContext context = new AppContext())
            {
                return context.Users.
                    Include(u => u.Chats).
                    Include(u => u.Friends).
                    Include(u => u.Blocked).
                    FirstOrDefault(u => u.Email == email);
            }
        }

        public User? GetUserByTag(string tag)
        {
            using (AppContext context = new AppContext())
            {
                return context.Users.FirstOrDefault(u =>  u.Tag == tag);
            }
        }

        public void AddUser(User user) 
        { 
            using(AppContext context = new AppContext()) 
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }


        public bool IsEmailExist(string email)
        {
            using (AppContext context = new AppContext())
            {
                return context.Users.Any(x => x.Email == email);
            }
        }

        public List<User>? GetUserFriends(int id)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Friends).FirstOrDefault(u => u.Id == id);

                return user?.Friends;
            }
        }

        public List<User>? GetUserBlockedUsers(int id)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Blocked).FirstOrDefault(u => u.Id == id);

                return user?.Blocked;
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

        public void AddUserToBlackList(int id, int blockedId)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Blocked).FirstOrDefault(u => u.Id == id);
                User? userToBlock = context.Users.FirstOrDefault(u => u.Id == blockedId);

                if(userToBlock != null && user != null)
                {
                    user.Blocked.Add(userToBlock);
                    context.SaveChanges();
                }
            }
        }
        public void RemoveUserFromBlackList(int userId, int blockedId)
        {
            using(AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Blocked).FirstOrDefault(u => u.Id == userId);
                User? userToRemove = context.Users.FirstOrDefault(u => u.Id == blockedId);

                if(userToRemove != null && user != null)
                {
                    user.Blocked.Remove(userToRemove);
                    context.SaveChanges();
                }
            }
        }

        public void AddUserToFriendList(int sender, int target)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Friends).FirstOrDefault(u => u.Id == sender);
                User? targetUser = context.Users.FirstOrDefault(u => u.Id == target);

                if(user != null && targetUser != null)
                {
                    user.Friends.Add(targetUser);
                    context.SaveChanges();
                }
            }
        }

        public void RemoveUserFromFriendList(int sender, int target)
        {
            using (AppContext context = new AppContext())
            {
                User? user = context.Users.Include(u => u.Friends).FirstOrDefault(u => u.Id == sender);
                User? targetUser = context.Users.FirstOrDefault(u => u.Id == target);

                if (user != null && targetUser != null)
                {
                    user.Friends.Remove(targetUser);
                    context.SaveChanges();
                }
            }
        }
    }
}
