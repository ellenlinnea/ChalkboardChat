using ChalkboardChat.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.DAL.DATAs
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options)
            : base(options)
        {
        }
        public DbSet<MessageModel> Messages => Set<MessageModel>();
    }
}
