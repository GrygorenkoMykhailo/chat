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
        public DbSet<BlackList> BlackList { get; set; }
        public ApplicationContext() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8PHJCE9\\FORPROJECTS;Database=CursachDB;Trusted_Connection=True;TrustServerCertificate=True"); 
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackList>(entity =>
            {
                entity.HasOne(e => e.Blocker)
                      .WithMany()
                      .HasForeignKey(e => e.BlockerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Blocked)
                      .WithMany()
                      .HasForeignKey(e => e.BlockedId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>().HasMany(u => u.Chats).WithMany(c => c.Users);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

        public void AddInitialData()
        {
            var user1 = new User { Username = "User1", Email = "bob", Salt = "salt1", Hash = "hash1" };
            var user2 = new User { Username = "User2", Email = "john", Salt = "salt2", Hash = "hash2" };

            // Создаем чат
            var chat1 = new Chat { Name = "Chat1" };

            Users.AddRange(user1, user2);
            Chats.Add(chat1);
            SaveChanges();

            // Создаем сообщения
            var message1 = new Message { Content = "Hello!", ChatId = chat1.Id };
            var message2 = new Message { Content = "Hi!", ChatId = chat1.Id };

            SaveChanges();

            // Создаем запись в BlackList
            var blackListEntry = new BlackList { BlockerId = user1.Id, BlockedId = user2.Id };

            // Добавляем созданные объекты в контекст данных

            Messages.AddRange(message1, message2);
            BlackList.Add(blackListEntry);

            // Сохраняем изменения в базе данных
            SaveChanges();

            user1.Chats.Add(chat1);
            user2.Chats.Add(chat1);

            SaveChanges();
        }
    }
}
