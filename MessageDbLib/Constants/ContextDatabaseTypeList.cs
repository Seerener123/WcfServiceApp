using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.Constants
{
    public static class ContextDatabaseTypeList
    {
        public const string RelationalDb = "RELATIONAL";
        public const string Nosql = "NOSQL";

        public static readonly Dictionary<string, string> ContextTypeDictionary = new Dictionary<string, string>()
        {
            { DbContextConstant.MsSqlDbContext, RelationalDb },
            { DbContextConstant.MySqlDbContext, RelationalDb },
            { DbContextConstant.MongoNoSqlDbContext, Nosql}
        };
    }
}
