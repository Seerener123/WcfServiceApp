using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    public abstract class MessageAbstractDbContext : DbContext
    {
        public virtual DbSet<MessageTable> MessageTables { get; set; }
        //public virtual DbSet<MessageTransactionTable> MessageTransactionTables { get; set; }
        //public virtual DbSet<UserTable> UserTables { get; set; }

        protected MessageAbstractDbContext(string connectionString) : base(connectionString)
        {
            //
        }
    }
}
