using MessageDbLib.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContextFactorys
{
    public static class DatabaseOptionConfigRetriever
    {
        public static readonly string DatabaseOptionAppSetting;

        static DatabaseOptionConfigRetriever()
        {
            string appsettingValue = ConfigurationManager.AppSettings["Dbcontextoptiontype"];
            DatabaseOptionAppSetting = appsettingValue != null && appsettingValue != string.Empty ? appsettingValue.ToUpper() : 
                MessageDbContextConstant.MsSqlMessageDbContext;
        }
    }
}
