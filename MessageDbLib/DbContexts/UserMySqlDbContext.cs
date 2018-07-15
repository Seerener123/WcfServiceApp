using MessageDbLib.MessagingEntities;
//using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserMySqlDbContext : UserAbstractDbContext
    {
        public UserMySqlDbContext() : base("name=MessageMySQLDbContext")
        {
            //
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            UserTableConfiguration(modelBuilder);
            UserTableDiscriminatorConfig(modelBuilder);
            MapUserToTable(modelBuilder);
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
        }

        private void UserTableDiscriminatorConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().Map<UserTable>(u => u.Requires("ISADVANCEDUSER")
                .HasValue(false));

            modelBuilder.Entity<UserTable>().Map<AdvancedUser>(au => au.Requires("ISADVANCEDUSER")
                .HasValue(true));
        }

        private void MapUserToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("UserTable", "wcfMessaging");
        }
    }
}
