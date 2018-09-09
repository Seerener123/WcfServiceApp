using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    /* Fluent Api, is an api that is part of the entity
     * which allows us to configure the domain (entity)
     * class in the datacontext.
     * */
    public class MessageDbContext : MessageAbstractDbContext
    {
        //public virtual DbSet<MessageTransactionTable> MessageTransactionTables { get; set; }
        //public virtual DbSet<UserTable> UserTables { get; set; }

        public MessageDbContext() : base("name=MessageDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            UserTableConfiguration(modelBuilder);
            MessageTableConfigureation(modelBuilder);
            UserTableDiscriminatorConfig(modelBuilder);

            MapUserToTable(modelBuilder);
            MapMessageToTable(modelBuilder);
        }

        private void MapUserToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("UserTable", "dbo");
        }

        private void UserTableConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().Property(e => e.UserName)
               .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().HasMany(e => e.Messages).WithOptional(e => e.User)
                .HasForeignKey(e => e.SenderId);
        }

        private void MapMessageToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().ToTable("MessageTable", "messagedbo");
        }

        private void MessageTableConfigureation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().Property(e => e.MessageText)
                .IsUnicode(false);

            modelBuilder.Entity<MessageTable>().HasMany(e => e.MessageTransactions).WithOptional(e => e.Message)
                .HasForeignKey(e => e.MessageId);
        }

        private void MapMessageTransactionToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTransactionTable>().ToTable("MessageTransactionTable", "messagedbo");
        }

        private void MessageTransactionTableConfigureation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTransactionTable>().Property(e => e.EmailAddress)
                .IsUnicode(false);
        }

        private void UserTableDiscriminatorConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().Map<UserTable>(u => u.Requires("ISADVANCEDUSER")
                .HasValue(false));

            modelBuilder.Entity<UserTable>().Map<AdvancedUser>(au => au.Requires("ISADVANCEDUSER")
                .HasValue(true));
        }

        // https://social.msdn.microsoft.com/Forums/en-US/b70d1b99-410b-4bfc-be85-675fe3652e72/problem-using-tph-discriminators-with-code-first-migrations-in-ef-43?forum=adodotnetentityframework
    }
}
