using MessageDbLib.MessagingEntities;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
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
            modelBuilder.Entity<UserTable>().HasKey(u => u.Id);

            modelBuilder.Entity<UserTable>().Property(u => u.UserName)
               .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(u => u.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(u => u.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(u => u.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>().Property(u => u.Gender)
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
