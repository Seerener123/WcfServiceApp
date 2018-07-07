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
    public class UserDbContext : UserAbstractDbContext
    {
        public UserDbContext() : base("name=MessageDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //MapUserToTable(modelBuilder);
            UserTableConfiguration(modelBuilder);
            UserTableDiscriminatorConfig(modelBuilder);
        }

        private void MapUserToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("UserTable", "dbo");
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
    }
}
