using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;

namespace MessageDbLib.DbContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MessageMySqlDbContext : MessageAbstractDbContext
    {
        public MessageMySqlDbContext() : base("name=MessageMySQLDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            UserTableConfiguration(modelBuilder);
            MessageTableConfigureation(modelBuilder);
            UserTableDiscriminatorConfig(modelBuilder);

            MapUserToTable(modelBuilder);
            MapMessageToTable(modelBuilder);
        }

        private void MapUserToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("UserTable", "wcfMessaging");
        }

        private void UserTableConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().Property(e => e.USERNAME)
               .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.FIRSTNAME)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.SURNAME)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(e => e.GENDER)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().HasMany(e => e.Messages).WithOptional(e => e.User)
                .HasForeignKey(e => e.SENDERID);
        }

        private void MapMessageToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().ToTable("MessageTable", "wcfMessaging");
        }

        private void MessageTableConfigureation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().Property(e => e.MESSAGETEXT)
                .IsUnicode(false);

            modelBuilder.Entity<MessageTable>().HasMany(e => e.MessageTransactions).WithOptional(e => e.Message)
                .HasForeignKey(e => e.MESSAGEID);
        }

        /*private void MapMessageTransactionToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTransactionTable>().ToTable("MessageTransactionTable", "messagedbo");
        }

        private void MessageTransactionTableConfigureation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTransactionTable>().Property(e => e.EMAILADDRESS)
                .IsUnicode(false);
        }*/

        private void UserTableDiscriminatorConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().Map<UserTable>(u => u.Requires("ISADVANCEDUSER")
                .HasValue(false));

            modelBuilder.Entity<UserTable>().Map<AdvancedUser>(au => au.Requires("ISADVANCEDUSER")
                .HasValue(true));
        }
    }
}
