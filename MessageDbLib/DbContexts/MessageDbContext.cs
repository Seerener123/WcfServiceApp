using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext() : base("name=MessageDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<MessageTable> MessageTables { get; set; }
        public virtual DbSet<MessageTransactionTable> MessageTransactionTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            UserTableDiscriminatorConfig(modelBuilder);
        }

        /* Fluent Api, is an api that is part of the entity
         * which allows us configure the domain (entity)
         * class in the datacontext.
         * */
        private void UserTableDiscriminatorConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>()
                .Map<UserTable>(u => u.Requires("ISADVANCEDUSER").HasValue(false));
            modelBuilder.Entity<UserTable>()
                .Map<AdvancedUser>(au => au.Requires("ISADVANCEDUSER").HasValue(true));
        }

        /*private void dump()
        {
            modelBuilder.Entity<MessageTable>()
                .Property(e => e.MESSAGETEXT)
                .IsUnicode(false);

            modelBuilder.Entity<MessageTransactionTable>()
                .Property(e => e.EMAILADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);
        }*/
    }
}
