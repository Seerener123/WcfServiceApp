using MessageDbLib.MessagingEntities;
using MySql.Data.EntityFramework;
//using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    /* We were getting error with the dbcontext not connecting to our mysql db on our virtual machine.
     * This is because MySql.Data.Entity 6.10.7 isn't compatible with MySql.Data 8.0.11.
     * To rsolve this issue, we are db-configuration-type from mysql.data.entityframework.
     * 
     * https://stackoverflow.com/questions/50216643/error-attempt-by-method-x-set-dbconnectionsystem-data-common-dbconnection-to
     * */

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MessageMySqlDbContext : MessageAbstractDbContext
    {
        public MessageMySqlDbContext() : base("name=MessageMySQLDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<UserTable>(); // These tables don't exist in mysql db, so we're ingoring them for now
            modelBuilder.Ignore<MessageTransactionTable>();
            MessageTableConfigureation(modelBuilder);
            MapMessageToTable(modelBuilder);
        }

        private void MapMessageToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().ToTable("MessageTable", "wcfMessage");
        }

        private void MessageTableConfigureation(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTable>().HasKey(m => m.ID);
            modelBuilder.Entity<MessageTable>().Property(m => m.MESSAGETEXT)
                .IsUnicode(false);
        }
    }
}
