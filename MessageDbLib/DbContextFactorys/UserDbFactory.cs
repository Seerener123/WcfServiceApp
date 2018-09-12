using MessageBaseDbLib.Constants;
using MessageDbLib.DbContexts;
using System;

namespace MessageDbLib.DbContextFactorys
{
    public static class UserDbFactory
    {
        public static UserAbstractDbContext GetUserDbContext(string dbcontextOption)
        {
            string uppercaseDbcontextOption = dbcontextOption.ToUpper();
            switch (uppercaseDbcontextOption)
            {
                case DbContextConstant.MsSqlDbContext:
                    {
                        return new UserDbContext();
                    }
                case DbContextConstant.MySqlDbContext:
                    {
                        return new UserMySqlDbContext();
                    }
                default:
                    throw new InvalidOperationException("UserDbConext option does not exist in the user factory options.");
            }
        }
    }
}
