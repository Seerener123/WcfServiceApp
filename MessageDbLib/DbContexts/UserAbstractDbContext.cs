using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContexts
{
    public abstract class UserAbstractDbContext : DbContext
    {
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected UserAbstractDbContext(string connectionString) : base(connectionString)
        {
            //
        }
    }
}
