using MessageDbLib.Constants;
using MessageDbLib.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContextFactorys
{
    public static class UserDbFactory
    {
        public static UserAbstractDbContext GetUserDbContext(string dbcontextOption)
        {
            switch (dbcontextOption)
            {
                case UserDbContextConstant.MsSqlUserDbContext:
                    return new UserDbContext();
                default:
                    throw new InvalidOperationException("Select");
            }
        }
    }
}
