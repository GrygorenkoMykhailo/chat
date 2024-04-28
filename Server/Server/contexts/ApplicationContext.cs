using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.classes;
using Message = Server.classes.Message;

namespace Server.contexts
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext() 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8PHJCE9\\FORPROJECTS;Database=CursachDB;Trusted_Connection=True;TrustServerCertificate=True"); 
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .UsingEntity<FriendList>(
                    j => j
                        .HasOne(fl => fl.Friend)
                        .WithMany()
                        .HasForeignKey(fl => fl.FriendID),
                    j => j
                        .HasOne(fl => fl.User)
                        .WithMany()
                        .HasForeignKey(fl => fl.UserId),
                    j =>
                    {
                        j.ToTable("FriendList");
                        j.HasKey(fl => fl.Id);
                    });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Blocked)
                .WithMany()
                .UsingEntity<BlackList>(
                    j => j
                        .HasOne(bl => bl.Blocked)
                        .WithMany()
                        .HasForeignKey(bl => bl.BlockedId),
                    j => j
                        .HasOne(bl => bl.Blocker)
                        .WithMany()
                        .HasForeignKey(bl => bl.BlockerId),
                    j =>
                    {
                        j.ToTable("BlackList");
                        j.HasKey(bl => bl.Id);
                    });

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

        public void AddInitialData()
        {
            var user1 = new User { Username = "user1", Email = "user1@example.com", Salt = "salt1", Hash = "hash1" };
            var user2 = new User { Username = "user2", Email = "user2@example.com", Salt = "salt2", Hash = "hash2" };
            var user3 = new User { Username = "user3", Email = "user3@example.com", Salt = "salt3", Hash = "hash3" };
            var user4 = new User { Username = "user4", Email = "user4@example.com", Salt = "salt4", Hash = "hash4" };
            var user5 = new User { Username = "user5", Email = "user5@example.com", Salt = "salt5", Hash = "hash5" };
            var user6 = new User { Username = "user6", Email = "user6@example.com", Salt = "salt6", Hash = "hash6" };

            // Добавляем пользователей в контекст данных
            Users.AddRange(user1, user2, user3, user4, user5, user6);
            SaveChanges();

            // Создаем чаты
            var chat1 = new Chat { Name = "chat1" };
            var chat2 = new Chat { Name = "chat2" };
            var chat3 = new Chat { Name = "chat3" };

            // Добавляем чаты в контекст данных
            Chats.AddRange(chat1, chat2, chat3);
            SaveChanges();

            user1.Chats.Add(chat1); 
            user2.Chats.Add(chat1);
            user3.Chats.Add(chat2);
            user4.Chats.Add(chat2);
            user5.Chats.Add(chat3);
            user6.Chats.Add(chat3);
            SaveChanges();

            // Создаем сообщения
            var message1 = new Message { Content = "Hello!", SenderUsername = user1.Username, Chat = chat1 };
            var message2 = new Message { Content = "Hi!", SenderUsername = user2.Username, Chat = chat1 };
            var message3 = new Message { Content = "How are you?", SenderUsername = user3.Username, Chat = chat1 };
            var message4 = new Message { Content = "Fine, thank you!", SenderUsername = user4.Username, Chat = chat1 };
            var message5 = new Message { Content = "Goodbye!", SenderUsername = user5.Username, Chat = chat1 };
            var message6 = new Message { Content = "See you later!", SenderUsername = user6.Username, Chat = chat1 };

            // Добавляем сообщения в контекст данных
            Messages.AddRange(message1, message2, message3, message4, message5, message6);
            SaveChanges();

            // Создаем записи в FriendList
            user1.Friends = new List<User> { user2, user3 };
            user2.Friends = new List<User> { user1, user4 };
            user3.Friends = new List<User> { user1, user5 };
            user4.Friends = new List<User> { user2, user6 };

            SaveChanges();
        }
    }
}
