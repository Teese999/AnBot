using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Telegram.Bot.Types;
using AnBot.Models;

namespace AnBot
{
    public class BotDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Click> Clicks { get; set; }

        public BotDataContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Click>(
            eb =>
            {
                eb.HasKey(x => x.Date);
            });
            modelBuilder.Entity<User>(
            eb =>
            {
                eb.HasKey(x => x.UserId);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@$"Server=217.28.223.127,17160;User Id=user_e70b9;Password=b-8G3dE=_4;Database=db_a5a2a;");
            }
        }
    }
}

       
    
    //public class AppDbContextFactory : IDesignTimeDbContextFactory<BotDataContext>
    //{
    //    public BotDataContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<BotDataContext>();
    //        optionsBuilder.UseSqlServer(@"*****");

    //        return new BotDataContext(optionsBuilder.Options);
    //    }
    //}


