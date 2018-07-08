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
            UserTableConfiguration(modelBuilder);
            UserTableDiscriminatorConfig(modelBuilder);

            /* When mapping an entity class to a table in the database using fluent api, make 
             * sure that the ToTable() function is invoked at the end of all the mapping 
             * configuration.
             * 
             * If it's not done this way, when we are configuring the descriminator, EF will throw
             * an invalid exception, saying that this user type is already mapped.
             * */
            MapUserToTable(modelBuilder);

            /* The link below, section Mapping Multiple Entity Types to One Table in the Database (Table Splitting)
             * demonstrates using Totable() propery.
             * https://msdn.microsoft.com/en-us/library/jj591617(v=vs.113).aspx
             * */
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

            /*modelBuilder.Entity<UserTable>()
                .Map<UserTable>(u => u.Requires("ISADVANCEDUSER").HasValue(false))
                .Map<AdvancedUser>(au => au.Requires("ISADVANCEDUSER").HasValue(true));*/
        }

        private void MapUserToTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("UserTable", "dbo");
            // modelBuilder.Entity<UserTable>().ToTable("UserTable1", "messagedbo");
        }
    }
}
